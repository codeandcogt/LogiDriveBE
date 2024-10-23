using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using LogiDriveBE.DAL.Dao;

namespace LogiDriveBE.DAL.Services
{
    public class PreliminaryInspectionSheetDaoService : IPreliminaryInspectionSheetDao
    {
        private readonly LogiDriveDbContext _context;

        public PreliminaryInspectionSheetDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<PreliminaryInspectionSheetDto>> CreatePreliminaryInspectionSheetAsync(PreliminaryInspectionSheetDto preliminaryInspectionSheetDto)
        {
            try
            {
                var preliminaryInspectionSheet = new PreliminaryInspectionSheet
                {
                    Comment = preliminaryInspectionSheetDto.Comment,
                    IdVehicleAssignment = preliminaryInspectionSheetDto.IdVehicleAssignment,
                    Status = preliminaryInspectionSheetDto.Status,
                    DateSheet = preliminaryInspectionSheetDto.DateSheet
                };

                _context.PreliminaryInspectionSheets.Add(preliminaryInspectionSheet);
                await _context.SaveChangesAsync();
                return new OperationResponse<PreliminaryInspectionSheetDto>(200, "Preliminary Inspection Sheet created successfully", preliminaryInspectionSheetDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<PreliminaryInspectionSheetDto>(500, $"Error creating preliminary inspection sheet: {ex.Message}");
            }
        }

        public async Task<OperationResponse<PreliminaryInspectionSheetDto>> GetPreliminaryInspectionSheetByIdAsync(int id)
        {
            try
            {
                var preliminaryInspectionSheet = await _context.PreliminaryInspectionSheets.FindAsync(id);
                if (preliminaryInspectionSheet == null)
                {
                    return new OperationResponse<PreliminaryInspectionSheetDto>(404, "Preliminary Inspection Sheet not found");
                }

                var dto = new PreliminaryInspectionSheetDto
                {
                    IdPreliminaryInspectionSheet = preliminaryInspectionSheet.IdPreliminaryInspectionSheet,
                    Comment = preliminaryInspectionSheet.Comment,
                    IdVehicleAssignment = preliminaryInspectionSheet.IdVehicleAssignment,
                    Status = preliminaryInspectionSheet.Status,
                    DateSheet = preliminaryInspectionSheet.DateSheet
                };

                return new OperationResponse<PreliminaryInspectionSheetDto>(200, "Preliminary Inspection Sheet retrieved successfully", dto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<PreliminaryInspectionSheetDto>(500, $"Error retrieving preliminary inspection sheet: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<PreliminaryInspectionSheetDto>>> GetAllPreliminaryInspectionSheetsAsync()
        {
            try
            {
                var sheets = await _context.PreliminaryInspectionSheets.ToListAsync();
                var dtoList = sheets.Select(s => new PreliminaryInspectionSheetDto
                {
                    IdPreliminaryInspectionSheet = s.IdPreliminaryInspectionSheet,
                    Comment = s.Comment,
                    IdVehicleAssignment = s.IdVehicleAssignment,
                    Status = s.Status,
                    DateSheet = s.DateSheet
                });

                return new OperationResponse<IEnumerable<PreliminaryInspectionSheetDto>>(200, "Preliminary Inspection Sheets retrieved successfully", dtoList);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<PreliminaryInspectionSheetDto>>(500, $"Error retrieving preliminary inspection sheets: {ex.Message}");
            }
        }

        public async Task<OperationResponse<PreliminaryInspectionSheetDto>> UpdatePreliminaryInspectionSheetAsync(PreliminaryInspectionSheetDto dto)
        {
            try
            {
                var sheet = await _context.PreliminaryInspectionSheets.FindAsync(dto.IdPreliminaryInspectionSheet);
                if (sheet == null)
                {
                    return new OperationResponse<PreliminaryInspectionSheetDto>(404, "Preliminary Inspection Sheet not found");
                }

                sheet.Comment = dto.Comment;
                sheet.IdVehicleAssignment = dto.IdVehicleAssignment;
                sheet.Status = dto.Status;
                sheet.DateSheet = dto.DateSheet;

                _context.Entry(sheet).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new OperationResponse<PreliminaryInspectionSheetDto>(200, "Preliminary Inspection Sheet updated successfully", dto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<PreliminaryInspectionSheetDto>(500, $"Error updating preliminary inspection sheet: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeletePreliminaryInspectionSheetAsync(int id)
        {
            try
            {
                var sheet = await _context.PreliminaryInspectionSheets.FindAsync(id);
                if (sheet == null)
                {
                    return new OperationResponse<bool>(404, "Preliminary Inspection Sheet not found");
                }

                _context.PreliminaryInspectionSheets.Remove(sheet);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Preliminary Inspection Sheet deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting preliminary inspection sheet: {ex.Message}");
            }
        }
    }
}
