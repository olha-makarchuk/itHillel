using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.RefDataFeatures.Commands.GenreCommand
{
    public class CreateGenreCommand : IRequest<Genre>
    {
        public string Name { get; set; } = null!;
    }
    public class CreateMovieCommandHandler : IRequestHandler<CreateGenreCommand, Genre>
    {
        private readonly IApplicationDbContext _context;

        public CreateMovieCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
        {
            var genre = new Genre
            {
                Name = command.Name
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return genre;
        }
    }
}
