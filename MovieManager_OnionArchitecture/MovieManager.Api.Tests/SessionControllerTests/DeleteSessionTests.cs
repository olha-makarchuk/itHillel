using Application.MovieFeatures.Commands.SessionCommands;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers.v1;

namespace MovieManager.Api.Tests.SessionControllerTests
{
    public class DeleteSessionTests
    {
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task Delete_IfSessionFoundAndDeleted_ShouldReturnsOkAndDeletedSession()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Session>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteSessionByIdCommand>(), CancellationToken.None))
                        .ReturnsAsync(movie);

            var controller = new SessionController(mediatorMock.Object);

            var result = await controller.Delete(movie.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovie = Assert.IsAssignableFrom<Session>(okResult.Value);
            Assert.Equal(movie, actualMovie);
        }

        [Fact]
        public async Task Delete_IfSessionNotFound_ShouldReturnsNotFountResult()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Session>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteSessionByIdCommand>(), CancellationToken.None))
                        .ThrowsAsync(new Exception());

            var controller = new SessionController(mediatorMock.Object);

            var result = await controller.Delete(movie.Id);

            var okResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
