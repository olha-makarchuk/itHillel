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
    public class GetCategoryByIdHandler : IRequestHandler<int, CategoryResponse>
    {
        private readonly OnlineStoreContext _context;

        public GetCategoryByIdHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse> Handle(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .Where(x => x.IdCategory == categoryId)
                .Select(x => new CategoryResponse
                {
                    IdCategory = x.IdCategory,
                    Name = x.Name
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
