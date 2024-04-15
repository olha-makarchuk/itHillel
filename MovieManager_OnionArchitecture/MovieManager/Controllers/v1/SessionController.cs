using Application.MovieFeatures.Commands.SessionCommands;
using Application.MovieFeatures.Queries.SessionQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieManager.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SessionController : BaseApiController
    {
        public SessionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllSessionsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await Mediator.Send(new  GetSessionByIdQuery { Id = id });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await Mediator.Send(new DeleteSessionByIdCommand { Id = id });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,  UpdateSessionCommand command)
        {
            try
            {
                if (id != command.Id)
                {
                    return BadRequest();
                }

                var result = await Mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
