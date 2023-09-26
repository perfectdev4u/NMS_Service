using Newtonsoft.Json;
using NmsService;
using NmsService.Models;
using NmsService.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace NmsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ProgramManager.LoadAppSettings();

            if (Environment.UserInteractive)
            {
                FormMain formMain = new FormMain();
                formMain.ShowDialog();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new HesService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }       

    }
}
