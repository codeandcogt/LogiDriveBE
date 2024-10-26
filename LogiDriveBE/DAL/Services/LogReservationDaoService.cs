using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class LogReservationDaoService : ILogReservationDao
    {
        private readonly LogiDriveDbContext _context;

        public LogReservationDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<LogReservationDto>> CreateLogReservationAsync(LogReservationDto logReservationDto)
        {
            try
            {
                if (!logReservationDto.NumberPeople.HasValue)
                {
                    logReservationDto.NumberPeople = 1;
                }

                var logReservation = new LogReservation
                {
                    IdCollaborator = logReservationDto.IdCollaborator,
                    Comment = logReservationDto.Comment,
                    IdTown = logReservationDto.IdTown,
                    NumberPeople = logReservationDto.NumberPeople,
                    StatusReservation = logReservationDto.StatusReservation,
                    Justify = logReservationDto.Justify,
                    Addres = logReservationDto.Addres,
                    Status = logReservationDto.Status,
                    CreationDate = logReservationDto.CreationDate
                };

                _context.LogReservations.Add(logReservation);
                await _context.SaveChangesAsync();
                return new OperationResponse<LogReservationDto>(200, "Log reservation created successfully", logReservationDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogReservationDto>(500, $"Error creating log reservation: {ex.Message}");
            }
        }

        public async Task<OperationResponse<LogReservationDto>> GetLogReservationByIdAsync(int id)
        {
            try
            {
                var logReservation = await _context.LogReservations.FindAsync(id);
                if (logReservation == null)
                {
                    return new OperationResponse<LogReservationDto>(404, "Log reservation not found");
                }

                var logReservationDto = new LogReservationDto
                {
                    IdCollaborator = logReservation.IdCollaborator,
                    Comment = logReservation.Comment,
                    IdTown = logReservation.IdTown,
                    NumberPeople = logReservation.NumberPeople,
                    StatusReservation = logReservation.StatusReservation,
                    Justify = logReservation.Justify,
                    Addres = logReservation.Addres,
                    Status = logReservation.Status,
                    CreationDate = logReservation.CreationDate
                };

                return new OperationResponse<LogReservationDto>(200, "Log reservation retrieved successfully", logReservationDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogReservationDto>(500, $"Error retrieving log reservation: {ex.Message}");
            }
        }
        public async Task<OperationResponse<IEnumerable<GetLogReservationDto>>> GetAllLogReservationsAsync()
        {
            try
            {
                var logReservations = await _context.LogReservations
                                                    .Include(lr => lr.IdCollaboratorNavigation) // Incluir la navegación hacia el colaborador
                                                    .Where(a => a.Status == true)
                                                    .ToListAsync();

                var logReservationDtos = logReservations.Select(lr => new GetLogReservationDto
                {
                    IdLogReservation = lr.IdLogReservation,
                    IdCollaborator = lr.IdCollaborator,
                    Name = lr.IdCollaboratorNavigation != null ? lr.IdCollaboratorNavigation.Name : string.Empty,
                    LastName = lr.IdCollaboratorNavigation != null ? lr.IdCollaboratorNavigation.LastName : string.Empty,
                    Comment = lr.Comment,
                    IdTown = lr.IdTown,
                    NumberPeople = lr.NumberPeople,
                    StatusReservation = lr.StatusReservation,
                    Justify = lr.Justify,
                    Addres = lr.Addres,
                    Status = lr.Status,
                    CreationDate = lr.CreationDate
                });

                return new OperationResponse<IEnumerable<GetLogReservationDto>>(200, "Log reservations retrieved successfully", logReservationDtos);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<GetLogReservationDto>>(500, $"Error retrieving log reservations: {ex.Message}");
            }
        }


        public async Task<OperationResponse<LogReservationDto>> UpdateLogReservationAsync(LogReservationDto logReservationDto)
        {
            try
            {
                var logReservation = await _context.LogReservations.FindAsync(logReservationDto.IdLogReservation);
                if (logReservation == null)
                {
                    return new OperationResponse<LogReservationDto>(404, "Log reservation not found");
                }

                logReservation.IdCollaborator = logReservationDto.IdCollaborator;
                logReservation.Comment = logReservationDto.Comment;
                logReservation.IdTown = logReservationDto.IdTown;
                logReservation.NumberPeople = logReservationDto.NumberPeople ?? 1;
                logReservation.StatusReservation = logReservationDto.StatusReservation;
                logReservation.Justify = logReservationDto.Justify;
                logReservation.Addres = logReservationDto.Addres;
                logReservation.Status = logReservationDto.Status;

                _context.Entry(logReservation).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OperationResponse<LogReservationDto>(200, "Log reservation updated successfully", logReservationDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogReservationDto>(500, $"Error updating log reservation: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteLogReservationAsync(int id)
        {
            try
            {
                var logReservation = await _context.LogReservations.FindAsync(id);
                if (logReservation == null)
                {
                    return new OperationResponse<bool>(404, "Log reservation not found");
                }

                _context.LogReservations.Remove(logReservation);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Log reservation deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting log reservation: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> UpdateStatusReservationAsync(int id, UpdateStatusReservationDto updateStatusReservationDto)
        {
            try
            {
                var logReservation = await _context.LogReservations.FindAsync(id);
                if (logReservation == null)
                {
                    return new OperationResponse<bool>(404, "Log reservation not found");
                }

                logReservation.StatusReservation = updateStatusReservationDto.StatusReservation;
                logReservation.Justify = updateStatusReservationDto.Justify;

                _context.Entry(logReservation).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OperationResponse<bool>(200, "Log reservation status updated successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error updating log reservation status: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteLogReservationStatusAsync(int id)
        {
            try
            {
                var reservation = await _context.LogReservations.FindAsync(id);
                if (reservation == null)
                {
                    return new OperationResponse<bool>(404, "Reservation not found");
                }

                // Cambiar el estado en lugar de eliminar físicamente
                reservation.Status = false;
                await _context.SaveChangesAsync();

                return new OperationResponse<bool>(200, "Reservation logically deleted (status set to false) successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting Reservation: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<LogReservationDto>>> GetLogReservationsByUserIdAsync(int userId)
        {
            try
            {
                var logReservations = await _context.LogReservations
                                                    .Include(lr => lr.IdCollaboratorNavigation)
                                                    .Where(lr => lr.IdCollaboratorNavigation.IdUser == userId && lr.Status == true)
                                                    .ToListAsync();

                var logReservationDtos = logReservations.Select(lr => new LogReservationDto
                {
                    IdLogReservation = lr.IdLogReservation,
                    IdCollaborator = lr.IdCollaborator,
                    Comment = lr.Comment,
                    IdTown = lr.IdTown,
                    NumberPeople = lr.NumberPeople,
                    StatusReservation = lr.StatusReservation,
                    Justify = lr.Justify,
                    Addres = lr.Addres,
                    Status = lr.Status,
                    CreationDate = lr.CreationDate
                });

                return new OperationResponse<IEnumerable<LogReservationDto>>(200, "Log reservations retrieved successfully", logReservationDtos);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<LogReservationDto>>(500, $"Error retrieving log reservations by user ID: {ex.Message}");
            }
        }

    }
}

