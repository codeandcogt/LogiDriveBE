using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using BCrypt.Net;

namespace LogiDriveBE.BAL.Services
{
    public class AppUserBaoService : IAppUserBao
    {
        private readonly IAppUserDao _appUserDao;
        private readonly IRoleDao _roleDao;
        private readonly ICollaboratorDao _collaboratorDao;

        public AppUserBaoService(IAppUserDao appUserDao, IRoleDao roleDao, ICollaboratorDao collaboratorDao)
        {
            _appUserDao = appUserDao;
            _roleDao = roleDao;
            _collaboratorDao = collaboratorDao;
        }

        public async Task<OperationResponse<AppUser>> CreateAppUserWithCollaboratorAsync(AppUser appUser, Collaborator collaborator, int roleId)
        {
            var roleResponse = await _roleDao.GetRoleByIdAsync(roleId);
            if (roleResponse.Code != 200)
            {
                return new OperationResponse<AppUser>(400, "Invalid Role ID");
            }

            appUser.IdRole = roleId;
            appUser.Password = BCrypt.Net.BCrypt.HashPassword(appUser.Password);

            var userResponse = await _appUserDao.CreateAppUserAsync(appUser);
            if (userResponse.Code != 200)
            {
                return userResponse;
            }

            collaborator.IdUser = userResponse.Data.IdAppUser;
            var collaboratorResponse = await _collaboratorDao.CreateCollaboratorAsync(collaborator);

            if (collaboratorResponse.Code != 200)
            {
                // Rollback user creation
                await _appUserDao.DeleteAppUserAsync(userResponse.Data.IdAppUser);
                return new OperationResponse<AppUser>(500, "Failed to create collaborator");
            }

            return userResponse;
        }

        public async Task<OperationResponse<AppUser>> GetAppUserByIdAsync(int id)
        {
            return await _appUserDao.GetAppUserByIdAsync(id);
        }

        public async Task<OperationResponse<AppUser>> GetAppUserByEmailAsync(string email)
        {
            return await _appUserDao.GetAppUserByEmailAsync(email);
        }

        public async Task<OperationResponse<IEnumerable<AppUser>>> GetAllAppUsersAsync()
        {
            return await _appUserDao.GetAllAppUsersAsync();
        }

        public async Task<OperationResponse<AppUser>> UpdateAppUserAsync(AppUser appUser)
        {
            if (!string.IsNullOrEmpty(appUser.Password))
            {
                appUser.Password = BCrypt.Net.BCrypt.HashPassword(appUser.Password);
            }
            return await _appUserDao.UpdateAppUserAsync(appUser);
        }

        public async Task<OperationResponse<bool>> DeleteAppUserAsync(int id)
        {
            return await _appUserDao.DeleteAppUserAsync(id);
        }
    }
}
