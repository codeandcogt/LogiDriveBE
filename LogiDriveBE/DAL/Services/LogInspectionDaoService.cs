using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class LogInspectionDaoService : ILogInspectionDao
    {
        private readonly LogiDriveDbContext _context;

        public LogInspectionDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<LogInspectionDto>> CreateLogInspectionAsync(LogInspectionDto logInspectionDto)
        {
            try
            {
                var logInspection = new LogInspection
                {
                    IdCollaborator = logInspectionDto.IdCollaborator,
                    IdVehicleAssignment = logInspectionDto.IdVehicleAssignment,
                    Comment = logInspectionDto.Comment,
                    Odometer = logInspectionDto.Odometer,
                    Fuel = logInspectionDto.Fuel,
                    TypeInspection = logInspectionDto.TypeInspection,
                    Status = logInspectionDto.Status,
                    Image = logInspectionDto.Image,
                    CreationDate = DateTime.Now
                };

                _context.LogInspections.Add(logInspection);
                await _context.SaveChangesAsync();

                foreach (var partDto in logInspectionDto.PartsInspected)
                {
                    var inspectionPart = new LogInspectionPart
                    {
                        IdLogInspection = logInspection.IdLogInspection,
                        IdPartVehicle = partDto.IdPartVehicle,
                        Comment = partDto.Comment,
                        Status = partDto.Status,
                        Image = partDto.Image,
                        DateInspection = DateTime.Now
                    };

                    _context.LogInspectionParts.Add(inspectionPart);
                }

                await _context.SaveChangesAsync();
                return new OperationResponse<LogInspectionDto>(200, "Inspection created successfully", logInspectionDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogInspectionDto>(500, $"Error creating inspection: {ex.Message}");
            }
        }

        public async Task<OperationResponse<LogInspectionDto>> GetLogInspectionByIdAsync(int id)
        {
            try
            {
                var logInspection = await _context.LogInspections
                    .Include(i => i.LogInspectionParts)
                    .FirstOrDefaultAsync(i => i.IdLogInspection == id);

                if (logInspection == null)
                {
                    return new OperationResponse<LogInspectionDto>(404, "Inspection not found");
                }

                var logInspectionDto = new LogInspectionDto
                {
                    IdCollaborator = logInspection.IdCollaborator,
                    IdVehicleAssignment = logInspection.IdVehicleAssignment,
                    Comment = logInspection.Comment,
                    Odometer = logInspection.Odometer,
                    Fuel = logInspection.Fuel,
                    TypeInspection = logInspection.TypeInspection,
                    Status = logInspection.Status,
                    Image = logInspection.Image,
                    PartsInspected = logInspection.LogInspectionParts.Select(p => new LogInspectionPartDto
                    {
                        IdPartVehicle = p.IdPartVehicle,
                        Comment = p.Comment,
                        Status = p.Status,
                        Image = p.Image
                    }).ToList()
                };

                return new OperationResponse<LogInspectionDto>(200, "Inspection retrieved successfully", logInspectionDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogInspectionDto>(500, $"Error retrieving inspection: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<LogInspectionDto>>> GetAllLogInspectionsAsync()
        {
            try
            {
                var logInspections = await _context.LogInspections
                    .Include(i => i.LogInspectionParts)
                    .ToListAsync();

                var logInspectionDtos = logInspections.Select(i => new LogInspectionDto
                {
                    IdCollaborator = i.IdCollaborator,
                    IdVehicleAssignment = i.IdVehicleAssignment,
                    Comment = i.Comment,
                    Odometer = i.Odometer,
                    Fuel = i.Fuel,
                    TypeInspection = i.TypeInspection,
                    Status = i.Status,
                    Image = i.Image,
                    PartsInspected = i.LogInspectionParts.Select(p => new LogInspectionPartDto
                    {
                        IdPartVehicle = p.IdPartVehicle,
                        Comment = p.Comment,
                        Status = p.Status,
                        Image = p.Image
                    }).ToList()
                }).ToList();

                return new OperationResponse<IEnumerable<LogInspectionDto>>(200, "Inspections retrieved successfully", logInspectionDtos);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<LogInspectionDto>>(500, $"Error retrieving inspections: {ex.Message}");
            }
        }

        public async Task<OperationResponse<LogInspectionDto>> UpdateLogInspectionAsync(int id, LogInspectionDto logInspectionDto)
        {
            try
            {
                var logInspection = await _context.LogInspections.FindAsync(id);
                if (logInspection == null)
                {
                    return new OperationResponse<LogInspectionDto>(404, "Inspection not found");
                }

                logInspection.Comment = logInspectionDto.Comment;
                logInspection.Odometer = logInspectionDto.Odometer;
                logInspection.Fuel = logInspectionDto.Fuel;
                logInspection.TypeInspection = logInspectionDto.TypeInspection;
                logInspection.Status = logInspectionDto.Status;
                logInspection.Image = logInspectionDto.Image;

                _context.Entry(logInspection).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OperationResponse<LogInspectionDto>(200, "Inspection updated successfully", logInspectionDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogInspectionDto>(500, $"Error updating inspection: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteLogInspectionAsync(int id)
        {
            try
            {
                var logInspection = await _context.LogInspections.FindAsync(id);
                if (logInspection == null)
                {
                    return new OperationResponse<bool>(404, "Inspection not found");
                }

                _context.LogInspections.Remove(logInspection);
                await _context.SaveChangesAsync();

                return new OperationResponse<bool>(200, "Inspection deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting inspection: {ex.Message}");
            }
        }

        // Method to get inspection by VehicleAssignment and inspection type
        public async Task<OperationResponse<LogInspection>> GetLogInspectionByVehicleAssignmentAndTypeAsync(int idVehicleAssignment, string typeInspection)
        {
            try
            {
                var logInspection = await _context.LogInspections
                    .FirstOrDefaultAsync(i => i.IdVehicleAssignment == idVehicleAssignment && i.TypeInspection == typeInspection);

                if (logInspection == null)
                {
                    return new OperationResponse<LogInspection>(404, "Inspection not found");
                }

                return new OperationResponse<LogInspection>(200, "Inspection retrieved successfully", logInspection);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogInspection>(500, $"Error retrieving inspection: {ex.Message}");
            }
        }
    }
}
