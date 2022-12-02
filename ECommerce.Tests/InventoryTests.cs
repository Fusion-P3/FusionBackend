using Moq;
using ECommerce.Models;
using ECommerce.Service;
using ECommerce.Data;
using ECommerce.Data.Entities;

namespace Tests;
public class InventoryTest
{
    [Fact]
    public void InventoryDTOCreates()
    {
        InventoryDTO inv = new();
        inv.productName = "test";

        Assert.NotNull(inv);
        Assert.NotNull(inv.productName);
    }

    public static List<Inventory> items = new List<Inventory>();
    public InventoryService CreateInventoryService()
    {
        var repo = new Mock<IRepository>();
        repo.Setup(repo => repo.GetInventory(It.IsAny<Guid>())).Returns((Guid userId) =>
        {
            return items;
        });

        return new InventoryService(repo.Object);
    }

    [Fact]
    public void InventoryServiceCreates()
    {
        var service = CreateInventoryService();
        Assert.NotNull(service);
    }

    [Fact]
    public async void CanGetInventory()
    {
        var service = CreateInventoryService();

        Inventory item = new();
        item.UserId = Guid.NewGuid();
        item.ProductId = Guid.NewGuid();
        item.Quantity = 10;
        items.Add(item);

        var ret_items = await service.GetInventoryAsync(item.UserId.Value);

        Assert.Equal(items.Count, ret_items.Count);
        items.Clear();
    }

    [Fact]
    public void LCResponseTypeCreates()
    {
        LCResponseType lCResponseType = new();
        lCResponseType.matchedUser = new();
        lCResponseType.matchedUser.username = "test";
        lCResponseType.matchedUser.submitStats = new();
        lCResponseType.matchedUser.submitStats.aCSubmissionNum = new ACSubmissionNumType[1];
        var item = lCResponseType.matchedUser.submitStats.aCSubmissionNum[0];
        item = new();
        item.count = 1;
        item.difficulty = "test";
        item.submissions = 1;

        Assert.NotNull(lCResponseType);
    }




}