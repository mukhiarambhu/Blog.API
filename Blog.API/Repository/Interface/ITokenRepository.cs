using Microsoft.AspNetCore.Identity;

namespace Blog.API.Repository.Interface
{
    public interface ITokenRepository
    {
        string Token(IdentityUser user, List<string> roles);
    }
}
