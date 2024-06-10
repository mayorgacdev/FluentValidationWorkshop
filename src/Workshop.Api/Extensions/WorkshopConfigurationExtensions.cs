namespace Workshop.Api.Extensions;

public static class WorkshopConfigurationExtensions
{
    public static IServiceCollection AddStores(this IServiceCollection Services)
    {
        Services.AddScoped<IStore, Store<Entity>>();
        Services.AddScoped(typeof(IStore<>), typeof(Store<>));

        //Services.AddScoped<ICustomerManagerDeprecated, CustomerManagerDeprecated>();
        Services.AddScoped<ICustomerManager, CustomerManager>();
        
        return Services;
    }
}
