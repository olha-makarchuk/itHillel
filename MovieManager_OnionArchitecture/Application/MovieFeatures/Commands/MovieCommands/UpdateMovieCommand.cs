using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MovieFeatures.Commands.MovieCommands
{
    public class UpdateMovieCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateMovieCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
            {
                var movie = await _context.Movies.Where(a => a.Id == command.Id)
                    .FirstOrDefaultAsync(cancellationToken) 
                    ?? throw new Exception("Movie not found");

                movie.Title = command.Title;
                movie.Description = command.Description;
                movie.ReleaseDate = command.ReleaseDate;

                await _context.SaveChangesAsync();

                return movie.Id;
            }
        }
    }
}
