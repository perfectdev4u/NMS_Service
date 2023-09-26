using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService.Models
{
    internal class GatewayRequest
    {
        public int acctype { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    }
}
