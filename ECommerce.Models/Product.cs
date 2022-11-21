using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Product
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public string? description { get; set; }
        public string? image { get; set; }

        public Product() { }

        public Product(int id, string name, int quantity, decimal price, string description, string image)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
            this.price = price;
            this.description = description;
            this.image = image;
        }
    }
}
