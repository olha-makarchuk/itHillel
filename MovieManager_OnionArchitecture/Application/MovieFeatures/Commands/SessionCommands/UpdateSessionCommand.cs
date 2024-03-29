using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MovieFeatures.Commands.SessionCommands
{
    public class UpdateSessionCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string RoomName { get; set; }
        public DateTime StartDateTime { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateSessionCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateSessionCommand command, CancellationToken cancellationToken)
            {
                var session = await _context.Sessions.Where(a => a.Id == command.Id)
                    .FirstOrDefaultAsync(cancellationToken) 
                    ?? throw new Exception("Session not found");

                session.MovieId = command.MovieId;
                session.RoomName = command.RoomName;
                session.StartDateTime = command.StartDateTime;

                await _context.SaveChangesAsync();

                return session.Id;
            }
        }
    }
}
