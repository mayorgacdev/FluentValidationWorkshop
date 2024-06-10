namespace Workshop.Api;

using FluentValidation;
using FluentValidation.Validators;

public class AsyncCustomerPhoneNumberValidator<T> : AsyncPropertyValidator<T, string>
{
    public override string Name => "AsyncCustomerPhoneNumberValidator";

    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string Value, CancellationToken Cancellation)
    {
        IStore<Customer> CustomerStore = (Context.RootContextData[nameof(IStore<Customer>)] as IStore<Customer>)!;
        var StoreResult = await CustomerStore.FetchCustomerByPhoneNumberAsync(Value);
        return StoreResult is null;
    }
}