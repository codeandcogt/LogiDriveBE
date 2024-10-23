using LogiDriveBE.UTILS;
using LogiDriveBE.DAL.Models.DTO;
namespace LogiDriveBE.BAL.Bao
{
    public interface IPreliminaryInspectionSheetBao
    {
        Task<OperationResponse<PreliminaryInspectionSheetDto>> CreatePreliminaryInspectionSheetAsync(PreliminaryInspectionSheetDto preliminaryInspectionSheetDto);
        Task<OperationResponse<PreliminaryInspectionSheetDto>> GetPreliminaryInspectionSheetByIdAsync(int id);
        Task<OperationResponse<IEnumerable<PreliminaryInspectionSheetDto>>> GetAllPreliminaryInspectionSheetsAsync();
        Task<OperationResponse<PreliminaryInspectionSheetDto>> UpdatePreliminaryInspectionSheetAsync(PreliminaryInspectionSheetDto preliminaryInspectionSheetDto);
        Task<OperationResponse<bool>> DeletePreliminaryInspectionSheetAsync(int id);
    }
}
