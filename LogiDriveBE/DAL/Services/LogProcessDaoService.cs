using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Services
{
    public class LogProcessDaoService : ILogProcessDao
    {
        private readonly LogiDriveDbContext _context;

        public LogProcessDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<LogProcessDto>> CreateLogProcessAsync(LogProcessDto logProcessDto)
        {
            try
            {
                var logProcess = new LogProcess
                {
                    Action = logProcessDto.Action,
                    IdCollaborator = logProcessDto.IdCollaborator,
                    IdVehicleAssignment = logProcessDto.IdVehicleAssignment,
                    IdLogInspection = null  // Inicialmente establecemos como null
                };

                // Si viene IdLogReservation, lo asignamos
                if (logProcessDto.IdLogReservation > 0)
                {
                    var reservation = await _context.LogReservations.FindAsync(logProcessDto.IdLogReservation);
                    if (reservation == null)
                    {
                        return new OperationResponse<LogProcessDto>(400, "La reserva especificada no existe");
                    }
                    logProcess.IdLogReservation = logProcessDto.IdLogReservation;
                }
                else
                {
                    // Si no viene IdLogReservation, creamos uno temporal o asignamos un valor por defecto
                    // Esto dependerá de tu lógica de negocio
                    var defaultReservation = await _context.LogReservations.FirstOrDefaultAsync();
                    if (defaultReservation == null)
                    {
                        return new OperationResponse<LogProcessDto>(500, "No existe una reserva por defecto en el sistema");
                    }
                    logProcess.IdLogReservation = defaultReservation.IdLogReservation;
                }

                _context.LogProcesses.Add(logProcess);
                await _context.SaveChangesAsync();

                // Si tenemos un IdLogInspection, actualizamos el proceso después de crear
                if (logProcessDto.IdLogInspection.HasValue && logProcessDto.IdLogInspection.Value > 0)
                {
                    logProcess.IdLogInspection = logProcessDto.IdLogInspection;
                    _context.LogProcesses.Update(logProcess);
                    await _context.SaveChangesAsync();
                }

                var resultDto = new LogProcessDto
                {
                    IdLogProcess = logProcess.IdLogProcess,
                    IdLogReservation = logProcess.IdLogReservation,
                    Action = logProcess.Action,
                    IdCollaborator = logProcess.IdCollaborator,
                    IdVehicleAssignment = logProcess.IdVehicleAssignment,
                    IdLogInspection = logProcess.IdLogInspection
                };

                return new OperationResponse<LogProcessDto>(200, "Log process created successfully", resultDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogProcessDto>(500, 
                    $"Error creating log process: {ex.Message} - Inner: {ex.InnerException?.Message}");
            }
        }

        public async Task<OperationResponse<LogProcessDto>> GetLogProcessByIdAsync(int id)
        {
            try
            {
                var logProcess = await _context.LogProcesses.FindAsync(id);
                if (logProcess == null)
                {
                    return new OperationResponse<LogProcessDto>(404, "Log process not found");
                }

                var logProcessDto = new LogProcessDto
                {
                    IdLogReservation = logProcess.IdLogReservation,
                    Action = logProcess.Action,
                    IdCollaborator = logProcess.IdCollaborator,
                    IdVehicleAssignment = logProcess.IdVehicleAssignment,
                    IdLogInspection = logProcess.IdLogInspection
                };

                return new OperationResponse<LogProcessDto>(200, "Log process retrieved successfully", logProcessDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogProcessDto>(500, $"Error retrieving log process: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<LogProcessDto>>> GetAllLogProcessesAsync()
        {
            try
            {
                var logProcesses = await _context.LogProcesses.ToListAsync();
                var logProcessDtos = logProcesses.Select(lp => new LogProcessDto
                {
                    IdLogReservation = lp.IdLogReservation,
                    Action = lp.Action,
                    IdCollaborator = lp.IdCollaborator,
                    IdVehicleAssignment = lp.IdVehicleAssignment,
                    IdLogInspection = lp.IdLogInspection
                }).ToList();

                return new OperationResponse<IEnumerable<LogProcessDto>>(200, "Log processes retrieved successfully", logProcessDtos);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<LogProcessDto>>(500, $"Error retrieving log processes: {ex.Message}");
            }
        }

        public async Task<OperationResponse<LogProcessDto>> UpdateLogProcessAsync(LogProcessDto logProcessDto)
        {
            try
            {
                var logProcess = await _context.LogProcesses.FindAsync(logProcessDto.IdLogReservation);
                if (logProcess == null)
                {
                    return new OperationResponse<LogProcessDto>(404, "Log process not found");
                }

                logProcess.Action = logProcessDto.Action;
                logProcess.IdCollaborator = logProcessDto.IdCollaborator;
                logProcess.IdVehicleAssignment = logProcessDto.IdVehicleAssignment;
                logProcess.IdLogInspection = logProcessDto.IdLogInspection;

                _context.Entry(logProcess).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OperationResponse<LogProcessDto>(200, "Log process updated successfully", logProcessDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogProcessDto>(500, $"Error updating log process: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteLogProcessAsync(int id)
        {
            try
            {
                var logProcess = await _context.LogProcesses.FindAsync(id);
                if (logProcess == null)
                {
                    return new OperationResponse<bool>(404, "Log process not found");
                }

                _context.LogProcesses.Remove(logProcess);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Log process deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting log process: {ex.Message}");
            }
        }
    }
}
