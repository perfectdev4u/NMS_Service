using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService.Models
{
    internal class Node
    {
        public long Id { get; set; }
        public long? MeterId { get; set; }
        public string NodeId { get; set; }
        public long? GatewayDBId { get; set; }
        public string AddressHEX { get; set; }
        public bool? IsSink { get; set; }
    }
}
