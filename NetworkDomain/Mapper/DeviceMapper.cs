using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkDomain.Mapper
{
    public class DeviceMapper
    {
        public int Id { get; set; }
        public int UID { get; set; }
        public string Vendor { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public int GatewayId { get; set; }
    }
}
