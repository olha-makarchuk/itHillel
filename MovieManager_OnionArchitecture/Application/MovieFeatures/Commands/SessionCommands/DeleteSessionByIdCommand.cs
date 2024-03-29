using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MovieFeatures.Commands.SessionCommands
{
    public class DeleteSessionByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteSessionCommandHandler :  IRequestHandler<DeleteSessionByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public DeleteSessionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteSessionByIdCommand command, CancellationToken cancellationToken)
            {
                var session = await _context.Sessions.Where(a => a.Id == command.Id)
                    .FirstOrDefaultAsync(cancellationToken) 
                    ?? throw new Exception("Session not Found");

                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync(cancellationToken);

                return session.Id;
            }
        }
    }
}
