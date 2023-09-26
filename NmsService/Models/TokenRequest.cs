using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService.Models
{
    public class TokenRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
    public class Data
    {
        [JsonProperty("userid")]
        public string UserId { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("firstlogin")]
        public int FirstLogin { get; set; }
        [JsonProperty("acctype")]
        public int AccType { get; set; }
    }

    public class TokenResponse
    {
        [JsonProperty("respcode")]
        public int Code { get; set; }

        [JsonProperty("respmsg")]
        public string Response { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }


    public class HESTokenRequest
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }

    public class HESTokenResponse
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }


}
