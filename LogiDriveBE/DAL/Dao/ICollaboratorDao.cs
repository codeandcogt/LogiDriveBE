using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.DAL.Dao
{
    public interface ICollaboratorDao
    {
        Task<OperationResponse<Collaborator>> CreateCollaboratorAsync(Collaborator collaborator);
        Task<OperationResponse<Collaborator>> GetCollaboratorByIdAsync(int id);
        Task<OperationResponse<IEnumerable<Collaborator>>> GetAllCollaboratorsAsync();
        Task<OperationResponse<Collaborator>> UpdateCollaboratorAsync(Collaborator collaborator);
        Task<OperationResponse<bool>> DeleteCollaboratorAsync(int id);
        Task<OperationResponse<bool>> DeleteCollaboratorStatusAsync(int id);
        Task<OperationResponse<Collaborator>> GetCollaboratorByUserIdAsync(int userId);
    }
}
