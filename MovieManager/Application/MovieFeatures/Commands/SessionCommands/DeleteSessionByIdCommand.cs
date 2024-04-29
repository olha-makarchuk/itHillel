using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MovieFeatures.Commands.SessionCommands
{
    public class DeleteSessionByIdCommand : IRequest<Session>
    {
        public int Id { get; set; }
    }

    public class DeleteSessionByIdCommandHandler : IRequestHandler<DeleteSessionByIdCommand, Session>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSessionByIdCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Session> Handle(DeleteSessionByIdCommand command, CancellationToken cancellationToken)
        {
            var session = await _context.Sessions.Where(a => a.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Session not Found");

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync(cancellationToken);

            return session;
        }
    }
}
