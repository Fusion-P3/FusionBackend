using ECommerce.Data;
using ECommerce.Models;

namespace ECommerce.Service;

public class InventoryService : IInventoryService
{

    private readonly IRepository _repo;

    public InventoryService(IRepository repo)
    {
        _repo = repo;
    }


    public async Task CreateInventoryItem(Guid userId, Guid productId, int quantity)
    {
        await _repo.CreateInventoryItem(userId, productId, quantity);
    }

    public async Task<List<InventoryDTO>> GetInventoryAsync(Guid userId)
    {
        var inventoryEnts = _repo.GetInventory(userId);
        List<InventoryDTO> inventory = new();
        foreach (var item in inventoryEnts)
        {
            InventoryDTO dto = new();
            dto.ProductId = item.ProductId;
            if (item.ProductId != null)
            {
                var product = await _repo.GetProductByIdAsync(item.ProductId.Value);
                if (product != null)
                    dto.productName = product.ProductName;
            }
            dto.quantity = item.Quantity;
            dto.UserId = item.UserId;
            inventory.Add(dto);
        }

        return inventory;
    }

    public async Task UpdateInventoryItem(Guid userId, Guid productId, int diff)
    {
        await _repo.UpdateInventoryItem(userId, productId, diff);
    }
}