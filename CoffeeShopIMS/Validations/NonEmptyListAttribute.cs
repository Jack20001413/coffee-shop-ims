using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShopIMS.Validations;

public class NonEmptyListAttribute : ValidationAttribute
{    
    public string GetErrorMessage() => ErrorMessage ?? "List must contain at least one item.";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var list = value as IList<string>;

        if (list.IsNullOrEmpty())
        {
            return new ValidationResult(GetErrorMessage());
        }

        return ValidationResult.Success;
    }
}
