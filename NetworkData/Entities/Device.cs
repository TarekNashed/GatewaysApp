using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetworkData.Entities
{
    public class Device:EntityBase
    {
        public int UID { get; set; }
        public string Vendor { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        [ForeignKey("Gateway")]
        public int GatewayId { get; set; }
        public virtual Gateway Gateway { get; set; }
    }
}
