using ECommerce.Models;

namespace ECommerce.Service
{
    public interface IInventoryService
    {
        Task CreateInventoryItem(Guid userId, Guid productId, int quantity);
        Task<List<InventoryDTO>> GetInventoryAsync(Guid userId);
        Task UpdateInventoryItem(Guid userId, Guid productId, int diff);
    }
}