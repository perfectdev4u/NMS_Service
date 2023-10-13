using Newtonsoft.Json;
using NmsService.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService
{
    internal class ApiResponse
    {
        public static string GetApiResponse(AppSettings appSettings)
        {
            var response = string.Empty;
            Task<string> task = ApiRequest.CreateWebRequest(appSettings);
            var deserializeObject = ((task != null && task.Result != null) ? JsonConvert.DeserializeObject<Root>(task.Result) : null);
            if (deserializeObject != null
                && deserializeObject.results != null && deserializeObject.results.Count > 0
                && deserializeObject.results[0].series != null
                && deserializeObject.results[0].series.Count > 0)
            {
                var objSeries = deserializeObject.results[0].series;
                var serializedObject = JsonConvert.SerializeObject(objSeries);
                response = serializedObject.Remove(0, 1);
                response = response.Remove(response.Length - 1, 1);
            }
            else
            {
                response = "{}";
            }
            return response;
        }

        public static GatewayResponse GetGatewayResponse(AppSettings appSettings, GatewayRequest requestObj)
        {
            string postData = JsonConvert.SerializeObject(requestObj);
            var response = ApiRequest.PostRequest<GatewayResponse>(appSettings, postData).Result;
            return response;
        }

        public static List<HESGatewayStatusResponse> GetHESGatewayStatusResponse(AppSettings appSettings, string token)
        {
            var response = ApiRequest.GetRequest<List<HESGatewayStatusResponse>>(appSettings, token).Result;
            return response;
        }

        public static GatewayResponse GetHESGatewayStatusResponse(AppSettings appSettings, GatewayRequest requestObj)
        {
            string postData = JsonConvert.SerializeObject(requestObj);
            var response = ApiRequest.PostRequest<GatewayResponse>(appSettings, postData).Result;
            return response;
        }


        public static TokenResponse GetGatewayTokenResponse(AppSettings appSettings, TokenRequest requestObj)
        {
            string postData = JsonConvert.SerializeObject(requestObj);
            var response = ApiRequest.PostRequest<TokenResponse>(appSettings, postData).Result;
            return response;
        }

        public static HESTokenResponse GetHESTokenResponse(AppSettings appSettings)
        {
            var response = ApiRequest.PostRequest<HESTokenResponse>(appSettings, null, true).Result;
            return response;
        }

        public static List<MeterMasterDataResponse> GetMeterMasterDataResponse(AppSettings appSettings, MeterMasterDataRequest requestObj)
        {
            string postData = JsonConvert.SerializeObject(requestObj);
            var response = ApiRequest.PostRequest<List<MeterMasterDataResponse>>(appSettings, postData).Result;
            return response;
        }


        public static List<ColumnName251> ParseJson251(string jsonResponse)
        {
            var settings = new JsonSerializerSettings { Converters = new[] { new ColumnarDataToListConverter<ColumnName251>() } };
            var columns = JsonConvert.DeserializeObject<List<ColumnName251>>(jsonResponse, settings);
            return columns;
        }
        public static List<ColumnName252> ParseJson252(string jsonResponse)
        {
            var settings = new JsonSerializerSettings { Converters = new[] { new ColumnarDataToListConverter<ColumnName252>() } };
            var columns = JsonConvert.DeserializeObject<List<ColumnName252>>(jsonResponse, settings);
            return columns;
        }
        public static List<ColumnName253> ParseJson253(string jsonResponse)
        {
            var settings = new JsonSerializerSettings { Converters = new[] { new ColumnarDataToListConverter<ColumnName253>() } };
            var columns = JsonConvert.DeserializeObject<List<ColumnName253>>(jsonResponse, settings);
            return columns;
        }
        public static List<ColumnName254> ParseJson254(string jsonResponse)
        {
            var settings = new JsonSerializerSettings { Converters = new[] { new ColumnarDataToListConverter<ColumnName254>() } };
            var columns = JsonConvert.DeserializeObject<List<ColumnName254>>(jsonResponse, settings);
            return columns;
        }

        public static List<AnalyticsNodeState> ParseJsonAnalytics(string jsonResponse)
        {
            var settings = new JsonSerializerSettings { Converters = new[] { new ColumnarDataToListConverter<AnalyticsNodeState>() } };
            var columns = JsonConvert.DeserializeObject<List<AnalyticsNodeState>>(jsonResponse, settings);
            return columns;
        }

        public static RootModel GetMeterMasterDataResponseWFM(AppSettings appSettings)
        {
            var response = ApiRequest.PostRequestWFM<RootModel>(appSettings).Result;
            return response;
        }

        public static GatewayDetailsRoot GetGatewayDetailsBasedOnDate(AppSettings appSettings)
        {
            var response = ApiRequest.PostRequestWFM<GatewayDetailsRoot>(appSettings).Result;
            return response;
        }
    }
}
