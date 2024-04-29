using Application.MovieFeatures.Queries.MovieQueries;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers.v1;

namespace MovieManager.Api.Tests.MovieControllerTests
{
    public class GetAllMovieTests
    {
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task GetAll_IfMovieExists_ShouldReturnOkAndMovies()
        {
            var fixture = new Fixture();

            var expectedMovies = new List<Movie>();
            expectedMovies.Add(fixture.Build<Movie>().With(x => x.Id, 1).Create());
            expectedMovies.Add(fixture.Build<Movie>().With(x => x.Id, 2).Create());

            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllMoviesQuery>(), CancellationToken.None))
                        .ReturnsAsync(expectedMovies);

            var controller = new MovieController(mediatorMock.Object);

            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovies = Assert.IsAssignableFrom<IEnumerable<Movie>>(okResult.Value);
            Assert.Equal(expectedMovies, actualMovies);
        }

        [Fact]
        public async Task GetAll_IfMovieNotExists_ShouldReturnNull()
        {
            var expectedMovies = new List<Movie>();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllMoviesQuery>(), CancellationToken.None))
                        .ReturnsAsync(expectedMovies);

            var controller = new MovieController(mediatorMock.Object);

            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovies = Assert.IsAssignableFrom<IEnumerable<Movie>>(okResult.Value);
            Assert.Equal(0, actualMovies.ToList()?.Count);
        }
    }
}