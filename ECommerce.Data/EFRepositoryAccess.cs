using ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data;

public class EFRepositoryAccess : IRepository
{
    private readonly fusionContext _DBcontext;

    public EFRepositoryAccess(fusionContext dbcontext)
    {
        _DBcontext = dbcontext;
    }

    public async Task AddCartItem(CartItem item)
    {
        await _DBcontext.CartItems.AddAsync(item);
        await _DBcontext.SaveChangesAsync();
    }

    public async Task<OrderDetail> AddOrderDetailsAsync(OrderDetail detail)
    {
        await _DBcontext.OrderDetails.AddAsync(detail);
        await _DBcontext.SaveChangesAsync();
        return detail;
    }

    public async Task ClearCart(Guid user_id)
    {
        List<CartItem> items = GetCartItemsByUserId(user_id);
        foreach (CartItem item in items)
        {
            _DBcontext.CartItems.Remove(item);
        }
        await _DBcontext.SaveChangesAsync();
    }

    public async Task CreateInventoryItem(Guid userId, Guid productId, int quantity)
    {
        Inventory item = new();
        item.Id = Guid.NewGuid();
        item.ProductId = productId;
        item.UserId = userId;
        item.Quantity = quantity;
        item.Product = await _DBcontext.Products.FirstOrDefaultAsync<Product>(x => x.Id == productId);
        await _DBcontext.AddAsync(item);
        await _DBcontext.SaveChangesAsync();
    }

    public async Task<Guid> CreateNewUserAndReturnUserIdAsync(User newUser)
    {
        await _DBcontext.Users.AddAsync(newUser);
        await _DBcontext.SaveChangesAsync();
        Guid id = _DBcontext.Users.Where(x => x.Id == newUser.Id).ToList<User>().ElementAt(0).Id;
        return id;
    }

    public List<Product> GetAllProducts()
    {

        return _DBcontext.Products.Where(x => x.ProductName != "Xenon" && x.ProductName != "Lead" && x.ProductName != "Hydrogen").ToList();
    }

    public List<Product> GetSaleProducts()
    {
        return _DBcontext.Products.Where(x => x.ProductName == "Xenon" && x.ProductName == "Lead").ToList();
    }

    public List<CartItem> GetCartItemsByUserId(Guid user_id)
    {
        return _DBcontext.CartItems.Where(x => x.UserId == user_id).ToList();
    }

    public List<Inventory> GetInventory(Guid userId)
    {
        return _DBcontext.Inventories.Where(x => x.UserId == userId).ToList();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        var productEnt = await _DBcontext.Products.FirstOrDefaultAsync<Product>(x => x.Id == id);
        return productEnt;
    }

    public async Task<Product?> GetProductByNameAsync(string productName)
    {
        var productEnt = await _DBcontext.Products.FirstOrDefaultAsync<Product>(x => x.ProductName == productName);
        return productEnt;
    }

    public User GetUserByUsername(string? username)
    {
        List<User> user = _DBcontext.Users.Where(x => x.UserName == username).ToList<User>();
        if (user.Count == 0)
        {
            return new User();
        }
        return user.ElementAt(0);
    }

    public Task<User> GetUserLoginAsync(string password, string email)
    {
        throw new NotImplementedException();
    }

    public Task ReduceInventoryByIdAsync(Guid id, int purchased)
    {
        throw new NotImplementedException();
    }

    public Product SubtractProductQuantity(Guid? productId, int? quantity)
    {
        Product p = _DBcontext.Products.Where(x => x.Id == productId).ToArray()[0];
        p.ProductQuantity -= quantity * p.ProductPrice;
        _DBcontext.Products.Update(p);
        p = _DBcontext.Products.Where(x => x.Id == productId).ToArray()[0];
        return p;
    }

    public async Task UpdateInventoryItem(Guid userId, Guid productId, int diff)
    {
        var invItem = await _DBcontext.Inventories.FirstOrDefaultAsync<Inventory>(x => x.UserId == userId && x.ProductId == productId);
        if (invItem != null)
        {
            invItem.Quantity += diff;
            if (invItem.Quantity < 0)
            {
                _DBcontext.Inventories.Update(invItem);
                await _DBcontext.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateUserProblemsCompleted(Guid userId, int problemsCompleted)
    {
        var user = await _DBcontext.Users.FirstOrDefaultAsync<User>(x => x.Id == userId);
        if (user != null)
        {
            user.ProblemsCompleted = problemsCompleted;
            _DBcontext.Users.Update(user);
            await _DBcontext.SaveChangesAsync();
        }
    }
}