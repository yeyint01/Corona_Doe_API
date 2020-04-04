using System.ComponentModel.DataAnnotations;

namespace Entity.shared
{
    public class ObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
      object value,
      ValidationContext validationContext)
        {
            if (value != null)
                return ValidationResult.Success;
            return new ValidationResult(ErrorMessage, new string[1]
            {
                validationContext.MemberName
            });
        }
    }
}
