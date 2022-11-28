using ECommerce.Data;
using ECommerce.Models;

namespace ECommerce.Service;

public class ProductService : IProductService
{
    private readonly IRepository _repo;

    public ProductService(IRepository repo)
    {
        _repo = repo;
    }

    public List<Product> GetAllProducts()
    {
        var products = _repo.GetAllProducts();
        return products;
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var product = await _repo.GetProductByIdAsync(id);
        return product;
    }

    public Task ReduceInventoryByIdAsync(Guid id, int quantity)
    {
        throw new NotImplementedException();
    }
}