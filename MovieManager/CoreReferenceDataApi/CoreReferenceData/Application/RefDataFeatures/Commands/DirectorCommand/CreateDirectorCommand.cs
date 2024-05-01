using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.RefDataFeatures.Commands.DirectorCommand
{
    public class CreateDirectorCommand : IRequest<Director>
    {
        public string Name { get; set; } = null!;
    }
    public class CreateMovieCommandHandler : IRequestHandler<CreateDirectorCommand, Director>
    {
        private readonly IApplicationDbContext _context;

        public CreateMovieCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Director> Handle(CreateDirectorCommand command, CancellationToken cancellationToken)
        {
            var director = new Director
            {
                Name = command.Name
            };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            return director;
        }
    }
}
