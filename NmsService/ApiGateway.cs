using NmsService.Models;
using System;
using System.Configuration;
using System.Globalization;
using System.Text;

namespace NmsService
{
    public class ApiGateway
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static string Token = string.Empty;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.ApiGatewaySearch + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                appSettings.IsTokenRequired = true;
                string token = GenerateToken(appSettings);
                Token = token;
                GatewayRequest gatewayRequest = new GatewayRequest();
                gatewayRequest.acctype = 0;
                gatewayRequest.email = appSettings.ApiUserName;
                gatewayRequest.password = appSettings.ApiPassword;
                gatewayRequest.token = token;
                Log.WriteToLog(appSettings, "ApiGateway ApiResponse.GetGatewayResponse() Start Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                var apiResponse = ApiResponse.GetGatewayResponse(appSettings, gatewayRequest);
                Log.WriteToLog(appSettings, "ApiGateway ApiResponse.GetGatewayResponse() End Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                Log.WriteToLog(appSettings, "ApiGateway Update() Start Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                //update into gateway and insert into gatewaystatushistory table
                Update(apiResponse, appSettings);
                Log.WriteToLog(appSettings, "ApiGateway Update() End Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));

                log.Append("," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + ",Success,");
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
                log.Append("," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + ",Fail," + ex.Message);
            }

            Log.WriteToLogStartEnd(log.ToString() + Environment.NewLine);

            LastRunTime = DateTime.Now;

        }

        private static void Update(GatewayResponse result, AppSettings appSettings)
        {
            DBDataContext context = new DBDataContext();
            if (result != null && result.Devices != null && result.Devices.Count > 0)
            {
                int cnt = 0;
                foreach (var item in result.Devices)
                {
                    try
                    {
                        GatewayData gatewayData = new GatewayData();
                        var latLong = GetLatLong(appSettings, item.Description.Trim());
                        gatewayData.GatewayName = item.GatewayName.Trim();
                        gatewayData.Lat = !string.IsNullOrEmpty(latLong) ? Convert.ToDecimal(latLong.Split('-')[0]) : 0;
                        gatewayData.Long = !string.IsNullOrEmpty(latLong) ? Convert.ToDecimal(latLong.Split('-')[1]) : 0;
                        gatewayData.Status = item.Status == 1 ? true : false;
                        gatewayData.Timestamp = item.Timestamp;
                        gatewayData.GatewayId = GenerateGatewayId(appSettings, item.GatewayName.Trim(), cnt);
                        Intf intf = GetMacAndIPAddress(item.sysinfo, appSettings);
                        gatewayData.MacAddress = (intf != null && !string.IsNullOrEmpty(intf.mac)) ? intf.mac : string.Empty;
                        gatewayData.IPv4Address = (intf != null && intf.ipv4 != null) ? String.Join(", ", intf.ipv4) : string.Empty;
                        gatewayData.IPv6Address = (intf != null && intf.ipv6 != null) ? String.Join(", ", intf.ipv6) : string.Empty;
                        gatewayData.InterfaceType = (intf != null && !string.IsNullOrEmpty(intf.name)) ? intf.name : string.Empty;
                        gatewayData.DeviceId = item.DeviceId.Trim();

                        Log.WriteToLog(appSettings, "(" + cnt + ") Processed Gateway Name - " + gatewayData.GatewayName);
                        //insert/update gateway
                        int gatewayId = context.NMS_UpdateGateway(gatewayData.GatewayName, gatewayData.FeederId, gatewayData.DTRId, gatewayData.SDOId, gatewayData.Lat, gatewayData.Long, gatewayData.Status, gatewayData.Timestamp, gatewayData.GatewayId, gatewayData.MacAddress, gatewayData.IPv4Address, gatewayData.IPv6Address, gatewayData.InterfaceType, item.DeviceId);
                        //insert gateway status history
                        context.NMS_InsertGatewayStatusHistory(gatewayId, gatewayData.Status, gatewayData.OnBattery, false, gatewayData.Timestamp);

                        cnt++;
                    }
                    catch (Exception ex)
                    {
                        Log.WriteToLog(appSettings, "Error at " + cnt + " index");
                        Log.WriteToLog(appSettings, "Error : " + ex.ToString());
                    }
                }
            }
        }

        public static string GenerateToken(AppSettings appSettings)
        {
            TokenRequest tokenRequest = new TokenRequest();
            try
            {
                tokenRequest.name = Constants.Name;
                tokenRequest.email = appSettings.ApiUserName;
                tokenRequest.password = appSettings.ApiPassword;
                var tokenReponse = ApiResponse.GetGatewayTokenResponse(appSettings, tokenRequest);
                return (tokenReponse != null && tokenReponse.Data != null && tokenReponse.Data.Token != null) ? tokenReponse.Data.Token : string.Empty;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, "Error : " + ex.ToString());
            }
            return "";
        }

        private static string GenerateGatewayId(AppSettings appSettings, string gatewayName, int index)
        {
            string gatewayId = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(gatewayName) && gatewayName.Length > 4)
                {
                    string GatewayIdPrefix = "";
                    bool HasTwoPrefix = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("HasTwoPrefix"));

                    string lastValue = gatewayName.Substring(gatewayName.Length - 4, 4);
                    if (!string.IsNullOrEmpty(lastValue))
                    {
                        if(HasTwoPrefix)
                        {
                            var lastValueInt = int.Parse(lastValue);
                            if (lastValueInt >= 4001 && lastValueInt < 4500)
                            {
                                GatewayIdPrefix = ConfigurationManager.AppSettings.Get("GatewayIdPrefix");
                            }
                            else if (lastValueInt >= 4500 && lastValueInt <= 4999)
                            {
                                GatewayIdPrefix = ConfigurationManager.AppSettings.Get("GatewayIdPrefix2");
                            }
                        }
                        else
                        {
                            GatewayIdPrefix = ConfigurationManager.AppSettings.Get("GatewayIdPrefix");
                        }

                        gatewayId += GatewayIdPrefix + lastValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, "GenerateGatewayId(" + index + ")  Gateway Name: " + gatewayName + Environment.NewLine + "Error : " + ex.ToString());
            }
            return gatewayId;
        }

        private static Intf GetMacAndIPAddress(Sysinfo sysinfo, AppSettings appSettings)
        {
            Intf intf = new Intf();
            try
            {
                if (sysinfo != null
                    && sysinfo.device != null
                    && sysinfo.device.net != null
                    && sysinfo.device.net.intf != null
                    && sysinfo.device.net.intf.Count > 0)
                {
                    foreach (var item in sysinfo.device.net.intf)
                    {
                        if (item.ipv4 != null && item.ipv4.Count > 0
                            && item.ipv6 != null && item.ipv6.Count > 0
                            && !string.IsNullOrEmpty(item.name)
                            && (item.name.Trim().ToLower().Equals("ppp0")
                                || item.name.Trim().ToLower().Equals("usb0")))
                        {
                            intf.ipv4 = item.ipv4;
                            intf.ipv6 = item.ipv6;
                            intf.mac = item.mac.Trim();
                            intf.name = item.name.Trim();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, "Error : " + ex.ToString());
            }
            return intf;
        }

        private static string GetLatLong(AppSettings appSettings, string desc)
        {
            string latLong = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(desc) && desc.IndexOf("-") != -1)
                {
                    string[] arrayItems = desc.Split('-');
                    if (!string.IsNullOrEmpty(arrayItems[1]) && arrayItems[1].Trim().IndexOf(",") != -1)
                    {
                        latLong = arrayItems[1].Trim().Split(',')[0].Trim() + "-" + arrayItems[1].Trim().Split(',')[1].Trim();
                    }
                    else if (!string.IsNullOrEmpty(arrayItems[1]) && arrayItems[1].Trim().IndexOf(" ") != -1)
                    {
                        latLong = arrayItems[1].Trim().Split(' ')[0].Trim() + "-" + arrayItems[1].Trim().Split(' ')[1].Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, "Error : " + ex.ToString());
            }
            return latLong;
        }
    }
}
