using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class VehicleAssignmentDaoService : IVehicleAssignmentDao
    {
        private readonly LogiDriveDbContext _context;

        public VehicleAssignmentDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<IEnumerable<VehicleAssignmentDto>>> GetVehicleAssignmentsByUserIdAsync(int userId)
        {
            var assignments = await _context.VehicleAssignments
                .Include(va => va.IdLogReservationNavigation)  
                .Where(va => va.IdLogReservationNavigation.IdCollaborator == userId)  // Filtrar por IdCollaborator (usuario)
                .Select(va => new VehicleAssignmentDto
                {
                    IdVehicleAssignment = va.IdVehicleAssignment,
                    Comment = va.Comment,
                    TripType = va.TripType,
                    StartDate = va.StartDate,
                    EndDate = va.EndDate,
                    IdVehicle = va.IdVehicle,
                    IdLogReservation = va.IdLogReservation,
                    Status = va.Status,
                    CreationDate = va.CreationDate
                })
                .ToListAsync();

            return new OperationResponse<IEnumerable<VehicleAssignmentDto>>(200, "Vehicle Assignments retrieved successfully", assignments);
        }


        public async Task<OperationResponse<VehicleAssignmentDto>> CreateVehicleAssignmentAsync(VehicleAssignmentDto vehicleAssignmentDto)
        {
            try
            {
                var vehicleAssignment = new VehicleAssignment
                {
                    Comment = vehicleAssignmentDto.Comment,
                    TripType = vehicleAssignmentDto.TripType,
                    StartDate = vehicleAssignmentDto.StartDate,
                    EndDate = vehicleAssignmentDto.EndDate,
                    IdVehicle = vehicleAssignmentDto.IdVehicle,
                    IdLogReservation = vehicleAssignmentDto.IdLogReservation,
                    Status = vehicleAssignmentDto.Status,
                    CreationDate = vehicleAssignmentDto.CreationDate
                };

                _context.VehicleAssignments.Add(vehicleAssignment);
                await _context.SaveChangesAsync();
                return new OperationResponse<VehicleAssignmentDto>(200, "Vehicle Assignment created successfully", vehicleAssignmentDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<VehicleAssignmentDto>(500, $"Error creating vehicle assignment: {ex.Message}");
            }
        }

        public async Task<OperationResponse<VehicleAssignmentDto>> GetVehicleAssignmentByIdAsync(int id)
        {
            try
            {
                var vehicleAssignment = await _context.VehicleAssignments.FindAsync(id);
                if (vehicleAssignment == null)
                {
                    return new OperationResponse<VehicleAssignmentDto>(404, "Vehicle Assignment not found");
                }

                var vehicleAssignmentDto = new VehicleAssignmentDto
                {
                    Comment = vehicleAssignment.Comment,
                    TripType = vehicleAssignment.TripType,
                    StartDate = vehicleAssignment.StartDate,
                    EndDate = vehicleAssignment.EndDate,
                    IdVehicle = vehicleAssignment.IdVehicle,
                    IdLogReservation = vehicleAssignment.IdLogReservation,
                    Status = vehicleAssignment.Status,
                    CreationDate = vehicleAssignment.CreationDate
                };

                return new OperationResponse<VehicleAssignmentDto>(200, "Vehicle Assignment retrieved successfully", vehicleAssignmentDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<VehicleAssignmentDto>(500, $"Error retrieving vehicle assignment: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<VehicleAssignmentDto>>> GetAllVehicleAssignmentsAsync()
        {
            try
            {
                var vehicleAssignments = await _context.VehicleAssignments
                                                       .Where(a => a.Status == true || a.Status == true)
                                                       .ToListAsync();
                var vehicleAssignmentDtos = vehicleAssignments.Select(v => new VehicleAssignmentDto
                {
                    IdVehicleAssignment = v.IdVehicleAssignment,
                    Comment = v.Comment,
                    TripType = v.TripType,
                    StartDate = v.StartDate,
                    EndDate = v.EndDate,
                    IdVehicle = v.IdVehicle,
                    IdLogReservation = v.IdLogReservation,
                    Status = v.Status,
                    CreationDate = v.CreationDate
                });

                return new OperationResponse<IEnumerable<VehicleAssignmentDto>>(200, "Vehicle Assignments retrieved successfully", vehicleAssignmentDtos);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<VehicleAssignmentDto>>(500, $"Error retrieving vehicle assignments: {ex.Message}");
            }
        }

        public async Task<OperationResponse<VehicleAssignmentDto>> UpdateVehicleAssignmentAsync(VehicleAssignmentDto vehicleAssignmentDto)
        {
            try
            {
                var vehicleAssignment = await _context.VehicleAssignments.FindAsync(vehicleAssignmentDto.IdVehicleAssignment);
                if (vehicleAssignment == null)
                {
                    return new OperationResponse<VehicleAssignmentDto>(404, "Vehicle Assignment not found");
                }

                vehicleAssignment.Comment = vehicleAssignmentDto.Comment;
                vehicleAssignment.TripType = vehicleAssignmentDto.TripType;
                vehicleAssignment.StartDate = vehicleAssignmentDto.StartDate;
                vehicleAssignment.EndDate = vehicleAssignmentDto.EndDate;
                vehicleAssignment.IdVehicle = vehicleAssignmentDto.IdVehicle;
                vehicleAssignment.IdLogReservation = vehicleAssignmentDto.IdLogReservation;
                vehicleAssignment.Status = vehicleAssignmentDto.Status;

                _context.Entry(vehicleAssignment).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OperationResponse<VehicleAssignmentDto>(200, "Vehicle Assignment updated successfully", vehicleAssignmentDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<VehicleAssignmentDto>(500, $"Error updating vehicle assignment: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteVehicleAssignmentAsync(int id)
        {
            try
            {
                var vehicleAssignment = await _context.VehicleAssignments.FindAsync(id);
                if (vehicleAssignment == null)
                {
                    return new OperationResponse<bool>(404, "Vehicle Assignment not found");
                }

                _context.VehicleAssignments.Remove(vehicleAssignment);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Vehicle Assignment deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting vehicle assignment: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteVehicleAssigmentStatusAsync(int id)
        {
            try
            {
                var vehicleAssigment = await _context.VehicleAssignments.FindAsync(id);
                if (vehicleAssigment == null)
                {
                    return new OperationResponse<bool>(404, "VehicleAssigment not found");
                }

                // Cambiar el estado en lugar de eliminar físicamente
                vehicleAssigment.Status = false;
                await _context.SaveChangesAsync();

                return new OperationResponse<bool>(200, "VehicleAssigment logically deleted (status set to false) successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting vehicleAssigment: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<VehicleAssignmentView>>> GetVehicleAssignmentsByDateWithStatusUpdateAsync(DateTime specificDate)
        {
            try
            {
                // Obtener todas las asignaciones activas para la fecha específica desde la vista
                var vehicleAssignmentsFromView = await _context.VehicleAssignmentViews
                    .FromSqlRaw("SELECT * FROM vw_VehicleAssignmentsByDate WHERE CAST(DepartureTime AS DATE) = {0}", specificDate.Date)
                    .ToListAsync();

                // Verificar si han pasado dos horas desde DepartureTime y cambiar el estado en VehicleAssignments si es necesario
                var currentTime = DateTime.UtcNow;
                var vehicleAssignmentsToUpdate = await _context.VehicleAssignments
                                                       .Where(a => a.Status == true && a.StartDate.Date == specificDate.Date)
                                                       .ToListAsync();

                foreach (var assignment in vehicleAssignmentsToUpdate)
                {
                    if ((currentTime - assignment.StartDate).TotalHours >= 2)
                    {
                        assignment.Status = false;
                        _context.Entry(assignment).State = EntityState.Modified;
                    }
                }

                // Guardar cambios
                await _context.SaveChangesAsync();

                // Retornar únicamente los datos de la vista
                return new OperationResponse<IEnumerable<VehicleAssignmentView>>(200, "Vehicle Assignments retrieved and updated successfully", vehicleAssignmentsFromView);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<VehicleAssignmentView>>(500, $"Error retrieving or updating vehicle assignments: {ex.Message}");
            }
        }





    }
}
