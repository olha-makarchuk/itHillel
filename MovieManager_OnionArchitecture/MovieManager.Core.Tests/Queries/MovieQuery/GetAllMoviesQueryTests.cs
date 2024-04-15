using Application.MovieFeatures.Queries.MovieQueries;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;
using static Application.MovieFeatures.Queries.MovieQueries.GetAllMoviesQuery;

namespace MovieManager.Core.Tests.Queries.MovieQuery
{
    public class GetAllMoviesQueryTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public GetAllMoviesQueryTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Fact]
        public void GetAllMovies_IfMoviesExists_ShouldReturnAllMovies()
        {
            var fixture = new Fixture();

            var movie1 = fixture.Build<Movie>().With(x => x.Id, 1).Create();
            var movie2 = fixture.Build<Movie>().With(x => x.Id, 2).Create();
            GetAllMoviesQuery moviesQuery = new();

            _dbContext.AddAndSaveRange(new List<Movie> { movie1, movie2 });

            _dbContext.Assert(async context =>
            {
                var sut = CreateSut(context);
                var result = await sut.Handle(moviesQuery, _cts.Token);

                Assert.Equal(2, result.Count());
                Assert.Equal(movie2.Id, result.Last().Id);
                Assert.Equal(movie2.Title, result.Last().Title);
                Assert.Equal(movie2.ReleaseDate, result.Last().ReleaseDate);
                Assert.Equal(movie2.Description, result.Last().Description);
            });
        }

        [Fact]
        public void GetAllMovies_IfMoviesNotExists_ShouldReturnNull()
        {
            var fixture = new Fixture();

            GetAllMoviesQuery moviesQuery = new();

            _dbContext.Assert(async context =>
            {
                var sut = CreateSut(context);
                var result = await sut.Handle(moviesQuery, _cts.Token);

                Assert.Empty(result);
            });
        }

        private static GetAllMoviesQueryHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
