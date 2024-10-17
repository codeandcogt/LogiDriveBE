using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface ICollaboratorBao
    {
        Task<OperationResponse<Collaborator>> CreateCollaboratorAsync(Collaborator collaborator);
        Task<OperationResponse<Collaborator>> GetCollaboratorByIdAsync(int id);
        Task<OperationResponse<IEnumerable<Collaborator>>> GetAllCollaboratorsAsync();
        Task<OperationResponse<Collaborator>> UpdateCollaboratorAsync(Collaborator collaborator);
        Task<OperationResponse<bool>> DeleteCollaboratorAsync(int id);
    }
}
