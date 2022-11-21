using ECommerce.Data;

namespace ECommerce.Service;

public class AuthService
{
    private readonly IRepository _repo;

    public AuthService(IRepository repo)
    {
        _repo = repo;
    }
}