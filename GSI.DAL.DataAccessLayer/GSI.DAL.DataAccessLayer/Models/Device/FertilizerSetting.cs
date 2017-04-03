using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device
{
    public class FertilizerSetting
    {
        public byte OutputNumber { get; set; }
        public long FertilizerID { get; set; }
        public bool ContinuousFert { get; set; }
        public decimal PulseSize { get; set; }
        public decimal PulseTime { get; set; }
        public int FerlizerFaillureTime { get; set; }
        public int Leakage { get; set; }
        public byte TypeID { get; set; }
        public byte PulseTypeID { get; set; }
        public byte FlowTypeID { get; set; }
        public bool IsEnabled { set; get; }
        public decimal NominalFlow { get; set; }
    }
}
