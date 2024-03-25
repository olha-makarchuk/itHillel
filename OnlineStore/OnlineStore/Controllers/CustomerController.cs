using Microsoft.AspNetCore.Mvc;
using OnlineStore.Contract.Requests;
using OnlineStore.Contract.Response;
using OnlineStore.Service;
using OnlineStore.Service.Commands.CategoryCommands;
using OnlineStore.Service.Commands.CustomerCommand;
using OnlineStore.Service.Commands.CustomerCommands;

namespace OnlineStore.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCustumerAsync([FromServices] IRequestHandler<IList<CustomerResponse>> getCustomerQuery)
        {
            return Ok(await getCustomerQuery.Handle());
        }

        [HttpPost]
        public async Task<IActionResult> UpserCustomerAsync([FromServices] IRequestHandler<UpsertCustomerCommand, CustomerResponse> upsertCustomerCommand, [FromBody] UpsertCustomerRequest request)
        {
            var customer = await upsertCustomerCommand.Handle(new UpsertCustomerCommand
            {
                IdCustomer = request.IdCustomer,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Age = request.Age
            });

            return Ok(customer);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomerById(int customerId, [FromServices] IRequestHandler<DeleteCustomerCommand, bool> deleteCustomerByIdCommand)
        {
            var result = await deleteCustomerByIdCommand.Handle(new DeleteCustomerCommand { CustomerId = customerId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int customerId, [FromServices] IRequestHandler<int, CustomerResponse> getCustomerByIdQuery)
        {
            var result = await getCustomerByIdQuery.Handle(customerId);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
