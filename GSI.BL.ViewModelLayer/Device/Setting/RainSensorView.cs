using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device.Setting
{
    public class RainSensorView
    {
        public bool Active { get; set; }
        public byte SuspendDays { get; set; }
        public bool NC { get; set; }

        public RainSensorView(MainPipeSettings s )
        {
            Active = s.RainDetectorEnabled;
            SuspendDays = s.RainyDaysStopper;
            NC = s.RainDetectorNC; 
        }


        public RainSensorView()
        {

        }

    }
}
