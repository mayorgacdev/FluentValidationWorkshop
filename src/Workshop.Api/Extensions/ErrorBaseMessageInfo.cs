namespace Workshop.Api.Extensions;

public record class ErrorBaseMessageInfo(string Id, string Message, string? Description = null);