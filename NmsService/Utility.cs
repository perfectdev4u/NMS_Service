using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NmsService.Models;
using System.IO;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;

namespace NmsService
{
    internal class Utility
    {
        internal static List<AppSettings> AppSettings;
        public static Dictionary<string, Int64> NodeIdList = new Dictionary<string, long>();

        public static List<AppSettings> ReadSettings()
        {
            string settingsFile = ConfigurationManager.AppSettings.Get("AppSettingsJson");
            string text = File.ReadAllText(settingsFile);
            var setting = JsonConvert.DeserializeObject<List<AppSettings>>(text);
            return setting;
        }

        public static DateTime? DateTimeParse(string dateTime)
        {
            DateTime? dateTime1 = null;
            try
            {
                dateTime1 = DateTime.Parse(dateTime);
            }
            catch (Exception ex)
            {
                dateTime1 = null;
            }
            return dateTime1;
        }

        public static bool CheckTimeInterval(DateTime lastRuneTime, int timeInterval)
        {
            bool flag = false;
            try
            {
                if (lastRuneTime == DateTime.MinValue)
                {
                    flag = true;
                }
                else
                {
                    DateTime currentDateTime = DateTime.Now;
                    TimeSpan timeSpan = currentDateTime.Subtract(lastRuneTime);

                    if (timeSpan.TotalMinutes > timeInterval)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                flag = true;
            }
            return flag;
        }

        public static List<string> GetServiceRunTimeList(AppSettings appsettings)
        {
            List<string> listItems = new List<string>();
            string[] splitArray = appsettings.ServiceRunTime.Split(',');
            foreach (string time in splitArray)
            {
                listItems.Add(time);
            }
            return listItems;
        }

        public static bool CheckServiceRunTime(AppSettings appsettings)
        {
            var serviceRunTimeList = GetServiceRunTimeList(appsettings);
            foreach (string serviceRunTime in serviceRunTimeList)
            {
                string startTime = serviceRunTime.Split('-')[0];
                string endTime = serviceRunTime.Split('-')[1];

                DateTime dateTimeStart = DateTime.ParseExact(startTime, "HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime dateTimeEnd = DateTime.ParseExact(endTime, "HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime now = DateTime.Now;

                if ((now.TimeOfDay >= dateTimeStart.TimeOfDay && now.TimeOfDay <= dateTimeEnd.TimeOfDay))
                {
                    return true;
                }
            }

            return false;
        }

        public static List<Neighborhood> GetNeighborhoods(string message_50_18)
        {
            var neighborhoods = new List<Neighborhood>();

            if (!string.IsNullOrEmpty(message_50_18) && message_50_18.IndexOf("[") != -1 && message_50_18.IndexOf("]") != -1)
            {
                string[] splitArray = message_50_18.Split(',');
                string address = string.Empty;
                string operatingFreq = string.Empty;
                string radioPowerDB = string.Empty;
                string neighbourType = string.Empty;
                string rssiDbm = string.Empty;
                string cost = string.Empty;
                string quality = string.Empty;
                bool flag = false;

                foreach (string item in splitArray)
                {
                    //EP252
                    if (item.ToLower().IndexOf("message_50_1_1=") != -1)
                    {
                        address = item.Split('=')[1];
                    }
                    if (item.ToLower().IndexOf("message_50_1_2=") != -1)
                    {
                        operatingFreq = item.Split('=')[1];
                    }
                    if (item.ToLower().IndexOf("message_50_1_3=") != -1)
                    {
                        radioPowerDB = item.Split('=')[1];
                    }
                    if (item.ToLower().IndexOf("message_50_1_4=") != -1)
                    {
                        neighbourType = item.Split('=')[1];
                    }
                    if (item.ToLower().IndexOf("message_50_1_5=") != -1)
                    {
                        rssiDbm = item.Split('=')[1].Replace("]", "");
                        flag = true;
                    }
                    //EP253
                    if (item.ToLower().IndexOf("message_50_18_2=") != -1)
                    {
                        address = item.Split('=')[1];
                    }
                    if (item.ToLower().IndexOf("message_50_18_3=") != -1)
                    {
                        cost = item.Split('=')[1];
                    }
                    if (item.ToLower().IndexOf("message_50_18_4=") != -1)
                    {
                        quality = item.Split('=')[1].Replace("]", "");
                        flag = true;
                    }
                    if (flag)
                    {
                        flag = false;
                        Neighborhood neighborhood = new Neighborhood();
                        neighborhood.NextHopAddress = address;
                        neighborhood.OperatingFreq = operatingFreq;
                        neighborhood.RadioPowerDB = radioPowerDB;
                        neighborhood.NeighbourType = neighbourType;
                        neighborhood.RssiDbm = rssiDbm;
                        if (!string.IsNullOrEmpty(cost))
                        {
                            neighborhood.Cost = int.Parse(cost);
                        }
                        if (!string.IsNullOrEmpty(quality))
                        {
                            neighborhood.Quality = quality;
                        }
                        neighborhoods.Add(neighborhood);
                    }
                }
            }
            return neighborhoods;
        }

        public static String ObjectToXMLGeneric<T>(T filter)
        {
            string xml = null;
            using (StringWriter sw = new StringWriter())
            {

                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(sw, filter);
                try
                {
                    xml = sw.ToString();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return xml;
        }

        public static XmlDocument SerializeObject(Object myObject)
        {
            XmlDocument XmlObject = new XmlDocument();
            String XmlizedString = string.Empty;

            try
            {
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(myObject.GetType());
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, myObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            XmlObject.LoadXml(XmlizedString);
            return XmlObject;
        }

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }
    }
}
