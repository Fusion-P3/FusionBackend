using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ProductDTO
    {
        public Guid id { get; set; }
        public int quantity { get; set; }

        public ProductDTO() { }

        public ProductDTO(Guid id, int quantity)
        {
            this.id = id;
            this.quantity = quantity;
        }
    }
}
