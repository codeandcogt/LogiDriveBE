using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class CollaboratorBaoService : ICollaboratorBao
    {
        private readonly ICollaboratorDao _collaboratorDao;
        private readonly IAreaDao _areaDao;

        public CollaboratorBaoService(ICollaboratorDao collaboratorDao, IAreaDao areaDao)
        {
            _collaboratorDao = collaboratorDao;
            _areaDao = areaDao;
        }

        public async Task<OperationResponse<Collaborator>> CreateCollaboratorAsync(Collaborator collaborator)
        {
            var areaResponse = await _areaDao.GetAreaByIdAsync(collaborator.IdArea);
            if (areaResponse.Code != 200)
            {
                return new OperationResponse<Collaborator>(400, "Invalid Area ID");
            }

            return await _collaboratorDao.CreateCollaboratorAsync(collaborator);
        }

        public async Task<OperationResponse<Collaborator>> GetCollaboratorByIdAsync(int id)
        {
            return await _collaboratorDao.GetCollaboratorByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<Collaborator>>> GetAllCollaboratorsAsync()
        {
            return await _collaboratorDao.GetAllCollaboratorsAsync();
        }

        public async Task<OperationResponse<Collaborator>> UpdateCollaboratorAsync(Collaborator collaborator)
        {
            return await _collaboratorDao.UpdateCollaboratorAsync(collaborator);
        }

        public async Task<OperationResponse<bool>> DeleteCollaboratorAsync(int id)
        {
            return await _collaboratorDao.DeleteCollaboratorAsync(id);
        }
    }
}
