
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using NmsService.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace NmsService
{
    internal class ApiMeterMasterDataFromExcel
    {
        public static DateTime LastRunTime = DateTime.MinValue;
        public static void Save()
        {
            try
            {
                //List<MeterMasterDataResponseFromExcel> response = new List<MeterMasterDataResponseFromExcel>();
                //string assetsFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
                //string[] excelFiles = Directory.GetFiles(assetsFolderPath, "*.xls");

                //foreach (string excelFilePath in excelFiles)
                //{
                //    using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                //    {
                //        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                //        if (worksheet != null)
                //        {
                //            var rows = worksheet.Cells.Where(x => x.Value != null).Last().End.Row;
                //            for (int row = 2; row <= rows; row++)
                //            {
                //                MeterMasterDataResponseFromExcel res = new MeterMasterDataResponseFromExcel();
                //                res.MeterNumber = worksheet.Cells[row, 31].Value == null ? string.Empty : worksheet.Cells[row, 31].Value.ToString();
                //                if (!string.IsNullOrEmpty(res.MeterNumber))
                //                {
                //                    res.MeterInstallationDate = worksheet.Cells[row, 11].Value == null ? DateTime.MinValue : DateTime.Parse(worksheet.Cells[row, 11].Value.ToString());
                //                    res.SDO = worksheet.Cells[row, 66].Value.ToString();
                //                    res.Latitude = worksheet.Cells[row, 43].Value != null ? worksheet.Cells[row, 43].Value.ToString().Split(',')[0] : string.Empty;
                //                    res.Longitude = worksheet.Cells[row, 43].Value != null ? worksheet.Cells[row, 43].Value.ToString().Split(',')[1] : string.Empty;
                //                    res.DTR = worksheet.Cells[row, 69].Value.ToString();
                //                    res.Feeder = worksheet.Cells[row, 68].Value.ToString();
                //                    response.Add(res);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            Console.WriteLine("Worksheet not found.");
                //        }
                //    }
                //}

                ////var apiResponse = new List<MeterMasterDataResponseFromExcel>();
                //if (response != null && response.Count > 0)
                //{
                //    Create(response);
                //}

                //bind nodes into dictionary
                BindAllNodes();
            }
            catch (Exception ex)
            {
            }

            LastRunTime = DateTime.Now;
        }

        private static void Create(List<MeterMasterDataResponseFromExcel> results)
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
                    sdoId = context.NMS_CheckAndCreateSDOID(item.SDO?.Trim());

                    //get FeederId based on FeederCode
                    string feederName = string.IsNullOrEmpty(item.Feeder) ? "Unknown-" + item.SDO : item.Feeder?.Trim();
                    feederId = context.NMS_CheckAndCreateFeederID(feederName, sdoId);

                    //get DTRId based on DTCode
                    dtrId = context.NMS_CheckAndCreateDTRID(item.DTR?.Trim(), feederId);

                    //get MeterID based on NewMeterNumber, sdoId, feederId, dtrId
                    meterId = context.NMS_CheckAndCreateMeterID(item.MeterNumber?.Trim(), decimal.Parse(item.Latitude), decimal.Parse(item.Longitude), sdoId, feederId, dtrId, item.MeterInstallationDate);

                    //get NodeID based on NewMeterNumber(after reomoving first two characters),MeterId
                    nodeId = Convert.ToString(item.MeterNumber).Length > 1
                                       ? Convert.ToString(item.MeterNumber).Remove(0, 1) : item.MeterNumber;

                    nodeDBId = context.NMS_CheckAndCreateNodeIDWithMeterId(meterId, nodeId);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private static void BindAllNodes()
        {
            DBDataContext context = new DBDataContext();
            Utility.NodeIdList.Clear();
            var nodes = context.NMS_GetAllNodes().ToList();
            foreach (var item in nodes)
            {
                Utility.NodeIdList.Add(item.NodeId, item.Id);
            }
        }
    }
}
