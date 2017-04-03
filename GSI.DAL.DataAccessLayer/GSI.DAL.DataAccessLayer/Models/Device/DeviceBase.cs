using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device
{
    public  class DeviceBase
    {
        public string Name { get; set; }
        public string SN { get; set; }
        public long DeviceID { get; set; }

        public int Type { get; set; }
        public DateTime CreationDate { get; set; }
        public long CurrentConfigID { get; set; }
      
    }
}
