using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Data.Entities;
using OnlineStore.Service.Commands.CategoryCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Commands.CustomerCommands
{
    public class DeleteCustomerCommand
    {
        public int CustomerId { get; set; }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly OnlineStoreContext _context;

        public DeleteCustomerCommandHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await GetCustomerAsync(request.CustomerId, cancellationToken);

            if (customer != null)
            {
                _context.Remove(customer);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            return false;
        }

        private async Task<Category> GetCustomerAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.IdCategory == categoryId, cancellationToken);
        }
    }
}
