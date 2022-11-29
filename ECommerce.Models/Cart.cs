namespace ECommerce.Models;

// interface Cart {
//   cartCount: number;
//   products: {
//     product: Product,
//     quantity: number
//   }[];
//   totalPrice: number;
// }

public class Cart
{
    public int? cartCount { get; set; } = -1;
    public List<ProductInternal> products { get; set; } = new List<ProductInternal>();
    public int? totalPrice { get; set; } = -1;
}