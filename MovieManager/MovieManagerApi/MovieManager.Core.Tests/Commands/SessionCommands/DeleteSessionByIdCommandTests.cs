using Application.MovieFeatures.Commands.SessionCommands;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;
using System.Diagnostics.CodeAnalysis;

namespace MovieManager.Core.Tests.Commands.SessionCommands
{
    public class DeleteSessionByIdCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public DeleteSessionByIdCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Fact]
        public void DeleteSessionCommand_IfSessionNotFound_ShouldReturnThrowExeption()
        {
            var fixture = new Fixture();

            var movieCommand = fixture.Build<DeleteSessionByIdCommand>()
                .With(x => x.Id, 1)
                .Create();

            _dbContext.Assert(async context =>
            {
                var sut = DeleteSut(context);

                await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(movieCommand, _cts.Token));
            });
        }

        [Fact]
        public void DeleteSessionCommand_IfSessionFoundAndDeleted_ShouldReturnDeletedSession()
        {
            var fixture = new Fixture();

            var session = fixture.Build<Session>()
                .With(x => x.Id, 1)
                .Create();
            _dbContext.AddAndSave(session);

            DeleteSessionByIdCommand a = new() { Id = session.Id };

            _dbContext.Assert(async context =>
            {
                var deleteSut = DeleteSut(context);

                var result = await deleteSut.Handle(a, _cts.Token);
                Assert.Equal(session.Id, result.Id);
            });
        }

        private static DeleteSessionByIdCommandHandler DeleteSut(ApplicationDbContext context) => new(context);
    }
}
