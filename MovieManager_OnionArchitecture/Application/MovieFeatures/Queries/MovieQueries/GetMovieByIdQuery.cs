using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MovieFeatures.Queries.MovieQueries
{
    public class GetMovieByIdQuery : IRequest<Movie>
    {
        public int Id { get; set; }

        public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie>
        {
            private readonly IApplicationDbContext _context;

            public GetMovieByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Movie> Handle(GetMovieByIdQuery query, CancellationToken cancellationToken)
            {
                return await _context.Movies.Where(a => a.Id == query.Id)
                    .FirstOrDefaultAsync(cancellationToken) 
                    ?? throw new Exception("Movie not found");
            }
        }
    }
}
