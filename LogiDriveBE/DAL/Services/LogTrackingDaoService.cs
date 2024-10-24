
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
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

        public async Task<OperationResponse<LogTracking>> CreateLogTrackingAsync(LogTracking logTracking)
        {
            try
            {
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
