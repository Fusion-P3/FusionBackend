namespace ECommerce.Models;

// export interface CartDto {
//   userId: string,
//   cart: Cart
// }

public class CartDto
{
    public Guid userId { get; set; } = Guid.Empty;
    public Cart? cart { get; set; }
}