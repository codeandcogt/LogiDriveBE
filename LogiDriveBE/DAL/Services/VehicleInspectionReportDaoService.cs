using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LogiDriveBE.DAL.LogiDriveContext;

namespace LogiDriveBE.DAL.Services
{
    public class VehicleInspectionReportDaoService : IVehicleInspectionReportDao
    {
        private readonly LogiDriveDbContext _context;

        public VehicleInspectionReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleInspectionReportDto>> GetVehicleInspectionReportAsync()
        {
            return await _context.AllInspections.ToListAsync();
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleInspectionPdfReportAsync()
        {
            var inspections = await GetVehicleInspectionReportAsync();

            using (var memoryStream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                document.Add(new Paragraph("Reporte de Inspecciones")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20).SetBold());

                Table table = new Table(6, true);
                table.AddHeaderCell("ID Inspección");
                table.AddHeaderCell("Colaborador");
                table.AddHeaderCell("Comentario");
                table.AddHeaderCell("Fecha de Inspección");
                table.AddHeaderCell("Tipo de Inspección");
                table.AddHeaderCell("Placa del Vehículo");

                foreach (var inspection in inspections)
                {
                    table.AddCell(inspection.InspectionID.ToString());
                    table.AddCell(inspection.CollaboratorName);
                    table.AddCell(inspection.InspectionComment);
                    table.AddCell(inspection.InspectionDate.ToString("dd/MM/yyyy"));
                    table.AddCell(inspection.InspectionType);
                    table.AddCell(inspection.VehiclePlate);
                }

                document.Add(table);
                document.Close();

                return new OperationResponse<byte[]>(200, "Reporte PDF generado exitosamente", memoryStream.ToArray());
            }
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleInspectionCsvReportAsync()
        {
            var inspections = await GetVehicleInspectionReportAsync();
            var csvContent = new StringBuilder();

            csvContent.AppendLine("ID Inspección,Colaborador,Comentario,Fecha de Inspección,Tipo de Inspección,Placa del Vehículo");

            foreach (var inspection in inspections)
            {
                csvContent.AppendLine($"{inspection.InspectionID},{inspection.CollaboratorName},{inspection.InspectionComment},{inspection.InspectionDate:dd/MM/yyyy},{inspection.InspectionType},{inspection.VehiclePlate}");
            }

            return new OperationResponse<byte[]>(200, "Reporte CSV generado exitosamente", Encoding.UTF8.GetBytes(csvContent.ToString()));
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleInspectionExcelReportAsync()
        {
            var inspections = await GetVehicleInspectionReportAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Inspecciones");
                worksheet.Cells[1, 1].Value = "ID Inspección";
                worksheet.Cells[1, 2].Value = "Colaborador";
                worksheet.Cells[1, 3].Value = "Comentario";
                worksheet.Cells[1, 4].Value = "Fecha de Inspección";
                worksheet.Cells[1, 5].Value = "Tipo de Inspección";
                worksheet.Cells[1, 6].Value = "Placa del Vehículo";

                int row = 2;
                foreach (var inspection in inspections)
                {
                    worksheet.Cells[row, 1].Value = inspection.InspectionID;
                    worksheet.Cells[row, 2].Value = inspection.CollaboratorName;
                    worksheet.Cells[row, 3].Value = inspection.InspectionComment;
                    worksheet.Cells[row, 4].Value = inspection.InspectionDate.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 5].Value = inspection.InspectionType;
                    worksheet.Cells[row, 6].Value = inspection.VehiclePlate;
                    row++;
                }

                return new OperationResponse<byte[]>(200, "Reporte Excel generado exitosamente", package.GetAsByteArray());
            }
        }
    }
}
