using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.DAL.Dao
{
    public interface IAppUserDao
    {
        Task<OperationResponse<AppUser>> CreateAppUserAsync(AppUser appUser);
        Task<OperationResponse<AppUser>> GetAppUserByIdAsync(int id);
        Task<OperationResponse<AppUser>> GetAppUserByEmailAsync(string email);
        Task<OperationResponse<IEnumerable<AppUser>>> GetAllAppUsersAsync();
        Task<OperationResponse<AppUser>> UpdateAppUserAsync(AppUser appUser);
        Task<OperationResponse<bool>> DeleteAppUserAsync(int id);
        Task<OperationResponse<bool>> DeleteAppUserStatusAsync(int id);
        Task<IEnumerable<AppUserCollaboratorDto>> GetAllAppUserCollaboratorAsync();
        Task<OperationResponse<bool>> UpdatePasswordAsync(int id, string newPassword);

    }
}
