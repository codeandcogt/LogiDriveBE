using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<OperationResponse<LogInspection>> CreateLogInspectionAsync(LogInspection logInspection)
        {
            try
            {
                _context.LogInspections.Add(logInspection);
                await _context.SaveChangesAsync();
                return new OperationResponse<LogInspection>(200, "Inspección creada exitosamente", logInspection);
            }
            catch (DbUpdateException ex)
            {
                return new OperationResponse<LogInspection>(500, $"Error creando la inspección: {ex.Message}");
            }
        }

        public async Task<OperationResponse<LogInspection>> GetLogInspectionByIdAsync(int id)
        {
            var logInspection = await _context.LogInspections.FindAsync(id);
            if (logInspection == null)
            {
                return new OperationResponse<LogInspection>(404, "Inspección no encontrada");
            }

            return new OperationResponse<LogInspection>(200, "Inspección recuperada exitosamente", logInspection);
        }

        public async Task<OperationResponse<IEnumerable<LogInspection>>> GetAllLogInspectionsAsync()
        {
            var inspections = await _context.LogInspections.ToListAsync();
            return new OperationResponse<IEnumerable<LogInspection>>(200, "Inspecciones recuperadas exitosamente", inspections);
        }

        public async Task<OperationResponse<LogInspection>> UpdateLogInspectionAsync(int id, LogInspection logInspection)
        {
            var existingInspection = await _context.LogInspections.FindAsync(id);
            if (existingInspection == null)
            {
                return new OperationResponse<LogInspection>(404, "Inspección no encontrada");
            }

            _context.Entry(logInspection).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new OperationResponse<LogInspection>(200, "Inspección actualizada exitosamente", logInspection);
        }

        public async Task<OperationResponse<bool>> DeleteLogInspectionAsync(int id)
        {
            var logInspection = await _context.LogInspections.FindAsync(id);
            if (logInspection == null)
            {
                return new OperationResponse<bool>(404, "Inspección no encontrada");
            }

            _context.LogInspections.Remove(logInspection);
            await _context.SaveChangesAsync();
            return new OperationResponse<bool>(200, "Inspección eliminada exitosamente", true);
        }

        public async Task<OperationResponse<LogInspection>> GetLogInspectionByVehicleAssignmentAsync(int vehicleAssignmentId, string inspectionType)
        {
            var logInspection = await _context.LogInspections
                .FirstOrDefaultAsync(i => i.IdVehicleAssignment == vehicleAssignmentId && i.TypeInspection == inspectionType);

            if (logInspection == null)
            {
                return new OperationResponse<LogInspection>(404, "No se encontró una inspección de tipo entrega para este vehículo.");
            }

            return new OperationResponse<LogInspection>(200, "Inspección encontrada.", logInspection);
        }

    }
}
