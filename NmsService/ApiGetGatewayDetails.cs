using DocumentFormat.OpenXml.Wordprocessing;
using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService
{
    internal class ApiGetGatewayDetails
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.ApiMeterMasterData + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                var apiResponse = ApiResponse.GetGatewayDetailsBasedOnDate(appSettings);
                if (apiResponse.data != null && apiResponse.data.Count > 0)
                {
                    Log.WriteToLog(appSettings, "ApiGetGatewayDetails.Create() started with Count (" + apiResponse.data.Count + ")");
                    //create master data
                    Update(apiResponse.data, appSettings);
                    Log.WriteToLog(appSettings, "ApiGetGatewayDetails.Create() ended");
                }
                else
                {
                    Log.WriteToLog(appSettings, "ApiGetGatewayDetails.Save() Data not found for Date: " + appSettings.ServiceRunTime);
                }
                Log.WriteToLog(appSettings, "ApiGetGatewayDetails.Create() ended ParamToDate : " + appSettings.ServiceRunTime);

                //bind nodes into dictionary
                Helpers.BindNodesHelper.BindAllNodes();
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


        private static void Update(List<GatewayDetails> results, AppSettings appSettings)
        {
            DBDataContext context = new DBDataContext();
            foreach (var item in results)
            {
                int SDOId = 0;
                int gatewayId = 0;

                try
                {
                    SDOId = context.NMS_CheckAndCreateSDOID(item.SDOname?.Trim());
                    var lat = !string.IsNullOrEmpty(item?.Latitude) ? Convert.ToDecimal(item?.Latitude) : 0;
                    var longitude = !string.IsNullOrEmpty(item?.Longitude) ? Convert.ToDecimal(item?.Longitude) : 0;

                    gatewayId = context.NMS_UpdateGatewayDetails(item?.GatewaySerialnumber, lat, longitude, Utility.DateTimeParse(item?.Installationdate), SDOId);
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(appSettings, "ApiGetGatewayDetails.Create()" + string.Format("sdoId:{0}, gatewayId:{1}", SDOId, gatewayId));
                    Log.WriteToLog(appSettings, ex);
                }
            }
        }
    }
}
