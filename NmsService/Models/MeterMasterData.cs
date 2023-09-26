using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService.Models
{
    public class MeterMasterDataResponse
    {
        [JsonProperty("Subdivision Name")]
        public string SubdivisionName { get; set; }

        [JsonProperty("Decommissioning Reason")]
        public string DecommissioningReason { get; set; }
        public string Latitude { get; set; }

        [JsonProperty("Installation Date")]
        public string InstallationDate { get; set; }

        [JsonProperty("Manufacturer Name")]
        public string ManufacturerName { get; set; }
        public string Longitude { get; set; }

        [JsonProperty("Prepaid Flag")]
        public string PrepaidFlag { get; set; }

        [JsonProperty("Decommissioning Date")]
        public string DecommissioningDate { get; set; }

        [JsonProperty("Meter Type")]
        public string MeterType { get; set; }

        [JsonProperty("New Meter Number")]
        public string NewMeterNumber { get; set; }
        public string HES { get; set; }

        [JsonProperty("Feeder Code")]
        public string FeederCode { get; set; }

        [JsonProperty("DT Code")]
        public string DTCode { get; set; }
    }

    public class MeterMasterDataRequest
    {
        [JsonProperty("apikey")]
        public string ApiKey { get; set; }

        [JsonProperty("HES")]
        public string Hes { get; set; }       

        [JsonProperty("paramFromDate")]
        public string ParamFromDate { get; set; }

        [JsonProperty("paramToDate")]
        public string ParamToDate { get; set; }
    }

    public class MeterMasterDataResponseFromExcel
    {
        [JsonProperty("MeterNumber")]
        public string MeterNumber { get; set; }

        [JsonProperty("Longitude")]
        public string Longitude { get; set; }

        [JsonProperty("Latitude")]
        public string Latitude { get; set; }

        [JsonProperty("SDO")]
        public string SDO { get; set; }

        [JsonProperty("Feeder")]
        public string Feeder { get; set; }

        [JsonProperty("DTR")]
        public string DTR { get; set; }

        [JsonProperty("MeterInstallationDate")]
        public DateTime MeterInstallationDate { get; set; }
    }

}
