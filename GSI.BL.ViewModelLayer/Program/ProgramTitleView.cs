using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program
{
    public class ProgramTitleView
    {
        public string Name { get; set; }
        public long ID { get; set; }
        public int OrderNum { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType Status { get; set; }

        public ProgramTitleView(ProgramSetting p )
        {
            Name = p.Name;
            ID = p.ProgramID;
            OrderNum = p.ProgramNumber;
            Status = (StatusType)p.StatusID;
        }
    }
}
