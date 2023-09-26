using Newtonsoft.Json;
using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService
{
    internal class ApiEndPoint253
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.ApiEndPoint253 + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                var apiResponse = ApiResponse.GetApiResponse(appSettings);
                var columns = ApiResponse.ParseJson253(apiResponse);
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

        private static void UpdateNodeTimeSeries(List<ColumnName253> columns, AppSettings appSettings)
        {
            DBDataContext context = new DBDataContext();
            foreach (var item in columns)
            {
                try
                {
                    int? nodeDBId = null;
                    ISingleResult<NMS_CheckAndCreateNodeIDWithGatewayDBIdResult> nodeObj = null;
                    //get getway DB id
                    long ? gatewayDBId = context.NMS_GetGatewayDBId(item.GatewayId);
                    gatewayDBId = gatewayDBId == 0 ? null : gatewayDBId;

                    if (item.IsRouter != "4")
                    {
                        //get NodeDBId based on nodeid
                        nodeDBId = context.NMS_CheckNodeDBId(item.NodeId);
                        if (nodeDBId == 0)
                        {
                            Log.WriteToLog(appSettings, "NodeDBId is not found for this data item Node Id - " + item.NodeId);
                            continue;
                        }
                    }
                    else
                    {
                        //get nodedbid for sink data 
                        nodeObj = context.NMS_CheckAndCreateNodeIDWithGatewayDBId(gatewayDBId, item.NodeId);
                    }

                    decimal? linkQuality = null;
                    //get child node and update all the child node with parent node db id
                    var neighborhoods = Utility.GetNeighborhoods(item.Cost);
                    if (neighborhoods != null && neighborhoods.Count > 0)
                    {
                        linkQuality = Convert.ToDecimal(Math.Round(Convert.ToDouble(neighborhoods[0].Quality), 3));
                    }

                    NodeTimeSeries node = new NodeTimeSeries();
                    node.NodeDBId = nodeDBId;
                    node.GatewayDBId = gatewayDBId;
                    node.TimestampUTC = DateTimeOffset.Parse(item.Time).UtcDateTime;
                    node.UpdateTimeUTC = DateTime.UtcNow;
                    node.EP253TimeUTC = DateTimeOffset.Parse(item.Time).UtcDateTime;
                    node.MsgDelayLowLatency = Convert.ToInt32(item.TravelTimeMs);
                    node.HopCount = Convert.ToInt32(item.HopCount);
                    node.Status = Constants.Status;
                    node.Mode = item.CbMac.ToLower() == "true" ? Constants.LowLatency : Constants.LowEnergy;
                    node.MaxBufferUsage = Convert.ToDecimal(Math.Round(Convert.ToDouble(item.MaxBufferUsage), 3));
                    node.IsRouter = Convert.ToByte(item.IsRouter);
                    node.AutoRole = Convert.ToBoolean(item.AutoRole);
                    node.Voltage = Convert.ToDecimal(Math.Round(Convert.ToDouble(item.Voltage), 3));
                    node.LinkQuality = linkQuality;
                    node.BlackListedRadioChannelsCount = Convert.ToInt32(item.BlackListedRadioChannelsCount);
                    node.MemAllocFails = Convert.ToInt32(item.MemAllocFails);

                    //update NodeTimeSeries table
                    context.NMS_UpdateNodeTimeSeries253(node.NodeDBId
                        ,node.GatewayDBId
                        ,node.TimestampUTC
                        ,node.UpdateTimeUTC
                        ,node.EP253TimeUTC
                        ,node.Mode
                        ,node.Voltage
                        ,node.MaxBufferUsage
                        ,node.LinkQuality
                        ,node.HopCount
                        ,node.MsgDelayLowLatency
                        ,node.IsRouter
                        ,node.AutoRole                        
                        ,node.BlackListedRadioChannelsCount
                        ,node.MemAllocFails
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
