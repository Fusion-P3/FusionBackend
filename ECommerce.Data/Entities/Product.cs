using System;
using System.Collections.Generic;

namespace ECommerce.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            Inventories = new HashSet<Inventory>();
            OrderItems = new HashSet<OrderItem>();
        }

        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public int? ProductQuantity { get; set; }
        public int? ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImage { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
