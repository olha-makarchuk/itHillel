using Microsoft.AspNetCore.Mvc;
using OnlineStore.Contract.Requests;
using OnlineStore.Contract.Response;
using OnlineStore.Service;
using OnlineStore.Service.Commands.CategoryCommands;
using OnlineStore.Service.Commands.OrderCommand;
using OnlineStore.Service.Commands.ProductCommand;
using OnlineStore.Service.Commands.ProductCommands;

namespace OnlineStore.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProductAsync([FromServices] IRequestHandler<IList<ProductResponse>> getProductQuery)
        {
            return Ok(await getProductQuery.Handle());
        }


        [HttpPost]
        public async Task<IActionResult> UpserProductAsync([FromServices] IRequestHandler<UpsertProductCommand, ProductResponse> upsertProductCommand, [FromBody] UpsertProductRequest request)
        {
            var order = await upsertProductCommand.Handle(new UpsertProductCommand
            {
                IdProduct = request.IdProduct,
                IdCategory = request.IdCategory,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate,
                Name = request.Name
            });

            return Ok(order);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductById(int productId, [FromServices] IRequestHandler<DeleteProductCommand, bool> deleteProductByIdCommand)
        {
            var result = await deleteProductByIdCommand.Handle(new DeleteProductCommand { ProductId = productId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync(int productId, [FromServices] IRequestHandler<int, ProductResponse> getProductByIdQuery)
        {
            var result = await getProductByIdQuery.Handle(productId);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
