using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RefDataFeatures.Commands.DirectorCommand
{
    public class UpdateDirectorCommand : IRequest<Director>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class UpdateMovieCommandHandler : IRequestHandler<UpdateDirectorCommand, Director>
    {
        private readonly IApplicationDbContext _context;

        public UpdateMovieCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Director> Handle(UpdateDirectorCommand command, CancellationToken cancellationToken)
        {
            var director = await _context.Directors.Where(a => a.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Director not found");

            director.Name = command.Name;

            await _context.SaveChangesAsync();

            return director;
        }
    }
}
