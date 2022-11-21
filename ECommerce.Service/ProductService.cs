using ECommerce.Data;

namespace ECommerce.Service;

public class ProductService
{
    private readonly IRepository _repo;

    public ProductService(IRepository repo)
    {
        _repo = repo;
    }
}