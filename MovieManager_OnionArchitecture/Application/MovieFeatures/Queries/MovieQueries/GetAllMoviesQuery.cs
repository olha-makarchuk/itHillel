using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MovieFeatures.Queries.MovieQueries
{
    public class GetAllMoviesQuery: IRequest<IEnumerable<Movie>>
    {
        public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<Movie>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllMoviesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
            {
                var movieList = await _context.Movies.ToListAsync(cancellationToken);

                return movieList.AsReadOnly();
            }
        }
    }
}
