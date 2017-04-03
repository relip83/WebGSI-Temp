using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
    public class FertilizerSettingView
    {
        public byte OutputNumber { get; set; }
        public bool ContinuousFert { get; set; }
        public decimal PulseSize { get; set; }
        public decimal PulseTime { get; set; }
        public int FerlizerFaillureTime { get; set; }
        public int Leakage { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FertilizerType FertilizerType { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Fertilizer_PulseType PulseTypeID { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Fertilizer_FlowType FlowTypeID { get; set; }
        public bool IsEnabled { set; get; }
        public decimal NominalFlow { get; set; }


        public FertilizerSettingView()
        {

        }

        public FertilizerSettingView(FertilizerSetting f)
        {
            if (f == null)
                return;
            OutputNumber = f.OutputNumber;
            ContinuousFert = f.ContinuousFert;
            PulseSize = f.PulseSize;
            PulseTime = f.PulseTime;
            FerlizerFaillureTime = f.FerlizerFaillureTime;
            Leakage = f.Leakage;
            FertilizerType = (FertilizerType)f.TypeID;
            PulseTypeID = (Fertilizer_PulseType)f.PulseTypeID;
            FlowTypeID = (Fertilizer_FlowType)f.FlowTypeID;
            IsEnabled = f.IsEnabled;
            NominalFlow = f.NominalFlow;

        }


    }
}
