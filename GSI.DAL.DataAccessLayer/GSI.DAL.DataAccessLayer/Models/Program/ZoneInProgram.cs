using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program
{
    public class ZoneInProgram
    {
        public byte ZoneNumber { get; set; }
        public long ProgramID { get; set; }
        public long ID { get; set; }
        public decimal FertQuant { get; set; }
        public int FertTime { get; set; }
        public int WaterBefore { get; set; }
        public int WaterAfter { get; set; }
        public int OrderNumber { get; set; }
        public int WaterDuration { get; set; }
        public int WaterQuantity { get; set; }
        public decimal WaterPrecipitation_Quantity { get; set; }
        public decimal WaterPrecipitation_Duration { get; set; }
    }
}
