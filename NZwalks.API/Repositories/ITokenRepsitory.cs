using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepsitory
    {
        public string CreateJWToken(IdentityUser user, List<string> roles);
    }
}
