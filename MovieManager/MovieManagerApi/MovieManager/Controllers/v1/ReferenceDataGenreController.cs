using Application.Interfaces;
using Application.Services.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieManager.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReferenceDataGenreController : BaseApiController
    {
        private readonly IRefDataGenreService _refDataService;
        public ReferenceDataGenreController(IMediator mediator, IRefDataGenreService refDataService) : base(mediator)
        {
            _refDataService = refDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenresAsync()
        {
            return Ok(await _refDataService.GetData());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdGenresAsync(int id)
        {
            ModelId directorName = new ModelId { Id = id };
            var test = await _refDataService.GetByIdData(directorName);
            return Ok(test);
        }

        [HttpPost]
        public async Task<IActionResult> PostGenresAsync([FromBody] ModelName directorName)
        {
            var test = await _refDataService.PostData(directorName);
            return Ok(test);
        }

        [HttpPut]
        public async Task<IActionResult> PutGenresAsync([FromBody] ModelObject directorName)
        {
            var test = await _refDataService.PutData(directorName);
            return Ok(test);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGenresAsync([FromBody] ModelId directorName)
        {
            var test = await _refDataService.DeleteData(directorName);
            return Ok(test);
        }
    }
}
