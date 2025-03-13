using Microsoft.AspNetCore.Identity;

namespace MBlog.API.Repositories;

public interface ITokenRepository
{
    string CreateJWTToken(IdentityUser user, List<string> roles);
}
