using System;

namespace NmsService.Models
{
    public class NodeTimeSeries
    {
        public long Id { get; set; }
        public long? NodeDBId { get; set; }
        public long? GatewayDBId { get; set; }  
        public long ParentNodeDBId { get; set; }
        public DateTime? TimestampUTC { get; set; }
        public decimal? LinkQuality { get; set; }
        public decimal? MaxBufferUsage { get; set; }
        public string Mode { get; set; }
        public int? RSSI { get; set; }
        public bool? Status { get; set; }
        public Byte IsRouter { get; set; }
        public bool? AutoRole { get; set; }
        public int? MsgDelayLowLatency { get; set; }
        public DateTime? EP251TimeUTC { get; set; }
        public DateTime? EP252TimeUTC { get; set; }
        public DateTime? EP253TimeUTC { get; set; }
        public DateTime? EP254TimeUTC { get; set; }
        public DateTime? CreateTimeUTC { get; set; }
        public DateTime? UpdateTimeUTC { get; set; }
        public string FirmwareVersion { get; set; }
        public int ScratchpadSequence { get; set; }     
        public int? HopCount { get; set; }        
        public int BlackListedRadioChannelsCount { get; set; }
        public int MemAllocFails { get; set; }
        public decimal? Voltage { get; set; }
        public decimal? ChannelReliability { get; set; }


    }
} 
