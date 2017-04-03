using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program
{
    public class ScheduledIrrigationView
    {

        public string Name { get; set; }
        public long ProgramID { get; set; }
        public long ConfigID { get; set; }
        public bool IsFertilizerON { get; set; }
        public long StartTicks { get { return TimeConvertor.GetTicks(StartDate); } }
        public DateTime StartDate { get; set; }
        public long EndTicks { get { return TimeConvertor.GetTicks(EndDate); } }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public int Quantity { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public WaterProgramType ProgramType { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public HoursExecutionType HoursExecutionType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DaysExecutionType DaysExecutionType { get; set; }

        public int Interval { get; set; }
        public int Interval_total { get; set; }

        public List<ZoneScheduledItemView> Zones { get; set; }

    }
}
