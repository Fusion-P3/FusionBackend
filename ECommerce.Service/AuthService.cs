using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Data.Entities;
using System.Security.Cryptography;

namespace ECommerce.Service;

public class AuthService : IAuthService
{
    private readonly IRepository _repo;

    public AuthService(IRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> CreateNewUserAndGetIdAsync(UserRegisterDTO newUser)
    {
        byte[] passwordHash;
        byte[] passwordSalt;
        CreatePasswordHash(newUser.password!, out passwordHash, out passwordSalt);

        User dbUser = new User();
        dbUser.Id = Guid.NewGuid();
        dbUser.FirstName = newUser.firstName;
        dbUser.LastName = newUser.lastName;
        dbUser.UserName = newUser.username;
        dbUser.LeetCode = newUser.leetCodeName;
        dbUser.PasswordHash = passwordHash;
        dbUser.PasswordSalt = passwordSalt;
        dbUser.ProblemsCompleted = 0;

        Guid userID = await _repo.CreateNewUserAndReturnUserIdAsync(dbUser);

        return userID == dbUser.Id ? userID : Guid.Empty;
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (HMACSHA512 hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    public UserDTO LoginUser(UserDTO lR)
    {
        User user = _repo.GetUserByUsername(lR.username);
        if (user.UserName == null)
        {
            return new UserDTO();
        }

        if (VerifyPasswordHash(lR.password!, user.PasswordHash!, user.PasswordSalt!))
        {
            lR.leetCodeName = user.LeetCode;
            return lR;
        }
        else
        {
            return new UserDTO();
        }
    }

    public Guid GetIdByUsername(string username)
    {
        User users = _repo.GetUserByUsername(username);
        return users.Id;
    }
}