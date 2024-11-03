using LogiDriveBE.UTILS;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Bao
{
    public interface IVehicleProcessReservationReportBao
    {
        Task<OperationResponse<byte[]>> GenerateProcessReservationPdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateProcessReservationCsvReportAsync();  // Nuevo método para CSV
        Task<OperationResponse<byte[]>> GenerateProcessReservationExcelReportAsync();  // Nuevo método para Excel
    }
}
