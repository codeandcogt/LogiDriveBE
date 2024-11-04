using System.Threading.Tasks;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IVehicleAvailabilityReportBao
    {
        Task<OperationResponse<byte[]>> GenerateVehicleAvailabilityPdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateVehicleAvailabilityCsvReportAsync();
        Task<OperationResponse<byte[]>> GenerateVehicleAvailabilityExcelReportAsync();
    }
}
