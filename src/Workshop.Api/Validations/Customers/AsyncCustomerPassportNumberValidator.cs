using FluentValidation;
using FluentValidation.Validators;

namespace Workshop.Api;

public class AsyncCustomerPassportNumberValidator<T> : AsyncPropertyValidator<T, string>
{
    public override string Name => "AsyncCustomerPassportNumberValidator";

    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string Value, CancellationToken Cancellation)
    {
        IStore<Customer> CustomerStore = (Context.RootContextData[nameof(IStore<Customer>)] as IStore<Customer>)!;
        var StoreResult = await CustomerStore.FetchCustomerByPassportNumberAsync(Value);
        return StoreResult is null;
    }
}