using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieManager.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReferenceDataController : BaseApiController
    {
        private readonly IRefDataService _refDataService;
        public ReferenceDataController(IMediator mediator, IRefDataService refDataService) : base(mediator)
        {
            _refDataService = refDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectorsAsync()
        {
            return Ok(await _refDataService.GetData());
        }

        [HttpPost]
        public async Task<IActionResult> PostDirectorsAsync([FromBody] string directorName)
        {
            var test = await _refDataService.PostData(directorName);
            return Ok(test);
        }
    }
}
