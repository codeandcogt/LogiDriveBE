using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IReportBao
    {
        Task<OperationResponse<byte[]>> GenerateReportAsync(string reportType);
    }
}
