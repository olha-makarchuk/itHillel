using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MovieFeatures.Commands.MovieCommands
{
    public class CreateMovieCommand : IRequest<Movie>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
    }
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movie>
    {
        private readonly IApplicationDbContext _context;

        public CreateMovieCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
        {
            var movie = new Movie
            {
                Description = command.Description,
                ReleaseDate = command.ReleaseDate,
                Title = command.Title
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return movie;
        }
    }
}
