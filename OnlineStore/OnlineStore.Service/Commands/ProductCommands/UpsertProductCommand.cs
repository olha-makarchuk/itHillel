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

namespace OnlineStore.Service.Commands.ProductCommand
{
    public class UpsertProductCommand
    {
        public int IdProduct { get; set; }

        public int IdCategory { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Product UpsertProduct()
        {
            var product = new Product
            {
                IdProduct = IdProduct,
                IdCategory = IdCategory,
                Name = Name,
                Description = Description,
                ReleaseDate = ReleaseDate
            };

            return product;
        }
    }

    public class UpsertSessionCommandHandler : IRequestHandler<UpsertProductCommand, ProductResponse>
    {
        private readonly OnlineStoreContext _context;

        public UpsertSessionCommandHandler(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(UpsertProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await GetProductAsync(request.IdProduct, cancellationToken);

            if (product == null)
            {
                product = request.UpsertProduct();
                await _context.AddAsync(product, cancellationToken);
            }

            product.IdProduct = request.IdProduct;
            product.IdCategory = request.IdCategory;
            product.Name = request.Name;
            product.Description = request.Description;
            product.ReleaseDate = request.ReleaseDate;

            await _context.SaveChangesAsync(cancellationToken);

            return new ProductResponse
            {
                IdProduct = product.IdProduct,
                IdCategory = product.IdCategory,
                Name = product.Name,
                Description = product.Description,
                ReleaseDate = product.ReleaseDate,
                CategoryResponse = product?.Category != null ? 
                new CategoryResponse
                {
                    IdCategory = product.Category.IdCategory,
                    Name = product.Category.Name
                }:null
            };
        }

        private async Task<Product> GetProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(x => x.Category)
                .SingleOrDefaultAsync(x => x.IdProduct == productId, cancellationToken);
        }
    }
}
