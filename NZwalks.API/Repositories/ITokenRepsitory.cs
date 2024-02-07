using Microsoft.AspNetCore.Identity;

namespace NZwalks.API.Repositories
{
    public interface ITokenRepsitory
    {
        public string CreateJWToken(IdentityUser user, List<string> roles);
    }
}
