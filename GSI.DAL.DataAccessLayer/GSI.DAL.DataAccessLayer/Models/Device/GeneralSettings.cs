using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device
{
    public class GeneralSettings 
    {
        /**
        * From Table DeviceMain
        * */

        public string Name { get; set; }
        public string SN { get; set; }
        public long DeviceID { get; set; }

        public DateTime CreationDate { get; set; }
        public long CurrentConfigID { get; set; }

        public int ModelID { set; get; }

        public int CustomTimeZoneID { set; get; }

        public bool IsMetric { get; set; }

        public byte LandTypeID { get; set; }

        public bool Flag_AdvancedFert { get; set; }

        /**
        * From Table ConfigManager
        * */
        public byte Status { set; get; }


        /**
        * From Table DeviceModels
        * */
        public byte ZonesNumbers { set; get; }

    }
}
