using Application.MovieFeatures.Commands.MovieCommands;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers.v1;

namespace MovieManager.Api.Tests.MovieControllerTests
{
    public class DeleteMovieTests
    {
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task Delete_IfMovieFoundAndDeleted_ShouldReturnsOkAndDeletedMovie()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteMovieByIdCommand>(), CancellationToken.None))
                        .ReturnsAsync(movie);

            var controller = new MovieController(mediatorMock.Object);

            var result = await controller.Delete(movie.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovie = Assert.IsAssignableFrom<Movie>(okResult.Value);
            Assert.Equal(movie, actualMovie);
        }

        [Fact]
        public async Task Delete_IfMovieNotFound_ShouldReturnsNotFountResult()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteMovieByIdCommand>(), CancellationToken.None))
                        .ThrowsAsync(new Exception());

            var controller = new MovieController(mediatorMock.Object);

            var result = await controller.Delete(movie.Id);

            var okResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
