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
        string name1 = "test";
        string name2 = "test2";
        int quantity = 10;
        int quantity2 = 12;
        int price = 10;
        int price2 = 10;
        string description = "test description";
        string description2 = "test description2";
        string image = "test image";
        string image2 = "test image2";
        ECommerce.Models.Product testProd1 = new(guid1, name1, quantity, price, description, image);
        ECommerce.Models.Product testProd2 = new(guid2, name2, quantity2, price2, description2, image2);

        products.Add(testProd1);
        products.Add(testProd2);

        // two different products
        ECommerce.Models.Product product1 = await service.GetProductByIdAsync(guid1);
        ECommerce.Models.Product product2 = await service.GetProductByIdAsync(guid2);

        //invalid
        ECommerce.Models.Product product3 = await service.GetProductByIdAsync(Guid.Empty);

        Assert.NotNull(product1);
        Assert.NotNull(product2);
        Assert.NotNull(product3);

        Assert.Equal(testProd1.id, product1.id);
        Assert.Equal(testProd1.name, product1.name);
        Assert.Equal(testProd1.quantity, product1.quantity);
        Assert.Equal(testProd1.price, product1.price);
        Assert.Equal(testProd1.description, product1.description);
        Assert.Equal(testProd1.image, product1.image);

        Assert.Equal(testProd2.id, product2.id);
        Assert.Equal(testProd2.name, product2.name);
        Assert.Equal(testProd2.quantity, product2.quantity);
        Assert.Equal(testProd2.price, product2.price);
        Assert.Equal(testProd2.description, product2.description);
        Assert.Equal(testProd2.image, product2.image);

        Assert.Equal(product3.id, Guid.Empty);
        Assert.Null(product3.name);
        Assert.Null(product3.quantity);
        Assert.Null(product3.price);
        Assert.Null(product3.description);
        Assert.Null(product3.image);

        products.Clear();
    }
}