using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Data.Entities;
using OnlineStore.Service.Commands.CustomerCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Commands.OrderCommands
{
    public class DeleteOrderCommand
    {
        public int OrderId { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly OnlineStoreContext _context;

        public DeleteOrderCommandHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderAsync(request.OrderId, cancellationToken);

            if (order != null)
            {
                _context.Remove(order);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            return false;
        }

        private async Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.SingleOrDefaultAsync(x => x.IdOrder == orderId, cancellationToken);
        }
    }
}
