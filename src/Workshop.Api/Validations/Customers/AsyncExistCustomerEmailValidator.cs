namespace Workshop.Api.Validations.Customers;

using FluentValidation;
using FluentValidation.Validators;

public class AsyncExistCustomerEmailValidator<T> : AsyncPropertyValidator<T, string>
{
    public override string Name => "AsyncExistCustomerEmailValidator";

    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string Value, CancellationToken Cancellation)
    {
        IStore<Customer> CustomerStore = (Context.RootContextData[nameof(IStore<Customer>)] as IStore<Customer>)!;
        var StoreResult = await CustomerStore.FetchCustomerByEmailAsync(Value);
        return StoreResult is null;
    }
}
