using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using LogiDriveBE.DAL.LogiDriveContext;

namespace LogiDriveBE.DAL.Dao
{
    public class VehicleDaoService : IVehicleDao
    {
        private readonly LogiDriveDbContext _context;

        public VehicleDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Vehicle>> CreateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();
                return new OperationResponse<Vehicle>(200, "Vehicle created successfully", vehicle);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Vehicle>(500, $"Error creating vehicle: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Vehicle>> GetVehicleByIdAsync(int id)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(id);
                if (vehicle == null)
                {
                    return new OperationResponse<Vehicle>(404, "Vehicle not found");
                }
                return new OperationResponse<Vehicle>(200, "Vehicle retrieved successfully", vehicle);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Vehicle>(500, $"Error retrieving vehicle: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Vehicle>>> GetAllVehiclesAsync()
        {
            try
            {
                var vehicles = await _context.Vehicles
                                             .Where(a => a.Status == true || a.Status == true)
                                             .ToListAsync();
                return new OperationResponse<IEnumerable<Vehicle>>(200, "Vehicles retrieved successfully", vehicles);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Vehicle>>(500, $"Error retrieving vehicles: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Vehicle>> UpdateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                _context.Entry(vehicle).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OperationResponse<Vehicle>(200, "Vehicle updated successfully", vehicle);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Vehicle>(500, $"Error updating vehicle: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteVehicleAsync(int id)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(id);
                if (vehicle == null)
                {
                    return new OperationResponse<bool>(404, "Vehicle not found");
                }
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Vehicle deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting vehicle: {ex.Message}");
            }
        }
    }
}
