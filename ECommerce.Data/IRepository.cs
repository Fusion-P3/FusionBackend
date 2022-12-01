using ECommerce.Models;
using ECommerce.Data.Entities;

namespace ECommerce.Data
{
    public interface IRepository
    {
        public Task<OrderDetail> AddOrderDetailsAsync(OrderDetail detail);
        Task ClearCart(Guid user_id);

        public List<Entities.Product> GetAllProducts();
        public Task<Entities.Product?> GetProductByIdAsync(Guid id);
        public Task<Entities.Product?> GetProductByNameAsync(string productName);
        public Task ReduceInventoryByIdAsync(Guid id, int purchased);
        // public Task<User> GetUserLoginAsync(string password, string email);
        public Task<Guid> CreateNewUserAndReturnUserIdAsync(User newUser);
        public List<CartItem> GetCartItemsByUserId(Guid user_id);
        public User GetUserByUsername(string? username);
        public List<Entities.Product> GetSaleProducts();
        Entities.Product SubtractProductQuantity(Guid? productId, int? quantity);
        Task AddCartItem(CartItem item);
    }
}