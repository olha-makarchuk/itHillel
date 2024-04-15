using Application.MovieFeatures.Queries.MovieQueries;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers.v1;

namespace MovieManager.Api.Tests.MovieControllerTests
{
    public class GetByIdMovieTests
    {
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task GetById_IfMovieFound_ShouldReturnOkAndMovie()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetMovieByIdQuery>(), CancellationToken.None))
                        .ReturnsAsync(movie);

            var controller = new MovieController(mediatorMock.Object);

            var result = await controller.GetById(movie.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovie = Assert.IsAssignableFrom<Movie>(okResult.Value);
            Assert.Equal(movie, actualMovie);
        }

        [Fact]
        public async Task GetById_IfMovieNotFound_ShouldReturnNotFoundResult()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetMovieByIdQuery>(), CancellationToken.None))
                        .ThrowsAsync(new Exception());

            var controller = new MovieController(mediatorMock.Object);

            var result = await controller.GetById(movie.Id);

            var okResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
