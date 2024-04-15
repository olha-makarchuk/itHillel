using Application.MovieFeatures.Commands.MovieCommands;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers.v1;

namespace MovieManager.Api.Tests.MovieControllerTests
{
    public class CreateMovieTests
    {
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task Create_IfMovieCreated_ShouldReturnOkAndMovie()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<CreateMovieCommand>().With(x => x.Id, 1).Create();
            
            var movieExpected = new Movie()
            {
                Id = movie.Id,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                Title = movie.Title
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateMovieCommand>(), CancellationToken.None))
                        .ReturnsAsync(movieExpected);

            var controller = new MovieController(mediatorMock.Object);

            var result = await controller.Create(movie);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovie = Assert.IsAssignableFrom<Movie>(okResult.Value);
            Assert.Equal(movieExpected, actualMovie);
        }
    }
}
