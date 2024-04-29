using Application.MovieFeatures.Commands.SessionCommands;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace MovieManager.Core.Tests.Commands.SessionCommands
{
    public class UpdateSessionCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public UpdateSessionCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Fact]
        public void UpdateSessionCommand_IfSessionNotFound_ShouldReturnThrowExeption()
        {
            var fixture = new Fixture();

            var movieCommandToUpdate = fixture.Build<UpdateSessionCommand>()
                .With(x => x.Id, 1)
                .Create();

            _dbContext.Assert(async context =>
            {
                var sut = UpdateSut(context);

                await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(movieCommandToUpdate, _cts.Token));
            });
        }

        [Fact]
        public void UpdateSessionCommand_IfSessionFoundAndUpdated_ShouldReturnUpdatedSession()
        {
            var fixture = new Fixture();

            var session = fixture.Build<Session>()
                .With(x => x.Id, 1)
                .Create();
            _dbContext.AddAndSave(session);

            var sessionCommandToUpdate = fixture.Build<UpdateSessionCommand>()
                .With(x => x.Id, session.Id)
                .Create();

            _dbContext.Assert(async context =>
            {
                var sut2 = UpdateSut(context);
                var result = await sut2.Handle(sessionCommandToUpdate, _cts.Token);

                Assert.Equal(session.Id, result.Id);
            });
        }

        private static UpdateSessionCommandHandler UpdateSut(ApplicationDbContext context) => new(context);
    }
}
