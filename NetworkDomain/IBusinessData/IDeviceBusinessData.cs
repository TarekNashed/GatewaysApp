using NetworkDomain.Dto;
using NetworkDomain.Mapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDomain.IBusinessData
{
   public interface IDeviceBusinessData
    {
        IEnumerable<DeviceMapper> GetAllDevices();
        DeviceMapper GetDeviceById(int id);
        DeviceMapper AddNewDevice(DeviceMapper deviceMapper);
        void UpdateDevice(int id, DeviceMapper deviceMapper);
        void RemoveDevice(int id);
        bool CheckValidGatewayId(int Id);
        bool CheckIfExceedNumOnGateway(int getwayId);
        bool CheckValidDeviceId(int id);
        string GetNameById(int id);
    }
}
