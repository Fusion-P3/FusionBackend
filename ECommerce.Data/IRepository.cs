using ECommerce.Models;
using ECommerce.Data.Entities;

namespace ECommerce.Data
{
    public interface IRepository
    {
        public List<Entities.Product> GetAllProducts();
        public Task<Entities.Product?> GetProductByIdAsync(Guid id);
        public Task ReduceInventoryByIdAsync(Guid id, int purchased);
        // public Task<User> GetUserLoginAsync(string password, string email);
        public Task<Guid> CreateNewUserAndReturnUserIdAsync(User newUser);
        User GetUserByUsername(string? username);
    }
}