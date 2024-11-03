using System.Threading.Tasks;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IProcessLogReportBao
    {
        Task<OperationResponse<byte[]>> GenerateProcessLogPdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateProcessLogCsvReportAsync();
        Task<OperationResponse<byte[]>> GenerateProcessLogExcelReportAsync();
    }
}
