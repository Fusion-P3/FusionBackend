using ECommerce.Data.Entities;

namespace ECommerce.Data;

public class EFRepositoryAccess : IRepository
{
    private readonly fusionContext _DBcontext;

    public EFRepositoryAccess(fusionContext dbcontext)
    {
        _DBcontext = dbcontext;
    }

    public Task<int> CreateNewUserAndReturnUserIdAsync(Models.User newUser)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Models.Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Models.Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.User> GetUserLoginAsync(string password, string email)
    {
        throw new NotImplementedException();
    }

    public Task ReduceInventoryByIdAsync(int id, int purchased)
    {
        throw new NotImplementedException();
    }
}