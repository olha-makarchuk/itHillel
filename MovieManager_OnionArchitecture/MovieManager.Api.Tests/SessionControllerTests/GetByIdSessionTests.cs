using Application.MovieFeatures.Queries.SessionQueries;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers.v1;

namespace MovieManager.Api.Tests.SessionControllerTests
{
    public class GetByIdSessionTests
    {
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task GetById_IfSessionFound_ShouldReturnOkAndSession()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Session>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetSessionByIdQuery>(), CancellationToken.None))
                        .ReturnsAsync(movie);

            var controller = new SessionController(mediatorMock.Object);

            var result = await controller.GetById(movie.Id);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovie = Assert.IsAssignableFrom<Session>(okResult.Value);
            Assert.Equal(movie, actualMovie);
        }

        [Fact]
        public async Task GetById_IfSessionNotFound_ShouldReturnNotFoundResult()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Session>().With(x => x.Id, 1).Create();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetSessionByIdQuery>(), CancellationToken.None))
                        .ThrowsAsync(new Exception());

            var controller = new SessionController(mediatorMock.Object);

            var result = await controller.GetById(movie.Id);

            var okResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
