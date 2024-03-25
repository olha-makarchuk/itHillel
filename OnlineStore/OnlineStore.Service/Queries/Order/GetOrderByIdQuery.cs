using Microsoft.EntityFrameworkCore;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Context;
using OnlineStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Queries.Order
{
    public class GetOrderByIdQueryHandler : IRequestHandler<int, OrderResponse>
    {
        private readonly OnlineStoreContext _context;

        public GetOrderByIdQueryHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(int orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(x => x.IdOrder == orderId)
                .Select(x => new OrderResponse
                {
                    IdOrder = x.IdOrder,
                    IdCustomer = x.IdCustomer,
                    DateTimeOrder = x.DateTimeOrder,
                    IdProduct = x.IdProduct
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
