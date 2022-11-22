using ECommerce.Models;

namespace ECommerce.Service;

public interface IProductService
{
    public List<Product> GetAllProducts();
    public Task<Product> GetProductByIdAsync(int id);
    public Task ReduceInventoryByIdAsync(int id, int quantity);
}