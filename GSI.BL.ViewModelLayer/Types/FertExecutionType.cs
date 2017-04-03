using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types
{
    public enum FertExecutionType
    {
        Quantity_ByLiters = 1,
        ByDuration_Seconds = 2,
        Quantity_ByLitersPerCube = 3,
        Quantity_ByLitersInProgramSpread = 4,
        ByPercent = 5,
        Quantity_ByGallons = 6,
        Quantity_ByTHGallons = 7,
        Quantity_GalconSpread = 8,
        NotAvailable = 9
    }
}
