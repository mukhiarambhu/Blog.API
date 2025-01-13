using Blog.API.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.API.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository( IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Token(IdentityUser user, List<string> roles)
        {
            //create claims
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Email, user.Email),
            };

            claims.AddRange(roles.Select(role=> new Claim(ClaimTypes.Role, role)));
            //create jwt security Toke

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience : configuration["jwt:Audience"],
                claims:claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credential

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
