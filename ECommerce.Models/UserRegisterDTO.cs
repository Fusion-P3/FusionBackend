namespace ECommerce.Models
{
    public class UserRegisterDTO
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? username { get; set; }
        public string? leetCodeName { get; set; }
        public string? password { get; set; }

        public UserRegisterDTO() { }

        public UserRegisterDTO(string firstName, string lastName, string username, string leetcodename, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.leetCodeName = leetcodename;
            this.password = password;
        }
    }
}