using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Services
{
    public class LogInspectionDaoService : ILogInspectionDao
    {
        private readonly LogiDriveDbContext _context;
        private readonly ILogInspectionPartDao _logInspectionPartDao;

        public LogInspectionDaoService(LogiDriveDbContext context, ILogInspectionPartDao logInspectionPartDao)
        {
            _context = context;
            _logInspectionPartDao = logInspectionPartDao;
        }

        public async Task<OperationResponse<LogInspection>> CreateLogInspectionAsync(LogInspection logInspection)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Limpiar el tracking de entidades
                _context.ChangeTracker.Clear();

                // 1. Crear la inspección sin las partes
                var newInspection = new LogInspection
                {
                    IdCollaborator = logInspection.IdCollaborator,
                    IdVehicleAssignment = logInspection.IdVehicleAssignment,
                    Comment = logInspection.Comment,
                    Odometer = logInspection.Odometer,
                    Fuel = logInspection.Fuel,
                    TypeInspection = logInspection.TypeInspection,
                    Status = logInspection.Status,
                    CreationDate = DateTime.Now,
                    Image = logInspection.Image,
                    IdLogProcess = logInspection.IdLogProcess
                };

                // Agregar y guardar la inspección
                await _context.LogInspections.AddAsync(newInspection);
                await _context.SaveChangesAsync();

                // 2. Crear las partes si existen
                if (logInspection.LogInspectionParts != null && logInspection.LogInspectionParts.Any())
                {
                    foreach (var part in logInspection.LogInspectionParts)
                    {
                        // No establecer el IdLogInspectionPart, dejar que la base de datos lo genere
                        var newPart = new LogInspectionPart
                        {
                            IdLogInspection = newInspection.IdLogInspection,
                            IdPartVehicle = part.IdPartVehicle,
                            Comment = part.Comment,
                            Status = part.Status,
                            DateInspection = DateTime.Now,
                            Image = part.Image
                    };

                        await _context.LogInspectionParts.AddAsync(newPart);
                        await _context.SaveChangesAsync(); // Guardar cada parte individualmente
                }
                }

                await transaction.CommitAsync();

                // 3. Retornar la inspección completa
                var result = await _context.LogInspections
                    .AsNoTracking()
                    .Include(i => i.LogInspectionParts)
                    .FirstOrDefaultAsync(i => i.IdLogInspection == newInspection.IdLogInspection);

                return new OperationResponse<LogInspection>(200, "Inspección creada exitosamente", result);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new OperationResponse<LogInspection>(500,
                    $"Error creando la inspección: {ex.Message} - Inner: {ex.InnerException?.Message}");
            }
        }
        public async Task<OperationResponse<LogInspection>> GetLogInspectionByIdAsync(int id)
        {
            try
            {
                var logInspection = await _context.LogInspections
                    .Include(i => i.LogInspectionParts)
                    .FirstOrDefaultAsync(i => i.IdLogInspection == id);

                if (logInspection == null)
                {
                    return new OperationResponse<LogInspection>(404, "Inspección no encontrada");
                }

                return new OperationResponse<LogInspection>(200, "Inspección recuperada exitosamente", logInspection);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogInspection>(500, $"Error recuperando la inspección: {ex.Message}");
            }
        }

        public async Task<OperationResponse<LogInspection>> UpdateLogInspectionAsync(int id, LogInspection logInspection)
        {
            try
            {
                var existingInspection = await _context.LogInspections
                    .Include(i => i.LogInspectionParts)
                    .FirstOrDefaultAsync(i => i.IdLogInspection == id);

                if (existingInspection == null)
                {
                    return new OperationResponse<LogInspection>(404, "Inspección no encontrada");
                }

                // Actualizar propiedades principales
                _context.Entry(existingInspection).CurrentValues.SetValues(logInspection);

                // Actualizar partes
                if (logInspection.LogInspectionParts != null)
                {
                    // Eliminar partes existentes
                    _context.LogInspectionParts.RemoveRange(existingInspection.LogInspectionParts);

                    // Agregar nuevas partes
                    foreach (var part in logInspection.LogInspectionParts)
                    {
                        part.IdLogInspection = id;
                        _context.LogInspectionParts.Add(part);
                    }
                }

                await _context.SaveChangesAsync();

                // Recargar la entidad completa
                var updatedInspection = await _context.LogInspections
                    .Include(i => i.LogInspectionParts)
                    .FirstOrDefaultAsync(i => i.IdLogInspection == id);

                return new OperationResponse<LogInspection>(200, "Inspección actualizada exitosamente", updatedInspection);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogInspection>(500,
                    $"Error actualizando la inspección: {ex.Message} - Inner: {ex.InnerException?.Message}");
            }
        }




        public async Task<OperationResponse<IEnumerable<LogInspection>>> GetAllLogInspectionsAsync()
        {
            var inspections = await _context.LogInspections.ToListAsync();
            return new OperationResponse<IEnumerable<LogInspection>>(200, "Inspecciones recuperadas exitosamente", inspections);
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

        public async Task<OperationResponse<bool>> UpdateLogInspectionPartsAsync(int idLogInspection, List<LogInspectionPart> parts)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Limpiar el tracking
                _context.ChangeTracker.Clear();

                // 1. Eliminar partes existentes
                var existingParts = await _context.LogInspectionParts
                    .Where(p => p.IdLogInspection == idLogInspection)
                    .ToListAsync();

                if (existingParts.Any())
                {
                    _context.LogInspectionParts.RemoveRange(existingParts);
                    await _context.SaveChangesAsync();
                }

                // 2. Agregar nuevas partes
                foreach (var part in parts)
                {
                    var newPart = new LogInspectionPart
                    {
                        IdLogInspection = idLogInspection,
                        IdPartVehicle = part.IdPartVehicle,
                        Comment = part.Comment,
                        Status = part.Status,
                        DateInspection = DateTime.Now,
                        Image = part.Image
                    };

                    await _context.LogInspectionParts.AddAsync(newPart);
                    await _context.SaveChangesAsync(); // Guardar cada parte individualmente
                }

                await transaction.CommitAsync();
                return new OperationResponse<bool>(200, "Partes de la inspección actualizadas exitosamente", true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new OperationResponse<bool>(500, $"Error actualizando las partes de la inspección: {ex.Message}", false);
            }
        }

    }
}
