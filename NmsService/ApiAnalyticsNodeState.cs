using Newtonsoft.Json;
using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NmsService
{
    internal class ApiAnalyticsNodeState
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.ApiAnalyticsNodeState + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                Log.WriteToLog(appSettings, "ApiAnalyticsNodeState ApiResponse.GetApiResponse() Start Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                var apiResponse = ApiResponse.GetApiResponse(appSettings);
                var columns = ApiResponse.ParseJsonAnalytics(apiResponse);
                Log.WriteToLog(appSettings, "ApiAnalyticsNodeState ApiResponse.GetApiResponse() End Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                if (columns != null && columns.Count > 0)
                {
                    columns = columns.GroupBy(r => r.NodeId).Select(s => s.OrderByDescending(o => o.Time).FirstOrDefault()).ToList();
                    Log.WriteToLog(appSettings, "ApiAnalyticsNodeState.UpdateNodeTimeSeries() Start Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                    UpdateNodeTimeSeries(columns, appSettings);
                    Log.WriteToLog(appSettings, "ApiAnalyticsNodeState.UpdateNodeTimeSeries() End Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));                   
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
        private static void UpdateNodeTimeSeries(List<AnalyticsNodeState> columns, AppSettings appSettings)
        {
            DBDataContext context = new DBDataContext();

            List<BulkUpdateNodeStatus> bulkUpdateNodeStatusList = new List<BulkUpdateNodeStatus>();           
            foreach (var item in columns)
            {
                try
                {
                    //get NodeDBId based on nodeid from Dictionary                  
                    long nodeDBId = Utility.NodeIdList.ContainsKey(item.NodeId) ? Convert.ToInt64(Utility.NodeIdList[item.NodeId]) : 0;
                    if (nodeDBId == 0)
                    {
                        Log.WriteToLog(appSettings, "NodeDBId is not found for this data item Node Id - " + item.NodeId);
                        continue;
                    }
                    //status
                    bool? status = null;
                    switch (item.Online)
                    {
                        case "0":
                            status = false;
                            break;
                        case "1":
                            status = null;
                            break;
                        case "2":
                            status = true;
                            break;
                    }

                    BulkUpdateNodeStatus bulkUpdateNodeStatus = new BulkUpdateNodeStatus();                    
                    bulkUpdateNodeStatus.NodeDBId = nodeDBId;
                    bulkUpdateNodeStatus.NodeId = item.NodeId;
                    bulkUpdateNodeStatus.TimestampUTC = DateTimeOffset.Parse(item.Time).UtcDateTime;
                    bulkUpdateNodeStatus.Status = status;
                    bulkUpdateNodeStatusList.Add(bulkUpdateNodeStatus);
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(appSettings, ex);
                }
            }

            if (bulkUpdateNodeStatusList.Count > 0)
            {
                Log.WriteToLog(appSettings, "ApiAnalyticsNodeState.BulkUpdateNodeTimeSeries() Data Bound in List Start Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                BulkUpdateNodeTimeSeries(bulkUpdateNodeStatusList, appSettings);
                Log.WriteToLog(appSettings, "ApiAnalyticsNodeState.BulkUpdateNodeTimeSeries() Data Bound in List End Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
            }
        }

        private static void BulkUpdateNodeTimeSeries(List<BulkUpdateNodeStatus> columns, AppSettings appSettings)
        {
            try
            {
                DBDataContext context = new DBDataContext();
                IEnumerable<List<BulkUpdateNodeStatus>> chunks = columns.ChunkBy(appSettings.ChunkSize);
                int cnt = 1;
                foreach (var nodes in chunks)
                {
                    XmlSerializer xsSubmit = new XmlSerializer(typeof(List<BulkUpdateNodeStatus>));
                    var xmlString = "";
                    using (var strignWriter = new StringWriter())
                    {
                        using (XmlWriter writer = XmlWriter.Create(strignWriter))
                        {
                            xsSubmit.Serialize(writer, nodes);
                            xmlString = strignWriter.ToString();
                            XElement element = XElement.Parse(xmlString);
                            Log.WriteToLog(appSettings, "ApiAnalyticsNodeState.NMS_BulkUpdateNodeTimeSeriesStatus() - [Chunk #" + cnt + "] Start Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                            context.NMS_BulkUpdateNodeTimeSeriesStatus(element);
                            Log.WriteToLog(appSettings, "ApiAnalyticsNodeState.NMS_BulkUpdateNodeTimeSeriesStatus() - [Chunk #" + cnt + "] End Time: " + DateTime.Now.ToString("yyyy-MMM-dd hh:mm:ss.ttt"));
                            cnt++;
                        }
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

