using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NmsService.Models
{
    public class Agent
    {
        public string config_version { get; set; }
        public string image_date { get; set; }
        public string image_name { get; set; }
        public int image_type { get; set; }
        public string image_version { get; set; }
    }

    public class Board
    {
        public string board_name { get; set; }
        public string board_vendor { get; set; }
        public string product_name { get; set; }
    }

    public class Device
    {
        public DateTime createdat { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("geolocation")]
        public Geolocation GeoLocation { get; set; }

        [JsonProperty("id")]
        public string DeviceId { get; set; }

        [JsonProperty("lastseen")]
        public DateTime Timestamp { get; set; }
        public string mac { get; set; }

        [JsonProperty("name")]
        public string GatewayName { get; set; }
        public string profileid { get; set; }
        public string profilename { get; set; }
       
        [JsonProperty("status")]
        public int Status { get; set; }
        public Sysinfo sysinfo { get; set; }
    }

    public class Device2
    {
        public Agent agent { get; set; }
        public Board board { get; set; }
        public string model { get; set; }
        public Net net { get; set; }
        public List<string> wifi { get; set; }
    }

    public class Geolocation
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
    }

    public class Intf
    {
        public List<string> ipv4 { get; set; }
        public List<string> ipv6 { get; set; }
        public string mac { get; set; }
        public string name { get; set; }
    }

    public class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("lng")]
        public double Long { get; set; }
    }

    public class MenderAttribute
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Net
    {
        public List<Intf> intf { get; set; }
    }

    public class GatewayResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("devices")]
        public List<Device> Devices { get; set; }
    }

    public class Sysinfo
    {
        public object conf { get; set; }
        public Device2 device { get; set; }
        public List<MenderAttribute> mender_attributes { get; set; }
        public List<Tags2> tags2 { get; set; }
        public Tamper tamper { get; set; }
    }

    public class Tags2
    {
        public string data { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }

    public class Tamper
    {
        public string id { get; set; }
        public string state { get; set; }
    }

}
