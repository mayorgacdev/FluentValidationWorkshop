

WebApplicationBuilder ApplicationBuilder = WebApplication.CreateBuilder(args);

ApplicationBuilder.Configuration.AddErrorMessages();

ApplicationBuilder
    .Services
    .AddWorkshopOptions()
    .AddStores()
    .AddControllers();

WebApplication Application = ApplicationBuilder.Build();

Application.UseHttpsRedirection();
Application.MapControllers();

Application.Run();

