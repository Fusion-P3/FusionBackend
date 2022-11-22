using Moq;
using ECommerce.Models;
using ECommerce.Service;
using ECommerce.Data;
using ECommerce.Data.Entities;

namespace Tests;
public class AuthTest
{
    [Fact]
    public void UserDTOCreates()
    {
        UserDTO user = new UserDTO("test", "test");
        Assert.Equal("test", user.username);
        Assert.Equal("test", user.password);
        Assert.NotNull(user);
    }

    [Fact]
    public void UserRegisterDTOCreates()
    {
        UserRegisterDTO user = new UserRegisterDTO("test", "test", "test", "test", "test");
        Assert.Equal("test", user.firstName);
        Assert.Equal("test", user.leetCodeName);
        Assert.Equal("test", user.lastName);
        Assert.Equal("test", user.username);
        Assert.Equal("test", user.password);
        Assert.NotNull(user);
    }

    public static List<User> users = new List<User>();
    public AuthService CreateAuthService()
    {
        var repo = new Mock<IRepository>();
        repo.Setup(repo => repo.CreateNewUserAndReturnUserIdAsync(It.IsAny<User>())).ReturnsAsync((User user) =>
        {
            foreach (User u in users)
            {
                if (u.UserName == user.UserName)
                {
                    return Guid.Empty;
                }
            }
            users.Add(user);
            return user.Id;
        });

        repo.Setup(repo => repo.GetUserByUsername(It.IsAny<string>())).Returns((string username) =>
        {
            foreach (User u in users)
            {
                if (u.UserName == username)
                {
                    return u;
                }
            }
            return new();
        });
        return new AuthService(repo.Object);
    }

    [Fact]
    public void AuthServiceCreates()
    {
        var service = CreateAuthService();
        Assert.NotNull(service);
    }

    [Fact]
    public async void CanCreateUser()
    {
        var service = CreateAuthService();

        UserRegisterDTO user = new UserRegisterDTO("test", "test", "test", "test", "test");
        Guid id = await service.CreateNewUserAndGetIdAsync(user);
        Assert.NotEqual(Guid.Empty, id);
        id = await service.CreateNewUserAndGetIdAsync(user);
        Assert.Equal(Guid.Empty, id);
        users.Clear();
    }

    [Fact]
    public async void CanLogin()
    {
        var service = CreateAuthService();
        await service.CreateNewUserAndGetIdAsync(new UserRegisterDTO("test", "test", "test", "test", "test"));

        UserDTO user = service.LoginUser(new UserDTO("test", "test"));
        Assert.NotNull(user);
        Assert.Equal("test", user.username);

        user = service.LoginUser(new UserDTO("test", "wrong password"));
        Assert.Null(user.username);

        user = service.LoginUser(new UserDTO("not in database", "test"));
        Assert.Null(user.username);
        users.Clear();
    }
}