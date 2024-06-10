namespace Workshop.Api;

public record CreateCustomerRequest(
    string FirstName, 
    string PassportNumber, 
    string PhoneNumber, 
    string Address, 
    string Email) : IRequest;