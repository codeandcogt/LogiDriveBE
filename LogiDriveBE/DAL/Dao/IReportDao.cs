using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.DAL.Dao
{
    public interface IReportDao
    {
        Task<OperationResponse<byte[]>> GenerateReportAsync(string reportType);
        Task<List<ReportCollaboratorDto>> GetReportCollaboratorsAsync();
    }
}
