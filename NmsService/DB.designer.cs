﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NmsService
{
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Data;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System;


    [global::System.Data.Linq.Mapping.DatabaseAttribute(Name = "NMS2")]
    public partial class DBDataContext : System.Data.Linq.DataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public DBDataContext() :
                base(global::NmsService.Properties.Settings.Default.NMS2ConnectionString, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(string connection) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(System.Data.IDbConnection connection) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
                base(connection, mappingSource)
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_CheckAndCreateDTRID")]
        public int NMS_CheckAndCreateDTRID([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DTRCode", DbType = "VarChar(50)")] string dTRCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FeederId", DbType = "BigInt")] System.Nullable<long> feederId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), dTRCode, feederId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_CheckAndCreateFeederID")]
        public int NMS_CheckAndCreateFeederID([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FeederCode", DbType = "VarChar(50)")] string feederCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SdoId", DbType = "BigInt")] System.Nullable<long> sdoId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), feederCode, sdoId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_CheckAndCreateMeterID")]
        public int NMS_CheckAndCreateMeterID([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MeterNumber", DbType = "VarChar(50)")] string meterNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Lat", DbType = "Decimal(12,9)")] System.Nullable<decimal> lat, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Long", DbType = "Decimal(12,9)")] System.Nullable<decimal> @long, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SDOId", DbType = "BigInt")] System.Nullable<long> sDOId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FeederId", DbType = "BigInt")] System.Nullable<long> feederId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DTRId", DbType = "BigInt")] System.Nullable<long> dTRId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationDate", DbType = "DateTime")] System.Nullable<System.DateTime> installationDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), meterNumber, lat, @long, sDOId, feederId, dTRId, installationDate);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_CheckAndCreateNodeTimeSeriesID")]
        public int NMS_CheckAndCreateNodeTimeSeriesID([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NodeDBId", DbType = "BigInt")] System.Nullable<long> nodeDBId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nodeDBId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_CheckAndCreateSDOID")]
        public int NMS_CheckAndCreateSDOID([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SubdivisionName", DbType = "VarChar(50)")] string subdivisionName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subdivisionName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_CheckNodeDBId")]
        public int NMS_CheckNodeDBId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NodeId", DbType = "VarChar(50)")] string nodeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nodeId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_GetGatewayDBId")]
        public int NMS_GetGatewayDBId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GatewayId", DbType = "VarChar(50)")] string gatewayId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gatewayId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_GetNodeTimeSeriesActiveStatus")]
        public ISingleResult<NMS_GetNodeTimeSeriesActiveStatusResult> NMS_GetNodeTimeSeriesActiveStatus()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<NMS_GetNodeTimeSeriesActiveStatusResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_InsertGatewayStatusHistory")]
        public int NMS_InsertGatewayStatusHistory([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GatewayId", DbType = "BigInt")] System.Nullable<long> gatewayId,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Bit")] System.Nullable<bool> status,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "OnBattery", DbType = "Bit")] System.Nullable<bool> onBattery,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsBatteryFlagOn", DbType = "Bit")] System.Nullable<bool> isBatteryFlagOn,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Timestamp", DbType = "DateTime")] System.Nullable<System.DateTime> timestamp)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gatewayId, status, onBattery, isBatteryFlagOn, timestamp);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_UpdateNodeTimeSeriesParentDBId")]
        public int NMS_UpdateNodeTimeSeriesParentDBId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NodeDBId", DbType = "BigInt")] System.Nullable<long> nodeDBId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ParentDBId", DbType = "BigInt")] System.Nullable<long> parentDBId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nodeDBId, parentDBId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_CheckAndCreateNodeIDWithMeterId")]
        public int NMS_CheckAndCreateNodeIDWithMeterId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MeterId", DbType = "BigInt")] System.Nullable<long> meterId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NodeId", DbType = "VarChar(50)")] string nodeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), meterId, nodeId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_UpdateNodeTimeSeries251")]
        public int NMS_UpdateNodeTimeSeries251([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NodeDBId", DbType = "BigInt")] System.Nullable<long> nodeDBId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GatewayDBId", DbType = "BigInt")] System.Nullable<long> gatewayDBId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TimestampUTC", DbType = "DateTime")] System.Nullable<System.DateTime> timestampUTC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UpdateTimeUTC", DbType = "DateTime")] System.Nullable<System.DateTime> updateTimeUTC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EP251TimeUTC", DbType = "DateTime")] System.Nullable<System.DateTime> eP251TimeUTC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MsgDelayLowLatency", DbType = "Int")] System.Nullable<int> msgDelayLowLatency, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HopCount", DbType = "Int")] System.Nullable<int> hopCount, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ChannelReliability", DbType = "Decimal(12,3)")] System.Nullable<decimal> channelReliability, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Bit")] System.Nullable<bool> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nodeDBId, gatewayDBId, timestampUTC, updateTimeUTC, eP251TimeUTC, msgDelayLowLatency, hopCount, channelReliability, status);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_UpdateNodeTimeSeries252")]
        public int NMS_UpdateNodeTimeSeries252([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NodeDBId", DbType = "BigInt")] System.Nullable<long> nodeDBId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GatewayDBId", DbType = "BigInt")] System.Nullable<long> gatewayDBId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TimestampUTC", DbType = "DateTime")] System.Nullable<System.DateTime> timestampUTC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UpdateTimeUTC", DbType = "DateTime")] System.Nullable<System.DateTime> updateTimeUTC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EP252TimeUTC", DbType = "DateTime")] System.Nullable<System.DateTime> eP252TimeUTC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MsgDelayLowLatency", DbType = "Int")] System.Nullable<int> msgDelayLowLatency, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RSSI", DbType = "Int")] System.Nullable<int> rSSI, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HopCount", DbType = "Int")] System.Nullable<int> hopCount, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Bit")] System.Nullable<bool> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nodeDBId, gatewayDBId, timestampUTC, updateTimeUTC, eP252TimeUTC, msgDelayLowLatency, rSSI, hopCount, status);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_UpdateNodeTimeSeries253")]
        public int NMS_UpdateNodeTimeSeries253(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NodeDBId", DbType = "BigInt")] System.Nullable<long> nodeDBId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GatewayDBId", DbType = "BigInt")] System.Nullable<long> gatewayDBId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TimestampUTC", DbType = "DateTime")] System.Nullable<System.DateTime> timestampUTC,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UpdateTimeUTC", DbType = "DateTime")] System.Nullable<System.DateTime> updateTimeUTC,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EP253TimeUTC", DbType = "DateTime")] System.Nullable<System.DateTime> eP253TimeUTC,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Mode", DbType = "VarChar(50)")] string mode,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Voltage", DbType = "Decimal(12,3)")] System.Nullable<decimal> voltage,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MaxBufferUsage", DbType = "Decimal(12,3)")] System.Nullable<decimal> maxBufferUsage,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LinkQuality", DbType = "Decimal(12,3)")] System.Nullable<decimal> linkQuality,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HopCount", DbType = "Int")] System.Nullable<int> hopCount,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MsgDelayLowLatency", DbType = "Int")] System.Nullable<int> msgDelayLowLatency,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsRouter", DbType = "TinyInt")] System.Nullable<byte> isRouter,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AutoRole", DbType = "Bit")] System.Nullable<bool> autoRole,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BlackListedRadioChannelsCount", DbType = "Int")] System.Nullable<int> blackListedRadioChannelsCount,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MemAllocFails", DbType = "Int")] System.Nullable<int> memAllocFails,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Bit")] System.Nullable<bool> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nodeDBId, gatewayDBId, timestampUTC, updateTimeUTC, eP253TimeUTC, mode, voltage, maxBufferUsage, linkQuality, hopCount, msgDelayLowLatency, isRouter, autoRole, blackListedRadioChannelsCount, memAllocFails, status);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_UpdateNodeTimeSeries254")]
        public int NMS_UpdateNodeTimeSeries254([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NodeDBId", DbType = "BigInt")] System.Nullable<long> nodeDBId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GatewayDBId", DbType = "BigInt")] System.Nullable<long> gatewayDBId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TimestampUTC", DbType = "DateTime")] System.Nullable<System.DateTime> timestampUTC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UpdateTimeUTC", DbType = "DateTime")] System.Nullable<System.DateTime> updateTimeUTC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EP254TimeUTC", DbType = "DateTime")] System.Nullable<System.DateTime> eP254TimeUTC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Mode", DbType = "VarChar(50)")] string mode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FirmwareVersion", DbType = "VarChar(50)")] string firmwareVersion, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ScratchpadSequence", DbType = "Int")] System.Nullable<int> scratchpadSequence, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HopCount", DbType = "Int")] System.Nullable<int> hopCount, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MsgDelayLowLatency", DbType = "Int")] System.Nullable<int> msgDelayLowLatency, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Bit")] System.Nullable<bool> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nodeDBId, gatewayDBId, timestampUTC, updateTimeUTC, eP254TimeUTC, mode, firmwareVersion, scratchpadSequence, hopCount, msgDelayLowLatency, status);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_CheckAndCreateNodeIDWithGatewayDBId")]
        public ISingleResult<NMS_CheckAndCreateNodeIDWithGatewayDBIdResult> NMS_CheckAndCreateNodeIDWithGatewayDBId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GatewayDBId", DbType = "BigInt")] System.Nullable<long> gatewayDBId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NodeId", DbType = "VarChar(50)")] string nodeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gatewayDBId, nodeId);
            return ((ISingleResult<NMS_CheckAndCreateNodeIDWithGatewayDBIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_UpdateNodeTimeSeriesStatus")]
        public int NMS_UpdateNodeTimeSeriesStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NodeDBId", DbType = "BigInt")] System.Nullable<long> nodeDBId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TimestampUTC", DbType = "DateTime")] System.Nullable<System.DateTime> timestampUTC, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Bit")] System.Nullable<bool> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nodeDBId, timestampUTC, status);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_UpdateGateway")]
        public int NMS_UpdateGateway([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GatewayName", DbType = "VarChar(50)")] string gatewayName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FeederId", DbType = "BigInt")] System.Nullable<long> feederId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DTRId", DbType = "BigInt")] System.Nullable<long> dTRId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SDOId", DbType = "BigInt")] System.Nullable<long> sDOId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Lat", DbType = "Decimal(12,9)")] System.Nullable<decimal> lat, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Long", DbType = "Decimal(12,9)")] System.Nullable<decimal> @long, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Bit")] System.Nullable<bool> status, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Timestamp", DbType = "DateTime")] System.Nullable<System.DateTime> timestamp, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GatewayId", DbType = "VarChar(50)")] string gatewayId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MacAddress", DbType = "VarChar(50)")] string macAddress, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IPv4Address", DbType = "VarChar(200)")] string iPv4Address, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IPv6Address", DbType = "VarChar(200)")] string iPv6Address, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InterfaceType", DbType = "VarChar(50)")] string interfaceType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DeviceId", DbType = "VarChar(50)")] string deviceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gatewayName, feederId, dTRId, sDOId, lat, @long, status, timestamp, gatewayId, macAddress, iPv4Address, iPv6Address, interfaceType, deviceId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_UpdateGateWayBatteryFlag")]
        public int NMS_UpdateGateWayBatteryFlag([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GatewayId", DbType = "VarChar(50)")] string gatewayId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "OnBattery", DbType = "Bit")] System.Nullable<bool> onBattery)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gatewayId, onBattery);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_GetAllNodes")]
        public ISingleResult<NMS_GetAllNodesResult> NMS_GetAllNodes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<NMS_GetAllNodesResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.NMS_BulkUpdateNodeTimeSeriesStatus")]
        public int NMS_BulkUpdateNodeTimeSeriesStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Nodes", DbType = "Xml")] System.Xml.Linq.XElement nodes)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), nodes);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class NMS_GetNodeTimeSeriesActiveStatusResult
    {

        private long _Id;

        private System.Nullable<long> _NodeDBId;

        private System.Nullable<System.DateTime> _TimestampUTC;

        private System.Nullable<decimal> _LinkQuality;

        private System.Nullable<decimal> _MaxBufferUsage;

        private string _Mode;

        private System.Nullable<int> _RSSI;

        private System.Nullable<bool> _Status;

        private System.Nullable<byte> _IsRouter;

        private System.Nullable<bool> _AutoRole;

        private System.Nullable<int> _MsgDelayLowLatency;

        private System.Nullable<System.DateTime> _EP251TimeUTC;

        private System.Nullable<System.DateTime> _EP252TimeUTC;

        private System.Nullable<System.DateTime> _EP253TimeUTC;

        private System.Nullable<System.DateTime> _EP254TimeUTC;

        private System.DateTime _CreateTimeUTC;

        private System.DateTime _UpdateTimeUTC;

        private string _FirmwareVersion;

        private System.Nullable<long> _ScratchpadSequence;

        private System.Nullable<long> _ParentNodeDBId;

        private System.Nullable<int> _HopCount;

        public NMS_GetNodeTimeSeriesActiveStatusResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "BigInt NOT NULL")]
        public long Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NodeDBId", DbType = "BigInt")]
        public System.Nullable<long> NodeDBId
        {
            get
            {
                return this._NodeDBId;
            }
            set
            {
                if ((this._NodeDBId != value))
                {
                    this._NodeDBId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TimestampUTC", DbType = "DateTime")]
        public System.Nullable<System.DateTime> TimestampUTC
        {
            get
            {
                return this._TimestampUTC;
            }
            set
            {
                if ((this._TimestampUTC != value))
                {
                    this._TimestampUTC = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LinkQuality", DbType = "Decimal(12,3)")]
        public System.Nullable<decimal> LinkQuality
        {
            get
            {
                return this._LinkQuality;
            }
            set
            {
                if ((this._LinkQuality != value))
                {
                    this._LinkQuality = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaxBufferUsage", DbType = "Decimal(12,3)")]
        public System.Nullable<decimal> MaxBufferUsage
        {
            get
            {
                return this._MaxBufferUsage;
            }
            set
            {
                if ((this._MaxBufferUsage != value))
                {
                    this._MaxBufferUsage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Mode", DbType = "VarChar(50)")]
        public string Mode
        {
            get
            {
                return this._Mode;
            }
            set
            {
                if ((this._Mode != value))
                {
                    this._Mode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RSSI", DbType = "Int")]
        public System.Nullable<int> RSSI
        {
            get
            {
                return this._RSSI;
            }
            set
            {
                if ((this._RSSI != value))
                {
                    this._RSSI = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "Bit")]
        public System.Nullable<bool> Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this._Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsRouter", DbType = "TinyInt")]
        public System.Nullable<byte> IsRouter
        {
            get
            {
                return this._IsRouter;
            }
            set
            {
                if ((this._IsRouter != value))
                {
                    this._IsRouter = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AutoRole", DbType = "Bit")]
        public System.Nullable<bool> AutoRole
        {
            get
            {
                return this._AutoRole;
            }
            set
            {
                if ((this._AutoRole != value))
                {
                    this._AutoRole = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MsgDelayLowLatency", DbType = "Int")]
        public System.Nullable<int> MsgDelayLowLatency
        {
            get
            {
                return this._MsgDelayLowLatency;
            }
            set
            {
                if ((this._MsgDelayLowLatency != value))
                {
                    this._MsgDelayLowLatency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EP251TimeUTC", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EP251TimeUTC
        {
            get
            {
                return this._EP251TimeUTC;
            }
            set
            {
                if ((this._EP251TimeUTC != value))
                {
                    this._EP251TimeUTC = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EP252TimeUTC", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EP252TimeUTC
        {
            get
            {
                return this._EP252TimeUTC;
            }
            set
            {
                if ((this._EP252TimeUTC != value))
                {
                    this._EP252TimeUTC = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EP253TimeUTC", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EP253TimeUTC
        {
            get
            {
                return this._EP253TimeUTC;
            }
            set
            {
                if ((this._EP253TimeUTC != value))
                {
                    this._EP253TimeUTC = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EP254TimeUTC", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EP254TimeUTC
        {
            get
            {
                return this._EP254TimeUTC;
            }
            set
            {
                if ((this._EP254TimeUTC != value))
                {
                    this._EP254TimeUTC = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreateTimeUTC", DbType = "DateTime NOT NULL")]
        public System.DateTime CreateTimeUTC
        {
            get
            {
                return this._CreateTimeUTC;
            }
            set
            {
                if ((this._CreateTimeUTC != value))
                {
                    this._CreateTimeUTC = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UpdateTimeUTC", DbType = "DateTime NOT NULL")]
        public System.DateTime UpdateTimeUTC
        {
            get
            {
                return this._UpdateTimeUTC;
            }
            set
            {
                if ((this._UpdateTimeUTC != value))
                {
                    this._UpdateTimeUTC = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FirmwareVersion", DbType = "VarChar(50)")]
        public string FirmwareVersion
        {
            get
            {
                return this._FirmwareVersion;
            }
            set
            {
                if ((this._FirmwareVersion != value))
                {
                    this._FirmwareVersion = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ScratchpadSequence", DbType = "BigInt")]
        public System.Nullable<long> ScratchpadSequence
        {
            get
            {
                return this._ScratchpadSequence;
            }
            set
            {
                if ((this._ScratchpadSequence != value))
                {
                    this._ScratchpadSequence = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ParentNodeDBId", DbType = "BigInt")]
        public System.Nullable<long> ParentNodeDBId
        {
            get
            {
                return this._ParentNodeDBId;
            }
            set
            {
                if ((this._ParentNodeDBId != value))
                {
                    this._ParentNodeDBId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HopCount", DbType = "Int")]
        public System.Nullable<int> HopCount
        {
            get
            {
                return this._HopCount;
            }
            set
            {
                if ((this._HopCount != value))
                {
                    this._HopCount = value;
                }
            }
        }
    }

    public partial class NMS_CheckAndCreateNodeIDWithGatewayDBIdResult
    {

        private long _Id;

        private System.Nullable<long> _MeterId;

        private string _NodeId;

        private string _AddressHEX;

        private System.Nullable<long> _GatewayDBId;

        private System.Nullable<bool> _IsSink;

        private string _ApplicationVersion;

        private string _HardwareVersion;

        private System.Nullable<decimal> _Lat;

        private System.Nullable<decimal> _Long;

        public NMS_CheckAndCreateNodeIDWithGatewayDBIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "BigInt NOT NULL")]
        public long Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MeterId", DbType = "BigInt")]
        public System.Nullable<long> MeterId
        {
            get
            {
                return this._MeterId;
            }
            set
            {
                if ((this._MeterId != value))
                {
                    this._MeterId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NodeId", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string NodeId
        {
            get
            {
                return this._NodeId;
            }
            set
            {
                if ((this._NodeId != value))
                {
                    this._NodeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AddressHEX", DbType = "VarChar(50)")]
        public string AddressHEX
        {
            get
            {
                return this._AddressHEX;
            }
            set
            {
                if ((this._AddressHEX != value))
                {
                    this._AddressHEX = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GatewayDBId", DbType = "BigInt")]
        public System.Nullable<long> GatewayDBId
        {
            get
            {
                return this._GatewayDBId;
            }
            set
            {
                if ((this._GatewayDBId != value))
                {
                    this._GatewayDBId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsSink", DbType = "Bit")]
        public System.Nullable<bool> IsSink
        {
            get
            {
                return this._IsSink;
            }
            set
            {
                if ((this._IsSink != value))
                {
                    this._IsSink = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ApplicationVersion", DbType = "VarChar(50)")]
        public string ApplicationVersion
        {
            get
            {
                return this._ApplicationVersion;
            }
            set
            {
                if ((this._ApplicationVersion != value))
                {
                    this._ApplicationVersion = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HardwareVersion", DbType = "VarChar(50)")]
        public string HardwareVersion
        {
            get
            {
                return this._HardwareVersion;
            }
            set
            {
                if ((this._HardwareVersion != value))
                {
                    this._HardwareVersion = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Lat", DbType = "Decimal(12,9)")]
        public System.Nullable<decimal> Lat
        {
            get
            {
                return this._Lat;
            }
            set
            {
                if ((this._Lat != value))
                {
                    this._Lat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Long", DbType = "Decimal(12,9)")]
        public System.Nullable<decimal> Long
        {
            get
            {
                return this._Long;
            }
            set
            {
                if ((this._Long != value))
                {
                    this._Long = value;
                }
            }
        }
    }

    public partial class NMS_GetAllNodesResult
    {

        private long _Id;

        private string _NodeId;

        public NMS_GetAllNodesResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "BigInt NOT NULL")]
        public long Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NodeId", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string NodeId
        {
            get
            {
                return this._NodeId;
            }
            set
            {
                if ((this._NodeId != value))
                {
                    this._NodeId = value;
                }
            }
        }
    }
}
#pragma warning restore 1591
