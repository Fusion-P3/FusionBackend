using Moq;
using ECommerce.Models;
using ECommerce.Service;
using ECommerce.Data;
using ECommerce.Data.Entities;

namespace Tests;
public class CheckoutTests
{
    [Fact]
    public void CheckoutDTOWorks()
    {
        CheckoutDTO dto = new CheckoutDTO();
        dto.user_id = Guid.NewGuid();
        Assert.NotNull(dto);
        Assert.NotEqual(Guid.Empty, dto.user_id);
    }
    public CheckoutService CreateService()
    {
        var repo = new Mock<IRepository>();
        repo.Setup(repo => repo.GetCartItemsByUserId(It.IsAny<Guid>())).Returns((Guid id) =>
        {
            List<CartItem> items = new();
            items.Add(new CartItem
            {
                UserId = id,
                ProductId = Guid.NewGuid(),
                Quantity = 0
            });
            items.Add(new CartItem
            {
                UserId = id,
                ProductId = Guid.NewGuid(),
                Quantity = 1
            });
            return items;
        });
        repo.Setup(repo => repo.SubtractProductQuantity(It.IsAny<Guid>(), It.IsAny<int>())).Returns(
            (Guid id, int amt) =>
            {
                return new ECommerce.Data.Entities.Product { Id = id, ProductQuantity = amt };
            }
        );

        repo.Setup(repo => repo.AddOrderDetailsAsync(It.IsAny<OrderDetail>())).ReturnsAsync((OrderDetail det) =>
        {
            return det;
        });

        return new CheckoutService(repo.Object);
    }

    [Fact]
    public async void CheckoutServiceWorks()
    {
        CheckoutService service = CreateService();
        CheckoutDTO dto = new CheckoutDTO { user_id = Guid.NewGuid() };
        CheckoutDTO dto2 = await service.CheckoutAsync(dto);
        Assert.Equal(dto, dto2);
    }
}