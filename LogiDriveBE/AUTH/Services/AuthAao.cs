using LogiDriveBE.AUTH.Aao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.AUTH.Services
{
    public class AuthAao : IAuthAao
    {
        private readonly LogiDriveDbContext _context;

        public AuthAao(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _context.AppUsers
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IList<string>> GetUserRolesAsync(int userId)
        {
            var user = await _context.AppUsers
                .Include(u => u.IdRoleNavigation)
                .FirstOrDefaultAsync(u => u.IdAppUser == userId);

            return user != null ? new List<string> { user.IdRoleNavigation.Name } : new List<string>();
        }

        public async Task<IList<string>> GetUserPermissionsAsync(int userId)
        {
            var permissions = await _context.AppUsers
                .Where(u => u.IdAppUser == userId)
                .SelectMany(u => u.IdRoleNavigation.IdPermissions)
                .Select(p => p.Name)
                .ToListAsync();

            return permissions;
        }
    }
}
