using LogiDriveBE.BAL.Bao;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportBao _reportBao;
        private readonly IVehicleAssignmentReportBao _vehicleAssignmentReportBao;

        public ReportController(IReportBao reportBao, IVehicleAssignmentReportBao vehicleAssignmentReportBao)
        {
            _reportBao = reportBao;
            _vehicleAssignmentReportBao = vehicleAssignmentReportBao;
        }

        [HttpGet("generateReport")]
        public async Task<IActionResult> GenerateReport([FromQuery] string reportType)
        {
            byte[] reportBytes;

            switch (reportType)
            {
                case "collaboratorPdf":
                    var response = await _reportBao.GenerateReportAsync("pdf");
                    if (response.Code == 200)
                    {
                        reportBytes = response.Data;
                        string mimeType = GetMimeType(reportType);
                        return File(reportBytes, mimeType, $"report.{reportType}");
                    }
                    return StatusCode(response.Code, response);

                case "assignedVehiclesPdf":
                    reportBytes = (await _vehicleAssignmentReportBao.GenerateVehicleAssignmentPdfReportAsync()).Data;
                    string assignedVehicleMimeType = GetMimeType(reportType);
                    return File(reportBytes, assignedVehicleMimeType, $"reporte_{reportType}.pdf");

                // Puedes añadir más tipos de reportes aquí según necesites.
                default:
                    return BadRequest("Tipo de reporte no válido.");
            }
        }

        private string GetMimeType(string reportType)
        {
            return reportType switch
            {
                "pdf" => "application/pdf",
                "collaboratorPdf" => "application/pdf",
                "assignedVehiclesPdf" => "application/pdf",
                "csv" => "text/csv",
                "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream",
            };
        }
    }
}
