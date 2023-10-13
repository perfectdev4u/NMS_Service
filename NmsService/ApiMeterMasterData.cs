using Newtonsoft.Json;
using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace NmsService
{
    internal class ApiMeterMasterData
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.ApiMeterMasterData + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                var days = string.IsNullOrEmpty(appSettings.StartDate) ? appSettings.NumOfPrevDays : (DateTime.Now - Convert.ToDateTime(appSettings.StartDate)).TotalDays + 1;
                for (int i = 0; i < days; i++)
                {
                    MeterMasterDataRequest requestObj = new MeterMasterDataRequest();
                    if (string.IsNullOrEmpty(appSettings.StartDate))
                    {
                        requestObj.ParamFromDate = DateTime.Now.AddDays(-appSettings.NumOfPrevDays).ToString("yyyy-MM-dd");
                        appSettings.StartDate = DateTime.Now.AddDays(-appSettings.NumOfPrevDays).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        requestObj.ParamFromDate = Convert.ToDateTime(appSettings.StartDate).AddDays(i).ToString("yyyy-MM-dd");
                    }
                    requestObj.ApiKey = appSettings.ApiKey;
                    requestObj.Hes = Constants.Hes;
                    requestObj.ParamToDate = Convert.ToDateTime(appSettings.StartDate).AddDays(i).ToString("yyyy-MM-dd");
                    Log.WriteToLog(appSettings, "ApiMeterMasterData.Create() started ParamFromDate : " + requestObj.ParamFromDate);
                    var apiResponse = ApiResponse.GetMeterMasterDataResponse(appSettings, requestObj);
                    if (apiResponse != null && apiResponse.Count > 0)
                    {
                        Log.WriteToLog(appSettings, "ApiMeterMasterData.Create() started with Count (" + apiResponse.Count + ")");
                        //create master data
                        Create(apiResponse, appSettings);
                        Log.WriteToLog(appSettings, "ApiMeterMasterData.Create() ended");
                    }
                    else
                    {
                        Log.WriteToLog(appSettings, "ApiMeterMasterData.Save() Data not found for Date: " + requestObj.ParamFromDate);
                    }
                    Log.WriteToLog(appSettings, "ApiMeterMasterData.Create() ended ParamToDate : " + requestObj.ParamToDate);
                }

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

        private static void Create(List<MeterMasterDataResponse> results, AppSettings appSettings)
        {
            DBDataContext context = new DBDataContext();

            foreach (var item in results)
            {
                int sdoId = 0;
                int feederId = 0;
                int dtrId = 0;
                int meterId = 0;
                int nodeDBId = 0;
                string nodeId = string.Empty;

                try
                {
                    //get SDOId based on SubdivisionName
                    sdoId = context.NMS_CheckAndCreateSDOID(item.SubdivisionName?.Trim());

                    //get FeederId based on FeederCode
                    string feederName = string.IsNullOrEmpty(item.FeederCode) ? "Unknown-" + item.SubdivisionName : item.FeederCode?.Trim();
                    feederId = context.NMS_CheckAndCreateFeederID(feederName, sdoId);

                    //get DTRId based on DTCode
                    dtrId = context.NMS_CheckAndCreateDTRID(item.DTCode?.Trim(), feederId);

                    //get MeterID based on NewMeterNumber, sdoId, feederId, dtrId
                    meterId = context.NMS_CheckAndCreateMeterID(item.NewMeterNumber?.Trim(), decimal.Parse(item.Latitude), decimal.Parse(item.Longitude), sdoId, feederId, dtrId, Utility.DateTimeParse(item.InstallationDate));

                    //get NodeID based on NewMeterNumber(after reomoving first two characters),MeterId
                    nodeId = Convert.ToString(item.NewMeterNumber).Length > 2
                                       ? Convert.ToString(item.NewMeterNumber).Remove(0, 2) : item.NewMeterNumber;

                    nodeDBId = context.NMS_CheckAndCreateNodeIDWithMeterId(meterId, nodeId);
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(appSettings, "ApiMeterMasterData.Create()" + string.Format("sdoId:{0}, feederId:{1}, dtrId:{2}, meterId:{3}, nodeId:{4}, nodeDBId:{5}", sdoId, feederId, dtrId, meterId, nodeId, nodeDBId));
                    Log.WriteToLog(appSettings, ex);
                }
            }
        }
    }
}
