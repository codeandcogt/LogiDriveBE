﻿using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class VehicleAssignmentBaoService : IVehicleAssignmentBao
    {
        private readonly IVehicleAssignmentDao _vehicleAssignmentDao;

        public VehicleAssignmentBaoService(IVehicleAssignmentDao vehicleAssignmentDao)
        {
            _vehicleAssignmentDao = vehicleAssignmentDao;
        }

        public async Task<OperationResponse<VehicleAssignmentDto>> CreateVehicleAssignmentAsync(VehicleAssignmentDto vehicleAssignmentDto)
        {
            return await _vehicleAssignmentDao.CreateVehicleAssignmentAsync(vehicleAssignmentDto);
        }
        public async Task<OperationResponse<IEnumerable<VehicleAssignmentDto>>> GetVehicleAssignmentsByUserIdAsync(int userId)
        {
            return await _vehicleAssignmentDao.GetVehicleAssignmentsByUserIdAsync(userId);
        }

        public async Task<OperationResponse<VehicleAssigmentWithBrandDto>> GetVehicleAssignmentByIdAsync(int id)
        {
            return await _vehicleAssignmentDao.GetVehicleAssignmentByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<VehicleAssignmentDto>>> GetAllVehicleAssignmentsAsync()
        {
            return await _vehicleAssignmentDao.GetAllVehicleAssignmentsAsync();
        }

        public async Task<OperationResponse<VehicleAssignmentDto>> UpdateVehicleAssignmentAsync(VehicleAssignmentDto vehicleAssignmentDto)
        {
            return await _vehicleAssignmentDao.UpdateVehicleAssignmentAsync(vehicleAssignmentDto);
        }

        public async Task<OperationResponse<bool>> DeleteVehicleAssignmentAsync(int id)
        {
            return await _vehicleAssignmentDao.DeleteVehicleAssignmentAsync(id);
        }

        public async Task<OperationResponse<bool>> DeleteVehicleAssignmentStatusAsync(int id)
        {
            return await _vehicleAssignmentDao.DeleteVehicleAssigmentStatusAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<VehicleAssignmentView>>> GetVehicleAssignmentsByDateWithStatusUpdateAsync(DateTime specificDate)
        {
            return await _vehicleAssignmentDao.GetVehicleAssignmentsByDateWithStatusUpdateAsync(specificDate);
        }

        public async Task<OperationResponse<IEnumerable<VehicleAssignmentWithDetailsDto>>> GetVehicleAssignmentsByUserIdWithDetailsAsync(int userId, int hoursThreshold)
        {
            return await _vehicleAssignmentDao.GetVehicleAssignmentsByUserIdWithDetailsAsync(userId, hoursThreshold);
        }

        public async Task<OperationResponse<VehicleAssignmentWithDetailsDto>> UpdateVehicleAssignmentStatusTripAsync(int id, bool statusTrip)
        {
            return await _vehicleAssignmentDao.UpdateVehicleAssignmentStatusTripAsync(id, statusTrip);
        }
        // VehicleAssignmentBaoService.cs

        public async Task<OperationResponse<IEnumerable<VehiclePartDto>>> GetPartsByVehicleAssignmentIdAsync(int vehicleAssignmentId)
        {
            return await _vehicleAssignmentDao.GetPartsByVehicleAssignmentIdAsync(vehicleAssignmentId);
        }


    }
}
