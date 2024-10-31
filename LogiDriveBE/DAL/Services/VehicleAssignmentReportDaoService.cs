using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using System.IO;
using LogiDriveBE.DAL.LogiDriveContext;

namespace LogiDriveBE.DAL.Services
{
    public class VehicleAssignmentReportDaoService : IVehicleAssignmentReportDao
    {
        private readonly LogiDriveDbContext _context;

        public VehicleAssignmentReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        // Método para obtener los datos desde la vista
        public async Task<List<VehicleAssignmentReportDto>> GetVehicleAssignmentReportAsync()
        {
            var result = await _context.VehicleAssignmentWithCollaborator // Aquí accedes a la vista como entidad
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

        // Método para generar el reporte PDF
        public async Task<byte[]> GenerateVehicleAssignmentPdfReportAsync()
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

                Table table = new Table(6, true); // 6 columnas
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

                return memoryStream.ToArray();
            }
        }
    }
}
