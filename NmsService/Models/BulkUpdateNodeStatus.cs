using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService.Models
{
    public class BulkUpdateNodeStatus
    {
        public long? NodeDBId { get; set; }
        public string NodeId { get; set; }
        public DateTime TimestampUTC { get; set; }
        public bool? Status { get; set; }       
    }
}
