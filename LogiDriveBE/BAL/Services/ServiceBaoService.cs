using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Services
{
    public class ServiceBaoService : IServiceBao
    {
        private readonly IServiceDao _serviceDao;

        public ServiceBaoService(IServiceDao serviceDao)
        {
            _serviceDao = serviceDao;
        }

        public async Task<OperationResponse<Service>> CreateServiceAsync(Service service)
        {
            try
            {
                // Add any business logic here
                return await _serviceDao.CreateServiceAsync(service);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Service>(500, $"Error in BAO while creating service: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Service>> GetServiceAsync(int idService)
        {

            try
            {
                return await _serviceDao.GetServiceAsync(idService);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Service>(500, $"Error in BAO while retrieving service: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Service>>> GetAllServicesAsync()
        {
            try
            {
                return await _serviceDao.GetAllServicesAsync();
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Service>>(500, $"Error in BAO while retrieving all services: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Service>>> GetServicesByVehicleAsync(int idVehicle)
        {
            try
            {
                return await _serviceDao.GetServicesByVehicleAsync(idVehicle);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Service>>(500, $"Error in BAO while retrieving services by vehicle: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Service>>> GetServicesByTypeAsync(int idTypeService)
        {
            try
            {
                return await _serviceDao.GetServicesByTypeAsync(idTypeService);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Service>>(500, $"Error in BAO while retrieving services by type: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Service>> UpdateServiceAsync(Service service)
        {
            try
            {
                return await _serviceDao.UpdateServiceAsync(service);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Service>(500, $"Error in BAO while updating service: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteServiceAsync(int idService)
        {
            try
            {
                return await _serviceDao.DeleteServiceAsync(idService);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error in BAO while deleting service: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> ChangeServiceStatusAsync(int idService, bool status)
        {
            try
            {
                return await _serviceDao.ChangeServiceStatusAsync(idService, status);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error in BAO while changing service status: {ex.Message}");
            }
        }
    }
}