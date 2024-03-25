using Microsoft.EntityFrameworkCore;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Context;
using OnlineStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Queries.Product
{
    public class GetProductByIdQueryHandler : IRequestHandler<int, ProductResponse>
    {
        private readonly OnlineStoreContext _context;

        public GetProductByIdQueryHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(x => x.IdProduct == productId)
                .Select(x => new ProductResponse
                {
                    IdProduct = x.IdProduct,
                    IdCategory = x.IdCategory,
                    Description = x.Description,
                    Name = x.Name,
                    ReleaseDate = x.ReleaseDate
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
