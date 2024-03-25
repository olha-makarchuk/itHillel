using Microsoft.AspNetCore.Mvc;
using OnlineStore.Contract.Requests;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Entities;
using OnlineStore.Service;
using OnlineStore.Service.Commands.CategoryCommands;
using OnlineStore.Service.Commands.CustomerCommand;
using OnlineStore.Service.Commands.OrderCommand;
using OnlineStore.Service.Commands.OrderCommands;

namespace OnlineStore.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOrderAsync([FromServices] IRequestHandler<IList<OrderResponse>> getOrderQuery)
        {
            return Ok(await getOrderQuery.Handle());
        }

        [HttpPost]
        public async Task<IActionResult> UpserOrderAsync([FromServices] IRequestHandler<UpsertOrderCommand, OrderResponse> upsertOrderCommand, [FromBody] UpsertOrderRequest request)
        {
            var order = await upsertOrderCommand.Handle(new UpsertOrderCommand
            {
                IdOrder = request.IdOrder,
                DateTimeOrder = request.DateTimeOrder,
                IdCustomer = request.IdCustomer,
                IdProduct = request.IdProduct
            });

            return Ok(order);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderById(int orderId, [FromServices] IRequestHandler<DeleteOrderCommand, bool> deleteOrderByIdCommand)
        {
            var result = await deleteOrderByIdCommand.Handle(new DeleteOrderCommand { OrderId = orderId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(int orderId, [FromServices] IRequestHandler<int, OrderResponse> getOrderByIdQuery)
        {
            var result = await getOrderByIdQuery.Handle(orderId);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
