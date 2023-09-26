using NmsService;
using NmsService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService
{
    class Log
    {
        //static string logFolder = @"c:\temp\logs2";
        public static void WriteToLog(AppSettings appSettings, string text, bool isError = false)
        {
            try
            {
                string logsFolder = "";
                if (appSettings == null || appSettings.LogsFolderPath == "")
                {
                    logsFolder = GetLogsFolderFromSettings(appSettings);
                }
                else
                {
                    logsFolder = appSettings.LogsFolderPath;
                }
                string dateStr = getDateStringForLogs(DateTime.Now, isError);
                if (!Directory.Exists(logsFolder))
                {
                    Directory.CreateDirectory(logsFolder);
                }
                string logFile = logsFolder + @"\" + dateStr + ".txt";
                string textToWrite = DateTime.Now.ToString();
                //textToWrite = textToWrite + " " + text + Environment.NewLine;
                textToWrite = text + Environment.NewLine;
                File.AppendAllText(logFile, textToWrite);
            }
            catch (Exception ex)
            {
            }
        }

        private static string GetLogsFolderFromSettings(AppSettings appSettings)
        {
            string logsFolder = appSettings.LogsFolderPath;
            return logsFolder;
        }

        private static string getDateStringForLogs(DateTime dateTime, bool isError)
        {
            string retVal = "";
            string day = dateTime.Day.ToString().PadLeft(2, '0');
            string month = dateTime.Month.ToString().PadLeft(2, '0');
            string year = dateTime.Year.ToString();
            string prefix = "log";
            if (isError) prefix = "error";

            retVal = $"{prefix}_{year}_{month}_{day}";
            return retVal;
        }

        public static void WriteToLog(AppSettings appSettings, Exception ex)
        {
            try
            {
                string txt = ex.Message + Environment.NewLine + ex.StackTrace;
                if (ex.InnerException != null)
                {
                    txt = txt + Environment.NewLine + Environment.NewLine;
                    txt = txt + ex.InnerException.Message + Environment.NewLine;
                    txt = txt + ex.InnerException.StackTrace;
                }

                Log.WriteToLog(appSettings, txt, false); // write to log file
                Log.WriteToLog(appSettings, txt, true); // write to exception file
            }
            catch (Exception ex1)
            {
            }
        }
        
        public static void WriteToLogStartEnd(string text)
        {
            try
            {
                string logsFolder = "";               
                 logsFolder = ConfigurationManager.AppSettings.Get("LogFolderStartEndPath");
               
                string dateStr = getDateStringForLogs(DateTime.Now, false);
                if (!Directory.Exists(logsFolder))
                {
                    Directory.CreateDirectory(logsFolder);
                }
                string logFile = logsFolder + @"\" + dateStr + ".csv";
                string textToWrite = string.Empty;
                //if log file does not exists the log header in file
                if (!File.Exists(logFile))
                {
                    textToWrite = "Service Name,Start Time,End Time,Success OR Fail,Error Message" + Environment.NewLine;
                    File.AppendAllText(logFile, textToWrite);
                }
                textToWrite = text + Environment.NewLine;
                File.AppendAllText(logFile, textToWrite);
            }
            catch (Exception ex)
            {
            }
        }

    }
}

