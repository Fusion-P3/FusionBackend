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
        List<Data.Entities.Product> productEnts = _repo.GetAllProducts();
        List<Product> products = new();
        foreach (var product in productEnts)
        {
            Models.Product productDTO = new();

            productDTO.id = product.Id;
            productDTO.name = product.ProductName;
            productDTO.quantity = product.ProductQuantity;
            productDTO.price = product.ProductPrice;
            productDTO.description = product.ProductDescription;
            productDTO.image = product.ProductImage;

            products.Add(productDTO);

        }
        return products;
    }

    public List<Product> GetSaleProducts()
    {
        List<Data.Entities.Product> productEnts = _repo.GetSaleProducts();
        List<Product> products = new();
        foreach (var product in productEnts)
        {
            Models.Product productDTO = new();

            productDTO.id = product.Id;
            productDTO.name = product.ProductName;
            productDTO.quantity = product.ProductQuantity;
            productDTO.price = product.ProductPrice;
            productDTO.description = product.ProductDescription;
            productDTO.image = product.ProductImage;

            products.Add(productDTO);

        }
        return products;
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var productEnt = await _repo.GetProductByIdAsync(id);

        Product product = new();
        if (productEnt != null)
        {
            product.id = productEnt.Id;
            product.description = productEnt.ProductDescription;
            product.image = productEnt.ProductImage;
            product.name = productEnt.ProductName;
            product.price = productEnt.ProductPrice;
            product.quantity = productEnt.ProductQuantity;

        }
        return product;

    }

    public async Task<Product> GetProductByNameAsync(string productName)
    {
        var productEnt = await _repo.GetProductByNameAsync(productName);

        Product product = new();
        if (productEnt != null)
        {
            product.id = productEnt.Id;
            product.description = productEnt.ProductDescription;
            product.image = productEnt.ProductImage;
            product.name = productEnt.ProductName;
            product.price = productEnt.ProductPrice;
            product.quantity = productEnt.ProductQuantity;

        }
        return product;
    }

    public Task ReduceInventoryByIdAsync(Guid id, int quantity)
    {
        throw new NotImplementedException();
    }
}