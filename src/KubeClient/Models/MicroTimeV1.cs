using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    using Converters;

    /// <summary>
    ///     MicroTime is version of Time with microsecond level precision.
    /// </summary>
    [JsonConverter(typeof(MicroTimeV1Converter))]
    public struct MicroTimeV1 // TODO: Implement IFormattable, IComparable<MicroTimeV1>, IEquatable<MicroTimeV1>, IComparable<DateTime>, IEquatable<DateTime>
    {
        /// <summary>
        ///     The underlying <see cref="DateTime"/> value.
        /// </summary>
        readonly DateTime _value;

        /// <summary>
        ///     Create a new <see cref="MicroTimeV1"/>.
        /// </summary>
        /// <param name="value">    
        ///     The underlying <see cref="DateTime"/> value.
        /// </param>
        public MicroTimeV1(DateTime value)
        {
            _value = value;
        }

        /// <summary>
        ///     The underlying <see cref="DateTime"/> value.
        /// </summary>
        public DateTime Value => _value;

        /// <summary>
        ///     Convert the <see cref="MicroTimeV1"/> to a string.
        /// </summary>
        /// <returns>
        ///     The string representation of the <see cref="MicroTimeV1"/>.
        /// </returns>
        public override string ToString() => _value.ToString("o");

        /// <summary>
        ///     Implicit cast operator from <see cref="MicroTimeV1"/> to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="microTime">The <see cref="MicroTimeV1"/> value to convert.</param>
        public static implicit operator DateTime(MicroTimeV1 microTime) => microTime.Value;

        /// <summary>
        ///     Implicit cast operator from <see cref="DateTime"/> to <see cref="MicroTimeV1"/>.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to convert.</param>
        public static implicit operator MicroTimeV1(DateTime value)
        {
            // TODO: Decide if we want to round value to nearest microsecond as part of conversion in this direction.

            return new MicroTimeV1(value);
        }
    }
}
