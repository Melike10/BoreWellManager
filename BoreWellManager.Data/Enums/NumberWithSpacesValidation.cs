using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Enums
{
    public class NumberWithSpacesValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            string input = value.ToString();
            if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[0-9 ]*$"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Geçersiz format.");
        }
    }
}
