using Newtonsoft.Json;
using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService
{
    internal class ApiEndPoint251
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.ApiEndPoint251 + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                var apiResponse = ApiResponse.GetApiResponse(appSettings);
                var columns = ApiResponse.ParseJson251(apiResponse);
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

        private static void UpdateNodeTimeSeries(List<ColumnName251> columns, AppSettings appSettings)
        {
            DBDataContext context = new DBDataContext();
            foreach (var item in columns)
            {
                try
                {
                    //get gateway DB id
                    long? gatewayDBId = context.NMS_GetGatewayDBId(item.GatewayId);
                    gatewayDBId = gatewayDBId == 0 ? null : gatewayDBId;

                    //get NodeDBId based on nodeid
                    long? nodeDBId = context.NMS_CheckNodeDBId(item.NodeId);
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
                    node.EP251TimeUTC = DateTimeOffset.Parse(item.Time).UtcDateTime;
                    node.MsgDelayLowLatency = Convert.ToInt32(item.TravelTimeMs);
                    node.HopCount = Convert.ToInt32(item.HopCount);
                    node.Status = Constants.Status;
                    node.ChannelReliability = Convert.ToDecimal(Math.Round(Convert.ToDouble(item.ChannelReliablity), 3));

                    //update NodeTimeSeries table
                    context.NMS_UpdateNodeTimeSeries251(node.NodeDBId
                            ,node.GatewayDBId
                            ,node.TimestampUTC
                            ,node.UpdateTimeUTC
                            ,node.EP251TimeUTC
                            ,node.MsgDelayLowLatency
                            ,node.HopCount                        
                            ,node.ChannelReliability
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
