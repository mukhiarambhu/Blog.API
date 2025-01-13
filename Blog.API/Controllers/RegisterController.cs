using Blog.API.Models.Dto;
using Blog.API.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public RegisterController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            //cretae identity user object
            var user = new IdentityUser
            {
                Email = registerDto.Email.Trim(),
                UserName = registerDto.Email.Trim(),

            };
            //create user

            var identityUser = await userManager.CreateAsync(user, registerDto.Password);

            if (identityUser.Succeeded)
                //add role
            {
                identityUser = await userManager.AddToRoleAsync(user, "Reader");

                if (identityUser.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityUser.Errors.Any())
                    {
                        foreach (var error in identityUser.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                    }
                }
            }
            else
            {
                if (identityUser.Errors.Any())
                {
                    foreach (var error in identityUser.Errors) { 
                    ModelState.AddModelError("",error.Description);
                    }

                }
            }

            return ValidationProblem(ModelState);

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Login_RequestDto requestDto)
        {
            //check email

            var identityUser = await userManager.FindByEmailAsync(requestDto.Email);

            if(identityUser is not null)
            {
                //check password

                var checkPassowrdResult = await userManager.CheckPasswordAsync(identityUser,requestDto.Password);

                if (checkPassowrdResult)
                {
                    //get roles 
                    var userRoles = await userManager.GetRolesAsync(identityUser);

                    //get Token
                    var jwtToken = tokenRepository.Token(identityUser,userRoles.ToList()); 

                    var response = new LoginResponseDto
                    {
                        Email = requestDto.Email,
                        Roles = userRoles.ToList(),
                        Token = jwtToken

                    };
                    //create jwt
                    return Ok(response);
                }
            }

            ModelState.AddModelError("", "Email or passowrd is wrong");
            return ValidationProblem(ModelState);
        }
    }
}
