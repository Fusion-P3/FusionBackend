using System;
using System.Collections.Generic;

namespace ECommerce.Data.Entities
{
    public partial class User
    {
        public User()
        {
            CartItems = new HashSet<CartItem>();
            Inventories = new HashSet<Inventory>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? LeetCode { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }
        public int? ProblemsCompleted { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
