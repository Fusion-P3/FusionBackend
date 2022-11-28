using Moq;
using ECommerce.Models;
using ECommerce.Service;
using ECommerce.Data;
using ECommerce.Data.Entities;

namespace Tests;
public class ProductTests
{
    [Fact]
    public void ProductDTOCreates()
    {
        Guid guid = Guid.NewGuid();
        ProductDTO productDTO = new(guid, 10);
        Assert.NotNull(productDTO);
        Assert.Equal(guid, productDTO.id);
        Assert.Equal(10, productDTO.quantity);
    }


    public static List<ECommerce.Models.Product> products = new();
    public ProductService CreateProductService()
    {
        var repo = new Mock<IRepository>();
        repo.Setup(repo => repo.GetAllProducts()).Returns(products);
        repo.Setup(repo => repo.GetProductByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
        {
            foreach (ECommerce.Models.Product product in products)
            {
                if (product.id == id)
                {
                    return product;
                }
            }
            return new ECommerce.Models.Product();
        });

        return new ProductService(repo.Object);
    }

    [Fact]
    public void ProductServiceCreates()
    {
        var service = CreateProductService();
        Assert.NotNull(service);
    }

    [Fact]
    public async void CanGetAllProducts()
    {
        var service = CreateProductService();
        Guid guid = Guid.NewGuid();
        string name = "test";
        int quantity = 10;
        int price = 10;
        string description = "test description";
        string image = "test image";
        ECommerce.Models.Product testProd = new(guid, name, quantity, price, description, image);

        products.Add(testProd);
        products.Add(testProd);
        products.Add(testProd);
        products.Add(testProd);

        List<ECommerce.Models.Product> retrievedProds = service.GetAllProducts();
        Assert.NotNull(retrievedProds);
        Assert.Equal(products.Count, retrievedProds.Count);
        products.Clear();
    }

    [Fact]
    public async void CanGetProductById()
    {
        var service = CreateProductService();
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();
        string name = "test";
        int quantity = 10;
        int price = 10;
        string description = "test description";
        string image = "test image";
        ECommerce.Models.Product testProd1 = new(guid1, name, quantity, price, description, image);
        ECommerce.Models.Product testProd2 = new(guid2, name, quantity, price, description, image);

        products.Add(testProd1);
        products.Add(testProd2);

        ECommerce.Models.Product product1 = await service.GetProductByIdAsync(guid1);
        ECommerce.Models.Product product2 = await service.GetProductByIdAsync(guid2);

        Assert.NotNull(product1);
        Assert.NotNull(product2);

        Assert.Equal(testProd1.id, product1.id);
        Assert.Equal(testProd2.id, product2.id);

        products.Clear();
    }
}