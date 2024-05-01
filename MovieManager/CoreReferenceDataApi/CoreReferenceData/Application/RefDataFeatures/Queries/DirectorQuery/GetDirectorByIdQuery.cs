using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RefDataFeatures.Queries.DirectorQuery
{

    public class GetDirectorByIdQuery : IRequest<Director>
    {
        public int Id { get; set; }

        public class GetMovieByIdQueryHandler : IRequestHandler<GetDirectorByIdQuery, Director>
        {
            private readonly IApplicationDbContext _context;

            public GetMovieByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Director> Handle(GetDirectorByIdQuery query, CancellationToken cancellationToken)
            {
                return await _context.Directors.Where(a => a.Id == query.Id)
                    .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new Exception("Director not found");
            }
        }
    }
}
