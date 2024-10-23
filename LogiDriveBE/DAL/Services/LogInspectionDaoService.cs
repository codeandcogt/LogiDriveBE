using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Services
{
    public class LogInspectionDaoService : ILogInspectionDao
    {
        private readonly LogiDriveDbContext _context;

        public LogInspectionDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<LogInspection>> CreateLogInspectionAsync(LogInspection inspection)
        {
            _context.LogInspections.Add(inspection);
            await _context.SaveChangesAsync();
            return new OperationResponse<LogInspection>(200, "Inspection created successfully", inspection);
        }

        public async Task<OperationResponse<LogInspection>> GetLogInspectionByIdAsync(int id)
        {
            var inspection = await _context.LogInspections
                .Include(i => i.LogInspectionParts)
                .FirstOrDefaultAsync(i => i.IdLogInspection == id);

            if (inspection == null)
                return new OperationResponse<LogInspection>(404, "Inspection not found");

            return new OperationResponse<LogInspection>(200, "Inspection retrieved successfully", inspection);
        }

        public async Task<OperationResponse<IEnumerable<LogInspection>>> GetAllLogInspectionsAsync()
        {
            var inspections = await _context.LogInspections
                .Include(i => i.LogInspectionParts)
                .ToListAsync();

            return new OperationResponse<IEnumerable<LogInspection>>(200, "Inspections retrieved successfully", inspections);
        }

        public async Task<OperationResponse<LogInspection>> UpdateLogInspectionAsync(int id, LogInspection logInspection)
        {
            var inspection = await _context.LogInspections.FindAsync(id);
            if (inspection == null)
                return new OperationResponse<LogInspection>(404, "Inspection not found");

            // Actualizar los datos del objeto LogInspection
            inspection.Comment = logInspection.Comment;
            inspection.Odometer = logInspection.Odometer;
            inspection.Fuel = logInspection.Fuel;
            inspection.TypeInspection = logInspection.TypeInspection;
            inspection.Status = logInspection.Status;
            inspection.Image = logInspection.Image;

            _context.Entry(inspection).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new OperationResponse<LogInspection>(200, "Inspection updated successfully", inspection);
        }

        public async Task<OperationResponse<bool>> DeleteLogInspectionAsync(int id)
        {
            var inspection = await _context.LogInspections.FindAsync(id);
            if (inspection == null)
                return new OperationResponse<bool>(404, "Inspection not found");

            _context.LogInspections.Remove(inspection);
            await _context.SaveChangesAsync();

            return new OperationResponse<bool>(200, "Inspection deleted successfully", true);
        }
    }
}
