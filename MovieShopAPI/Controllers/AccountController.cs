using ApplicationCore.Contract.Repository;
using ApplicationCore.Contract.Service;
using ApplicationCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AccountController(IAccountService accountService, IConfiguration configuration, IUserRepository userRepository)
        {
            _accountService = accountService;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var user = await _accountService.RegisterUser(model);
            return Ok(user);
        }

        [HttpGet]
        [Route("check-email")]
        public async Task<IActionResult> checkEmail(string email)
        {
            var emails = await _userRepository.GetUserByEmail(email);

            if (emails == null)
            {
                return NotFound("Email does not exist, please Register");
            }

            return Ok(emails);
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model.Email, model.Password);

            if (user != null)
            {
                // create token
                var jwtToken = CreateJwtToken(user);
                return Ok(new { token = jwtToken });
            }
            // iOS (), Android, Angular, React, Java
            // token, JWT (Json Web Token)

            // Client will send email/password to API, POST 
            // API will validate the email/password and if valid then API will create the JWT token and send to client
            // Its Client's responsibility to store the token some where
            // Angular, React (localstorage or sessionstorage in browser)
            // iOS/Android, device
            // when client needs some secure information or needs to perform any operation that requires users to be 
            // authenticated then client needs to send the token to the API in the Http Header
            // Once the API recieves the token from client it will validate the JWT token and if vlaid it will send the data back to the client
            // IF JWT token is in valid or token is expired then API will send 401 Unauthorized
            throw new UnauthorizedAccessException("Please check email and password");
            // return Unauthorized(new { errorMessage = "Please check email and password" });

        }

        private string CreateJwtToken(UserModel user)
        {
            // create the claims 

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim("Country", "USA"),
                new Claim("language", "english")
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // specify a secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));

            // specify the algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //specify the expiration of the token
            var tokenExpiration = DateTime.UtcNow.AddHours(2);

            // create and object with all the above information so create the token
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "MovieShop, Inc",
                Audience = "MovieShop Clients"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.CreateToken(tokenDetails);
            return tokenHandler.WriteToken(encodedJwt);
        }


    }

}

