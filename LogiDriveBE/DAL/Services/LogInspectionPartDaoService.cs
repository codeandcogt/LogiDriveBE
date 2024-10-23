using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;

namespace LogiDriveBE.DAL.Services
{
    public class LogInspectionPartDaoService : ILogInspectionPartDao
    {
        private readonly LogiDriveDbContext _context;

        public LogInspectionPartDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<LogInspectionPart>> CreateLogInspectionPartAsync(LogInspectionPart logInspectionPart)
        {
            _context.LogInspectionParts.Add(logInspectionPart);
            await _context.SaveChangesAsync();
            return new OperationResponse<LogInspectionPart>(200, "Inspection part created successfully", logInspectionPart);
        }

        public async Task<OperationResponse<LogInspectionPart>> GetLogInspectionPartByIdAsync(int id)
        {
            var inspectionPart = await _context.LogInspectionParts.FindAsync(id);
            if (inspectionPart == null)
            {
                return new OperationResponse<LogInspectionPart>(404, "Inspection part not found");
            }

            return new OperationResponse<LogInspectionPart>(200, "Inspection part retrieved successfully", inspectionPart);
        }

        public async Task<OperationResponse<IEnumerable<LogInspectionPart>>> GetAllLogInspectionPartsAsync()
        {
            var inspectionParts = await _context.LogInspectionParts.ToListAsync();
            return new OperationResponse<IEnumerable<LogInspectionPart>>(200, "Inspection parts retrieved successfully", inspectionParts);
        }

        public async Task<OperationResponse<LogInspectionPart>> UpdateLogInspectionPartAsync(LogInspectionPart logInspectionPart)
        {
            _context.Entry(logInspectionPart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new OperationResponse<LogInspectionPart>(200, "Inspection part updated successfully", logInspectionPart);
        }

        public async Task<OperationResponse<bool>> DeleteLogInspectionPartAsync(int id)
        {
            var inspectionPart = await _context.LogInspectionParts.FindAsync(id);
            if (inspectionPart == null)
            {
                return new OperationResponse<bool>(404, "Inspection part not found");
            }

            _context.LogInspectionParts.Remove(inspectionPart);
            await _context.SaveChangesAsync();
            return new OperationResponse<bool>(200, "Inspection part deleted successfully", true);
        }
    }
}
