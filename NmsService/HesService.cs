using NmsService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NmsService
{
    public partial class HesService : ServiceBase
    {
        Timer timer1 = new Timer();
        public HesService()
        {
            InitializeComponent();           
        }
        internal void StartServiceFromUI()
        {
            OnStart(null);
        }

        internal void StopServiceFromUI()
        {
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            AppSettings appSettings = new AppSettings();
            appSettings.LogsFolderPath = ConfigurationManager.AppSettings.Get("LogFolderPath");
            Log.WriteToLog(appSettings, "On Start Called");
            try
            {               
                SetEventHandlers();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
                throw ex;
            }
        }

        protected override void OnStop()
        {
            AppSettings appSettings = new AppSettings();
            appSettings.LogsFolderPath = ConfigurationManager.AppSettings.Get("LogFolderPath");
            Log.WriteToLog(appSettings, "On Stop Called");
            timer1.Stop();
        }

        private void SetEventHandlers()
        {
           // Timer timer = new Timer();
            timer1.Elapsed += new ElapsedEventHandler(OnTimerEvent);
            timer1.Interval = 5000;
            timer1.Enabled = true;
           
        }

        private void OnTimerEvent(object sender, ElapsedEventArgs e)
        {
           // Timer timer = (Timer)sender;
            timer1.Enabled = false;

            AppSettings appSettings = new AppSettings();
            appSettings.LogsFolderPath = ConfigurationManager.AppSettings.Get("LogFolderPath");
            
            try
            {                
                ProgramManager.ProcessApi();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(appSettings, ex);
            }
            timer1.Enabled = true;
        }
    }
}
