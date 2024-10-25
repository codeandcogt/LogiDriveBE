using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class CollaboratorDaoService : ICollaboratorDao
    {
        private readonly LogiDriveDbContext _context;

        public CollaboratorDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Collaborator>> CreateCollaboratorAsync(Collaborator collaborator)
        {
            try
            {
                _context.Collaborators.Add(collaborator);
                await _context.SaveChangesAsync();
                return new OperationResponse<Collaborator>(200, "Collaborator created successfully", collaborator);
            }
            catch (DbUpdateException ex)
            {
                // Log the full exception details
                Console.WriteLine($"DbUpdateException: {ex.ToString()}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return new OperationResponse<Collaborator>(500, $"Error creating collaborator: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                // Log the full exception details
                Console.WriteLine($"Exception: {ex.ToString()}");

                return new OperationResponse<Collaborator>(500, $"Error creating collaborator: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Collaborator>> GetCollaboratorByIdAsync(int id)
        {
            try
            {
                var collaborator = await _context.Collaborators.FindAsync(id);
                if (collaborator == null)
                {
                    return new OperationResponse<Collaborator>(404, "Collaborator not found");
                }
                return new OperationResponse<Collaborator>(200, "Collaborator retrieved successfully", collaborator);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Collaborator>(500, $"Error retrieving collaborator: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Collaborator>>> GetAllCollaboratorsAsync()
        {
            try
            {
                var collaborators = await _context.Collaborators
                                                  .Where(a => a.Status == true || a.Status == true)
                                                  .ToListAsync();
                return new OperationResponse<IEnumerable<Collaborator>>(200, "Collaborators retrieved successfully", collaborators);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Collaborator>>(500, $"Error retrieving collaborators: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Collaborator>> UpdateCollaboratorAsync(Collaborator collaborator)
        {
            try
            {
                // Intentar obtener el objeto Collaborator desde la base de datos por su Id
                var existingCollaborator = await _context.Collaborators
                    .FirstOrDefaultAsync(c => c.IdCollaborator == collaborator.IdCollaborator);

                // Verificar si no se encontró el Collaborator
                if (existingCollaborator == null)
                {
                    return new OperationResponse<Collaborator>(404, "Collaborator not found");
                }

                // Actualizar las propiedades necesarias
                _context.Entry(existingCollaborator).CurrentValues.SetValues(collaborator);

                // Guardar los cambios
                await _context.SaveChangesAsync();

                return new OperationResponse<Collaborator>(200, "Collaborator updated successfully", collaborator);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new OperationResponse<Collaborator>(409, "The data was modified by another user, please try again");
            }
            catch (Exception ex)
            {
                return new OperationResponse<Collaborator>(500, $"Error updating collaborator: {ex.Message}");
            }
        }



        public async Task<OperationResponse<bool>> DeleteCollaboratorAsync(int id)
        {
            try
            {
                var collaborator = await _context.Collaborators.FindAsync(id);
                if (collaborator == null)
                {
                    return new OperationResponse<bool>(404, "Collaborator not found");
                }

                _context.Collaborators.Remove(collaborator);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Collaborator deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting collaborator: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteCollaboratorStatusAsync(int id)
        {
            try
            {
                var collaborator = await _context.Collaborators.FindAsync(id);
                if (collaborator == null)
                {
                    return new OperationResponse<bool>(404, "Collaborator not found");
                }

                // Cambiar el estado en lugar de eliminar físicamente
                collaborator.Status = false;  
                await _context.SaveChangesAsync();

                return new OperationResponse<bool>(200, "Collaborator logically deleted (status set to false) successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting Collaborator: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Collaborator>> GetCollaboratorByUserIdAsync(int userId)
        {
            try
            {
                var collaborator = await _context.Collaborators.FirstOrDefaultAsync(c => c.IdUser == userId);
                if (collaborator == null)
                {
                    return new OperationResponse<Collaborator>(404, "Collaborator not found");
                }
                return new OperationResponse<Collaborator>(200, "Collaborator retrieved successfully", collaborator);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Collaborator>(500, $"Error retrieving collaborator: {ex.Message}");
            }
        }

    }
}

