using DocumentFormat.OpenXml.Bibliography;
using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService
{
    internal class ApiMeterMasterDataFromWEM
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.ApiMeterMasterData + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                var apiResponse = ApiResponse.GetMeterMasterDataResponseWFM(appSettings);
                if (apiResponse.data != null && apiResponse.data.Count > 0)
                {
                    Log.WriteToLog(appSettings, "ApiMeterMasterDataFromWEM.Create() started with Count (" + apiResponse.data.Count + ")");
                    //create master data
                    Create(apiResponse.data, appSettings);
                    Log.WriteToLog(appSettings, "ApiMeterMasterDataFromWEM.Create() ended");
                }
                else
                {
                    Log.WriteToLog(appSettings, "ApiMeterMasterDataFromWEM.Save() Data not found for Date: " + appSettings.ServiceRunTime);
                }
                Log.WriteToLog(appSettings, "ApiMeterMasterDataFromWEM.Create() ended ParamToDate : " + appSettings.ServiceRunTime);

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


        private static void Create(List<MeterMasterDataWFM> results, AppSettings appSettings)
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
                    sdoId = context.NMS_CheckAndCreateSDOID(item.SDOname?.Trim());

                    //get FeederId based on FeederCode
                    string feederName = string.IsNullOrEmpty(item.Feedernamewithcode) ? "Unknown-" + item.Feedernamewithcode : GetName(item.Feedernamewithcode)?.Trim();
                    feederId = context.NMS_CheckAndCreateFeederID(feederName, sdoId);

                    //get DTRId based on DTCode
                    dtrId = context.NMS_CheckAndCreateDTRID(GetName(item.DTnameandcode)?.Trim(), feederId);

                    //get MeterID based on NewMeterNumber, sdoId, feederId, dtrId
                    meterId = context.NMS_CheckAndCreateMeterID(item.MeterSerialnumber?.Trim(), decimal.Parse(item.Latitude), decimal.Parse(item.Longitude), sdoId, feederId, dtrId, Utility.DateTimeParse(item.Installationdate));

                    //get NodeID based on NewMeterNumber(after reomoving first two characters),MeterId
                    nodeId = Convert.ToString(item.MeterSerialnumber).Length > 1
                                       ? Convert.ToString(item.MeterSerialnumber).Remove(0, 1) : item.MeterSerialnumber;

                    nodeDBId = context.NMS_CheckAndCreateNodeIDWithMeterId(meterId, nodeId);
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(appSettings, "ApiMeterMasterDataFromWEM.Create()" + string.Format("sdoId:{0}, feederId:{1}, dtrId:{2}, meterId:{3}, nodeId:{4}, nodeDBId:{5}", sdoId, feederId, dtrId, meterId, nodeId, nodeDBId));
                    Log.WriteToLog(appSettings, ex);
                }
            }
        }
        private static string GetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string nameCode = name.Split('-').LastOrDefault();
                string resName = name.Replace("-" + nameCode, "");
                return resName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
