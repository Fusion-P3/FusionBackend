using ECommerce.Data;
using ECommerce.Models;

namespace ECommerce.Service;

public interface IAuthService
{
    public Task<Guid> CreateNewUserAndGetIdAsync(UserRegisterDTO newUser);
    UserDTO LoginUser(UserDTO lR);
}