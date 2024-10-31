using System.Text;
using OfficeOpenXml;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using LogiDriveBE.DAL.LogiDriveContext;
using System.IO;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Services
{
    public class VehicleAssignmentReportDaoService : IVehicleAssignmentReportDao
    {
        private readonly LogiDriveDbContext _context;

        public VehicleAssignmentReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleAssignmentPdfReportAsync()
        {
            var vehicleAssignments = await GetVehicleAssignmentReportAsync();

            using (var memoryStream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                document.Add(new Paragraph("Reporte de Asignaciones de Vehículos")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBold());

                Table table = new Table(6, true);
                table.AddHeaderCell("ID Asignación");
                table.AddHeaderCell("Comentario");
                table.AddHeaderCell("Tipo de Viaje");
                table.AddHeaderCell("Fecha Inicio");
                table.AddHeaderCell("Fecha Fin");
                table.AddHeaderCell("Placa del Vehículo");
                table.AddHeaderCell("Colaborador");

                foreach (var assignment in vehicleAssignments)
                {
                    table.AddCell(assignment.IdVehicleAssignment.ToString());
                    table.AddCell(assignment.Comment);
                    table.AddCell(assignment.TripType);
                    table.AddCell(assignment.StartDate.ToString("dd/MM/yyyy"));
                    table.AddCell(assignment.EndDate.ToString("dd/MM/yyyy"));
                    table.AddCell(assignment.VehiclePlate);
                    table.AddCell(assignment.CollaboratorName);
                }

                document.Add(table);
                document.Close();

                return new OperationResponse<byte[]>(200, "Reporte PDF generado exitosamente", memoryStream.ToArray());
            }
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleAssignmentCsvReportAsync()
        {
            var vehicleAssignments = await GetVehicleAssignmentReportAsync();
            var csvContent = new StringBuilder();

            csvContent.AppendLine("ID Asignación,Comentario,Tipo de Viaje,Fecha Inicio,Fecha Fin,Placa del Vehículo,Colaborador");

            foreach (var assignment in vehicleAssignments)
            {
                csvContent.AppendLine($"{assignment.IdVehicleAssignment},{assignment.Comment},{assignment.TripType},{assignment.StartDate:dd/MM/yyyy},{assignment.EndDate:dd/MM/yyyy},{assignment.VehiclePlate},{assignment.CollaboratorName}");
            }

            return new OperationResponse<byte[]>(200, "Reporte CSV generado exitosamente", Encoding.UTF8.GetBytes(csvContent.ToString()));
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleAssignmentExcelReportAsync()
        {
            var vehicleAssignments = await GetVehicleAssignmentReportAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Asignaciones de Vehículos");
                worksheet.Cells[1, 1].Value = "ID Asignación";
                worksheet.Cells[1, 2].Value = "Comentario";
                worksheet.Cells[1, 3].Value = "Tipo de Viaje";
                worksheet.Cells[1, 4].Value = "Fecha Inicio";
                worksheet.Cells[1, 5].Value = "Fecha Fin";
                worksheet.Cells[1, 6].Value = "Placa del Vehículo";
                worksheet.Cells[1, 7].Value = "Colaborador";

                int row = 2;
                foreach (var assignment in vehicleAssignments)
                {
                    worksheet.Cells[row, 1].Value = assignment.IdVehicleAssignment;
                    worksheet.Cells[row, 2].Value = assignment.Comment;
                    worksheet.Cells[row, 3].Value = assignment.TripType;
                    worksheet.Cells[row, 4].Value = assignment.StartDate.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 5].Value = assignment.EndDate.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 6].Value = assignment.VehiclePlate;
                    worksheet.Cells[row, 7].Value = assignment.CollaboratorName;
                    row++;
                }

                return new OperationResponse<byte[]>(200, "Reporte Excel generado exitosamente", package.GetAsByteArray());
            }
        }

        public async Task<List<VehicleAssignmentReportDto>> GetVehicleAssignmentReportAsync()
        {
            var result = await _context.VehicleAssignmentWithCollaborator
                                       .Select(va => new VehicleAssignmentReportDto
                                       {
                                           IdVehicleAssignment = va.IdVehicleAssignment,
                                           Comment = va.Comment,
                                           TripType = va.TripType,
                                           StartDate = va.StartDate,
                                           EndDate = va.EndDate,
                                           VehiclePlate = va.VehiclePlate,
                                           CollaboratorName = va.CollaboratorName
                                       }).ToListAsync();

            return result;
        }
    }
}
