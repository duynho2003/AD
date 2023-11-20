using Lab02.Models;

namespace Lab02.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        bool PostProduct(Product newProduct);
    }
}
