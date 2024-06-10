namespace Workshop.Api.Extensions;

public static class ErrorMessageConfigurationExtensions
{
    public static ConfigurationManager AddErrorMessages(this ConfigurationManager configuration)
    {
        configuration.AddJsonFile("errors.json", optional: false, reloadOnChange: true);

        IConfigurationSection errorSection = configuration.GetSection("Errors");

        IList<ErrorBaseMessageInfo> errors = errorSection.Get<List<ErrorBaseMessageInfo>>()!;

        ErrorCodeMessages.ErrorMessages = errors!.ToDictionary(
            MessageInfo => MessageInfo.Id, Param => new ErrorMessageInfo(Param.Message, Param.Description));

        return configuration;
    }
}