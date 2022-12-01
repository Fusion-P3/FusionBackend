namespace ECommerce.Models;

public class InventoryDTO
{
    public Guid? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public string? productName { get; set; }
    public int? quantity { get; set; }

}