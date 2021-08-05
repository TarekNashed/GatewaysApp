using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkDomain.Dto
{
   public class GatewayDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string IP4Address { get; set; }
    }
}
