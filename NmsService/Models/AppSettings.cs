using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService.Models
{
    public class AppSettings
    {
        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("logsFolderPath")]
        public string LogsFolderPath { get; set; }

        [JsonProperty("apiEndPoint")]
        public string ApiEndPoint { get; set; }

        [JsonProperty("apiUserName")]
        public string ApiUserName { get; set; }

        [JsonProperty("apiPassword")]
        public string ApiPassword { get; set; }

        [JsonProperty("pretty")]
        public string Pretty { get; set; }

        [JsonProperty("dbName")]
        public string DbName { get; set; }
        [JsonProperty("sqlQuery")]
        public string SqlQuery { get; set; }

        [JsonProperty("timeIntervalInMin")]
        public int TimeIntervalInMin { get; set; }

        [JsonProperty("offlineDurationInMin")]
        public int OfflineDurationInMin { get; set; }

        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }

        [JsonProperty("serviceRunTime")]
        public string ServiceRunTime { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("numOfPrevDays")]
        public int NumOfPrevDays { get; set; }

        [JsonProperty("apiTokenEndPoint")]
        public string ApiTokenEndPoint { get; set; }

        [JsonProperty("limit")]
        public string Limit { get; set; }

        [JsonProperty("offset")]
        public string Offset { get; set; }

        [JsonProperty("profileType")]
        public string ProfileType { get; set; }

        [JsonProperty("isTokenRequired")]
        public bool IsTokenRequired { get; set; }

        [JsonProperty("apiMethod")]
        public string ApiMethod { get; set; }

        [JsonProperty("apiTokenMethod")]
        public string ApiTokenMethod { get; set; }

        [JsonProperty("chunkSize")]
        public int ChunkSize { get; set; }

        [JsonProperty("serviceStartEndLogPath")]
        public string ServiceStartEndLogPath { get; set; }


        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

    }
}
