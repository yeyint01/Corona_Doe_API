using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.shared
{
    public class PasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
     object value,
     ValidationContext validationContext)
        {
            string source = value?.ToString();
            if (string.IsNullOrWhiteSpace(source))
                return new ValidationResult("Please fill password.", (IEnumerable<string>)new string[1]
                {
          validationContext.MemberName
                });
            if (source.Length < 6)
                return new ValidationResult("Password must be at least 6 characters long.", (IEnumerable<string>)new string[1]
                {
          validationContext.MemberName
                });
            if (!source.Any<char>((Func<char, bool>)(c => char.IsLetter(c))))
                return new ValidationResult("Password must have at least one non alphanumeric character.", (IEnumerable<string>)new string[1]
                {
          validationContext.MemberName
                });
            if (source.Any<char>((Func<char, bool>)(c => char.IsDigit(c))))
                return ValidationResult.Success;
            return new ValidationResult("Password must have at least one digit ('0'-'9')", (IEnumerable<string>)new string[1]
            {
        validationContext.MemberName
            });
        }
    }
}

