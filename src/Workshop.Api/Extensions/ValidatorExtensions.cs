namespace Workshop.Api;

using FluentValidation;
using Workshop.Api.Validations.Customers;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> UniqueCustomerEmailAsync<T>(this IRuleBuilder<T, string> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncExistCustomerEmailValidator<T>());

    public static IRuleBuilderOptions<T, string> UniqueCustomerPhoneNumberAsync<T>(this IRuleBuilder<T, string> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncCustomerPhoneNumberValidator<T>());

    public static IRuleBuilderOptions<T, string> UniqueCustomerPassportNumberAsync<T>(this IRuleBuilder<T, string> RuleBuilder)
        => RuleBuilder.SetAsyncValidator(new AsyncCustomerPassportNumberValidator<T>());
}
