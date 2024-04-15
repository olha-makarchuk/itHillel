using Application.MovieFeatures.Commands.MovieCommands;
using Application.MovieFeatures.Commands.SessionCommands;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers.v1;

namespace MovieManager.Api.Tests.SessionControllerTests
{
    public class UpdateSessionTests
    {
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task Update_IfSessionFoundAndUpdated_ShouldReturnOkAndUpdatedSession()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<UpdateSessionCommand>().With(x => x.Id, 1).Create();
            var movieExpected = new Session()
            {
                Id = movie.Id,
                MovieId = movie.Id,
                RoomName = movie.RoomName,
                StartDateTime = DateTime.Now,
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateSessionCommand>(), CancellationToken.None))
                        .ReturnsAsync(movieExpected);

            var controller = new SessionController(mediatorMock.Object);

            var result = await controller.Update(movie.Id, movie);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovie = Assert.IsAssignableFrom<Session>(okResult.Value);
            Assert.Equal(movieExpected, actualMovie);
        }

        [Fact]
        public async Task Update_IfSessionNotFound_ShouldReturnNotFoundResult()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<UpdateSessionCommand>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateSessionCommand>(), CancellationToken.None))
                        .ThrowsAsync(new Exception());

            var controller = new SessionController(mediatorMock.Object);

            var result = await controller.Update(movie.Id, movie);

            var okResult = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Update_IfSessionIdAndObjectIdNotTheSame_ShouldReturnBadRequestResult()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<UpdateSessionCommand>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateSessionCommand>(), CancellationToken.None))
                        .ThrowsAsync(new Exception());

            var controller = new SessionController(mediatorMock.Object);

            var result = await controller.Update(movie.Id+1, movie);

            var okResult = Assert.IsType<BadRequestResult>(result);
        }
    }
}
