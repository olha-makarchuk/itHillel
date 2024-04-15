using Application.MovieFeatures.Queries.MovieQueries;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;
using System.Diagnostics.CodeAnalysis;
using static Application.MovieFeatures.Queries.MovieQueries.GetMovieByIdQuery;

namespace MovieManager.Core.Tests.Queries.MovieQuery
{
    public class GetMovieByIdQueryTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public GetMovieByIdQueryTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Fact]
        public void GetMovieById_IfMovieExists_ShouldReturnMovie()
        {
            var fixture = new Fixture();

            var movie1 = fixture.Build<Movie>().With(x => x.Id, 1).Create();
            var movie2 = fixture.Build<Movie>().With(x => x.Id, 2).Create();
            GetMovieByIdQuery moviesQuery = new();
            moviesQuery.Id = movie2.Id;

            _dbContext.AddAndSaveRange(new List<Movie> { movie1, movie2 });

            _dbContext.Assert(async context =>
            {
                var sut = CreateSut(context);
                var result = await sut.Handle(moviesQuery, _cts.Token);

                Assert.NotNull(result);
                Assert.Equal(movie2.Id, result.Id);
                Assert.Equal(movie2.Title, result.Title);
                Assert.Equal(movie2.ReleaseDate, result.ReleaseDate);
                Assert.Equal(movie2.Description, result.Description);
            });
        }

        [Fact]
        public void GetMovieById_IfMovieNotExists_ShouldReturnThrowExeption()
        {
            var fixture = new Fixture();

            GetMovieByIdQuery moviesQuery = new();
            moviesQuery.Id = 1;

            _dbContext.Assert(async context =>
            {
                var sut = CreateSut(context);

                await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(moviesQuery, _cts.Token));
            });
        }
        private static GetMovieByIdQueryHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
