using NetworkData.Entities;
using NetworkData.Interfaces;
using NetworkDomain.Dto;
using NetworkDomain.IBusinessData;
using NetworkDomain.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkDomain.BusineessData
{
    public class DeviceBusinessData : IDeviceBusinessData
    {
        private readonly IUnitOfWorkData<Device> _unitOfWorkDevice;
        private readonly IUnitOfWorkData<Gateway> _unitOfWorkGateway;
        public DeviceBusinessData(IUnitOfWorkData<Device> unitOfWork,
            IUnitOfWorkData<Gateway> unitOfWorkGateway)
        {
            this._unitOfWorkDevice = unitOfWork;
            this._unitOfWorkGateway = unitOfWorkGateway;
        }
        public DeviceMapper AddNewDevice(DeviceMapper deviceMapper)
        {
            Device device = new Device();
            device.UID = deviceMapper.UID;
            device.Vendor = deviceMapper.Vendor;
            device.Status = deviceMapper.Status;
            device.CreatedDate = deviceMapper.CreatedDate;
            device.GatewayId = deviceMapper.GatewayId;
            _unitOfWorkDevice.repository.Add(device);
            _unitOfWorkDevice.Save();
            return deviceMapper;
        }

        public bool CheckIfExceedNumOnGateway(int getwayId)
        {
            int numOfDevice = _unitOfWorkDevice.repository.GetAll().Select(x => x.GatewayId == getwayId).Count();
            if (numOfDevice < 11)
                return true;
            return false;
        }

        public bool CheckValidDeviceId(int id)
        {
            var device = _unitOfWorkDevice.repository.GetById(id);
            if (device != null) return true;
            return false;
        }

        public bool CheckValidGatewayId(int Id)
        {
            if (_unitOfWorkGateway.repository.GetById(Id) != null) return true;
            return false;
        }

        public IEnumerable<DeviceMapper> GetAllDevices()
        {
            List<DeviceMapper> deviceDtos = new List<DeviceMapper>();
            var devices = _unitOfWorkDevice.repository.GetAll();
            if (devices != null)
            {
                foreach (var device in devices)
                {
                    DeviceMapper deviceDto = new DeviceMapper();
                    deviceDto.Id = device.Id;
                    deviceDto.UID = device.UID;
                    deviceDto.Vendor = device.Vendor;
                    deviceDto.Status = device.Status;
                    deviceDto.CreatedDate = device.CreatedDate;
                    deviceDtos.Add(deviceDto);
                }
            }
            return deviceDtos;
        }

        public DeviceMapper GetDeviceById(int id)
        {
            var device = _unitOfWorkDevice.repository.GetById(id);
            DeviceMapper deviceDto = new DeviceMapper();
            deviceDto.Id = device.Id;
            deviceDto.UID = device.UID;
            deviceDto.Vendor = device.Vendor;
            deviceDto.Status = device.Status;
            deviceDto.CreatedDate = device.CreatedDate;
            return deviceDto;
        }

        public string GetNameById(int id)
        {
            return _unitOfWorkDevice.repository.GetById(id).Vendor;
        }

        public void RemoveDevice(int id)
        {
            var device = _unitOfWorkDevice.repository.GetById(id);
            _unitOfWorkDevice.repository.Delete(device);
            _unitOfWorkDevice.Save();
        }

        public void UpdateDevice(int id, DeviceMapper deviceMapper)
        {
            Device device = new Device();
            device.Id = id;
            device.UID = deviceMapper.UID;
            device.Vendor = deviceMapper.Vendor;
            device.Status = deviceMapper.Status;
            device.CreatedDate = deviceMapper.CreatedDate;
            device.GatewayId = deviceMapper.GatewayId;
            device.Gateway = null;
            _unitOfWorkDevice.repository.Update(id, device);
            _unitOfWorkDevice.Save();
        }
    }
}
