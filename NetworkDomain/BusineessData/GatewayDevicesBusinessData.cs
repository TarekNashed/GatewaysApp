using NetworkData.Entities;
using NetworkData.Interfaces;
using NetworkDomain.Dto;
using NetworkDomain.IBusinessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkDomain.BusineessData
{
    public class GatewayDevicesBusinessData:IGatewayDevicesBusinessData
    {
        private readonly IUnitOfWorkData<Gateway> _unitOfWorkGatewayData;
        private readonly IUnitOfWorkData<Device> _unitOfWorkDeviceData;
        public GatewayDevicesBusinessData(IUnitOfWorkData<Gateway> unitOfWorkGatewayData,
            IUnitOfWorkData<Device> unitOfWorkDeviceData)
        {
            this._unitOfWorkGatewayData = unitOfWorkGatewayData;
            this._unitOfWorkDeviceData = unitOfWorkDeviceData;
        }
        public IEnumerable<GatewayDeviceDto> GetAllData()
        {

            var gatewaies = _unitOfWorkGatewayData.repository.GetAll();
            List<GatewayDeviceDto> ListdtoObj = new List<GatewayDeviceDto>();
            foreach (var gateway in gatewaies)
            {
                // get gateway data.
                GatewayDeviceDto GatewayDeviceDtoObj = new GatewayDeviceDto();
                GatewayDeviceDtoObj.GatewayId = gateway.Id;
                GatewayDeviceDtoObj.SerialNumer = gateway.SerialNumer;
                GatewayDeviceDtoObj.Name = gateway.Name;
                GatewayDeviceDtoObj.IP4Address = gateway.IP4Address;
                // get devices data.
                List<DeviceDto> deviceDtos = new List<DeviceDto>();
                var devices = _unitOfWorkDeviceData.repository.GetAll().Where(s => s.Gateway.Id == gateway.Id);
                foreach (var device in devices)
                {
                    DeviceDto deviceDto = new DeviceDto();
                    deviceDto.DeviceId = device.Id;
                    deviceDto.UID = device.UID;
                    deviceDto.Vendor = device.Vendor;
                    deviceDto.Status = device.Status;
                    deviceDto.CreatedDate = device.CreatedDate;
                    deviceDtos.Add(deviceDto);
                }
                GatewayDeviceDtoObj.AllDevices = deviceDtos;
                ListdtoObj.Add(GatewayDeviceDtoObj);
            }
            return ListdtoObj;
        }
        public GatewayDeviceDto GetAllDevicesDataByGatewayId(int Id)
        {
            var gateway = _unitOfWorkGatewayData.repository.GetById(Id);
            GatewayDeviceDto gatewayDeviceDtoObj = new GatewayDeviceDto();
            if (gateway == null) return gatewayDeviceDtoObj;
            // get gateway data.
            gatewayDeviceDtoObj.GatewayId = gateway.Id;
            gatewayDeviceDtoObj.SerialNumer = gateway.SerialNumer;
            gatewayDeviceDtoObj.Name = gateway.Name;
            gatewayDeviceDtoObj.IP4Address = gateway.IP4Address;
            // get devices data.
            List<DeviceDto> deviceDtos = new List<DeviceDto>();
            var devices = _unitOfWorkDeviceData.repository.GetAll().Where(s => s.GatewayId == gateway.Id);
            foreach (var device in devices)
            {
                DeviceDto deviceDto = new DeviceDto();
                deviceDto.DeviceId = device.Id;
                deviceDto.UID = device.UID;
                deviceDto.Vendor = device.Vendor;
                deviceDto.Status = device.Status;
                deviceDto.CreatedDate = device.CreatedDate;
                deviceDtos.Add(deviceDto);
            }
            gatewayDeviceDtoObj.AllDevices = deviceDtos;
            return gatewayDeviceDtoObj;
        }

    }
}
