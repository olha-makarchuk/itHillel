using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RefDataFeatures.Commands.GenreCommand
{
    public class UpdateGenreCommand : IRequest<Director>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class UpdateMovieCommandHandler : IRequestHandler<UpdateGenreCommand, Director>
    {
        private readonly IApplicationDbContext _context;

        public UpdateMovieCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Director> Handle(UpdateGenreCommand command, CancellationToken cancellationToken)
        {
            var movie = await _context.Directors.Where(a => a.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Movie not found");

            movie.Name = command.Name;

            await _context.SaveChangesAsync();

            return movie;
        }
    }
}
