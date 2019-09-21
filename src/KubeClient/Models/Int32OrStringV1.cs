using Newtonsoft.Json;
using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    using Converters;

    /// <summary>
    ///     Union type representing a K8s API value that can be either a string or an integer.
    /// </summary>
    /// <remarks>
    ///     Any string that cannot be parsed as a number will be treated as a string; otherwise, it will be treated as an <see cref="Int32"/>.
    /// </remarks>
    [JsonConverter(typeof(Int32OrStringV1Converter))]
    public class Int32OrStringV1
        : IEquatable<Int32OrStringV1>, IEquatable<int>, IEquatable<string>, IYamlConvertible
    {
        /// <summary>
        /// The CLR <see cref="Type"/> representing <see cref="Int32OrStringV1"/>.
        /// </summary>
        static readonly Type Int32OrStringV1Type = typeof(Int32OrStringV1);

        /// <summary>
        ///     The underlying value (if it is an <see cref="Int32"/>).
        /// </summary>
        int? _intValue;

        /// <summary>
        ///     The underlying value (if it not an <see cref="Int32"/>).
        /// </summary>
        string _stringValue;

        /// <summary>
        ///     Wrap an <see cref="Int32"/> in an <see cref="Int32OrStringV1"/>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="Int32"/> to wrap.
        /// </param>
        public Int32OrStringV1(int value)
        {
            _intValue = value;
            _stringValue = null;
        }

        /// <summary>
        ///     Wrap a <see cref="String"/> in an <see cref="Int32OrStringV1"/>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="String"/> to wrap.
        /// </param>
        public Int32OrStringV1(string value)
        {
            if (Int32.TryParse(value, out int intValue))
            {
                _intValue = intValue;
                _stringValue = null;
            }
            else
            {
                _stringValue = value;
                _intValue = null;
            }
        }

        /// <summary>
        /// YAML deserialisation constructor.
        /// </summary>
        public Int32OrStringV1()
        {
        }

        /// <summary>
        ///     The underlying value as an <see cref="Int32"/>.
        /// </summary>
        /// <exception cref="InvalidCastException">
        ///     The underlying value is not a valid 32-bit integer.
        /// </exception>
        public int Int32Value => _intValue ?? throw new InvalidCastException($"The value '{_stringValue}' is not a valid 32-bit integer.");
        
        /// <summary>
        ///     The underlying value as a <see cref="String"/>.
        /// </summary>
        public string StringValue => _intValue?.ToString() ?? _stringValue;

        /// <summary>
        ///     Is the underlying value a valid 32-bit integer?
        /// </summary>
        public bool IsInt32 => _intValue.HasValue;

        /// <summary>
        ///     Is the underlying value a (non-null) string?
        /// </summary>
        public bool IsString => _stringValue != null;

        /// <summary>
        ///     Determine whether the <see cref="Int32OrStringV1"/> is equivalent to another <see cref="Int32OrStringV1"/>.
        /// </summary>
        /// <param name="other">
        ///     The other <see cref="Int32OrStringV1"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="Int32OrStringV1"/> is equivalent to the other <see cref="Int32OrStringV1"/>; otherwise <c>false</c>.
        /// </returns>
        public bool Equals(Int32OrStringV1 other) => _intValue == other._intValue && _stringValue == other._stringValue;

        /// <summary>
        ///     Determine whether the <see cref="Int32OrStringV1"/> is equivalent to another <see cref="Int32"/>.
        /// </summary>
        /// <param name="other">
        ///     The other <see cref="Int32"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="Int32OrStringV1"/> is equivalent to the other <see cref="Int32"/>; otherwise <c>false</c>.
        /// </returns>
        public bool Equals(int other) => _intValue == other;

        /// <summary>
        ///     Determine whether the <see cref="Int32OrStringV1"/> is equivalent to another <see cref="String"/>.
        /// </summary>
        /// <param name="other">
        ///     The other <see cref="String"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="Int32OrStringV1"/> is equivalent to the other <see cref="String"/>; otherwise <c>false</c>.
        /// </returns>
        public bool Equals(string other) => _stringValue == other;

        /// <summary>
        ///     Determine whether the <see cref="Int32OrStringV1"/> is equivalent to another object.
        /// </summary>
        /// <param name="other">
        ///     The other object.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="Int32OrStringV1"/> is equivalent to the other object; otherwise <c>false</c>.
        /// </returns>
        public override bool Equals(object other)
        {
            if (other is Int32OrStringV1 otherInt32OrStringV1)
                return Equals(otherInt32OrStringV1);

            if (other is int otherInt32)
                return Equals(otherInt32);

            if (other is string otherString)
                return Equals(otherString);

            return false;
        }

        /// <summary>
        ///     Get a hash code to represent the <see cref="Int32OrStringV1"/>.
        /// </summary>
        /// <returns>
        ///     The hash code.
        /// </returns>
        public override int GetHashCode() => _intValue?.GetHashCode() ?? _stringValue?.GetHashCode() ?? 0;

        /// <summary>
        ///     Get a string representation of the <see cref="Int32OrStringV1"/>.
        /// </summary>
        public override string ToString() => StringValue;

        /// <summary>
        /// Read the <see cref="Int32OrStringV1"/>'s value from YAML.
        /// </summary>
        /// <param name="parser">The YAML parser to read from.</param>
        /// <param name="expectedType">The expected type to deserialise into (must be <see cref="Int32OrStringV1"/>).</param>
        /// <param name="nestedObjectDeserializer">An <see cref="ObjectDeserializer"/> that can be used to deserialise nested objects (unused for <see cref="Int32OrStringV1"/> since it's a scalar data-type).</param>
        void IYamlConvertible.Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
        {
            if ( parser == null )
                throw new ArgumentNullException(nameof(parser));

            if ( expectedType == null )
                throw new ArgumentNullException(nameof(expectedType));

            if (expectedType != Int32OrStringV1Type)
                throw new NotSupportedException($"The {nameof(IYamlConvertible)} implementation of {nameof(Int32OrStringV1)} can only handle values of type '{Int32OrStringV1Type.FullName}'.");

            switch ( parser.Current )
            {
                case Scalar scalar:
                {
                    if ( Int32.TryParse(scalar.Value, out int intValue) )
                    {
                        _intValue = intValue;
                        _stringValue = null;
                    }
                    else
                    {
                        _stringValue = scalar.Value;
                        _intValue = null;
                    }

                    break;
                }
                default:
                {
                    throw new YamlException($"Unexpected parser event '{parser.Current.GetType().Name}'.");
                }
            }

            // Leave the parser on the next token (we're done parsing this one).
            parser.MoveNext();
        }

        /// <summary>
        /// Write the <see cref="Int32OrStringV1"/>'s value to YAML.
        /// </summary>
        /// <param name="emitter">The YAML emitter that the value will be written to.</param>
        /// <param name="nestedObjectSerializer">An <see cref="ObjectSerializer"/> that can be used to serialise nested objects (unused for <see cref="Int32OrStringV1"/>).</param>
        void IYamlConvertible.Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
        {
            if ( emitter == null )
                throw new ArgumentNullException(nameof(emitter));

            if ( nestedObjectSerializer == null )
                throw new ArgumentNullException(nameof(nestedObjectSerializer));

            emitter.Emit(new Scalar(
                value: ToString()
            ));
        }

        /// <summary>
        ///     Explicitly convert an <see cref="Int32OrStringV1"/> to an <see cref="Int32"/>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="Int32OrStringV1"/> to convert.
        /// </param>
        /// <returns>
        ///     The <see cref="Int32"/>.
        /// </returns>
        /// <exception cref="InvalidCastException">
        ///     The underlying value is not a valid 32-bit integer.
        /// </exception>
        public static explicit operator Int32(Int32OrStringV1 value) => value._intValue ?? throw new InvalidCastException("The specified value is not an Int32.");

        /// <summary>
        ///     Explicitly convert an <see cref="Int32OrStringV1"/> to a <see cref="String"/>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="Int32OrStringV1"/> to convert.
        /// </param>
        /// <returns>
        ///     The <see cref="String"/>.
        /// </returns>
        public static explicit operator string(Int32OrStringV1 value) => value._stringValue ?? value._intValue?.ToString();

        /// <summary>
        ///     Implicitly convert an <see cref="Int32"/> to an <see cref="Int32OrStringV1"/>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="Int32"/> to convert.
        /// </param>
        /// <returns>
        ///     The <see cref="Int32OrStringV1"/>.
        /// </returns>
        public static implicit operator Int32OrStringV1(int value) => new Int32OrStringV1(value);

        /// <summary>
        ///     Implicitly convert a <see cref="String"/> to an <see cref="Int32OrStringV1"/>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="String"/> to convert.
        /// </param>
        /// <returns>
        ///     The <see cref="Int32OrStringV1"/>.
        /// </returns>
        public static implicit operator Int32OrStringV1(string value) => value != null ? new Int32OrStringV1(value) : null;

        /// <summary>
        ///     Test if 2 <see cref="Int32OrStringV1"/>s are equivalent.
        /// </summary>
        /// <param name="left">
        ///     The first <see cref="Int32OrStringV1"/>.
        /// </param>
        /// <param name="right">
        ///     The second <see cref="Int32OrStringV1"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="Int32OrStringV1"/>s are equivalent; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator==(Int32OrStringV1 left, Int32OrStringV1 right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
               return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
               return false;

            return left.Equals(right);
        }

        /// <summary>
        ///     Test if 2 <see cref="Int32OrStringV1"/>s are not equivalent.
        /// </summary>
        /// <param name="left">
        ///     The first <see cref="Int32OrStringV1"/>.
        /// </param>
        /// <param name="right">
        ///     The second <see cref="Int32OrStringV1"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="Int32OrStringV1"/>s are not equivalent; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator!=(Int32OrStringV1 left, Int32OrStringV1 right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
               return false;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
               return true;

            return !left.Equals(right);
        }

        /// <summary>
        ///     Test if an <see cref="Int32OrStringV1"/> is equivalent to an <see cref="Int32"/>.
        /// </summary>
        /// <param name="left">
        ///     The <see cref="Int32OrStringV1"/>.
        /// </param>
        /// <param name="right">
        ///     The <see cref="Int32"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="Int32OrStringV1"/> is equivalent to the <see cref="Int32"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator==(Int32OrStringV1 left, int right)
        {
            if (ReferenceEquals(left, null))
               return false;

            return left.Equals(right);
        }

        /// <summary>
        ///     Test if an <see cref="Int32OrStringV1"/> is not equivalent to an <see cref="Int32"/>.
        /// </summary>
        /// <param name="left">
        ///     The <see cref="Int32OrStringV1"/>.
        /// </param>
        /// <param name="right">
        ///     The <see cref="Int32"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="Int32OrStringV1"/> is not equivalent to the <see cref="Int32"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator!=(Int32OrStringV1 left, int right)
        {
            if (ReferenceEquals(left, null))
               return true;

            return !left.Equals(right);
        }

        /// <summary>
        ///     Test if an <see cref="Int32OrStringV1"/> is equivalent to a <see cref="String"/>.
        /// </summary>
        /// <param name="left">
        ///     The <see cref="Int32OrStringV1"/>.
        /// </param>
        /// <param name="right">
        ///     The <see cref="String"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="Int32OrStringV1"/> is equivalent to the <see cref="String"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator==(Int32OrStringV1 left, string right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
               return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
               return false;

            return left.Equals(right);
        }

        /// <summary>
        ///     Test if an <see cref="Int32OrStringV1"/> is not equivalent to a <see cref="String"/>.
        /// </summary>
        /// <param name="left">
        ///     The <see cref="Int32OrStringV1"/>.
        /// </param>
        /// <param name="right">
        ///     The <see cref="String"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="Int32OrStringV1"/> is not equivalent to the <see cref="String"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator!=(Int32OrStringV1 left, string right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
               return false;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
               return true;

            return !left.Equals(right);
        }
    }
}