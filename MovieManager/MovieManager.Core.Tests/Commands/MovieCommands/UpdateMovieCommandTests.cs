using Application.MovieFeatures.Commands.MovieCommands;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace MovieManager.Core.Tests.Commands.MovieCommands
{
    public class UpdateMovieCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public UpdateMovieCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Fact]
        public void UpdateMovieCommand_IfMovieNotFound_ShouldReturnThrowExeption()
        {
            var fixture = new Fixture();

            var movieCommandToUpdate = fixture.Build<UpdateMovieCommand>()
                .With(x => x.Id, 1)
                .Create();

            _dbContext.Assert(async context =>
            {
                var sut = UpdateSut(context);

                await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(movieCommandToUpdate, _cts.Token));
            });
        }

        [Fact]
        public void UpdateMovieCommand_IfMovieFoundAndUpdated_ShouldReturnUpdatedMovie()
        {
            var fixture = new Fixture();

            var movieEntityToCreate = fixture.Build<Movie>()
                .With(x => x.Id, 1)
                .Create();
            _dbContext.AddAndSave(movieEntityToCreate);
            
            var movieCommandToUpdate = fixture.Build<UpdateMovieCommand>()
                .With(x => x.Id, movieEntityToCreate.Id)
                .Create();
             
            _dbContext.Assert(async context =>
            {
                var sut2 = UpdateSut(context);
                var result = await sut2.Handle(movieCommandToUpdate, _cts.Token);

                Assert.Equal(movieCommandToUpdate.Title, result.Title);
            });
        }

        private static UpdateMovieCommandHandler UpdateSut(ApplicationDbContext context) => new(context);
    }
}
