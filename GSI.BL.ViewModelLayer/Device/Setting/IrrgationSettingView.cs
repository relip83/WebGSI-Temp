using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
    public class IrrgationSettingView
    {
        public DateTime[] RestrictedDates { get; set; }
        public bool IsLocalSequenceActive { get; set; }
        public bool ProgramsAsQueue { get; set; }


        public IrrgationSettingView()
        {

        }


        public IrrgationSettingView (RestrictedDates[] items , MainPipeSettings pipe)
        {
            if (items == null)
                return;
            RestrictedDates = items.Select(d => d.ExceptionDate).ToArray();
            IsLocalSequenceActive = pipe.IsLocalSequenceActive;
            ProgramsAsQueue = pipe.ProgramsAsQueue;
        }

    }
}
