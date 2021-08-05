using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkDomain.Dto
{
    public class GatewayDeviceDto
    {
        public int GatewayId { get; set; }
        public string SerialNumer { get; set; }
        public string Name { get; set; }
        public string IP4Address { get; set; }
        public List<DeviceDto> AllDevices { get; set; }
    }
}
