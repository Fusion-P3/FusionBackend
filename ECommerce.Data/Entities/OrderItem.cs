using System;
using System.Collections.Generic;

namespace ECommerce.Data.Entities
{
    public partial class OrderItem
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual OrderDetail? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
