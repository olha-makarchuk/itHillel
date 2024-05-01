using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RefDataFeatures.Commands.GenreCommand
{

    public class DeleteGenreCommand : IRequest<Director>
    {
        public int Id { get; set; }
    }

    public class DeleteDirectorByIdCommandHandler : IRequestHandler<DeleteGenreCommand, Director>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDirectorByIdCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Director> Handle(DeleteGenreCommand command, CancellationToken cancellationToken)
        {
            var movie = await _context.Directors.Where(a => a.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Genre not Found");

            _context.Directors.Remove(movie);
            await _context.SaveChangesAsync(cancellationToken);

            return movie;
        }
    }
}
