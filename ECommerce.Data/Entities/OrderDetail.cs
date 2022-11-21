using System;
using System.Collections.Generic;

namespace ECommerce.Data.Entities
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Amount { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
