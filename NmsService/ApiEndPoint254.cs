using Newtonsoft.Json;
using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService
{
    internal class ApiEndPoint254
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.ApiEndPoint254 + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                var apiResponse = ApiResponse.GetApiResponse(appSettings);
                var columns = ApiResponse.ParseJson254(apiResponse);
                if (columns != null && columns.Count > 0)
                {
                    UpdateNodeTimeSeries(columns, appSettings);                  
                }
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

        private static void UpdateNodeTimeSeries(List<ColumnName254> columns, AppSettings appSettings)
        {
            DBDataContext context = new DBDataContext();
            foreach (var item in columns)
            {
                try
                {
                    //get getway DB id
                    long? gatewayDBId = context.NMS_GetGatewayDBId(item.GatewayId);
                    gatewayDBId = gatewayDBId == 0 ? null : gatewayDBId;

                    //get NodeDBId based on nodeid
                    int? nodeDBId = context.NMS_CheckNodeDBId(item.NodeId);
                    if (nodeDBId == 0)
                    {
                        Log.WriteToLog(appSettings, "NodeDBId is not found for this data item Node Id - " + item.NodeId);
                        continue;
                    }

                    NodeTimeSeries node = new NodeTimeSeries();

                    node.NodeDBId = nodeDBId;
                    node.GatewayDBId = gatewayDBId;
                    node.TimestampUTC = DateTimeOffset.Parse(item.Time).UtcDateTime;
                    node.UpdateTimeUTC = DateTime.UtcNow;
                    node.EP254TimeUTC = DateTimeOffset.Parse(item.Time).UtcDateTime;
                    node.Mode = item.CbMac.ToLower() == "true" ? Constants.LowLatency : Constants.LowEnergy;
                    node.FirmwareVersion = item.SWVersionMajor + "." + item.SWVersionMinor + "." + item.SWVersionMaintenance + "." + item.SWVersionDevel;
                    node.MsgDelayLowLatency = Convert.ToInt32(item.TravelTimeMs);
                    node.ScratchpadSequence = Convert.ToInt32(item.ScratchPadSequence);
                    node.HopCount = Convert.ToInt32(item.HopCount);
                    node.Status = Constants.Status;

                    //update NodeTimeSeries table
                    context.NMS_UpdateNodeTimeSeries254(node.NodeDBId
                        ,node.GatewayDBId
                        ,node.TimestampUTC
                        ,node.UpdateTimeUTC
                        ,node.EP254TimeUTC
                        ,node.Mode
                        ,node.FirmwareVersion
                        ,node.ScratchpadSequence
                        ,node.HopCount
                        ,node.MsgDelayLowLatency
                        ,node.Status
                        );
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(appSettings, ex);
                }
            }
        }
    }
}
