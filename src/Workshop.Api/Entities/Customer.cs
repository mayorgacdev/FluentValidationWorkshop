namespace Workshop.Api;

/// <summary>
/// Represents a customer in the database.
/// </summary>
public record class Customer (
    Guid CustomerId, 
    string FirtsName, 
    string PassportNumber,
    string PhoneNumber,
    string Address,
    string Email) : Entity;

// Path: src/Workshop.Api/Entities/Customer.cs