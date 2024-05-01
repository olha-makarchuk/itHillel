using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RefDataFeatures.Commands.DirectorCommand
{
    public class DeleteDirectorCommand : IRequest<Director>
    {
        public int Id { get; set; }
    }

    public class DeleteMovieByIdCommandHandler : IRequestHandler<DeleteDirectorCommand, Director>
    {
        private readonly IApplicationDbContext _context;

        public DeleteMovieByIdCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Director> Handle(DeleteDirectorCommand command, CancellationToken cancellationToken)
        {
            var director = await _context.Directors.Where(a => a.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Director not Found");

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync(cancellationToken);

            return director;
        }
    }
}
