using Application.MovieFeatures.Queries.SessionQueries;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;
using System.Diagnostics.CodeAnalysis;
using static Application.MovieFeatures.Queries.SessionQueries.GetAllSessionsQuery;

namespace MovieManager.Core.Tests.Queries.SessionQuery
{
    public class GetAllSessionsQueryTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public GetAllSessionsQueryTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Fact]
        public void GetAllMovies_IfSessionsExists_ShouldReturnAllMovies()
        {
            var fixture = new Fixture();

            var session1 = fixture.Build<Session>().With(x => x.Id, 1).Create();
            var session2 = fixture.Build<Session>().With(x => x.Id, 2).Create();
            GetAllSessionsQuery sessionQuery = new();

            _dbContext.AddAndSaveRange(new List<Session> { session1, session2 });

            _dbContext.Assert(async context =>
            {
                var sut = CreateSut(context);
                var result = await sut.Handle(sessionQuery, _cts.Token);

                Assert.Equal(2, result.Count());
                Assert.Equal(session2.Id, result.Last().Id);
                Assert.Equal(session2.RoomName, result.Last().RoomName);
                Assert.Equal(session2.StartDateTime, result.Last().StartDateTime) ;
            });
        }

        [Fact]
        public void GetMovieById_IfSessionsNotExists_ShouldNull()
        {
            var fixture = new Fixture();

            GetAllSessionsQuery sesionsQuery = new();

            _dbContext.Assert(async context =>
            {
                var sut = CreateSut(context);
                var result = await sut.Handle(sesionsQuery, _cts.Token);

                Assert.Empty(result);
            });
        }

        private static GetAllSessionsQueryHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
