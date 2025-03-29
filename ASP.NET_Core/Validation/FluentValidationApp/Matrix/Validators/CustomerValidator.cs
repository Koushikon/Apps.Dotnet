using FluentValidation;
using Library;

namespace Matrix.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        // Check for LastName is null
        RuleFor(customer => customer.FirstName)
            .NotNull();


        // Fluent Validation chaining rules
        // If a rule fails validation stops with Cascade(CascadeMode.Stop)
        // Check for LastName is empty with explicit error message
        // Check for Last Name length between 2 to 150 - Length(Min, Max)
        // Check for BeAValidName a Custom Valudation
        // Later {PropertyName} will be replaced with actual name
        // Later {TotalLength} will be replaced with actual data length, Works only for string
        RuleFor(customer => customer.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} must not be empry.")
            .Length(2, 150).WithMessage("{PropertyName} length {TotalLength}, It must be of 2 to 150 character.")
            .Must(BeAValidName).WithMessage("{PropertyName} Contains invalid characters.");


        RuleFor(customer => customer.DateOfBirth)
            .Must(BeAValidAge).WithMessage("Invalid {PropertyName}");


        // Check for Discount in between 0 to 100.0 with explicit error message
        // Later {PropertyName} will be replaced with actual name
        RuleFor(customer => customer.Discount)
            .InclusiveBetween(0, 100.0).WithMessage("{PropertyName} must be between 0 to 100");


        // Check for Address validation with another valicator class
        RuleFor(customer => customer.Address)
            .SetValidator(new AddressValidator());


        // Or, We could do it like this
        //RuleFor(customer => customer.Address.ZipCode).NotNull();
    }

    // Setting Up a Custom Rule
    private bool BeAValidName(string name)
    {
        name = name.Replace(" ", "");
        name = name.Replace("-", "");
        return name.All(Char.IsLetter);
    }

    private bool BeAValidAge(DateTime date)
    {
        int currentYear = DateTime.Now.Year;
        int dobYear = date.Year;

        if (dobYear <= currentYear && dobYear > (currentYear - 100))
            return true;
        return false;
    }
}

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        // Use CascadeMode.Stop for all rules
        // If a rule fails validation stops with Cascade(CascadeMode.Stop)
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(customer => customer.Street)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(customer => customer.City)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(customer => customer.State)
            .NotEmpty()
            .MinimumLength(2);

        // Chaining rules with explicit messages
        // Later {PropertyName} will be replaced with actual name
        // Later {TotalLength} will be replaced with actual data length, Works only for string
        RuleFor(address => address.ZipCode)
            .NotEmpty().WithMessage("{PropertyName} is Empty")
            .Length(6).WithMessage("{PropertyName} length {TotalLength}, It must be with 6 number.");
    }
}