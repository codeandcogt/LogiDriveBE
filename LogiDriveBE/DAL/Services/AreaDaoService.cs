using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class AreaDaoService : IAreaDao
    {
        private readonly LogiDriveDbContext _context;

        public AreaDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Area>> CreateAreaAsync(Area area)
        {
            try
            {
                _context.Areas.Add(area);
                await _context.SaveChangesAsync();
                return new OperationResponse<Area>(200, "Area created successfully", area);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Area>(500, $"Error creating area: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Area>> GetAreaByIdAsync(int id)
        {
            try
            {
                var area = await _context.Areas.FindAsync(id);
                if (area == null)
                {
                    return new OperationResponse<Area>(404, "Area not found");
                }
                return new OperationResponse<Area>(200, "Area retrieved successfully", area);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Area>(500, $"Error retrieving area: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Area>>> GetAllAreasAsync()
        {
            try
            {
                // Filtrar solo las áreas donde el status es true o 1
                var areas = await _context.Areas
                                          .Where(a => a.Status == true || a.Status == true)
                                          .ToListAsync();
                return new OperationResponse<IEnumerable<Area>>(200, "Areas retrieved successfully", areas);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Area>>(500, $"Error retrieving areas: {ex.Message}");
            }
        }


        public async Task<OperationResponse<Area>> UpdateAreaAsync(Area area)
        {
            try
            {
                _context.Entry(area).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OperationResponse<Area>(200, "Area updated successfully", area);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Area>(500, $"Error updating area: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteAreaAsync(int id)
        {
            try
            {
                var area = await _context.Areas.FindAsync(id);
                if (area == null)
                {
                    return new OperationResponse<bool>(404, "Area not found");
                }

                _context.Areas.Remove(area);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Area deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting area: {ex.Message}");
            }
        }
    }
}
