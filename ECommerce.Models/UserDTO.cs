using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class UserDTO
    {
        public string? email { get; set; }
        public string? password { get; set; }

        public UserDTO() { }
        public UserDTO( string email, string password) 
        {
            this.email = email;
            this.password = password;
        }
    }
}
