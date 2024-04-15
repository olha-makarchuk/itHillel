using Application.MovieFeatures.Commands.SessionCommands;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers.v1;

namespace MovieManager.Api.Tests.SessionControllerTests
{
    public class CreateSessionTests
    {
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task Create_IfSessionCreated_ShouldReturnOkAndSession()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<CreateSessionCommand>().With(x => x.Id, 1).Create();
            
            var movieExpected = new Session()
            {
                Id = movie.Id,
                StartDateTime = DateTime.UtcNow,
                RoomName = movie.RoomName,
                MovieId = movie.Id
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateSessionCommand>(), CancellationToken.None))
                        .ReturnsAsync(movieExpected);

            var controller = new SessionController(mediatorMock.Object);

            var result = await controller.Create(movie);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovie = Assert.IsAssignableFrom<Session>(okResult.Value);
            Assert.Equal(movieExpected, actualMovie);
        }
    }
}
