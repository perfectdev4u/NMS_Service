using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NmsService
{
    public class ApiHESGateway
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static string Token = string.Empty;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.ApiHESGatewayList + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                appSettings.IsTokenRequired = true;
                string token = GenerateToken(appSettings);
                Token = token;


                Log.WriteToLog(appSettings, "ApiHESGateway ApiResponse.GetHESGatewayStatusResponse() Start Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                var apiResponse = ApiResponse.GetHESGatewayStatusResponse(appSettings, Token);
                Log.WriteToLog(appSettings, "ApiHESGateway ApiResponse.GetHESGatewayStatusResponse() End Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                Log.WriteToLog(appSettings, "ApiHESGateway Update() Start Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                //update into gateway and insert into gatewaystatushistory table
                Update(apiResponse, appSettings);
                Log.WriteToLog(appSettings, "ApiHESGateway Update() End Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));

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


        private static void Update(List<HESGatewayStatusResponse> result, AppSettings appSettings)
        {
            DBDataContext context = new DBDataContext();
            if (result != null & result.Any())
            {
                int cnt = 0;
                foreach (var item in result.Where(x => x.Status != null))
                {
                    try
                    {
                        GatewayData gatewayData = new GatewayData();
                        gatewayData.OnBattery = (item.Status == 0 || item.Status == 2);
                        gatewayData.GatewayId = item.GatewayId;

                        Log.WriteToLog(appSettings, "(" + cnt + ") Processed Gateway Id - " + gatewayData.GatewayId);

                        //insert/update gateway
                        int gatewayId = context.NMS_UpdateGateWayBatteryFlag(gatewayData.GatewayId, gatewayData.OnBattery);

                        //insert gateway status history
                        context.NMS_InsertGatewayStatusHistory(gatewayId, gatewayData.Status, gatewayData.OnBattery, true, DateTime.UtcNow);
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
            try
            {
                var tokenReponse = ApiResponse.GetHESTokenResponse(appSettings);
                return (tokenReponse != null && tokenReponse.Token != null) ? tokenReponse.Token : string.Empty;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, "Error : " + ex.ToString());
            }
            return "";
        }
    }
}
