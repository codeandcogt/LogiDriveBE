﻿using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class MaintenancePartDaoService : IMaintenancePartDao
    {
        private readonly LogiDriveDbContext _context;

        public MaintenancePartDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<MaintenancePart>> CreateMaintenancePartAsync(MaintenancePart maintenancePart)
        {
            try
            {
                _context.MaintenanceParts.Add(maintenancePart);
                await _context.SaveChangesAsync();
                return new OperationResponse<MaintenancePart>(200, "Maintenance part created successfully", maintenancePart);
            }
            catch (Exception ex)
            {
                return new OperationResponse<MaintenancePart>(500, $"Error creating maintenance part: {ex.Message}");
            }
        }

        public async Task<OperationResponse<MaintenancePart>> GetMaintenancePartAsync(int id)
        {
            try
            {
                var maintenancePart = await _context.MaintenanceParts.FindAsync(id);
                if (maintenancePart == null)
                {
                    return new OperationResponse<MaintenancePart>(404, "Maintenance part not found");
                }
                return new OperationResponse<MaintenancePart>(200, "Maintenance part retrieved successfully", maintenancePart);
            }
            catch (Exception ex)
            {
                return new OperationResponse<MaintenancePart>(500, $"Error retrieving maintenance part: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<MaintenancePart>>> GetAllMaintenancePartAsync()
        {
            try
            {
                var maintenanceParts = await _context.MaintenanceParts.ToListAsync();
                return new OperationResponse<IEnumerable<MaintenancePart>>(200, "Maintenance parts retrieved successfully", maintenanceParts);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<MaintenancePart>>(500, $"Error retrieving maintenance parts: {ex.Message}");
            }
        }

        public async Task<OperationResponse<MaintenancePart>> UpdateMaintenancePartAsync(MaintenancePart maintenancePart)
        {
            try
            {
                _context.Entry(maintenancePart).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OperationResponse<MaintenancePart>(200, "Maintenance part updated successfully", maintenancePart);
            }
            catch (Exception ex)
            {
                return new OperationResponse<MaintenancePart>(500, $"Error updating maintenance part: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteMaintenancePartAsync(int id)
        {
            try
            {
                var maintenancePart = await _context.MaintenanceParts.FindAsync(id);
                if (maintenancePart == null)
                {
                    return new OperationResponse<bool>(404, "Maintenance part not found");
                }
                _context.MaintenanceParts.Remove(maintenancePart);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Maintenance part deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting maintenance part: {ex.Message}");
            }
        }
    }
}