using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepsitory tokenRepsitory;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepsitory tokenRepsitory)
        {
            this.userManager = userManager;
            this.tokenRepsitory = tokenRepsitory;
        }


        // create a new user
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // add roles to the user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was register successfully! Please login.");
                    }
                }
            }
            return BadRequest("Something went wrong!");
        }


        // login and create token
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // get user roles 
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {

                        // create token
                        var jwtToken = tokenRepsitory.CreateJWToken(user, roles.ToList());

                        // map to dto
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };

                        return Ok(response);

                    }
                }

            }
            return BadRequest("Username or password incorrect.");
        }
    }
};