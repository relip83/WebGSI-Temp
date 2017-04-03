using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program
{
    public class ZoneInProgramView
    {


        public ZoneInProgramView(ZoneInProgram z)
        {
            if (z == null)
                return;
            ZoneNumber = z.ZoneNumber;
            OrderNumber = z.OrderNumber;
            ID = z.ID;
            WaterQuantity = z.WaterQuantity;
            FertQuant = z.FertQuant;
            FertTime = z.FertTime;
            WaterBefore = z.WaterBefore;
            WaterAfter = z.WaterAfter;
            WaterDuration = z.WaterDuration;
            Precipitation_Duration = z.WaterPrecipitation_Duration;
            Precipitation_Quantity = z.WaterPrecipitation_Quantity;
        }

        public ZoneInProgramView()
        {

        }


        public byte ZoneNumber { get; set; }
        public int OrderNumber { get; set; }
        public long ID { get; set; }
        public int WaterQuantity { get; set; }
        public decimal FertQuant { get; set; }
        public int FertTime { get; set; }

        public int WaterBefore { get; set; }
        public int WaterAfter { get; set; }
        public int WaterDuration { get; set; }
        public decimal WaterPrecipitation { get; set; }
        public decimal Precipitation_Duration { get; set; }
        public decimal Precipitation_Quantity { get; set; }
    }
}
