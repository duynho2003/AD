using Lab02.Models;
using Lab02.Repository;
using Microsoft.EntityFrameworkCore;

namespace Lab02.Service
{
    public class ProductService : IProductRepository
    {
        private readonly AddbContext _dbContext;
        public ProductService(AddbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public bool PostProduct(Product newProduct)
        {
            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
