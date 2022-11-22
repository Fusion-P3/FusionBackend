using System;
using System.Collections.Generic;

namespace ECommerce.Data.Entities
{
    public partial class SuperHero
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Place { get; set; }
    }
}
