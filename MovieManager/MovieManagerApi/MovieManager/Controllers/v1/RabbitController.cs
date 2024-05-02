using Application.MovieFeatures.Queries.MovieQueries;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieManager.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RabbitController : BaseApiController
    {
        public RabbitController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> ListeningM()
        {
            Consumer consumer = new Consumer();
            
            return Ok(consumer.Listening());
        }
    }
} 