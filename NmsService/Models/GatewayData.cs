using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService.Models
{
    internal class GatewayData
    {       
        public string GatewayName { get; set; }
        public string GatewayId { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public bool Status { get; set; }
        public bool OnBattery { get; set; }
        public DateTime Timestamp { get; set; }
        public int? FeederId { get; set; }
        public int? DTRId { get; set; }
        public int? SDOId { get; set; }
        public string MacAddress { get; set; }
        public string IPv4Address { get; set; }
        public string IPv6Address { get; set; }
        public string InterfaceType { get; set; }
        public string DeviceId { get; set; }
    }
}
