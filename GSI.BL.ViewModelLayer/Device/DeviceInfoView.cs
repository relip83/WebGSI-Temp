using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
    public class DeviceInfoView
    {
        public string SN { set; get; }

        public string Name { set; get; }
        public long ModelID { set; get; }
        public int ZonesNum { set; get; }

        public DeviceInfoView()
        {
                
        }

        public DeviceInfoView(GeneralSettings setting)
        {
            if (setting == null)
                return;
            Name = setting.Name;
            SN = setting.SN;
            ModelID = setting.ModelID;
            ZonesNum = setting.ZonesNumbers;
        }


    }
}
