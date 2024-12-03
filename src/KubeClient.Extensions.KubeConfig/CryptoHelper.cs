using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

using BCX509Certificate = Org.BouncyCastle.X509.X509Certificate;
using X509Certificate2 = System.Security.Cryptography.X509Certificates.X509Certificate2;

#if NET9_0_OR_GREATER
using X509CertificateLoader = System.Security.Cryptography.X509Certificates.X509CertificateLoader;
#endif // NET9_0_OR_GREATER

namespace KubeClient.Extensions.KubeConfig
{
    /// <summary>
    ///     Cryptographic helper methods.
    /// </summary>
    static class CryptoHelper
    {
        /// <summary>
        ///     Convert a BouncyCastle X.509 certificate to a native (System.Security.Cryptography) certificate.
        /// </summary>
        /// <param name="certificate">
        ///     The <see cref="BCX509Certificate"/> to convert.
        /// </param>
        /// <returns>
        ///     The converted <see cref="X509Certificate2"/>.
        /// </returns>
        public static X509Certificate2 ToNativeX509Certificate(this BCX509Certificate certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException(nameof(certificate));

            byte[] derEncodedCertificate = certificate.GetEncoded();

#if !NET9_0_OR_GREATER
            return new X509Certificate2(derEncodedCertificate);
#else // !NET9_0_OR_GREATER
            return X509CertificateLoader.LoadCertificate(derEncodedCertificate);
#endif // !NET9_0_OR_GREATER
        }

        /// <summary>
        ///     Get the thumbprint of a BouncyCastle X.509 certificate.
        /// </summary>
        /// <param name="certificate">
        ///     The <see cref="BCX509Certificate"/>.
        /// </param>
        /// <returns>
        ///     The certificate thumbprint.
        /// </returns>
        public static string GetThumbprint(this BCX509Certificate certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException(nameof(certificate));

            using (X509Certificate2 nativeCertificate = certificate.ToNativeX509Certificate())
            {
                return nativeCertificate.Thumbprint;
            }
        }

        /// <summary>
        ///     Build a PFX (PKCS12) store containing the specified certificate and private key.
        /// </summary>
        /// <param name="certificatePem">
        ///     A PEM block containing the certificate data.
        /// </param>
        /// <param name="keyPem">
        ///     A PEM block containing the private key data.
        /// </param>
        /// <param name="password">
        ///     The password to use for protecting the exported data.
        /// </param>
        /// <returns>
        ///     A byte array containing the exported data.
        /// </returns>
        public static byte[] BuildPfx(string certificatePem, string keyPem, string password)
        {
            if (String.IsNullOrWhiteSpace(certificatePem))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'certificateData'.", nameof(certificatePem));
            
            if (String.IsNullOrWhiteSpace(keyPem))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'keyData'.", nameof(keyPem));

            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'password'.", nameof(password));

            List<X509CertificateEntry> chain = new List<X509CertificateEntry>();
            AsymmetricCipherKeyPair privateKey = null;

            foreach (object pemObject in EnumeratePemObjects(password, certificatePem, keyPem))
            {
                if (pemObject is BCX509Certificate certificate)
                    chain.Add(new X509CertificateEntry(certificate));
                else if (pemObject is AsymmetricCipherKeyPair keyPair)
                    privateKey = keyPair;
            }

            if (chain.Count == 0)
                throw new CryptographicException("Cannot find X.509 certificate in PEM data.");

            if (privateKey == null)
                throw new CryptographicException("Cannot find private key in PEM data.");

            string certificateSubject = chain[0].Certificate.SubjectDN.ToString();

            Pkcs12Store store = new Pkcs12StoreBuilder().Build();
            store.SetKeyEntry(certificateSubject, new AsymmetricKeyEntry(privateKey.Private), chain.ToArray());

            using (MemoryStream pfxData = new MemoryStream())
            {
                store.Save(pfxData, password.ToCharArray(), new SecureRandom());

                return pfxData.ToArray();
            }
        }

        /// <summary>
        ///     Enumerate the objects in one or more PEM-encoded blocks.
        /// </summary>
        /// <param name="pemPassword">
        ///     The password used to protect the encoded data.
        /// </param>
        /// <param name="pemBlocks">
        ///     The PEM-encoded blocks.
        /// </param>
        /// <returns>
        ///     A sequence of BouncyCastle cryptographic objects (e.g. <see cref="BCX509Certificate"/>, <see cref="AsymmetricCipherKeyPair"/>, etc).
        /// </returns>
        public static IEnumerable<object> EnumeratePemObjects(string pemPassword, params string[] pemBlocks) => EnumeratePemObjects(pemPassword, (IEnumerable<string>)pemBlocks);

        /// <summary>
        ///     Enumerate the objects in one or more PEM-encoded blocks.
        /// </summary>
        /// <param name="pemPassword">
        ///     The password used to protect the encoded data.
        /// </param>
        /// <param name="pemBlocks">
        ///     The PEM-encoded blocks.
        /// </param>
        /// <returns>
        ///     A sequence of BouncyCastle cryptographic objects (e.g. <see cref="BCX509Certificate"/>, <see cref="AsymmetricCipherKeyPair"/>, etc).
        /// </returns>
        public static IEnumerable<object> EnumeratePemObjects(string pemPassword, IEnumerable<string> pemBlocks)
        {
            if (pemBlocks == null)
                throw new ArgumentNullException(nameof(pemBlocks));

            if (String.IsNullOrWhiteSpace(pemPassword))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'password'.", nameof(pemPassword));

            var passwordStore = new StaticPasswordStore(pemPassword);

            foreach (string pemBlock in pemBlocks)
            {
                if (String.IsNullOrWhiteSpace(pemBlock))
                    continue;

                using (StringReader pemBlockReader = new StringReader(pemBlock))
                {
                    PemReader pemReader = new PemReader(pemBlockReader, passwordStore);

                    object pemObject;
                    while ((pemObject = pemReader.ReadObject()) != null)
                        yield return pemObject;
                }
            }
        }

        /// <summary>
        ///     A static implementation of <see cref="IPasswordFinder"/> used to feed the decryption password to BouncyCastle.
        /// </summary>
        class StaticPasswordStore
            : IPasswordFinder
        {
            /// <summary>
            ///     The decryption password.
            /// </summary>
            readonly string _password;

            /// <summary>
            ///     Create a new <see cref="StaticPasswordStore"/>.
            /// </summary>
            /// <param name="password">
            ///     The decryption password.
            /// </param>
            public StaticPasswordStore(string password)
            {
                if (String.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'password'.", nameof(password));

                _password = password;
            }

            /// <summary>
            ///     Get a copy of the decryption password.
            /// </summary>
            /// <returns>
            ///     An array of characters representing the decryption password.
            /// </returns>
            public char[] GetPassword() => _password.ToCharArray();
        }
    }
}