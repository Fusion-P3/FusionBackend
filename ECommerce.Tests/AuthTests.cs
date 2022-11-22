using ECommerce.Models;

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
}