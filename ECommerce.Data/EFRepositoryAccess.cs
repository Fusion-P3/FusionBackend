using ECommerce.Data.Entities;

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
            Models.Product productDTO = new()
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

    public Task<Models.Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserLoginAsync(string password, string email)
    {
        throw new NotImplementedException();
    }

    public Task ReduceInventoryByIdAsync(int id, int purchased)
    {
        throw new NotImplementedException();
    }
}