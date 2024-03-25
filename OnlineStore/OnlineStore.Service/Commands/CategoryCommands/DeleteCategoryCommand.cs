using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Commands.CategoryCommands
{
    public class DeleteCategoryCommand
    {
        public int CategoryId { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly OnlineStoreContext _context;

        public DeleteCategoryCommandHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(request.CategoryId, cancellationToken);

            if(category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            return false;
        }

        private async Task<Customer> GetCategoryAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.IdCustomer == customerId, cancellationToken);
        }
    }
}