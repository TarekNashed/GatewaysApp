using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetworkData.Entities
{
    public class Gateway : EntityBase
    {
        public string SerialNumer { get; set; }
        public string Name { get; set; }
        public string IP4Address { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
