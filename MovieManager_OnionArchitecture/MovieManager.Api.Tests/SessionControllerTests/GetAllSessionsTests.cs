using Application.MovieFeatures.Queries.SessionQueries;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers.v1;

namespace MovieManager.Api.Tests.SessionControllerTests
{
    public class GetAllSessionsTests
    {
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task GetAll_IfSessionExists_ShouldReturnOkAndSessions()
        {
            var fixture = new Fixture();

            var expectedMovies = new List<Session>();
            expectedMovies.Add(fixture.Build<Session>().With(x => x.Id, 1).Create());
            expectedMovies.Add(fixture.Build<Session>().With(x => x.Id, 2).Create());

            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllSessionsQuery>(), CancellationToken.None))
                        .ReturnsAsync(expectedMovies);

            var controller = new SessionController(mediatorMock.Object);

            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovies = Assert.IsAssignableFrom<IEnumerable<Session>>(okResult.Value);
            Assert.Equal(expectedMovies, actualMovies);
        }

        [Fact]
        public async Task GetAll_IfSessionNotExists_ShouldReturnNull()
        {
            var expectedMovies = new List<Session>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllSessionsQuery>(), CancellationToken.None))
                        .ReturnsAsync(expectedMovies);

            var controller = new SessionController(mediatorMock.Object);

            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualMovies = Assert.IsAssignableFrom<IEnumerable<Session>>(okResult.Value);
            Assert.Equal(0, actualMovies.ToList()?.Count);
        }
    }
}