using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program
{
    public class WeeklyProgramSetting
    {
        public long ProgramID { set; get; }
        public bool Sunday { set; get; }
        public bool Monday { set; get; }
        public bool Tuesday { set; get; }
        public bool Wednesday { set; get; }
        public bool Thursday { set; get; }
        public bool Friday { set; get; }
        public bool Saturday { set; get; }

    }
}
