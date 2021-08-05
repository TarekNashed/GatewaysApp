using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetworkData.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
