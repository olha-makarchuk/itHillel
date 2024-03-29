using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MovieFeatures.Commands.MovieCommands
{
    public class DeleteMovieByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteMovieCommandHandler :  IRequestHandler<DeleteMovieByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public DeleteMovieCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteMovieByIdCommand command, CancellationToken cancellationToken)
            {
                var movie = await _context.Movies.Where(a => a.Id == command.Id)
                    .FirstOrDefaultAsync(cancellationToken) 
                    ?? throw new Exception("Movie not Found");

                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync(cancellationToken);

                return movie.Id;
            }
        }
    }
}
