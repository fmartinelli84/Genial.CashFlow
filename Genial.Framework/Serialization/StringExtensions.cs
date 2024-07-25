using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Genial.Framework.Serialization
{
    public static partial class StringExtensions
    {
        [GeneratedRegex(@"\D+")]
        private static partial Regex onlyNumbersRegEx();

        public static string? OnlyNumbers(this string? value) => 
            value is not null ? 
                string.Join(string.Empty, onlyNumbersRegEx().Split(value.Trim())) : 
                null;
    }
}
