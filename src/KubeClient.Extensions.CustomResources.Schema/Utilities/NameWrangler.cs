using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace KubeClient.Extensions.CustomResources.Schema.Utilities
{
    /// <summary>
    ///     Helper methods for working with names/identifiers in Kubernetes resource schemas.
    /// </summary>
    static class NameWrangler
    {
        /// <summary>
        ///     A regular expression used to capitalise names/identifiers.
        /// </summary>
        /// <remarks>
        ///     Used to split on either:
        ///     <list type="bullet">
        ///         <item>1 or more upper-case letters, NOT followed by 0 or more lower-case letters.</item>
        ///         <item>0 or 1 upper-case letters, followed by 1 or more lower-case letters.</item>
        ///         <item>1 or more digits.</item>
        ///     </list>
        /// </remarks>
        static readonly Regex Capitalizer = new Regex(@"([A-Z]+(?![a-z])|[A-Z]?[a-z]+|\d+)");

        /// <summary>
        ///     A regular expression used to sanitise names/identifiers.
        /// </summary>
        static readonly Regex Sanitizer = new Regex(@"([\-\$0-9])");

        /// <summary>
        ///     <see cref="TextInfo"/> representing text behaviour for the invariant culture (<see cref="CultureInfo.InvariantCulture"/>).
        /// </summary>
        static readonly TextInfo InvariantText = CultureInfo.InvariantCulture.TextInfo;

        /// <summary>
        ///     Capitalise the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name to capitalise.
        /// </param>
        /// <returns>
        ///     The capitalised name.
        /// </returns>
        /// <remarks>
        ///     This function is number-aware:
        ///     <list type="bullet">
        ///         <item>"v1" becomes "V1"</item>
        ///         <item>"v1beta1" becomes "V1Beta1"</item>
        ///         <item>"v12beta34" becomes "V12Beta34"</item>
        ///         <item>"v12etaBetaPi34" becomes "V12EtaBetaPi34"</item>
        ///         <item>"v12EtaBetaPi34" becomes "V12EtaBetaPi34"</item>
        ///         <item>"v12etabetaPi34" becomes "V12EtabetaPi34"</item>
        ///     </list>
        /// </remarks>
        public static string CapitalizeName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (String.IsNullOrWhiteSpace(name))
                return name;

            string[] nameComponents = Capitalizer.Split(name);
            for (int componentIndex = 0; componentIndex < nameComponents.Length; componentIndex++)
            {
                string nameComponent = nameComponents[componentIndex];
                nameComponents[componentIndex] = InvariantText.ToTitleCase(nameComponent);
            }

            return String.Join(String.Empty, nameComponents);
        }

        public static string SanitizeName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return Sanitizer.Replace(name, String.Empty);
        }
    }
}