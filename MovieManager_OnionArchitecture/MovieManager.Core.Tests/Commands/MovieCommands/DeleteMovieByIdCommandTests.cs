using Application.MovieFeatures.Commands.MovieCommands;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace MovieManager.Core.Tests.Commands.MovieCommands
{
    public class DeleteMovieByIdCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public DeleteMovieByIdCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Fact]
        public void DeleteMovieCommand_IfMovieNotFound_ShouldReturnThrowExeption()
        {
            var fixture = new Fixture();

            var movieCommand = fixture.Build<DeleteMovieByIdCommand>()
                .With(x => x.Id, 1)
                .Create();

            _dbContext.Assert(async context =>
            {
                var sut = DeleteSut(context);

                await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(movieCommand, _cts.Token));
            });
        }

        [Fact]
        public void DeleteMovieCommand_IfMovieFoundAndDeleted_ShouldReturnDeletedMovie()
        {
            var fixture = new Fixture();

            var movieEntityToCreate = fixture.Build<Movie>()
                .With(x => x.Id, 1)
                .Create();
            _dbContext.AddAndSave(movieEntityToCreate);

            DeleteMovieByIdCommand a = new();
            a.Id = movieEntityToCreate.Id;

            _dbContext.Assert(async context =>
            {
                var deleteSut = DeleteSut(context);

                var result = await deleteSut.Handle(a, _cts.Token);
                Assert.Equal(movieEntityToCreate.Id, result.Id);
            });
        }

        private static DeleteMovieByIdCommandHandler DeleteSut(ApplicationDbContext context) => new(context);
    }
}
