using Microsoft.EntityFrameworkCore;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Queries.Order
{
    public class GetOrdersQueryHandler : IRequestHandler<IList<OrderResponse>>
    {
        private readonly OnlineStoreContext _context;
        public GetOrdersQueryHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<IList<OrderResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(x=>x.Customer)
                .Include(x=>x.Product)
                .Select(x => new OrderResponse
                {
                    IdOrder = x.IdOrder,
                    IdCustomer = x.IdCustomer,
                    IdProduct = x.IdProduct,
                    DateTimeOrder = x.DateTimeOrder,
                    CustomerResponse = new CustomerResponse
                    {
                        IdCustomer = x.IdCustomer,
                        FirstName = x.Customer.FirstName,
                        MiddleName = x.Customer.MiddleName,
                        LastName = x.Customer.LastName,
                        Age = x.Customer.Age
                    },
                    ProductResponse = new ProductResponse
                    {
                        IdProduct = x.IdProduct,
                        IdCategory = x.Product.IdCategory,
                        Description = x.Product.Description,
                        Name = x.Product.Name,
                        ReleaseDate = x.Product.ReleaseDate,
                        CategoryResponse = new CategoryResponse
                        {
                            IdCategory = x.Product.IdCategory,
                            Name = x.Product.Category.Name
                        }
                    }
                })
                .OrderByDescending(x => x.IdOrder)
                .ToListAsync(cancellationToken);
        }
    }
}
