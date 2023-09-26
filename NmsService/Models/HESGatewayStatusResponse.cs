using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NmsService.Models
{
    public class HESGatewayStatusResponse
    {
        public string GatewayId { get; set; }

        public int? Status { get; set; }

        public string UpdatedOn { get; set; }
    }

    public class HESGatewayStatusRequest
    {
        public string Token { get; set; }

    }
}
