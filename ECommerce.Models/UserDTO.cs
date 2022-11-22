using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class UserDTO
    {
        public string? username { get; set; }
        public string? password { get; set; }

        public UserDTO() { }
        public UserDTO(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
