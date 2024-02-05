using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutomotiveEcommercePlatform.Server.Configurations;
using AutomotiveEcommercePlatform.Server.Data;
using AutomotiveEcommercePlatform.Server.Models;
using AutomotiveEcommercePlatform.Server.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AutomotiveEcommercePlatform.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController] 
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly JwtConfig _jwtConfig;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
           // _jwtConfig = jwtConfig;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            // validate the incoming request
            if (ModelState.IsValid)
            {
                // Checking if the email is already exist 
                var userExist = await _userManager.FindByEmailAsync(requestDto.Email);
                if (userExist != null)
                {
                    return BadRequest(new AuthResult ()
                    {
                        Result = false ,
                        Errors = new List<string> {
                        "Email is already exit"
                        }
                    });
                }
                // create a user 
                var newUser = new ApplicationUser()
                {
                    Email = requestDto.Email,
                    UserName = requestDto.Email 
                };
                var isCreated = await _userManager.CreateAsync(newUser , requestDto.Password);

                if (isCreated.Succeeded)
                {
                    // Generate the token
                    var token = GenerateJwtToken(newUser);
                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = token
                    });
                }

                return BadRequest(new AuthResult()
                {
                    Result = false ,
                    Errors = new List<string>
                    {
                        "Server Error !"
                    }

                });
            }

            return BadRequest();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginRequest)
        {
            if (ModelState.IsValid)
            {
                // check if the user exist 
                var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (existingUser == null)
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid payload"
                        }
                    });
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser,loginRequest.Password);

                if (!isCorrect)
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid Credentials"
                        }
                    });
                var jwtToken = GenerateJwtToken(existingUser);
                return Ok(new AuthResult()
                {
                    Result = true,
                    Token = jwtToken
                });

            }
            return BadRequest(new AuthResult()
            {
                Result = false ,
                Errors = new List<string>()
                {
                    "Invalid Payload"
                }
            });
        }
        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            // Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, value:user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires = DateTime.Now.AddMinutes(15), // will be changed 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
