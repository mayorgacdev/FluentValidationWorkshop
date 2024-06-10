namespace Workshop.Api;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(Customer => Customer.Email)
            .NotEmpty()
            .EmailAddress()
            .UniqueCustomerEmailAsync()
            .WithMessage("Customer with this email already exists.");

        RuleFor(Customer => Customer.PhoneNumber)
            .NotEmpty()
            .UniqueCustomerPhoneNumberAsync()
            .WithMessage("Phone number already exists, choose another.");

        RuleFor(Customer => Customer.PassportNumber)
            .NotEmpty()
            .UniqueCustomerPassportNumberAsync()
            .WithMessage("PassportNumber already exists, choose another.");
    }
}
