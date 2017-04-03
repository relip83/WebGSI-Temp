using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
    public class ValidDaysView
    {
        public DayIndex[] Days { get; set; }

        public ValidDaysView()
        {
                
        }

        public ValidDaysView(MainPipeSettings setting)
        {
            if (setting == null)
                return;
            Days = Enumerable.Range(0,7).Select(s=> new DayIndex() { Index = s }).ToArray();
            Days[0].IsEnabled = setting.DSundayState;
            Days[1].IsEnabled = setting.DMondayState;
            Days[2].IsEnabled = setting.DTuesdayState;
            Days[3].IsEnabled = setting.DWednesdayState;
            Days[4].IsEnabled = setting.DThursdayState;
            Days[5].IsEnabled = setting.DFridayState;
            Days[6].IsEnabled = setting.DSaturdayState;
        }

        public ValidDaysView(WeeklyProgramSetting setting)
        {
            if (setting == null)
                return;
            Days = Enumerable.Range(0, 7).Select(s => new DayIndex() { Index = s }).ToArray();
            Days[0].IsEnabled = setting.Sunday;
            Days[1].IsEnabled = setting.Monday;
            Days[2].IsEnabled = setting.Tuesday;
            Days[3].IsEnabled = setting.Wednesday;
            Days[4].IsEnabled = setting.Thursday;
            Days[5].IsEnabled = setting.Friday;
            Days[6].IsEnabled = setting.Saturday;
        }

    }

 
}
