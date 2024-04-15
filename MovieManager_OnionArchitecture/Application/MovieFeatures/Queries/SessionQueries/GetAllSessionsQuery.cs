using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.MovieFeatures.Queries.SessionQueries
{
    public class GetAllSessionsQuery : IRequest<IEnumerable<Session>>
    {
        public class GetAllSessionsQueryHandler : IRequestHandler<GetAllSessionsQuery, IEnumerable<Session>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllSessionsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Session>> Handle(GetAllSessionsQuery request, CancellationToken cancellationToken)
            {
                var sessionList = await _context.Sessions.ToListAsync(cancellationToken);

                return sessionList.AsReadOnly();
            }
        }
    }
}
