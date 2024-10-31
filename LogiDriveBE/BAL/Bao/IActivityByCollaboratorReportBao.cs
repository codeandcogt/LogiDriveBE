using System.Threading.Tasks;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IActivityByCollaboratorReportBao
    {
        Task<OperationResponse<byte[]>> GenerateActivityByCollaboratorPdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateActivityByCollaboratorCsvReportAsync();
        Task<OperationResponse<byte[]>> GenerateActivityByCollaboratorExcelReportAsync();
    }
}
