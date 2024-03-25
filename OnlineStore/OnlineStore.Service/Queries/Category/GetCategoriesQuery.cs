using Microsoft.EntityFrameworkCore;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Queries.Category
{
    public class GetCategoriesQueryHandler :
        IRequestHandler<IList<CategoryResponse>>
    {
        private readonly OnlineStoreContext _context;
        public GetCategoriesQueryHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<IList<CategoryResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(x => new CategoryResponse
                {
                    IdCategory = x.IdCategory,
                    Name = x.Name
                })
                .OrderByDescending(x => x.IdCategory)
                .ToListAsync(cancellationToken);
        }
    }
}
