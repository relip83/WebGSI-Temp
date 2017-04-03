using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program
{
    public class CyclicDayProgramView
    {
        public long StartDate { get; set; }
        public byte DaysInterval { get; set; }

        public CyclicDayProgramView()
        {

        }

        public CyclicDayProgramView(CyclicDayProgram setting)
        {
            if (setting == null)
                return;
            StartDate = TimeConvertor.GetTicks(setting.StartDate);
            DaysInterval = setting.DaysInterval;
        }
    }

}
