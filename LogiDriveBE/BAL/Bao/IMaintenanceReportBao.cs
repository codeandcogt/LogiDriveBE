using System.Threading.Tasks;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IMaintenanceReportBao
    {
        Task<OperationResponse<byte[]>> GenerateMaintenancePdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateMaintenanceCsvReportAsync();
        Task<OperationResponse<byte[]>> GenerateMaintenanceExcelReportAsync();
    }
}
