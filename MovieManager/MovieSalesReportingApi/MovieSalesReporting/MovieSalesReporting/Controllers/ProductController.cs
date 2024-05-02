using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MovieSalesReporting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public ProductController(IProductService _productService, IRabbitMQProducer rabbitMQProducer)
        {
            productService = _productService;
            _rabbitMQProducer = rabbitMQProducer;
        }

        [HttpGet("productlist")]
        public IEnumerable<Product> ProductList()
        {
            var productList = productService.GetProductList();
            return productList;
        }

        [HttpGet("getproductbyid")]
        public Product GetProductById(int Id)
        {
            return productService.GetProductById(Id);
        }

        [HttpPost("addproduct")]
        public Product AddProduct(Product product)
        {
            var productData = productService.AddProduct(product);
            _rabbitMQProducer.SendProductMessage(productData, "AddProduct");
            return productData;
        }

        [HttpPut("updateproduct")]
        public Product UpdateProduct(Product product)
        {
            _rabbitMQProducer.SendProductMessage(product, "UpdateProduct");
            return productService.UpdateProduct(product);
        }

        [HttpDelete("deleteproduct")]
        public bool DeleteProduct(int Id)
        {
            _rabbitMQProducer.SendProductMessage(Id, "DeleteProduct");
            return productService.DeleteProduct(Id);
        }
    }
}
