using NmsService.Models;
using Newtonsoft.Json;
using NmsService;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Text;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using Newtonsoft.Json.Linq;
using System.Linq;
using static NmsService.Program;
using System.Timers;
using System.ServiceProcess;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Net.Security;
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace NmsService
{
    internal class ApiRequest
    {
        public ApiRequest()
        {
            //System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, SslPolicyErrors) => true;
        }

        public static async Task<string> CreateWebRequest(AppSettings appSettings)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string url = GetUrl(appSettings);
                var options = new RestClientOptions(appSettings.ApiEndPoint)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(url, Method.Post);
                var body = @"";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                RestResponse response = await client.ExecuteAsync(request);
                return response != null && response.Content != null ? response.Content : null;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
                return null;
            }
        }

        public static async Task<T> PostRequest<T>(AppSettings appSettings, string postData = null, bool isParms = false) where T : class
        {
            RestResponse response = null;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var options = new RestClientOptions(appSettings.ApiEndPoint)
                {
                    MaxTimeout = -1,
                };

                string url = GetUrl(appSettings);
                var client = new RestClient(options);
                var request = new RestRequest(url, Method.Post);

                if (isParms)
                {
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("grant_type", "password");
                    request.AddParameter("username", appSettings.ApiUserName);
                    request.AddParameter("password", appSettings.ApiPassword);
                    request.AddParameter("client_id", appSettings.ClientId);
                    request.AddParameter("client_secret", appSettings.ClientSecret);
                }
                else
                {
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Cookie", "aicon-jwt=" + ApiGateway.Token);
                    var body = postData;
                    request.AddStringBody(body, DataFormat.Json);
                }

                response = await client.ExecuteAsync(request);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
                return null;
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
                return null;
            }
        }

        public static async Task<T> GetRequest<T>(AppSettings appSettings, string postData = null) where T : class
        {
            RestResponse response = null;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var options = new RestClientOptions(appSettings.ApiEndPoint)
                {
                    MaxTimeout = -1,
                };

                string url = GetUrl(appSettings);
                var client = new RestClient(options);
                var request = new RestRequest(url, Method.Get);

                request.AddHeader("Accept", "application/json");
                request.AddHeader("Authorization", "bearer " + postData);
                response = await client.ExecuteAsync(request);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
                return null;
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
                return null;
            }
        }

        private static string GetUrl(AppSettings appSettings)
        {
            string url = string.Empty;

            switch (appSettings.ServiceName.ToLower())
            {
                case Constants.ApiEndPoint251:
                    url = "query" + "?u=" + appSettings.ApiUserName + "&p=" + appSettings.ApiPassword + "&pretty=" + appSettings.Pretty + "&db=" + appSettings.DbName + "&q=" + appSettings.SqlQuery;
                    break;

                case Constants.ApiEndPoint252:
                    url = "query" + "?u=" + appSettings.ApiUserName + "&p=" + appSettings.ApiPassword + "&pretty=" + appSettings.Pretty + "&db=" + appSettings.DbName + "&q=" + appSettings.SqlQuery;
                    break;

                case Constants.ApiEndPoint253:
                    url = "query" + "?u=" + appSettings.ApiUserName + "&p=" + appSettings.ApiPassword + "&pretty=" + appSettings.Pretty + "&db=" + appSettings.DbName + "&q=" + appSettings.SqlQuery;
                    break;

                case Constants.ApiEndPoint254:
                    url = "query" + "?u=" + appSettings.ApiUserName + "&p=" + appSettings.ApiPassword + "&pretty=" + appSettings.Pretty + "&db=" + appSettings.DbName + "&q=" + appSettings.SqlQuery;
                    break;

                case Constants.ApiAnalyticsNodeState:
                    url = "query" + "?u=" + appSettings.ApiUserName + "&p=" + appSettings.ApiPassword + "&pretty=" + appSettings.Pretty + "&db=" + appSettings.DbName + "&q=" + appSettings.SqlQuery;
                    break;
                case Constants.MarkNodeOffline:
                    url = string.Empty;
                    break;

                case Constants.ApiMeterMasterData:
                    url = appSettings.ApiMethod;
                    break;

                case Constants.ApiGatewaySearch:
                    if (appSettings.IsTokenRequired)
                    {
                        url = appSettings.ApiTokenMethod;
                        appSettings.IsTokenRequired = false;
                    }
                    else
                    {
                        url = appSettings.ApiMethod + "?limit=" + appSettings.Limit + "&offset=" + appSettings.Offset + "&profileType=" + appSettings.ProfileType;
                    }
                    break;
                case Constants.ApiHESGatewayList:
                    if (appSettings.IsTokenRequired)
                    {
                        url = appSettings.ApiTokenMethod;
                        appSettings.IsTokenRequired = false;
                    }
                    else
                    {
                        url = appSettings.ApiMethod;
                        break;
                    }
                    break;
            }
            return url;

        }





















        public static T CreateWebRequest_NotInUSe<T>(AppSettings appSettings, string method, string postData = null) where T : class
        {
            string responseJson = string.Empty;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string url = appSettings.ApiEndPoint + "/" + GetUrl(appSettings);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                request.Method = method.ToUpper();
                request.ContentType = "application/json";
                if (!string.IsNullOrEmpty(ApiGateway.Token))
                {
                    request.Headers.Add("Cookie", "aicon-jwt=" + ApiGateway.Token);
                }

                if ((method == "PUT" || method == "POST") && postData != null)
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(postData);
                        streamWriter.Flush();
                    }
                }
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseJson = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(responseJson);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
                return null;
            }
        }

    }
}
