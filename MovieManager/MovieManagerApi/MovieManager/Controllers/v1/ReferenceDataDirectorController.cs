using Application.Interfaces;
using Application.Services;
using Application.Services.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieManager.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReferenceDataDirectorController : BaseApiController
    {
        private readonly IRefDataDirectorService _refDataService;
        public ReferenceDataDirectorController(IMediator mediator, IRefDataDirectorService refDataService) : base(mediator)
        {
            _refDataService = refDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectorsAsync()
        {
            return Ok(await _refDataService.GetData());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdDirectorsAsync(int id)
        {
            ModelId directorName = new ModelId { Id = id };
            var test = await _refDataService.GetByIdData(directorName);
            return Ok(test);
        }

        [HttpPost]
        public async Task<IActionResult> PostDirectorsAsync([FromBody] ModelName directorName)
        {
            var test = await _refDataService.PostData(directorName);
            return Ok(test);
        }

        [HttpPut]
        public async Task<IActionResult> PutDirectorsAsync([FromBody] ModelObject directorName)
        {
            var test = await _refDataService.PutData(directorName);
            return Ok(test);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDirectorsAsync([FromBody] ModelId directorName)
        {
            var test = await _refDataService.DeleteData(directorName);
            return Ok(test);
        }
    }
}
