namespace Workshop.Api;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerManagerDeprecated Manager) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CustomerCreatedResponse>> CreateCustomerAsync(CreateCustomerRequest Request)
    {
        EntityId? ManagerResult = await Manager.CreateCustomerAsync(Request);

        if (ManagerResult is null)
        {
            return StatusCode(StatusCodes.Status409Conflict, "Failed to create customer.");
        }

        return StatusCode(StatusCodes.Status200OK, new CustomerCreatedResponse(ManagerResult?.Id));
    }
}