using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        public string CreateJWToken(IdentityUser user, List<string> roles);
    }
}
