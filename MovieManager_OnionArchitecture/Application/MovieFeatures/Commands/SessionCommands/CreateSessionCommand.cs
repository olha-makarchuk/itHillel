using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MovieFeatures.Commands.SessionCommands
{
    public class CreateSessionCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string RoomName { get; set; }
        public DateTime StartDateTime { get; set; }

        public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateSessionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
            {
                var session = new Session
                {
                    MovieId = command.MovieId,
                    RoomName = command.RoomName,
                    StartDateTime = command.StartDateTime
                };

                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();

                return session.Id;
            }
        }
    }
}
