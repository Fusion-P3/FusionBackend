using ECommerce.Models;

namespace ECommerce.Service;

public interface IProductService
{
    public List<Product> GetAllProducts();
    public List<Product> GetSaleProducts();
    public Task<Product> GetProductByIdAsync(Guid id);
    public Task<Product> GetProductByNameAsync(string productName);
    public Task ReduceInventoryByIdAsync(Guid id, int quantity);
}