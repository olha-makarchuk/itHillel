using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RefDataFeatures.Queries.DirectorQuery
{
    public class GetAllDirectorsQuery : IRequest<IEnumerable<Director>>
    {
        public class GetAllMoviesQueryHandler : IRequestHandler<GetAllDirectorsQuery, IEnumerable<Director>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllMoviesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Director>> Handle(GetAllDirectorsQuery request, CancellationToken cancellationToken)
            {
                var movieList = await _context.Directors.ToListAsync(cancellationToken);

                return movieList.AsReadOnly();
            }
        }
    }
}
