using Microsoft.AspNetCore.Mvc;
using OnlineStore.Contract.Requests;
using OnlineStore.Contract.Response;
using OnlineStore.Service;
using OnlineStore.Service.Commands.CategoryCommands;

namespace OnlineStore.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategotyAsync([FromServices] IRequestHandler<IList<CategoryResponse>> getCategoryQuery)
        {
            return Ok(await getCategoryQuery.Handle());
        }

        [HttpPost]
        public async Task<IActionResult> UpserCategoryAsync([FromServices] IRequestHandler<UpsertCategoryCommand, CategoryResponse> upsertCategoryCommand, [FromBody] UpsertCategoryRequest request)
        {
            var category = await upsertCategoryCommand.Handle(new UpsertCategoryCommand
            {
                IdCategory = request.IdCategory,
                Name = request.Name
            });

            return Ok(category);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryById(int categoryId, [FromServices] IRequestHandler<DeleteCategoryCommand, bool> deleteCategoryByIdCommand)
        {
            var result = await deleteCategoryByIdCommand.Handle(new DeleteCategoryCommand { CategoryId = categoryId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int categoryId, [FromServices] IRequestHandler<int, CategoryResponse> getCategoryByIdQuery)
        {
            var result = await getCategoryByIdQuery.Handle(categoryId);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
