using LogiDriveBE.DAL.Models;

namespace LogiDriveBE.AUTH.Aao
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user, IList<string> roles, IList<string> permissions);
    }
}
