using Application.MovieFeatures.Commands.MovieCommands;
using AutoFixture;
using Persistence.Context;
namespace MovieManager.Core.Tests.Commands.MovieCommands
{
    public class CreateMovieCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public CreateMovieCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Fact]
        public void CreateMovieCommand_IfMovieCreates_ShouldReturnCreatedMovie()
        {
            var fixture = new Fixture();

            var movieCommand = fixture.Build<CreateMovieCommand>()
                .With(x => x.Id, 1)
                .Create();

            _dbContext.Assert(async context =>
            {
                var sut = CreateSut(context);

                var result = await sut.Handle(movieCommand, _cts.Token);

                Assert.Equal(movieCommand.Id, result.Id);
                Assert.Equal(movieCommand.Title, result.Title);
                Assert.Equal(movieCommand.ReleaseDate, result.ReleaseDate);
            });
        }

        private static CreateMovieCommandHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
