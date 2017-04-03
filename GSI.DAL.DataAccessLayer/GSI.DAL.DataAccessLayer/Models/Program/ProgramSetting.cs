using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program
{
    public class ProgramSetting
    {
        public long ProgramID { get; set; }
        public long ConfigID { get; set; }
        public string Name { get; set; }
        public byte Priority { get; set; }
        public byte StatusID { get; set; }
        public byte DaysExecutionsTypeID { get; set; }
        public byte WaterUnitID { get; set; }
        public byte HoursExecutionsTypeID { get; set; }
        public byte FertUnitID { get; set; }
        public DateTime? FinalStartDateLimit { get; set; }
        public DateTime? FinalEndDateLimit { get; set; }
        public int FinalStartHoursLimit { get; set; }
        public int FinalStopHoursLimit { get; set; }
        public int HourlyCyclesStartTime { get; set; }
        public int HourlyCyclesStopTime { get; set; }
        public byte ProgramNumber { get; set; }
        public bool IsFertilizerON { get; set; }
        public byte HourlyCyclesPerDay { get; set; }
        public int HourlyCycleTime { get; set; }
        public byte WaterFactor { get; set; }
    }
}
