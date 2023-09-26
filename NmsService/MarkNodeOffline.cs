using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NmsService
{
    internal class MarkNodeOffline
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static void Save(AppSettings appSettings)
        {
            StringBuilder log = new StringBuilder();
            log.Append(Constants.MarkNodeOffline + "," + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

            try
            {
                DBDataContext context = new DBDataContext();
                var nodeTimeSeries = context.NMS_GetNodeTimeSeriesActiveStatus().ToList();
                var offlineDuration = appSettings.OfflineDurationInMin;
                foreach (var item in nodeTimeSeries)
                {
                    DateTime timestampUTC = Convert.ToDateTime(item.TimestampUTC);
                    DateTime currentTimestamp = DateTime.UtcNow;
                    TimeSpan timeSpan = currentTimestamp.Subtract(timestampUTC);
                    double minutes = timeSpan.TotalMinutes;

                    if (minutes > offlineDuration)
                    {
                        context.NMS_UpdateNodeTimeSeriesStatus(item.NodeDBId, null, false);
                    }
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
    }
}
