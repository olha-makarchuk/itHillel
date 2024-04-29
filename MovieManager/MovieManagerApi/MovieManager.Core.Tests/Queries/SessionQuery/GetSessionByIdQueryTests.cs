using Application.MovieFeatures.Queries.SessionQueries;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;
using System.Diagnostics.CodeAnalysis;
using static Application.MovieFeatures.Queries.SessionQueries.GetSessionByIdQuery;

namespace MovieManager.Core.Tests.Queries.SessionQuery
{
    public class GetSessionByIdQueryTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public GetSessionByIdQueryTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Fact]
        public void GetMovieById_IfSessionExists_ShouldReturnSession()
        {
            var fixture = new Fixture();

            var session1 = fixture.Build<Session>().With(x => x.Id, 1).Create();
            var session2 = fixture.Build<Session>().With(x => x.Id, 2).Create();
            GetSessionByIdQuery sessionsQuery = new();
            sessionsQuery.Id = session2.Id;

            _dbContext.AddAndSaveRange(new List<Session> { session1, session2 });

            _dbContext.Assert(async context =>
            {
                var sut = CreateSut(context);
                var result = await sut.Handle(sessionsQuery, _cts.Token);

                Assert.NotNull(result);
                Assert.Equal(session2.Id, result.Id);
                Assert.Equal(session2.StartDateTime, result.StartDateTime);
                Assert.Equal(session2.RoomName, result.RoomName);
            });
        }

        [Fact]
        public void GetMovieById_IfSessionNotExists_ShouldReturnThrowExeption()
        {
            var fixture = new Fixture();

            GetSessionByIdQuery sessionsQuery = new();
            sessionsQuery.Id = 1;

            _dbContext.Assert(async context =>
            {
                var sut = CreateSut(context);

                await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(sessionsQuery, _cts.Token));
            });
        }
        private static GetSessionByIdQueryHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
