using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Services
{
    public class ServiceDaoService : IServiceDao
    {
        private readonly LogiDriveDbContext _context;

        public ServiceDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Service>> CreateServiceAsync(Service service)
        {
            try
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                return new OperationResponse<Service>(200, "Service created successfully", service);
            }
            catch (Exception ex)
            {


                return new OperationResponse<Service>(500, $"Error creating service: {ex.Message}");

            }
        }

        public async Task<OperationResponse<Service>> GetServiceAsync(int idService)
        {
            try
            {
                var service = await _context.Services.FindAsync(idService);
                if (service == null)
                {
                    return new OperationResponse<Service>(404, "Service not found");
                }
                return new OperationResponse<Service>(200, "Service retrieved successfully", service);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Service>(500, $"Error retrieving service: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Service>>> GetAllServicesAsync()
        {
            try
            {
                var services = await _context.Services.ToListAsync();
                return new OperationResponse<IEnumerable<Service>>(200, "Services retrieved successfully", services);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Service>>(500, $"Error retrieving services: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Service>>> GetServicesByVehicleAsync(int idVehicle)
        {
            try
            {
                var services = await _context.Services.Where(s => s.IdVehicle == idVehicle).ToListAsync();
                return new OperationResponse<IEnumerable<Service>>(200, "Services retrieved successfully", services);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Service>>(500, $"Error retrieving services by vehicle: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Service>>> GetServicesByTypeAsync(int idTypeService)
        {
            try
            {
                var services = await _context.Services
                    .Where(s => s.IdTypeService == idTypeService)
                    .ToListAsync();
                return new OperationResponse<IEnumerable<Service>>(200, "Services retrieved successfully", services);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Service>>(500, $"Error retrieving services by type: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Service>> UpdateServiceAsync(Service service)
        {
            try
            {
                _context.Entry(service).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OperationResponse<Service>(200, "Service updated successfully", service);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Service>(500, $"Error updating service: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteServiceAsync(int idService)
        {
            try
            {
                var service = await _context.Services.FindAsync(idService);
                if (service == null)
                {
                    return new OperationResponse<bool>(404, "Service not found");
                }
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Service deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting service: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> ChangeServiceStatusAsync(int idService, bool status)
        {
            try
            {
                var service = await _context.Services.FindAsync(idService);
                if (service == null)
                {
                    return new OperationResponse<bool>(404, "Service not found");
                }
                service.Status = status;
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Service status changed successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error changing service status: {ex.Message}");
            }
        }
    }
}