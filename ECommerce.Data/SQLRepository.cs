using ECommerce.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data
{
    public class SQLRepository : IRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<SQLRepository> _logger;

        public SQLRepository(string connectionString, ILogger<SQLRepository> logger)
        {
            this._connectionString = connectionString;
            this._logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            List<Product> products = new List<Product>();

            using SqlConnection connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            string cmdText = "SELECT [ProductId], [ProductName], [ProductQuantity], [ProductPrice], [ProductDescription],[ProductImage] FROM [ecd].[Products];";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int quant = reader.GetInt32(2);
                decimal price = reader.GetDecimal(3);
                string desc = reader.GetString(4);
                string image = reader.GetString(5);

                products.Add(new Product(id, name, quant, price, desc, image));
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllProductsAsync");

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            string cmdText = @"SELECT * FROM [ecd].[Products] WHERE [ProductId] = @ID;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@ID", id);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            Product tmp = new Product();
            while (reader.Read())
            {
                tmp.id = id;
                tmp.name = reader.GetString(1);
                tmp.quantity = reader.GetInt32(2);
                tmp.price = reader.GetDecimal(3);
                tmp.description = reader.GetString(4);
                tmp.image = reader.GetString(5);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetProductByIdAsync");

            return tmp;
        }

        public async Task ReduceInventoryByIdAsync(int id, int purchased)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            string cmdText = @"UPDATE [ecd].[Products] 
                               SET [ProductQuantity] = (
                                    SELECT [ProductQuantity] 
                                    FROM [ecd].[Products] 
                                    WHERE [ProductId] = @ID) - @P 
                               WHERE [ProductId] = @ID;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@P", purchased);

            await cmd.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            _logger.LogInformation("Executed ReduceInventoryAsync");
        }

        public async Task<UserRegisterDTO> GetUserLoginAsync(string password, string email)
        {
            UserRegisterDTO user = new UserRegisterDTO();

            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string cmdText = @"SELECT [UserId], [UserFirstName], [UserLastName], [UserEmail], [UserPassword]" +
                             "FROM [ecd].[Users]" +
                             $"WHERE [UserPassword] = @PW AND [UserEmail] = @EM;";

            using SqlCommand cmd = new(cmdText, connection);

            cmd.Parameters.AddWithValue("@PW", password);
            cmd.Parameters.AddWithValue("@EM", email);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string userFirstName = reader.GetString(1);
                string userLastName = reader.GetString(2);
                string userEmail = reader.GetString(3);
                string userPassword = reader.GetString(4);

                _logger.LogInformation(userFirstName);
                _logger.LogInformation(userLastName);
                _logger.LogInformation(userEmail);
                _logger.LogInformation(userPassword);

                user = new UserRegisterDTO();
            }

            await connection.CloseAsync();

            _logger.LogInformation($"Executed GetUsersAsync");
            _logger.LogInformation(password);
            _logger.LogInformation(email);

            return user;
        }

        public async Task<int> CreateNewUserAndReturnUserIdAsync(UserRegisterDTO newUser)
        {
            int UserId = 0;

            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string cmdText = "INSERT INTO [ecd].[Users] ([UserFirstName], [UserLastName], [UserEmail], [UserPassword])" +
                             @"VALUES (@UFN, @ULN, @UEM, @UPW);" +
                             "SELECT [UserId]" +
                             "FROM [ecd].[Users]" +
                             @"WHERE [UserEmail] = @UEM;";

            using SqlCommand cmd = new(cmdText, connection);

            cmd.Parameters.AddWithValue("@UFN", newUser.firstName);
            cmd.Parameters.AddWithValue("@ULN", newUser.lastName);
            cmd.Parameters.AddWithValue("@UEM", newUser.username);
            cmd.Parameters.AddWithValue("@UPW", newUser.password);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                UserId = reader.GetInt32(0);
            }

            await connection.CloseAsync();

            _logger.LogInformation($"Executed CreateNewUserAsync");

            return UserId;
        }

        public Task<Guid> CreateNewUserAndReturnUserIdAsync(Entities.User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
