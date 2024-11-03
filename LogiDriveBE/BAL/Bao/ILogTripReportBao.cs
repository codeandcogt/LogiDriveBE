using System.Threading.Tasks;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface ILogTripReportBao
    {
        Task<OperationResponse<byte[]>> GenerateLogTripPdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateLogTripCsvReportAsync();
        Task<OperationResponse<byte[]>> GenerateLogTripExcelReportAsync();
    }
}
