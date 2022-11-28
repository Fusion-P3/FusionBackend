using ECommerce.Models;
using ECommerce.Data.Entities;

namespace ECommerce.Data
{
    public interface IRepository
    {
        public Task<OrderDetail> AddOrderDetailsAsync(OrderDetail detail);
        Task ClearCart(Guid user_id);

        // public Task<IEnumerable<Product>> GetAllProductsAsync();
        // public Task<Product> GetProductByIdAsync(int id);
        // public Task ReduceInventoryByIdAsync(int id, int purchased);
        // public Task<User> GetUserLoginAsync(string password, string email);
        public Task<Guid> CreateNewUserAndReturnUserIdAsync(User newUser);
        public List<CartItem> GetCartItemsByUserId(Guid user_id);
        public User GetUserByUsername(string? username);
        Entities.Product SubtractProductQuantity(Guid? productId, int? quantity);
    }
}