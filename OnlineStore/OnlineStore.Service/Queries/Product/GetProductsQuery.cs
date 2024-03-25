using Microsoft.EntityFrameworkCore;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Queries.Product
{
    public class GetProductsQueryHandler : IRequestHandler<IList<ProductResponse>>
    {
        private readonly OnlineStoreContext _context;
        public GetProductsQueryHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<IList<ProductResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Select(x => new ProductResponse
                {
                    IdProduct = x.IdProduct,
                    IdCategory = x.IdCategory,
                    Name = x.Name,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate,
                    CategoryResponse = new CategoryResponse
                    {
                        IdCategory = x.Category.IdCategory,
                        Name = x.Category.Name
                    }
                })
                .OrderByDescending(x => x.IdCategory)
                .ToListAsync(cancellationToken);
        }
    }
}
