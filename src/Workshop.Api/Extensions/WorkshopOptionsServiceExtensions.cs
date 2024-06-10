namespace Workshop.Api.Extensions;

public static class WorkshopOptionsServiceExtensions
{
     public static IServiceCollection AddWorkshopOptions(this IServiceCollection Services)
    {
        Services.AddOptions<WorkshopOptions>().Configure<IConfiguration>(static (Options, Configuration) =>
        {
            Configuration.GetSection(WorkshopConstants.WorkshopOptions).Bind(Options);
            Options.ConnectionString = Configuration.GetConnectionString(DefaultConnection)!;

            if (string.IsNullOrEmpty(Options.ConnectionString))
            {
                Options.ConnectionString = Environment.GetEnvironmentVariable(ConnectionStringEnv)!;
            }
        });

        return Services;
    }
}
