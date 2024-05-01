using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RefDataFeatures.Queries.GenreQuery
{
    public class GetAllGenresQuery : IRequest<IEnumerable<Genre>>
    {
        public class GetAllMoviesQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<Genre>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllMoviesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Genre>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
            {
                var genreList = await _context.Genres.ToListAsync(cancellationToken);

                return genreList.AsReadOnly();
            }
        }
    }
}
