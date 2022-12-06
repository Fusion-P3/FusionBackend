using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using ECommerce.Service;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;

namespace ECommerce.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IInventoryService _inventoryService;
        private readonly IProductService _productService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService service, IInventoryService inventoryService, IProductService productService, ILogger<AuthController> logger)
        {
            this._productService = productService;
            this._inventoryService = inventoryService;
            this._service = service;
            this._logger = logger;
        }

        [Route("auth/register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegisterDTO newUser)
        {
            _logger.LogInformation("auth/register triggered");
            try
            {
                _logger.LogInformation("auth/register completed successfully");
                Guid id = await _service.CreateNewUserAndGetIdAsync(newUser);
                await _inventoryService.CreateInventoryItem(id, Guid.Parse("33914a4d-5e85-48cf-a443-e70672eb07d0"), 0);
                return id != Guid.Empty ? Ok(id) : BadRequest("username already taken");
            }
            catch
            {
                _logger.LogWarning("auth/register completed with errors");
                return BadRequest();
            }
        }


        [Route("auth/login")]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> LoginAsync([FromBody] UserDTO LR)
        {
            _logger.LogInformation("auth/login triggered");
            try
            {
                if (LR.username == null || LR.password == null)
                {
                    _logger.LogInformation("Null values for email/password");
                    return BadRequest();

                }

                UserDTO LoginAuth = _service.LoginUser(LR);
                var graphQLClient = new GraphQLHttpClient("https://leetcode.com/graphql", new NewtonsoftJsonSerializer());
                var leetcodeRequest = new GraphQLRequest
                {
                    Query = @"
                            { matchedUser(username: """ + LoginAuth.leetCodeName + @""") {
                            username
                            submitStats: submitStatsGlobal {
                            acSubmissionNum {
                            difficulty
                            count
                            submissions
                            }
                            }
                            }
                            }"
                };

                var graphQLResponse = await graphQLClient.SendQueryAsync<LCResponseType>(leetcodeRequest);
                int newProbsCompleted = graphQLResponse.Data.matchedUser.submitStats.aCSubmissionNum[0].count;
                if (LoginAuth.problemsCompleted != newProbsCompleted)
                {
                    Guid userId = LoginAuth.userId;
                    await _inventoryService.UpdateInventoryItem(userId, Guid.Parse("33914a4d-5e85-48cf-a443-e70672eb07d0"), (newProbsCompleted - LoginAuth.problemsCompleted.Value) * 100);
                    await _service.UpdateProblemsCompleted(userId, newProbsCompleted);
                    LoginAuth.problemsCompleted = newProbsCompleted;
                }


                _logger.LogInformation("auth/login completed successfully");
                return LoginAuth.username == null ? BadRequest("Unable to login.") : Ok(LoginAuth);
            }
            catch
            {
                _logger.LogWarning("auth/login completed with errors");
                return BadRequest();
            }
        }

        [Route("auth/logout")]
        [HttpPost]
        public ActionResult Logout()
        {
            _logger.LogInformation("auth/logout triggered");
            _logger.LogInformation("auth/logout completed successfully");
            return Ok();
        }

        [HttpGet]
        [Route("auth/{username}")]
        public ActionResult<Guid> GetIdByUsername(string username)
        {
            Guid id = _service.GetIdByUsername(username);
            if (id != Guid.Empty)
            {
                _logger.LogInformation($"Sent the id for {username}");
                return Ok(id);
            }
            _logger.LogError("Unable to send user, does it exist");
            return BadRequest("User does not exist");
        }

    }
}
