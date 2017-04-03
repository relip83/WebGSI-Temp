using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device;
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
    public class ProgramSettingView
    {
       
        public string Name { get; set; }
        public long ProgramID { get; set; }
        public byte ProgramNumber { get; set; }
        public byte Priority { get; set; }
        public long ConfigID { get; set; }
        public bool IsFertilizerON { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType Status { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public WaterProgramType ProgramType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FertExecutionType? FertExecutionType { get; set; }
        public byte WaterFactor { get; set; }

        public ValidDaysView WeeklyDaysProgram { get; set; }

        public int[] ExecutionHours { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HoursExecutionType HoursExecution { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DaysExecutionType DaysExecutionType { get; set; }

        public CyclicDayProgramView CyclicDayProgram { set; get; }

        public HourlyCycleView HourlyCycle { set; get; }

        public AdvancedProgramSettingView Advanced { set; get; }

        public ZoneInProgramView[] ZoneInProgram { set; get; }


        public ProgramSettingView()
        {

        }

        public ProgramSettingView(ProgramSetting setting,
                                  WeeklyProgramSetting Weeklysetting,
                                  CyclicDayProgram cSetting,
                                  ExecutionHours[] executionHours,
                                  ZoneInProgram[] zones)
        {
            Name = setting.Name;
            ProgramType = (WaterProgramType)setting.WaterUnitID;
            ProgramID = setting.ProgramID;
            ProgramNumber = setting.ProgramNumber;
            Priority = setting.Priority;
            ConfigID = setting.ConfigID;
            IsFertilizerON = setting.IsFertilizerON;
            Status = (StatusType)setting.StatusID;
            FertExecutionType = (FertExecutionType)setting.FertUnitID;
            WaterFactor = setting.WaterFactor;
            HoursExecution = (HoursExecutionType)setting.HoursExecutionsTypeID;
            DaysExecutionType = (DaysExecutionType)setting.DaysExecutionsTypeID;
            Advanced = new AdvancedProgramSettingView()
            {
                FinalEndDateLimit = setting.FinalEndDateLimit.HasValue ? (long?)TimeConvertor.GetTicks(setting.FinalEndDateLimit.Value) : null,
                FinalStartDateLimit = setting.FinalStartDateLimit.HasValue ? (long?)TimeConvertor.GetTicks(setting.FinalStartDateLimit.Value) : null,
                FinalStartHoursLimit = setting.FinalStartHoursLimit,
                FinalStopHoursLimit = setting.FinalStopHoursLimit
            };
            HourlyCycle = new HourlyCycleView()
            {
                HourlyCyclesPerDay = setting.HourlyCyclesPerDay,
                HourlyCyclesStartTime = setting.HourlyCyclesStartTime,
                HourlyCycleTime = setting.HourlyCycleTime,
                HourlyCyclesStopTime= setting.FinalStopHoursLimit
            };
            WeeklyDaysProgram = new ValidDaysView(Weeklysetting);
            CyclicDayProgram = new CyclicDayProgramView(cSetting);
            ExecutionHours = executionHours.Select(e=>e.ExecTime).ToArray();
            ZoneInProgram = zones.Select(z => new ZoneInProgramView(z)).ToArray();
        }


    }
}
