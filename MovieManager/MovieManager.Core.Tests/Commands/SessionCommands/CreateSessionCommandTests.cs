using Application.MovieFeatures.Commands.SessionCommands;
using AutoFixture;
using Persistence.Context;

namespace MovieManager.Core.Tests.Commands.SessionCommands
{
    public class CreateSessionCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public CreateSessionCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Fact]
        public void CreateSessionCommand_IfSessionCreates_ShouldReturnCreatedSession()
        {
            var fixture = new Fixture();

            var sessionCommand = fixture.Build<CreateSessionCommand>()
                .With(x => x.Id, 1)
                .Create();

            _dbContext.Assert(async context =>
            {
                var sut = CreateSut(context);

                var result = await sut.Handle(sessionCommand, _cts.Token);

                Assert.Equal(sessionCommand.Id, result.Id);
                Assert.Equal(sessionCommand.RoomName, result.RoomName);
                Assert.Equal(sessionCommand.StartDateTime, result.StartDateTime);
            });
        }

        private static CreateSessionCommandHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
