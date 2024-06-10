namespace Workshop.Api;

using FluentValidation.Results;

[GenerateAutomaticInterface]
public class CustomerManager(IStore Store) : ICustomerManager
{
    private readonly IStore<Customer> CustomerStore = Store.GetStore<Customer>();

    public async ValueTask<ErrorOr<EntityId?>> CreateCustomerAsync(CreateCustomerRequest Request)
    {
        ValidationContext<CreateCustomerRequest> Context = new ValidationContext<CreateCustomerRequest>(Request);
        Context.RootContextData[nameof(IStore<Customer>)] = CustomerStore;

        CreateCustomerValidator Validator = new CreateCustomerValidator();
        ValidationResult Result = await Validator.ValidateAsync(Context);

        if (Request is null)
        {
            Error.Failure(code: "CUSTOMER001NULL", description: "Request is null.");
        }
        
        if (!Result.IsValid)
        {
            return ErrorOr<EntityId?>.From(Result.Errors.ConvertAll(Prop => Error.Validation(code: Prop.ErrorCode, description: Prop.ErrorMessage)));
        }

        EntityId? StoreResult = await CustomerStore.CreateCustomerAsync(Request!);
        return StoreResult;
    }

}
