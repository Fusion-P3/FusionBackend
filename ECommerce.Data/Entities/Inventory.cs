using System;
using System.Collections.Generic;

namespace ECommerce.Data.Entities
{
    public partial class Inventory
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
