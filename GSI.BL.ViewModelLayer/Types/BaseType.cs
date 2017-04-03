using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types
{
    public class BaseType
    {
        public int TypeID { get; set; }
        public string Name { get; set; }

       
            public static IEnumerable<BaseType> GetValues<T>()
            {
                foreach (var item in System.Enum.GetValues(typeof(T)))
                {
                    yield return new BaseType() { Name = item.ToString(), TypeID = (int)item };
                
                };
            }
        
    }
}
