using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using BCrypt.Net;
using LogiDriveBE.DAL.Models.DTO;

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

        public async Task<OperationResponse<AppUser>> UpdateAppUserWithCollaboratorAsync(AppUser appUser, Collaborator collaborator, int roleId)
        {
            // Verificar si el rol es válido
            var roleResponse = await _roleDao.GetRoleByIdAsync(roleId);
            if (roleResponse.Code != 200)
            {
                return new OperationResponse<AppUser>(400, "Invalid Role ID");
            }

            // Actualizar el rol en el usuario
            appUser.IdRole = roleId;

            // Si la contraseña no está vacía, encriptarla
            if (!string.IsNullOrEmpty(appUser.Password))
            {
                appUser.Password = BCrypt.Net.BCrypt.HashPassword(appUser.Password);
            }

            // Intentar actualizar el usuario
            var userResponse = await _appUserDao.UpdateAppUserAsync(appUser);
            if (userResponse.Code != 200)
            {
                return new OperationResponse<AppUser>(500, "Failed to update user");
            }

            // Actualizar la información del colaborador, si está asociada al usuario
            collaborator.IdUser = userResponse.Data.IdAppUser;
            var collaboratorResponse = await _collaboratorDao.UpdateCollaboratorAsync(collaborator);

            if (collaboratorResponse.Code != 200)
            {
                return new OperationResponse<AppUser>(500, "Failed to update collaborator");
            }

            return new OperationResponse<AppUser>(200, "User and collaborator updated successfully", userResponse.Data);
        }

        public async Task<OperationResponse<bool>> DeleteUserAndCollaboratorStatusAsync(int userId)
        {
            // Paso 1: Verificar si el usuario existe
            var userResponse = await _appUserDao.GetAppUserByIdAsync(userId);
            if (userResponse.Code != 200)
            {
                return new OperationResponse<bool>(404, "User not found");
            }

            var appUser = userResponse.Data;

            // Paso 2: Buscar el colaborador relacionado al usuario
            var collaboratorResponse = await _collaboratorDao.GetCollaboratorByUserIdAsync(appUser.IdAppUser);
            if (collaboratorResponse.Code != 200)
            {
                return new OperationResponse<bool>(404, "Collaborator not found for the given user");
            }

            var collaborator = collaboratorResponse.Data;

            // Paso 3: Cambiar el estado de ambos a false
            var userDeleteResponse = await _appUserDao.DeleteAppUserStatusAsync(appUser.IdAppUser);
            if (userDeleteResponse.Code != 200)
            {
                return new OperationResponse<bool>(500, "Error deleting user status");
            }

            var collaboratorDeleteResponse = await _collaboratorDao.DeleteCollaboratorStatusAsync(collaborator.IdCollaborator);
            if (collaboratorDeleteResponse.Code != 200)
            {
                return new OperationResponse<bool>(500, "Error deleting collaborator status");
            }

            return new OperationResponse<bool>(200, "User and collaborator status updated successfully", true);
        }

        public async Task<OperationResponse<IEnumerable<AppUserCollaboratorDto>>> GetAllAppUserCollaboratorAsync()
        {
            var usersWithCollaborators = await _appUserDao.GetAllAppUserCollaboratorAsync();

            if (usersWithCollaborators == null || !usersWithCollaborators.Any())
            {
                return new OperationResponse<IEnumerable<AppUserCollaboratorDto>>(404, "No users with collaborators found");
            }

            return new OperationResponse<IEnumerable<AppUserCollaboratorDto>>(200, "Users with collaborators retrieved successfully", usersWithCollaborators);
        }

        public async Task<OperationResponse<bool>> UpdatePasswordAsync(int id, string newPassword)
        {
            return await _appUserDao.UpdatePasswordAsync(id, newPassword);
        }


    }
}
