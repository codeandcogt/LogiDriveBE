using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IAppUserBao
    {
        Task<OperationResponse<AppUser>> CreateAppUserWithCollaboratorAsync(AppUser appUser, Collaborator collaborator, int roleId);
        Task<OperationResponse<AppUser>> GetAppUserByIdAsync(int id);
        Task<OperationResponse<AppUser>> GetAppUserByEmailAsync(string email);
        Task<OperationResponse<IEnumerable<AppUser>>> GetAllAppUsersAsync();
        Task<OperationResponse<AppUser>> UpdateAppUserAsync(AppUser appUser);
        Task<OperationResponse<bool>> DeleteAppUserAsync(int id);
    }
}
