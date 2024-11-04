using System.Collections.Generic;
using System.Threading.Tasks;
using LogiDriveBE.DAL.Models.DTO;

namespace LogiDriveBE.DAL.Dao
{
    public interface IActivityByCollaboratorReportDao
    {
        Task<List<ActivityByCollaboratorReportDto>> GetActivityByCollaboratorReportAsync();
        Task<byte[]> GenerateActivityByCollaboratorPdfReportAsync();
        Task<byte[]> GenerateActivityByCollaboratorCsvReportAsync();
        Task<byte[]> GenerateActivityByCollaboratorExcelReportAsync();
    }
}
