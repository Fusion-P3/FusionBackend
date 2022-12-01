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


    public static List<ECommerce.Data.Entities.Product> products = new();
    public ProductService CreateProductService()
    {
        var repo = new Mock<IRepository>();
        repo.Setup(repo => repo.GetAllProducts()).Returns(products);
        repo.Setup(repo => repo.GetProductByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
        {
            foreach (var product in products)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }
            return new ECommerce.Data.Entities.Product();
        });
        repo.Setup(repo => repo.GetProductByNameAsync(It.IsAny<string>())).ReturnsAsync((string name) =>
        {
            foreach (var product in products)
            {
                if (product.ProductName == name)
                {
                    return product;
                }
            }
            return new ECommerce.Data.Entities.Product();
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
    public void CanGetAllProducts()
    {
        var service = CreateProductService();
        Guid guid = Guid.NewGuid();
        string name = "test";
        int quantity = 10;
        int price = 10;
        string description = "test description";
        string image = "test image";
        ECommerce.Data.Entities.Product testProd = new();
        testProd.Id = guid;
        testProd.ProductName = name;
        testProd.ProductQuantity = quantity;
        testProd.ProductPrice = price;
        testProd.ProductDescription = description;
        testProd.ProductImage = image;

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
        int quantity1 = 10;
        int quantity2 = 12;
        int price1 = 10;
        int price2 = 10;
        string description1 = "test description";
        string description2 = "test description2";
        string image1 = "test image";
        string image2 = "test image2";

        ECommerce.Data.Entities.Product testProd1 = new();
        testProd1.Id = guid1;
        testProd1.ProductName = name1;
        testProd1.ProductQuantity = quantity1;
        testProd1.ProductPrice = price1;
        testProd1.ProductDescription = description1;
        testProd1.ProductImage = image1;

        ECommerce.Data.Entities.Product testProd2 = new();
        testProd2.Id = guid2;
        testProd2.ProductName = name2;
        testProd2.ProductQuantity = quantity2;
        testProd2.ProductPrice = price2;
        testProd2.ProductDescription = description2;
        testProd2.ProductImage = image2;

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

        Assert.Equal(testProd1.Id, product1.id);
        Assert.Equal(testProd1.ProductName, product1.name);
        Assert.Equal(testProd1.ProductQuantity, product1.quantity);
        Assert.Equal(testProd1.ProductPrice, product1.price);
        Assert.Equal(testProd1.ProductDescription, product1.description);
        Assert.Equal(testProd1.ProductImage, product1.image);

        Assert.Equal(testProd2.Id, product2.id);
        Assert.Equal(testProd2.ProductName, product2.name);
        Assert.Equal(testProd2.ProductQuantity, product2.quantity);
        Assert.Equal(testProd2.ProductPrice, product2.price);
        Assert.Equal(testProd2.ProductDescription, product2.description);
        Assert.Equal(testProd2.ProductImage, product2.image);

        Assert.Equal(product3.id, Guid.Empty);
        Assert.Null(product3.name);
        Assert.Null(product3.quantity);
        Assert.Null(product3.price);
        Assert.Null(product3.description);
        Assert.Null(product3.image);

        products.Clear();
    }

    [Fact]
    public async void CanGetProductByName()
    {
        var service = CreateProductService();
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();
        string name1 = "test";
        string name2 = "test2";
        int quantity1 = 10;
        int quantity2 = 12;
        int price1 = 10;
        int price2 = 10;
        string description1 = "test description";
        string description2 = "test description2";
        string image1 = "test image";
        string image2 = "test image2";

        ECommerce.Data.Entities.Product testProd1 = new();
        testProd1.Id = guid1;
        testProd1.ProductName = name1;
        testProd1.ProductQuantity = quantity1;
        testProd1.ProductPrice = price1;
        testProd1.ProductDescription = description1;
        testProd1.ProductImage = image1;

        ECommerce.Data.Entities.Product testProd2 = new();
        testProd2.Id = guid2;
        testProd2.ProductName = name2;
        testProd2.ProductQuantity = quantity2;
        testProd2.ProductPrice = price2;
        testProd2.ProductDescription = description2;
        testProd2.ProductImage = image2;

        products.Add(testProd1);
        products.Add(testProd2);

        // two different products
        ECommerce.Models.Product product1 = await service.GetProductByNameAsync(name1);
        ECommerce.Models.Product product2 = await service.GetProductByNameAsync(name2);

        //invalid
        ECommerce.Models.Product product3 = await service.GetProductByNameAsync(String.Empty);

        Assert.NotNull(product1);
        Assert.NotNull(product2);
        Assert.NotNull(product3);

        Assert.Equal(testProd1.Id, product1.id);
        Assert.Equal(testProd1.ProductName, product1.name);
        Assert.Equal(testProd1.ProductQuantity, product1.quantity);
        Assert.Equal(testProd1.ProductPrice, product1.price);
        Assert.Equal(testProd1.ProductDescription, product1.description);
        Assert.Equal(testProd1.ProductImage, product1.image);

        Assert.Equal(testProd2.Id, product2.id);
        Assert.Equal(testProd2.ProductName, product2.name);
        Assert.Equal(testProd2.ProductQuantity, product2.quantity);
        Assert.Equal(testProd2.ProductPrice, product2.price);
        Assert.Equal(testProd2.ProductDescription, product2.description);
        Assert.Equal(testProd2.ProductImage, product2.image);

        Assert.Equal(product3.id, Guid.Empty);
        Assert.Null(product3.name);
        Assert.Null(product3.quantity);
        Assert.Null(product3.price);
        Assert.Null(product3.description);
        Assert.Null(product3.image);

        products.Clear();
    }
}