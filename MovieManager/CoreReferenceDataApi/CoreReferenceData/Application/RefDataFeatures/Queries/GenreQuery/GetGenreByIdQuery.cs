using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RefDataFeatures.Queries.GenreQuery
{
    public class GetGenreByIdQuery : IRequest<Genre>
    {
        public int Id { get; set; }

        public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, Genre>
        {
            private readonly IApplicationDbContext _context;

            public GetGenreByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Genre> Handle(GetGenreByIdQuery query, CancellationToken cancellationToken)
            {
                return await _context.Genres.Where(a => a.Id == query.Id)
                    .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new Exception("Genre not found");
            }
        }
    }
}
