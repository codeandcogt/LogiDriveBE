using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Dao
{
    public class LogTripDao : ILogTripDao
    {
        private readonly LogiDriveDbContext _context;

        public LogTripDao(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<LogTrip>> CreateLogTripAsync(LogTrip logTrip)
        {
            try
            {
                await _context.LogTrips.AddAsync(logTrip);
                await _context.SaveChangesAsync();
                return new OperationResponse<LogTrip>(200, "LogTrip created successfully", logTrip);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogTrip>(500, ex.Message);
            }
        }

        public async Task<OperationResponse<LogTrip>> UpdateLogTripAsync(LogTrip logTrip)
        {
            try
            {
                _context.LogTrips.Update(logTrip);
                await _context.SaveChangesAsync();
                return new OperationResponse<LogTrip>(200, "LogTrip updated successfully", logTrip);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogTrip>(500, ex.Message);
            }
        }

        public async Task<OperationResponse<bool>> DeleteLogTripAsync(int id)
        {
            try
            {
                var logTrip = await _context.LogTrips.FindAsync(id);
                if (logTrip == null)
                {
                    return new OperationResponse<bool>(404, "LogTrip not found");
                }

                _context.LogTrips.Remove(logTrip);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "LogTrip deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, ex.Message);
            }
        }

        public async Task<OperationResponse<bool>> DeleteLogTripStatusAsync(int id)
        {
            try
            {
                var logTrip = await _context.LogTrips.FindAsync(id);
                if (logTrip == null)
                {
                    return new OperationResponse<bool>(404, "LogTrip not found");
                }

                logTrip.Status = false; // Cambiamos el estado a false
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "LogTrip status updated to false", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, ex.Message);
            }
        }

        public async Task<OperationResponse<LogTrip>> GetLogTripByIdAsync(int id)
        {
            var logTrip = await _context.LogTrips.FindAsync(id);
            if (logTrip == null)
            {
                return new OperationResponse<LogTrip>(404, "LogTrip not found");
            }

            return new OperationResponse<LogTrip>(200, "LogTrip retrieved successfully", logTrip);
        }

        public async Task<OperationResponse<IEnumerable<LogTrip>>> GetAllLogTripsAsync()
        {
            var logTrips = await _context.LogTrips.ToListAsync();
            return new OperationResponse<IEnumerable<LogTrip>>(200, "LogTrips retrieved successfully", logTrips);
        }
    }
}
