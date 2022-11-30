using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Service;
using ECommerce.Data.Entities;
using Moq;

namespace Tests;

public class CartTests
{
    [Fact]
    public void CartCanBeCreated()
    {
        Cart cart = new();
        Assert.NotNull(cart);
    }

    [Fact]
    public void CartDtoCanBeCreated()
    {
        CartDto dto = new();
        Assert.NotNull(dto);
    }

    [Fact]
    public void ProductInternalCreates()
    {
        ProductInternal inter = new();
        Assert.NotNull(inter);
    }

    public static Guid idA = Guid.NewGuid(), idB = Guid.NewGuid();
    public static CartService CreateServie()
    {
        var repo = new Mock<IRepository>();

        repo.Setup(repo => repo.GetCartItemsByUserId(It.IsAny<Guid>())).Returns((Guid id) =>
        {
            return new List<CartItem> {new CartItem{
                UserId = id,
                ProductId = idA,
                Quantity = 1
            }, new CartItem{
                UserId = id,
                ProductId = idB,
                Quantity = 40
            }};
        });
        repo.Setup(repo => repo.GetAllProducts()).Returns(() =>
        {
            return new List<ECommerce.Data.Entities.Product> {
                new ECommerce.Data.Entities.Product{Id = idA}, new ECommerce.Data.Entities.Product{Id = idB}
            };
        });

        return new CartService(repo.Object);
    }

    [Fact]
    public void CartServiceWorks()
    {
        CartService service = CreateServie();
        Cart cart = service.GetCartByUserId(idA);
        Assert.NotNull(cart);
        CartDto dto = service.UpdateOrCreateCart(new CartDto
        {
            userId = idA,
            cart = cart
        });
        Assert.NotNull(dto);
    }
}