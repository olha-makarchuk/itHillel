using Microsoft.EntityFrameworkCore;
using OnlineStore.Contract.Response;
using OnlineStore.Data.Context;
using OnlineStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace OnlineStore.Service.Commands.OrderCommand
{
    public class UpsertOrderCommand
    {
        public int IdOrder { get; set; }

        public int IdProduct { get; set; }

        public int IdCustomer { get; set; }
        public DateTime DateTimeOrder { get; set; }


        public Order UpsertOrder()
        {
            var order = new Order
            {
                IdOrder = IdOrder,
                IdProduct = IdProduct,
                IdCustomer = IdCustomer,
                DateTimeOrder = DateTimeOrder
            };

            return order;
        }
    }

    public class UpsertOrderCommandHandler : IRequestHandler<UpsertOrderCommand, OrderResponse>
    {
        private readonly OnlineStoreContext _context;

        public UpsertOrderCommandHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(UpsertOrderCommand request, CancellationToken cancellationToken = default)
        {
            var order = await GetSessionAsync(request.IdOrder, cancellationToken);

            if (order == null)
            {
                order = request.UpsertOrder();
                await _context.AddAsync(order, cancellationToken);
            }

            order.IdOrder = request.IdOrder;
            order.IdProduct = request.IdProduct;
            order.IdCustomer = request.IdCustomer;
            order.DateTimeOrder = request.DateTimeOrder;

            await _context.SaveChangesAsync(cancellationToken);

            return new OrderResponse
            {
                IdOrder = order.IdOrder,
                IdProduct = order.IdProduct,
                IdCustomer = order.IdCustomer,
                DateTimeOrder = order.DateTimeOrder,
                ProductResponse = order?.Product != null ?
                new ProductResponse
                {
                    IdProduct = order.IdProduct,
                    IdCategory = order.Product.IdCategory,
                    Name = order.Product.Name,
                    ReleaseDate = order.Product.ReleaseDate,
                    CategoryResponse = order?.Product.Category != null ?
                    new CategoryResponse
                    {
                        IdCategory = order.Product.IdCategory,
                        Name = order.Product.Category.Name
                    } : null
                } : null
            };
        }

        private async Task<Order> GetSessionAsync(int orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .SingleOrDefaultAsync(x => x.IdOrder == orderId, cancellationToken);
        }
    }
}
