using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class AppUserDaoService : IAppUserDao
    {
        private readonly LogiDriveDbContext _context;

        public AppUserDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<AppUser>> CreateAppUserAsync(AppUser appUser)
        {
            try
            {
                _context.AppUsers.Add(appUser);
                await _context.SaveChangesAsync();
                return new OperationResponse<AppUser>(200, "AppUser created successfully", appUser);
            }
            catch (DbUpdateException ex)
            {
                // Log the full exception details
                Console.WriteLine($"DbUpdateException: {ex.ToString()}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return new OperationResponse<AppUser>(500, $"Error creating AppUser: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                // Log the full exception details
                Console.WriteLine($"Exception: {ex.ToString()}");

                return new OperationResponse<AppUser>(500, $"Error creating AppUser: {ex.Message}");
            }
        }

        public async Task<OperationResponse<AppUser>> GetAppUserByIdAsync(int id)
        {
            try
            {
                var appUser = await _context.AppUsers.FindAsync(id);
                if (appUser == null)
                {
                    return new OperationResponse<AppUser>(404, "AppUser not found");
                }
                return new OperationResponse<AppUser>(200, "AppUser retrieved successfully", appUser);
            }
            catch (Exception ex)
            {
                return new OperationResponse<AppUser>(500, $"Error retrieving AppUser: {ex.Message}");
            }
        }

        public async Task<OperationResponse<AppUser>> GetAppUserByEmailAsync(string email)
        {
            try
            {
                var appUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == email);
                if (appUser == null)
                {
                    return new OperationResponse<AppUser>(404, "AppUser not found");
                }
                return new OperationResponse<AppUser>(200, "AppUser retrieved successfully", appUser);
            }
            catch (Exception ex)
            {
                return new OperationResponse<AppUser>(500, $"Error retrieving AppUser: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<AppUser>>> GetAllAppUsersAsync()
        {
            try
            {
                var appUsers = await _context.AppUsers
                                    .Where(a => a.Status == true || a.Status == true)
                                    .ToListAsync();
                return new OperationResponse<IEnumerable<AppUser>>(200, "AppUsers retrieved successfully", appUsers);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<AppUser>>(500, $"Error retrieving AppUsers: {ex.Message}");
            }
        }

        

        public async Task<OperationResponse<AppUser>> UpdateAppUserAsync(AppUser appUser)
        {
            try
            {
                _context.Entry(appUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OperationResponse<AppUser>(200, "AppUser updated successfully", appUser);
            }
            catch (Exception ex)
            {
                return new OperationResponse<AppUser>(500, $"Error updating AppUser: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteAppUserAsync(int id)
        {
            try
            {
                var appUser = await _context.AppUsers.FindAsync(id);
                if (appUser == null)
                {
                    return new OperationResponse<bool>(404, "AppUser not found");
                }

                _context.AppUsers.Remove(appUser);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "AppUser deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting AppUser: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteAppUserStatusAsync(int id)
        {
            try
            {
                var appUser = await _context.AppUsers.FindAsync(id);
                if (appUser == null)
                {
                    return new OperationResponse<bool>(404, "AppUser not found");
                }

                // Cambiar el estado en lugar de eliminar físicamente
                appUser.Status = false;  // Marcamos el AppUser como inactiva
                await _context.SaveChangesAsync();

                return new OperationResponse<bool>(200, "AppUser logically deleted (status set to false) successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting AppUser: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AppUserCollaboratorDto>> GetAllAppUserCollaboratorAsync()
        {
            try
            {
                var query = await (from user in _context.AppUsers
                                   join collaborator in _context.Collaborators
                                   on user.IdAppUser equals collaborator.IdUser
                                   where user.Status == true && collaborator.Status == true
                                   select new AppUserCollaboratorDto
                                   {
                                       AppUserId = user.IdAppUser,
                                       Name = user.Name,
                                       Email = user.Email,
                                       Password = user.Password, // Opcional, dependiendo de tus requerimientos
                                       CollaboratorName = collaborator.Name,
                                       CollaboratorLastName = collaborator.LastName,
                                       Position = collaborator.Position,
                                       Phone = collaborator.Phone,
                                       IdRole = user.IdRole,
                                       IdArea = collaborator.IdArea
                                   }).ToListAsync();

                return query;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la consulta
                Console.WriteLine($"Error: {ex.Message}");
                return Enumerable.Empty<AppUserCollaboratorDto>(); // Retorna una lista vacía en caso de error
            }
        }

        public async Task<OperationResponse<bool>> UpdatePasswordAsync(int id, string newPassword)
        {
            try
            {
                var appUser = await _context.AppUsers.FindAsync(id);
                if (appUser == null)
                {
                    return new OperationResponse<bool>(404, "AppUser not found");
                }

                // Encriptar la nueva contraseña
                appUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

                _context.Entry(appUser).Property(u => u.Password).IsModified = true;
                await _context.SaveChangesAsync();

                return new OperationResponse<bool>(200, "Password updated successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error updating password: {ex.Message}");
            }
        }


    }
}
