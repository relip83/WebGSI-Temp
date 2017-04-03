using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types
{
    public enum WaterMeter_FlowType
    {
        LPH = 1,    //// LiterPerHour

        LPH_AS_M3PH_X10 = 2,
        LPH_AS_M3PH_X100 = 3, 
        M3PH = 11,  //// CubicLiterPerHour

        GPM = 20,   //// GallonPerMinute
        GPH = 21   ////  THGallonPerHour
    }
}
