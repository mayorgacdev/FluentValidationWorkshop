namespace Workshop.Api;

public class CustomerResponse
{
    public Guid CustomerId { get; set; }
    public string FirtsName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}