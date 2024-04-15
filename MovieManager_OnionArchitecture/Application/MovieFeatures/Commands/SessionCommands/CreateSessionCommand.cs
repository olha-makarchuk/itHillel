using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MovieFeatures.Commands.SessionCommands
{
    public class CreateSessionCommand : IRequest<Session>
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string RoomName { get; set; } = null!;
        public DateTime StartDateTime { get; set; }
    }

    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Session>
    {
        private readonly IApplicationDbContext _context;

        public CreateSessionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Session> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
        {
            var session = new Session
            {
                MovieId = command.MovieId,
                RoomName = command.RoomName,
                StartDateTime = command.StartDateTime
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return session;
        }
    }
}
