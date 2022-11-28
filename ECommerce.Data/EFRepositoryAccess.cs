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

    public async Task<Guid> CreateNewUserAndReturnUserIdAsync(User newUser)
    {
        await _DBcontext.Users.AddAsync(newUser);
        await _DBcontext.SaveChangesAsync();
        Guid id = _DBcontext.Users.Where(x => x.Id == newUser.Id).ToList<User>().ElementAt(0).Id;
        return id;
    }

    public List<Models.Product> GetAllProducts()
    {
        List<Models.Product> products = new();
        foreach (var product in _DBcontext.Products)
        {
            Models.Product productDTO = new();

            productDTO.id = product.Id;
            productDTO.name = product.ProductName;
            productDTO.quantity = product.ProductQuantity;
            productDTO.price = product.ProductPrice;
            productDTO.description = product.ProductDescription;
            productDTO.image = product.ProductImage;

            products.Add(productDTO);

        }
        return products;
    }

    public async Task<Models.Product> GetProductByIdAsync(Guid id)
    {
        var productEnt = await _DBcontext.Products.FirstOrDefaultAsync<Product>(x => x.Id == id);
        Models.Product product = new();
        if (productEnt != null)
        {
            product.id = productEnt.Id;
            product.description = productEnt.ProductDescription;
            product.image = productEnt.ProductImage;
            product.name = productEnt.ProductName;
            product.price = productEnt.ProductPrice;
            product.quantity = productEnt.ProductQuantity;

        }
        return product;
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
}