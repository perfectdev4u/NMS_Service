using Newtonsoft.Json;
using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService
{
    internal class ApiEndPoint252
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.ApiEndPoint252 + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                var apiResponse = ApiResponse.GetApiResponse(appSettings);
                var columns = ApiResponse.ParseJson252(apiResponse);
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

        private static void UpdateNodeTimeSeries(List<ColumnName252> columns, AppSettings appSettings)
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
                    node.EP252TimeUTC = DateTimeOffset.Parse(item.Time).UtcDateTime;
                    node.MsgDelayLowLatency = Convert.ToInt32(item.TravelTimeMs);
                    node.HopCount = Convert.ToInt32(item.HopCount);
                    node.RSSI = Convert.ToInt32(item.NextHopRssiDBm);
                    node.Status = Constants.Status;

                    //update NodeTimeSeries table
                    
                    context.NMS_UpdateNodeTimeSeries252(node.NodeDBId
                        ,node.GatewayDBId
                        ,node.TimestampUTC
                        ,node.UpdateTimeUTC
                        ,node.EP252TimeUTC
                        ,node.MsgDelayLowLatency
                        ,node.RSSI
                        ,node.HopCount
                        ,node.Status
                        );

                    //get child node and update all the child node with parent node db id
                    var neighborhoods = Utility.GetNeighborhoods(item.Neighbors);
                    foreach (var neighborhood in neighborhoods)
                    {
                        int childParentNodeDBId = context.NMS_CheckNodeDBId(neighborhood.NextHopAddress);

                        if (childParentNodeDBId == 0)
                        {
                            Log.WriteToLog(appSettings, "Parent/Child Node - (" + neighborhood.NextHopAddress + ") is not found for this data item NodeId - " + item.NodeId);
                            continue;
                        }

                        switch (neighborhood.NeighbourType.Trim())
                        {
                            case "1":
                                //child
                                context.NMS_UpdateNodeTimeSeriesParentDBId(childParentNodeDBId, nodeDBId);
                                break;
                            case "3":
                                //parent
                                context.NMS_UpdateNodeTimeSeriesParentDBId(nodeDBId, childParentNodeDBId);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(appSettings, ex);
                }
            }
        }
    }
}
