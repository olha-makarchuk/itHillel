using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MovieFeatures.Queries.SessionQueries
{
    public class GetSessionByIdQuery : IRequest<Session>
    {
        public int Id { get; set; }

        public class GetSessionByIdQueryHandler : IRequestHandler<GetSessionByIdQuery, Session>
        {
            private readonly IApplicationDbContext _context;

            public GetSessionByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Session> Handle(GetSessionByIdQuery query, CancellationToken cancellationToken)
            {
                return await _context.Sessions.Where(a => a.Id == query.Id)
                    .FirstOrDefaultAsync(cancellationToken) 
                    ?? throw new Exception("Session not found");
            }
        }
    }
}
