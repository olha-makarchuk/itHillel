using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MovieFeatures.Commands.MovieCommands
{
    public class DeleteMovieByIdCommand : IRequest<Movie>
    {
        public int Id { get; set; }
    }

    public class DeleteMovieByIdCommandHandler : IRequestHandler<DeleteMovieByIdCommand, Movie>
    {
        private readonly IApplicationDbContext _context;

        public DeleteMovieByIdCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Handle(DeleteMovieByIdCommand command, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies.Where(a => a.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Movie not Found");

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync(cancellationToken);

            return movie;
        }
    }
}
