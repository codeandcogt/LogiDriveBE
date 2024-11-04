using LogiDriveBE.DAL.Models;

namespace LogiDriveBE.AUTH.Aao
{
    public interface IAuthAao
    {
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<IList<string>> GetUserRolesAsync(int userId);
        Task<IList<string>> GetUserPermissionsAsync(int userId);
    }
}
