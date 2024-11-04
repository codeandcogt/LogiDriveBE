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
    public class MaintenanceReportDaoService : IMaintenanceReportDao
    {
        private readonly LogiDriveDbContext _context;

        public MaintenanceReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<List<MaintenanceReportDto>> GetMaintenanceReportAsync()
        {
            return await _context.MaintenanceReport.ToListAsync();
        }

        public async Task<byte[]> GenerateMaintenancePdfReportAsync()
        {
            var reportData = await GetMaintenanceReportAsync();
            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Reporte de Mantenimiento").SetFontSize(20).SetBold());

                var table = new Table(6, true);
                table.AddHeaderCell("Vehículo");
                table.AddHeaderCell("Tipo de Mantenimiento");
                table.AddHeaderCell("Próximo Servicio");
                table.AddHeaderCell("Comentario Servicio");
                table.AddHeaderCell("Tipo de Servicio");
                table.AddHeaderCell("Fecha y Comentario del Mantenimiento de Parte");

                foreach (var item in reportData)
                {
                    table.AddCell(item.IdVehicle.ToString());
                    table.AddCell(item.MaintenanceType ?? "N/A");
                    table.AddCell(item.NextServiceDate ?? "N/A");
                    table.AddCell(item.ServiceComment ?? "N/A");
                    table.AddCell(item.TypeServiceName ?? "N/A");
                    table.AddCell($"{item.PartMaintenanceDate?.ToString("dd/MM/yyyy") ?? "N/A"} - {item.PartMaintenanceComment ?? "N/A"}");
                }

                document.Add(table);
                document.Close();

                return memoryStream.ToArray();
            }
        }

        public async Task<byte[]> GenerateMaintenanceCsvReportAsync()
        {
            var reportData = await GetMaintenanceReportAsync();
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Vehículo,Tipo de Mantenimiento,Próximo Servicio,Comentario Servicio,Tipo de Servicio,Fecha y Comentario del Mantenimiento de Parte");

            foreach (var item in reportData)
            {
                csvBuilder.AppendLine($"{item.IdVehicle},{item.MaintenanceType},{item.NextServiceDate},{item.ServiceComment},{item.TypeServiceName},{item.PartMaintenanceDate?.ToString("dd/MM/yyyy")}-{item.PartMaintenanceComment}");
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        public async Task<byte[]> GenerateMaintenanceExcelReportAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var reportData = await GetMaintenanceReportAsync();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte de Mantenimiento");

                worksheet.Cells[1, 1].Value = "Vehículo";
                worksheet.Cells[1, 2].Value = "Tipo de Mantenimiento";
                worksheet.Cells[1, 3].Value = "Próximo Servicio";
                worksheet.Cells[1, 4].Value = "Comentario Servicio";
                worksheet.Cells[1, 5].Value = "Tipo de Servicio";
                worksheet.Cells[1, 6].Value = "Fecha y Comentario del Mantenimiento de Parte";

                int row = 2;
                foreach (var item in reportData)
                {
                    worksheet.Cells[row, 1].Value = item.IdVehicle;
                    worksheet.Cells[row, 2].Value = item.MaintenanceType ?? "N/A";
                    worksheet.Cells[row, 3].Value = item.NextServiceDate ?? "N/A";
                    worksheet.Cells[row, 4].Value = item.ServiceComment ?? "N/A";
                    worksheet.Cells[row, 5].Value = item.TypeServiceName ?? "N/A";
                    worksheet.Cells[row, 6].Value = $"{item.PartMaintenanceDate?.ToString("dd/MM/yyyy") ?? "N/A"} - {item.PartMaintenanceComment ?? "N/A"}";
                    row++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
