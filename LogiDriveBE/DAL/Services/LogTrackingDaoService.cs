
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Dao
{
    public class LogTrackingDao : ILogTrackingDao
    {
        private readonly LogiDriveDbContext _context;

        public LogTrackingDao(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<IEnumerable<LogTracking>>> GetActiveTrackingByUserIdAsync(int userId)
        {
            var logTrips = await _context.LogTrips
                .Include(lt => lt.IdTrackingNavigation)
                .Include(lt => lt.IdVehicleAssignmentNavigation)
                    .ThenInclude(va => va.IdLogReservationNavigation) // Navegar a LogReservation
                    .ThenInclude(lr => lr.IdCollaboratorNavigation) // Navegar a Collaborator
                .Where(lt => lt.IdVehicleAssignmentNavigation.IdLogReservationNavigation.IdCollaborator == userId && lt.Status == true)
                .ToListAsync();

            if (logTrips == null || !logTrips.Any())
            {
                return new OperationResponse<IEnumerable<LogTracking>>(404, "No active log tracking found for the user");
            }

            var logTrackings = logTrips.Select(lt => lt.IdTrackingNavigation).Where(tr => tr != null).ToList();

            return new OperationResponse<IEnumerable<LogTracking>>(200, "Active log trackings retrieved successfully", logTrackings);
        }


        public async Task<OperationResponse<LogTracking>> GetActiveLogTrackingByVehicleAssignmentIdAsync(int vehicleAssignmentId)
        {
            // Busca directamente en LogTracking usando la relación con LogTrip y el estado activo (Status = true)
            var activeLogTracking = await _context.LogTrackings
                .Include(lt => lt.LogTrip)
                .FirstOrDefaultAsync(lt => lt.LogTrip.IdVehicleAssignment == vehicleAssignmentId && lt.Status == true);

            if (activeLogTracking == null)
            {
                return new OperationResponse<LogTracking>(404, "Active log tracking not found");
            }

            return new OperationResponse<LogTracking>(200, "Active log tracking retrieved successfully", activeLogTracking);
        }

        public async Task<OperationResponse<LogTracking>> CreateLogTrackingAsync(TrakingDto trackingDto)
        {
            try
            {
                // Convertir el DTO en una instancia de LogTracking
                var logTracking = new LogTracking
                {
                    Latitude = trackingDto.Latitude,
                    Longitude = trackingDto.Longitude,
                    Status = trackingDto.Status,
                    IdLogTrip = trackingDto.IdLogTrip
                };

                await _context.LogTrackings.AddAsync(logTracking);
                await _context.SaveChangesAsync();

                return new OperationResponse<LogTracking>(200, "LogTracking created successfully", logTracking);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogTracking>(500, ex.Message);
            }
        }

        public async Task<OperationResponse<LogTracking>> UpdateLogTrackingAsync(LogTracking logTracking)
        {
            try
            {
                _context.LogTrackings.Update(logTracking);
                await _context.SaveChangesAsync();
                return new OperationResponse<LogTracking>(200, "LogTracking updated successfully", logTracking);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogTracking>(500, ex.Message);
            }
        }

        public async Task<OperationResponse<bool>> DeleteLogTrackingAsync(int id)
        {
            try
            {
                var logTracking = await _context.LogTrackings.FindAsync(id);
                if (logTracking == null)
                {
                    return new OperationResponse<bool>(404, "LogTracking not found");
                }

                _context.LogTrackings.Remove(logTracking);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "LogTracking deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, ex.Message);
            }
        }

        public async Task<OperationResponse<bool>> DeleteLogTrackingStatusAsync(int id)
        {
            try
            {
                var logTracking = await _context.LogTrackings.FindAsync(id);
                if (logTracking == null)
                {
                    return new OperationResponse<bool>(404, "LogTracking not found");
                }

                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "LogTracking status updated to false", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, ex.Message);
            }
        }

        public async Task<OperationResponse<LogTracking>> GetLogTrackingByIdAsync(int id)
        {
            var logTracking = await _context.LogTrackings.FindAsync(id);
            if (logTracking == null)
            {
                return new OperationResponse<LogTracking>(404, "LogTracking not found");
            }

            return new OperationResponse<LogTracking>(200, "LogTracking retrieved successfully", logTracking);
        }

        public async Task<OperationResponse<IEnumerable<LogTracking>>> GetAllLogTrackingsAsync()
        {
            var logTrackings = await _context.LogTrackings.ToListAsync();
            return new OperationResponse<IEnumerable<LogTracking>>(200, "LogTrackings retrieved successfully", logTrackings);
        }
    }
}
