namespace Workshop.Api;

[GenerateAutomaticInterface]
[Obsolete("This class is deprecated, use CustomerManager instead.")]
public class CustomerManagerDeprecated(IStore Store) : ICustomerManagerDeprecated
{
    private readonly IStore<Customer> CustomerStore = Store.GetStore<Customer>();

    public async ValueTask<EntityId?> CreateCustomerAsync(CreateCustomerRequest Request)
    {        
        if (Request is null)
        {
            return null;
        }

        CustomerResponse? CustomerByEmail = await CustomerStore.FetchCustomerByEmailAsync(Request.Email);
        
        if (CustomerByEmail is not null)
        {
            return null;
        }

        CustomerResponse? CustomerByPhone = await CustomerStore.FetchCustomerByPhoneNumberAsync(Request.PhoneNumber);

        if (CustomerByPhone is not null)
        {
            return null;
        }

        CustomerResponse? CustomerByPassport = await CustomerStore.FetchCustomerByPassportNumberAsync(Request.PassportNumber);

        if (CustomerByPassport is not null)
        {
            return null;
        }

        return await CustomerStore.CreateCustomerAsync(Request);
    }
}
