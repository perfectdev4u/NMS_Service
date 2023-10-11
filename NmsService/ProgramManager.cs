using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using System.Timers;

namespace NmsService
{
    internal class ProgramManager
    {
        #region Methods
        public static void LoadAppSettings()
        {
            if (Utility.AppSettings == null)
            {
                Utility.AppSettings = Utility.ReadSettings();
            }
        }

        public static void ProcessApi()
        {
            AppSettings appSettings = new AppSettings();
            bool masterDataFromExcel = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("MasterDataFromExcel"));
            try
            {
                //gateway
                ProcessHESGatewayList(appSettings);

                if (masterDataFromExcel)
                {
                    //MeterMasterData from Excel
                    ProcessMeterMasterDataFromExcel();
                }
                else
                {
                    //MeterMasterData
                    ProcessMeterMasterData(appSettings);
                }

                //gateway
                ProcessGateway(appSettings);

                //ApiAnalyticsNodeState
                ProcessAnalyticsNodeState(appSettings);

                //ApiEndPoint251
                ProcessEP251(appSettings);

                //ApiEndPoint252
                ProcessEP252(appSettings);

                //ApiEndPoint253
                ProcessEP253(appSettings);

                //ApiEndPoint254
                ProcessEP254(appSettings);

                //MarkNodeOffline
                ProcessMarkNodeOffline(appSettings);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
        }

        private static void ProcessGateway(AppSettings appSettings)
        {
            try
            {
                string log = string.Empty;
                appSettings = Utility.AppSettings.Where(x => x.ServiceName.ToLower() == Constants.ApiGatewaySearch).FirstOrDefault();
                var timeIntervalGateway = appSettings.TimeIntervalInMin;
                if (appSettings.Enabled && Utility.CheckTimeInterval(ApiGateway.LastRunTime, timeIntervalGateway))
                {
                    if (Utility.CheckServiceRunTime(appSettings))
                    {
                        ApiGateway.Save(appSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
        }

        private static void ProcessHESGatewayList(AppSettings appSettings)
        {
            try
            {
                string log = string.Empty;
                appSettings = Utility.AppSettings.Where(x => x.ServiceName.ToLower() == Constants.ApiHESGatewayList).FirstOrDefault();
                var timeIntervalGateway = appSettings.TimeIntervalInMin;
                if (appSettings.Enabled && Utility.CheckTimeInterval(ApiGateway.LastRunTime, timeIntervalGateway))
                {
                    if (Utility.CheckServiceRunTime(appSettings))
                    {
                        ApiHESGateway.Save(appSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
        }

        private static void ProcessMeterMasterDataFromExcel()
        {
            try
            {
                ApiMeterMasterDataFromExcel.Save();
            }
            catch (Exception ex)
            {
            }
        }

        private static void ProcessMeterMasterData(AppSettings appSettings)
        {
            try
            {
                appSettings = Utility.AppSettings.Where(x => x.ServiceName.ToLower() == Constants.ApiMeterMasterData).FirstOrDefault();
                var timeInterval = appSettings.TimeIntervalInMin;
                if (appSettings.Enabled && Utility.CheckTimeInterval(ApiMeterMasterData.LastRunTime, timeInterval))
                {
                    if (Utility.CheckServiceRunTime(appSettings))
                    {
                        ApiMeterMasterData.Save(appSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
        }

        private static void ProcessAnalyticsNodeState(AppSettings appSettings)
        {
            try
            {
                appSettings = Utility.AppSettings.Where(x => x.ServiceName.ToLower() == Constants.ApiAnalyticsNodeState).FirstOrDefault();
                var timeIntervalNodeState = appSettings.TimeIntervalInMin;
                if (appSettings.Enabled && Utility.CheckTimeInterval(ApiAnalyticsNodeState.LastRunTime, timeIntervalNodeState))
                {
                    if (Utility.CheckServiceRunTime(appSettings))
                    {
                        ApiAnalyticsNodeState.Save(appSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
        }
        private static void ProcessEP251(AppSettings appSettings)
        {
            try
            {
                appSettings = Utility.AppSettings.Where(x => x.ServiceName.ToLower() == Constants.ApiEndPoint251).FirstOrDefault();
                var timeInterval251 = appSettings.TimeIntervalInMin;
                if (appSettings.Enabled && Utility.CheckTimeInterval(ApiEndPoint251.LastRunTime, timeInterval251))
                {
                    if (Utility.CheckServiceRunTime(appSettings))
                    {
                        ApiEndPoint251.Save(appSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
        }

        private static void ProcessEP252(AppSettings appSettings)
        {
            try
            {
                appSettings = Utility.AppSettings.Where(x => x.ServiceName.ToLower() == Constants.ApiEndPoint252).FirstOrDefault();
                var timeInterval252 = appSettings.TimeIntervalInMin;
                if (appSettings.Enabled && Utility.CheckTimeInterval(ApiEndPoint252.LastRunTime, timeInterval252))
                {
                    if (Utility.CheckServiceRunTime(appSettings))
                    {
                        ApiEndPoint252.Save(appSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
        }

        private static void ProcessEP253(AppSettings appSettings)
        {
            try
            {
                appSettings = Utility.AppSettings.Where(x => x.ServiceName.ToLower() == Constants.ApiEndPoint253).FirstOrDefault();
                var timeInterval253 = appSettings.TimeIntervalInMin;
                if (appSettings.Enabled && Utility.CheckTimeInterval(ApiEndPoint253.LastRunTime, timeInterval253))
                {
                    if (Utility.CheckServiceRunTime(appSettings))
                    {
                        ApiEndPoint253.Save(appSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
        }

        private static void ProcessEP254(AppSettings appSettings)
        {
            try
            {
                appSettings = Utility.AppSettings.Where(x => x.ServiceName.ToLower() == Constants.ApiEndPoint254).FirstOrDefault();
                var timeInterval254 = appSettings.TimeIntervalInMin;
                if (appSettings.Enabled && Utility.CheckTimeInterval(ApiEndPoint254.LastRunTime, timeInterval254))
                {
                    if (Utility.CheckServiceRunTime(appSettings))
                    {
                        ApiEndPoint254.Save(appSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
        }

        private static void ProcessMarkNodeOffline(AppSettings appSettings)
        {
            try
            {
                appSettings = Utility.AppSettings.Where(x => x.ServiceName.ToLower() == Constants.MarkNodeOffline).FirstOrDefault();
                int timeIntervalOffline = appSettings.TimeIntervalInMin;
                if (appSettings.Enabled && Utility.CheckTimeInterval(MarkNodeOffline.LastRunTime, timeIntervalOffline))
                {
                    if (Utility.CheckServiceRunTime(appSettings))
                    {
                        MarkNodeOffline.Save(appSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
        }
        #endregion


    }
}
