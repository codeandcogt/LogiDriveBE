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
    public class ActivityByCollaboratorReportDaoService : IActivityByCollaboratorReportDao
    {
        private readonly LogiDriveDbContext _context;

        public ActivityByCollaboratorReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<List<ActivityByCollaboratorReportDto>> GetActivityByCollaboratorReportAsync()
        {
            return await _context.ActivityByCollaboratorReport.ToListAsync();
        }

        public async Task<byte[]> GenerateActivityByCollaboratorPdfReportAsync()
        {
            var reportData = await GetActivityByCollaboratorReportAsync();
            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Reporte de Actividad por Colaborador").SetFontSize(20).SetBold());

                var table = new Table(9, true);
                table.AddHeaderCell("Nombre");
                table.AddHeaderCell("Apellido");
                table.AddHeaderCell("Área");
                table.AddHeaderCell("Tipo de Actividad");
                table.AddHeaderCell("Fecha");
                table.AddHeaderCell("Categoría");
                table.AddHeaderCell("Tipo de Inspección");
                table.AddHeaderCell("Estado de Reserva");
                table.AddHeaderCell("Tipo de Viaje");

                foreach (var item in reportData)
                {
                    table.AddCell(item.CollaboratorName);
                    table.AddCell(item.CollaboratorLastName);
                    table.AddCell(item.AreaName ?? "N/A");
                    table.AddCell(item.ActivityType ?? "N/A");
                    table.AddCell(item.ActivityDate?.ToString("dd/MM/yyyy") ?? "N/A");
                    table.AddCell(item.ActivityCategory ?? "N/A");
                    table.AddCell(item.InspectionType ?? "N/A");
                    table.AddCell(item.ReservationStatus ?? "N/A");
                    table.AddCell(item.TripType ?? "N/A");
                }

                document.Add(table);
                document.Close();

                return memoryStream.ToArray();
            }
        }

        public async Task<byte[]> GenerateActivityByCollaboratorCsvReportAsync()
        {
            var reportData = await GetActivityByCollaboratorReportAsync();
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Nombre,Apellido,Área,Tipo de Actividad,Fecha,Categoría,Tipo de Inspección,Estado de Reserva,Tipo de Viaje");

            foreach (var item in reportData)
            {
                csvBuilder.AppendLine($"{item.CollaboratorName},{item.CollaboratorLastName},{item.AreaName},{item.ActivityType},{item.ActivityDate?.ToString("dd/MM/yyyy")},{item.ActivityCategory},{item.InspectionType},{item.ReservationStatus},{item.TripType}");
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        public async Task<byte[]> GenerateActivityByCollaboratorExcelReportAsync()
        {
            // Configuración de la licencia de EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var reportData = await GetActivityByCollaboratorReportAsync();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Actividad por Colaborador");

                worksheet.Cells[1, 1].Value = "Nombre";
                worksheet.Cells[1, 2].Value = "Apellido";
                worksheet.Cells[1, 3].Value = "Área";
                worksheet.Cells[1, 4].Value = "Tipo de Actividad";
                worksheet.Cells[1, 5].Value = "Fecha";
                worksheet.Cells[1, 6].Value = "Categoría";
                worksheet.Cells[1, 7].Value = "Tipo de Inspección";
                worksheet.Cells[1, 8].Value = "Estado de Reserva";
                worksheet.Cells[1, 9].Value = "Tipo de Viaje";

                int row = 2;
                foreach (var item in reportData)
                {
                    worksheet.Cells[row, 1].Value = item.CollaboratorName;
                    worksheet.Cells[row, 2].Value = item.CollaboratorLastName;
                    worksheet.Cells[row, 3].Value = item.AreaName ?? "N/A";
                    worksheet.Cells[row, 4].Value = item.ActivityType ?? "N/A";
                    worksheet.Cells[row, 5].Value = item.ActivityDate?.ToString("dd/MM/yyyy") ?? "N/A";
                    worksheet.Cells[row, 6].Value = item.ActivityCategory ?? "N/A";
                    worksheet.Cells[row, 7].Value = item.InspectionType ?? "N/A";
                    worksheet.Cells[row, 8].Value = item.ReservationStatus ?? "N/A";
                    worksheet.Cells[row, 9].Value = item.TripType ?? "N/A";
                    row++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
