using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Entity.shared
{
    public class PhoneNumberAttribute : DataTypeAttribute
    {
        private static Regex _regex = PhoneNumberAttribute.CreateRegEx();
        private const string _additionalPhoneNumberCharacters = "-.()";

        public PhoneNumberAttribute()
          : base(DataType.PhoneNumber)
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            string input = value as string;
            if (input == string.Empty)
                return true;
            if (PhoneNumberAttribute._regex != null)
                return input != null && PhoneNumberAttribute._regex.Match(input).Length > 0;
            if (input == null)
                return false;
            string str = PhoneNumberAttribute.RemoveExtension(input.Replace("+", string.Empty).TrimEnd());
            bool flag = false;
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                return false;
            foreach (char c in str)
            {
                if (!char.IsDigit(c) && !char.IsWhiteSpace(c) && "-.()".IndexOf(c) == -1)
                    return false;
            }
            return true;
        }

        private static Regex CreateRegEx()
        {
            TimeSpan matchTimeout = TimeSpan.FromSeconds(2.0);
            try
            {
                if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
                    return new Regex("^(\\+\\s?)?((?<!\\+.*)\\(\\+?\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+)([\\s\\-\\.]?(\\(\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+))*(\\s?(x|ext\\.?)\\s?\\d+)?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, matchTimeout);
            }
            catch
            {
            }
            return new Regex("^(\\+\\s?)?((?<!\\+.*)\\(\\+?\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+)([\\s\\-\\.]?(\\(\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+))*(\\s?(x|ext\\.?)\\s?\\d+)?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        }

        private static string RemoveExtension(string potentialPhoneNumber)
        {
            int length1 = potentialPhoneNumber.LastIndexOf("ext.", StringComparison.InvariantCultureIgnoreCase);
            if (length1 >= 0 && PhoneNumberAttribute.MatchesExtension(potentialPhoneNumber.Substring(length1 + 4)))
                return potentialPhoneNumber.Substring(0, length1);
            int length2 = potentialPhoneNumber.LastIndexOf("ext", StringComparison.InvariantCultureIgnoreCase);
            if (length2 >= 0 && PhoneNumberAttribute.MatchesExtension(potentialPhoneNumber.Substring(length2 + 3)))
                return potentialPhoneNumber.Substring(0, length2);
            int length3 = potentialPhoneNumber.LastIndexOf("x", StringComparison.InvariantCultureIgnoreCase);
            return length3 >= 0 && PhoneNumberAttribute.MatchesExtension(potentialPhoneNumber.Substring(length3 + 1)) ? potentialPhoneNumber.Substring(0, length3) : potentialPhoneNumber;
        }

        private static bool MatchesExtension(string potentialExtension)
        {
            potentialExtension = potentialExtension.TrimStart();
            if (potentialExtension.Length == 0)
                return false;
            foreach (char c in potentialExtension)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
