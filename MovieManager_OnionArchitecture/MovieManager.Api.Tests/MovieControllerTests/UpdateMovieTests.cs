using Application.MovieFeatures.Commands.MovieCommands;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers.v1;

namespace MovieManager.Api.Tests.MovieControllerTests
{
    public class UpdateMovieTests
    {
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task Update_IfMovieFoundAndUpdated_ShouldReturnOkAndUpdatedMovie()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<UpdateMovieCommand>().With(x => x.Id, 1).Create();
            var movieExpected = new Movie()
            {
                Id = movie.Id,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                Title = movie.Title
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateMovieCommand>(), CancellationToken.None))
                        .ReturnsAsync(movieExpected);

            var controller = new MovieController(mediatorMock.Object);

            var result = await controller.Update(movie.Id, movie);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovie = Assert.IsAssignableFrom<Movie>(okResult.Value);
            Assert.Equal(movieExpected, actualMovie);
        }

        [Fact]
        public async Task Update_IfMovieNotFound_ShouldReturnNotFoundResult()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<UpdateMovieCommand>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateMovieCommand>(), CancellationToken.None))
                        .ThrowsAsync(new Exception());

            var controller = new MovieController(mediatorMock.Object);

            var result = await controller.Update(movie.Id, movie);

            var okResult = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Update_IfMovieIdAndObjectIdNotTheSame_ShouldReturnBadRequestResult()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<UpdateMovieCommand>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateMovieCommand>(), CancellationToken.None))
                        .ThrowsAsync(new Exception());

            var controller = new MovieController(mediatorMock.Object);

            var result = await controller.Update(movie.Id+1, movie);

            var okResult = Assert.IsType<BadRequestResult>(result);
        }
    }
}
