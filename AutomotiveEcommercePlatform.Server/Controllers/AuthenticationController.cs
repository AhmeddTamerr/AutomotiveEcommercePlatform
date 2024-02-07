using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using AutomotiveEcommercePlatform.Server.Configurations;
using AutomotiveEcommercePlatform.Server.Data;
using AutomotiveEcommercePlatform.Server.Models;
using AutomotiveEcommercePlatform.Server.Models.DTOs;
using AutomotiveEcommercePlatform.Server.Services;
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
        private readonly IConfiguration _configuration;
        //private readonly RoleManager<IdentityRole> _roleManager;


        // private readonly IUserService _userService;

        public AuthenticationController(UserManager<ApplicationUser> userManager , IConfiguration configuration /*, RoleManager<IdentityRole> roleManager , IUserService userService;*/)
        {
            _userManager = userManager;
            _configuration = configuration;
            //_roleManager = roleManager;
            //_userService = userService;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            // validate the incoming request
            if (ModelState.IsValid)
            {

                // Validate the email 
                if (!Regex.IsMatch(requestDto.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string> {
                            "Invalid Email!"
                        }
                    });

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

                /*
                // Register as an admin from normal registration End Point is Forbidden 
                if (requestDto.Role.ToUpper() == "ADMIN")
                    return BadRequest(new AuthResult ()
                   {
                       Result = false ,
                       Errors = new List<string> {
                       "This Action is Forbidden!"
                       }
                   });
                */
                // Password Match 
                if (requestDto.Password != requestDto.ConfirmePassword) 
                    return BadRequest(new AuthResult ()
                   {
                       Result = false ,
                       Errors = new List<string> {
                       "Un Matched Passwords!"
                       }
                   });
                // Validate The Phone Number
                if (!Regex.IsMatch(requestDto.PhoneNumber , @"^\d{11}$")) 
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string> {
                            "Invalid Phone Number!"
                        }
                    });

                // create a user 
                var newUserIdentityData = new ApplicationUser()
                {
                    Email = requestDto.Email,
                    UserName = requestDto.Email ,
                    PhoneNumber = requestDto.PhoneNumber
                };
/*
                var newUser = new User()
                {
                    FirstName = requestDto.FirstName,
                    LastName  = requestDto.LastName,
                    DisplayName = $"{FirstName} {LastName}"
                };
*/
                var isCreated = await _userManager.CreateAsync(newUserIdentityData, requestDto.Password);
                

                if (isCreated.Succeeded)
                {
                    // Generate the token
                    var token = GenerateJwtToken(newUserIdentityData);

                    /*
                        // If his role is  user put him in a user table , if it's a trader put him in a trader table 
                        if (requestDto.Role.ToUpper == "USER")
                        {
                            await _userService.Add(newUser);
                            AddRoleAsync(requestDto.Email,"User")
                        }else if (requestDto.Role.ToUpper == "TRADER"){
                                await _TraderService.Add(newUser); // I will make this service 
                                AddRoleAsync(requestDto.Email,"Trader")             
                        }
                    */

                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = token
                    });

                }

                return  BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = isCreated.Errors.Select(des => des.Description).ToList()
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
                            "Invalid Email or Password !"
                        }
                    });
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser,loginRequest.Password);

                if (!isCorrect)
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid Email or Password !"
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
                    "Invalid Email or Password !"
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
/*        public async Task<string> AddRoleAsync(string email , string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _roleManager.RoleExistsAsync(role))
                return "Invalid User Id or Role ";
            if (await _userManager.IsInRoleAsync(user, role))
                return "User already assigned to this role";
            var result = await _userManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
                return string.Empty;
            else return "Something went wrong";
        }*/
    }
}
