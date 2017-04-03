using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Zone;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Zone
{
    public class ZoneSettingView
    {

        public string Name { get; set; }
        public byte OutputNumber { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType Status { get; set; }

        public byte LowFlowDeviation { get; set; }   // Value is represented in % 
        public short LowFlowDelay { get; set; }       // Value is is represented in seconds ->DB and  minute -> GUI
        public byte HighFlowDeviation { get; set; }  // Value is represented in % 
        public short HighFlowDelay { get; set; }      // Value is is represented in seconds ->DB and  minute -> GUI
        public short LineFillTime { get; set; }       // Value is is represented in seconds ->DB and  minute -> GUI
        public decimal? IrrigrationArea { get; set; }
        public int ZoneColor { set; get; }

        public decimal? PrecipitationRate { get; set; }
        public decimal LastFlow { get; set; }
        public DateTime LastFlow_Date { get; set; }
        public byte LastFlow_FlowTypeID { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ZoneType TypeID { get; set; }
        public bool FertilizerConnected { get; set; }
        public decimal? SetupNominalFlow { get; set; }
        public bool StopOnFertFailure { get;  set; }

        public ZoneSettingView()
        {
        }
        public ZoneSettingView(ZoneSetting z)
        {
            Name = z.Name;
            OutputNumber = z.OutputNumber;
            Status = (StatusType)z.StatusID;
            LowFlowDeviation = z.LowFlowDeviation;
            LowFlowDelay = z.LowFlowFaultDelay;
            HighFlowDeviation = z.HighFlowDeviation;
            HighFlowDelay = z.HighFlowFaultDelay;
            LineFillTime = z.TimeFillDelay;
            FertilizerConnected = z.FertilizerConnected;
            TypeID = (ZoneType)z.TypeID;
            LastFlow = z.LastFlow;
            LastFlow_FlowTypeID = z.LastFlow_FlowTypeID;
            LastFlow_Date = z.LastFlow_Date;
            PrecipitationRate = z.PrecipitationRate;
            ZoneColor = z.ZoneColor;
            IrrigrationArea = z.IrrigrationArea;
            SetupNominalFlow = z.SetupNominalFlow;
            StopOnFertFailure = z.StopOnFertFailure;
        }


    }
}
