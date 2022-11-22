using ECommerce.Models;
using ECommerce.Data.Entities;

namespace ECommerce.Data
{
    public interface IRepository
    {
        // public Task<IEnumerable<Product>> GetAllProductsAsync();
        // public Task<Product> GetProductByIdAsync(int id);
        // public Task ReduceInventoryByIdAsync(int id, int purchased);
        // public Task<User> GetUserLoginAsync(string password, string email);
        public Task<Guid> CreateNewUserAndReturnUserIdAsync(User newUser);
        User GetUserByUsername(string? username);
    }
}