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
    public class VehicleAssignmentReportController : ControllerBase
    {
        private readonly IVehicleAssignmentReportBao _vehicleAssignmentReportBao;

        public VehicleAssignmentReportController(IVehicleAssignmentReportBao vehicleAssignmentReportBao)
        {
            _vehicleAssignmentReportBao = vehicleAssignmentReportBao;
        }

        [HttpGet("generateVehicleAssignmentReport")]
        public async Task<IActionResult> GenerateVehicleAssignmentReport([FromQuery] string reportType)
        {
            byte[] reportBytes;

            // Manejar los diferentes tipos de reportes
            switch (reportType)
            {
                case "assignedVehiclesPdf":
                    reportBytes = (await _vehicleAssignmentReportBao.GenerateVehicleAssignmentPdfReportAsync()).Data;
                    break;

            
                default:
                    return BadRequest("Tipo de reporte no válido.");
            }

            string mimeType = GetMimeType(reportType);
            return File(reportBytes, mimeType, $"reporte_{reportType}.pdf");
        }

        // Determinar el tipo de archivo según el tipo de reporte solicitado
        private string GetMimeType(string reportType)
        {
            return reportType switch
            {
                "assignedVehiclesPdf" => "application/pdf",
                _ => "application/octet-stream",
            };
        }
    }
}
