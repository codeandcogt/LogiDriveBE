using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using OfficeOpenXml;

namespace LogiDriveBE.DAL.Services
{
    public class VehicleAvailabilityReportDaoService : IVehicleAvailabilityReportDao
    {
        private readonly LogiDriveDbContext _context;

        public VehicleAvailabilityReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleAvailabilityReportDto>> GetVehicleAvailabilityReportAsync()
        {
            return await _context.VehicleAvailabilityReport.ToListAsync();
        }

        public async Task<byte[]> GenerateVehicleAvailabilityPdfReportAsync()
        {
            var reportData = await GetVehicleAvailabilityReportAsync();
            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Reporte de Disponibilidad de Vehículos").SetFontSize(20).SetBold());

                var table = new Table(4, true);
                table.AddHeaderCell("Estado del Vehículo");
                table.AddHeaderCell("Fecha de Última Asignación");
                table.AddHeaderCell("Fecha de Última Inspección");
                table.AddHeaderCell("Tipo de Última Inspección");

                foreach (var item in reportData)
                {
                    table.AddCell(item.VehicleStatus);
                    table.AddCell(item.LastAssignmentEndDate?.ToString("dd/MM/yyyy") ?? "N/A");
                    table.AddCell(item.LastInspectionDate?.ToString("dd/MM/yyyy") ?? "N/A");
                    table.AddCell(item.LastInspectionType ?? "N/A");
                }

                document.Add(table);
                document.Close();

                return memoryStream.ToArray();
            }
        }

        public async Task<byte[]> GenerateVehicleAvailabilityCsvReportAsync()
        {
            var reportData = await GetVehicleAvailabilityReportAsync();
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Estado del Vehículo,Fecha de Última Asignación,Fecha de Última Inspección,Tipo de Última Inspección");

            foreach (var item in reportData)
            {
                csvBuilder.AppendLine(
                    $"{item.VehicleStatus}," +
                    $"{item.LastAssignmentEndDate?.ToString("dd/MM/yyyy") ?? "N/A"}," +
                    $"{item.LastInspectionDate?.ToString("dd/MM/yyyy") ?? "N/A"}," +
                    $"{item.LastInspectionType ?? "N/A"}"
                );
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        public async Task<byte[]> GenerateVehicleAvailabilityExcelReportAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var reportData = await GetVehicleAvailabilityReportAsync();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Disponibilidad de Vehículos");

                worksheet.Cells[1, 1].Value = "Estado del Vehículo";
                worksheet.Cells[1, 2].Value = "Fecha de Última Asignación";
                worksheet.Cells[1, 3].Value = "Fecha de Última Inspección";
                worksheet.Cells[1, 4].Value = "Tipo de Última Inspección";

                int row = 2;
                foreach (var item in reportData)
                {
                    worksheet.Cells[row, 1].Value = item.VehicleStatus;
                    worksheet.Cells[row, 2].Value = item.LastAssignmentEndDate?.ToString("dd/MM/yyyy") ?? "N/A";
                    worksheet.Cells[row, 3].Value = item.LastInspectionDate?.ToString("dd/MM/yyyy") ?? "N/A";
                    worksheet.Cells[row, 4].Value = item.LastInspectionType ?? "N/A";
                    row++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
