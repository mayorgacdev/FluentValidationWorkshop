namespace Workshop.Api;

public class CustomerWorkshopController(ICustomerManager Manager) : BaseControllerWorkshop
{
    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync(CreateCustomerRequest Request)
    {
        ErrorOr<EntityId?> Result = await Manager.CreateCustomerAsync(Request);
    
        return Result.Match(
            Customer => CreatedAtAction(nameof(FetchCustomerById), new { customerId = Customer }, Customer),
            Problem
        );
    }

    [HttpGet]
    public async Task<ActionResult<CustomerCreatedResponse>> FetchCustomerById(string customerId)
    {
        return StatusCode(StatusCodes.Status201Created, await Task.FromResult(new CustomerCreatedResponse(Guid.NewGuid())));
    }
}

