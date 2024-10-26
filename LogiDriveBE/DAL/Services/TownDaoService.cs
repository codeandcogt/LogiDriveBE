using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class TownDaoService : ITownDao
    {
        private readonly LogiDriveDbContext _context;

        public TownDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<TownDto>> CreateTownAsync(TownDto townDto)
        {
            try
            {
                var town = new Town
                {
                    Name = townDto.Name,
                    IdDepartment = townDto.IdDepartment,
                    Status = townDto.Status
                };

                _context.Towns.Add(town);
                await _context.SaveChangesAsync();
                return new OperationResponse<TownDto>(200, "Town created successfully", townDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<TownDto>(500, $"Error creating town: {ex.Message}");
            }
        }

        public async Task<OperationResponse<TownDto>> GetTownByIdAsync(int id)
        {
            try
            {
                var town = await _context.Towns.FindAsync(id);
                if (town == null)
                {
                    return new OperationResponse<TownDto>(404, "Town not found");
                }

                var townDto = new TownDto
                {
                    Name = town.Name,
                    IdDepartment = town.IdDepartment,
                    Status = town.Status
                };

                return new OperationResponse<TownDto>(200, "Town retrieved successfully", townDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<TownDto>(500, $"Error retrieving town: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<TownDto>>> GetAllTownsAsync()
        {
            try
            {
                var towns = await _context.Towns
                                          .Where(a => a.Status == true || a.Status == true)
                                          .ToListAsync();
                var townDtos = towns.Select(t => new TownDto
                {
                    IdTown = t.IdTown,
                    Name = t.Name,
                    IdDepartment = t.IdDepartment,
                    Status = t.Status
                });

                return new OperationResponse<IEnumerable<TownDto>>(200, "Towns retrieved successfully", townDtos);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<TownDto>>(500, $"Error retrieving towns: {ex.Message}");
            }
        }

        public async Task<OperationResponse<TownDto>> UpdateTownAsync(TownDto townDto)
        {
            try
            {
                var town = await _context.Towns.FindAsync(townDto.IdTown);
                if (town == null)
                {
                    return new OperationResponse<TownDto>(404, "Town not found");
                }

                // Actualizar las propiedades de la entidad Town
                town.Name = townDto.Name;
                town.IdDepartment = townDto.IdDepartment;
                town.Status = townDto.Status;

                _context.Entry(town).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OperationResponse<TownDto>(200, "Town updated successfully", townDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<TownDto>(500, $"Error updating town: {ex.Message}");
            }
        }


        public async Task<OperationResponse<bool>> DeleteTownAsync(int id)
        {
            try
            {
                var town = await _context.Towns.FindAsync(id);
                if (town == null)
                {
                    return new OperationResponse<bool>(404, "Town not found");
                }

                _context.Towns.Remove(town);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Town deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting town: {ex.Message}");
            }
        }
        public async Task<OperationResponse<bool>> DeleteTownStatusAsync(int id)
        {
            try
            {
                var town = await _context.Towns.FindAsync(id);
                if (town == null)
                {
                    return new OperationResponse<bool>(404, "Town not found");
                }

                // Cambiar el estado en lugar de eliminar físicamente
                town.Status = false;
                await _context.SaveChangesAsync();

                return new OperationResponse<bool>(200, "Town logically deleted (status set to false) successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting town: {ex.Message}");
            }
        }
        public async Task<OperationResponse<IEnumerable<TownDto>>> GetTownsByDepartmentIdAsync(int departmentId)
        {
            try
            {
                var towns = await _context.Towns
                                          .Where(t => t.IdDepartment == departmentId && t.Status == true)
                                          .ToListAsync();
                var townDtos = towns.Select(t => new TownDto
                {
                 
                    Name = t.Name,
                    IdDepartment = t.IdDepartment,
                    Status = t.Status
                });

                return new OperationResponse<IEnumerable<TownDto>>(200, "Towns retrieved successfully by department", townDtos);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<TownDto>>(500, $"Error retrieving towns by department: {ex.Message}");
            }
        }

    }
}
