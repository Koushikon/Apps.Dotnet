using FluentValidation;
using FluentValidation.Results;
using Library;
using Matrix.Validators;

namespace Matrix;

internal class Program
{
    static void Main()
    {
        Customer customer = new();
        CustomerValidator validator = new();

        customer.FirstName = "Test";
        customer.LastName = "Lili";
        customer.DateOfBirth = new DateTime(1950, 6, 15);
        customer.Discount = 5.3;
        customer.Address.Street = "KHB";
        customer.Address.City = "COB";
        customer.Address.State = "WB";
        customer.Address.ZipCode = "123456";

        // Use this to check wheather the all the validation is satisfied or not.
        ValidationResult result = validator.Validate(customer);

        if (!result.IsValid)
        {
            foreach (var failure in result.Errors)
            {
                Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
            }
        }

        // Use this to throw exception if validation fails
        validator.ValidateAndThrow(customer);
    }
}
