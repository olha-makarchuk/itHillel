using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Data.Entities;
using OnlineStore.Service.Commands.CustomerCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Commands.ProductCommands
{
    public class DeleteProductCommand
    {
        public int ProductId { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly OnlineStoreContext _context;

        public DeleteProductCommandHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await GetProductAsync(request.ProductId, cancellationToken);

            if (product != null)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            return false;
        }

        private async Task<Product> GetProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.SingleOrDefaultAsync(x => x.IdProduct == productId, cancellationToken);
        }
    }
}
