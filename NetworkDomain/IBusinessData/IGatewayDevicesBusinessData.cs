using NetworkDomain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkDomain.IBusinessData
{
    public interface IGatewayDevicesBusinessData
    {
        IEnumerable<GatewayDeviceDto> GetAllData();
        GatewayDeviceDto GetAllDevicesDataByGatewayId(int Id);
    }
}
