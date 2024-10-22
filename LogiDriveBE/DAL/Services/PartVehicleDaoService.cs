using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class PartVehicleDaoService : IPartVehicleDao
    {

        private readonly LogiDriveDbContext _context;

        public PartVehicleDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<PartVehicle>> CreatePartVehicleAsync(PartVehicle partVehicle)
        {
            try
            {
                _context.PartVehicles.Add(partVehicle);
                await _context.SaveChangesAsync();
                return new OperationResponse<PartVehicle>(200, "PartVehicle created successfully", partVehicle);
            }
            catch (Exception ex)
            {
                return new OperationResponse<PartVehicle>(500, $"Error creating PartVehicle: {ex.Message}");
            }
        }

        public async Task<OperationResponse<PartVehicle>> GetPartVehicleByIdAsync(int id)
        {
            try
            {
                var partVehicle = await _context.PartVehicles.FindAsync(id);
                if (partVehicle == null)
                {
                    return new OperationResponse<PartVehicle>(404, "PartVehicle not found");
                }
                return new OperationResponse<PartVehicle>(200, "PartVehicle retrieved successfully", partVehicle);
            }
            catch (Exception ex)
            {
                return new OperationResponse<PartVehicle>(500, $"Error retrieving Partvehicle: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<PartVehicle>>> GetAllPartVehiclesAsync()
        {
            try
            {
                var partVehicles = await _context.PartVehicles
                                                 .Where(a => a.Status == true || a.Status == true)
                                                 .ToListAsync();
                return new OperationResponse<IEnumerable<PartVehicle>>(200, "PartVehicles retrieved successfully", partVehicles);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<PartVehicle>>(500, $"Error retrieving Partvehicles: {ex.Message}");
            }
        }

        public async Task<OperationResponse<PartVehicle>> UpdatePartVehicleAsync(PartVehicle partVehicle)
        {
            try
            {
                _context.Entry(partVehicle).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OperationResponse<PartVehicle>(200, "PartVehicle updated successfully", partVehicle);
            }
            catch (Exception ex)
            {
                return new OperationResponse<PartVehicle>(500, $"Error updating Partvehicle: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeletePartVehicleAsync(int id)
        {
            try
            {
                var partVehicle = await _context.PartVehicles.FindAsync(id);
                if (partVehicle == null)
                {
                    return new OperationResponse<bool>(404, "PartVehicle not found");
                }
                _context.PartVehicles.Remove(partVehicle);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "PartVehicle deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting Partvehicle: {ex.Message}");
            }
        }


    }
}
