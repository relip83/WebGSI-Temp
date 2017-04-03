using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device.Setting
{
    public class CommunicationSettingsView
    {
        public int[] Connection { get; set; }
        public bool IsCommWindow { get; set; }
        public int ModemStartTime { get; set; }
        public int ModemEndTime { get; set; }
        public CommIntervalType CommIntervalType { get; set; }
    }

}
