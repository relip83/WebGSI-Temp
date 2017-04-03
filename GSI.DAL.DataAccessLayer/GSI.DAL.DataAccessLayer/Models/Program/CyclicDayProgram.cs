using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program
{
    public class CyclicDayProgram
    {
        public long ProgramID { get; set; }
        public DateTime StartDate { get; set; }
        public byte DaysInterval { get; set; }
    }
}
