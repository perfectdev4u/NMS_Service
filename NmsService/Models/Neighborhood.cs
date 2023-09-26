using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService.Models
{
    internal class Neighborhood
    {
        public string NextHopAddress { get; set; }
        public int Cost { get; set; }
        public string Quality { get; set; }
        public string OperatingFreq{ get; set; }
        public string RadioPowerDB { get; set; }
        public string NeighbourType { get; set; }
        public string RssiDbm { get; set; }
    }
}
