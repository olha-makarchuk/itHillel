using Domain.Entities;
using Persistence.Context;

namespace Application.Interfaces
{
	public class ProductService : IProductService
	{
		private readonly ApplicationDbContext _dbContext;
		public ProductService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IEnumerable<Product> GetProductList()
		{
			return _dbContext.Products.ToList();
		}
		public Product GetProductById(int id)
		{
			return _dbContext.Products.Where(x => x.ProductId == id).FirstOrDefault();
		}
		public Product AddProduct(Product product)
		{
			var result = _dbContext.Products.Add(product);
			_dbContext.SaveChanges();
			return result.Entity;
		}
		public Product UpdateProduct(Product product)
		{
            var filteredData = _dbContext.Products.Where(x => x.Id == product.Id).FirstOrDefault()
				?? throw new Exception("Product not Found");

            var result = _dbContext.Products.Update(product);
			_dbContext.SaveChanges();
			return result.Entity;
		}
		public bool DeleteProduct(int Id)
		{
			var filteredData = _dbContext.Products.Where(x => x.Id == Id).FirstOrDefault()
                ?? throw new Exception("Product not Found");

            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }
    }
}
