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
        return products.ToList();
    }

    public Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task ReduceInventoryByIdAsync(int id, int quantity)
    {
        throw new NotImplementedException();
    }
}