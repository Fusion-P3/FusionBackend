using ECommerce.Models;

namespace ECommerce.Service;

public interface IProductService
{
    public List<Product> GetAllProducts();
    public Task<Product> GetProductByIdAsync(Guid id);
    public Task ReduceInventoryByIdAsync(Guid id, int quantity);
}