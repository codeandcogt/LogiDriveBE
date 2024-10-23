using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using LogiDriveBE.BAL.Bao;

namespace LogiDriveBE.BAL.Services
{
    public class PreliminaryInspectionSheetBaoService : IPreliminaryInspectionSheetBao
    {
        private readonly IPreliminaryInspectionSheetDao _dao;

        public PreliminaryInspectionSheetBaoService(IPreliminaryInspectionSheetDao dao)
        {
            _dao = dao;
        }

        public async Task<OperationResponse<PreliminaryInspectionSheetDto>> CreatePreliminaryInspectionSheetAsync(PreliminaryInspectionSheetDto dto)
        {
            return await _dao.CreatePreliminaryInspectionSheetAsync(dto);
        }

        public async Task<OperationResponse<PreliminaryInspectionSheetDto>> GetPreliminaryInspectionSheetByIdAsync(int id)
        {
            return await _dao.GetPreliminaryInspectionSheetByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<PreliminaryInspectionSheetDto>>> GetAllPreliminaryInspectionSheetsAsync()
        {
            return await _dao.GetAllPreliminaryInspectionSheetsAsync();
        }

        public async Task<OperationResponse<PreliminaryInspectionSheetDto>> UpdatePreliminaryInspectionSheetAsync(PreliminaryInspectionSheetDto dto)
        {
            return await _dao.UpdatePreliminaryInspectionSheetAsync(dto);
        }

        public async Task<OperationResponse<bool>> DeletePreliminaryInspectionSheetAsync(int id)
        {
            return await _dao.DeletePreliminaryInspectionSheetAsync(id);
        }
    }
}
