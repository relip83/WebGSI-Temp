using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galcon.GSI.Systems.GSI;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Setting
{
    public class ViewModelLayerSettings
    {
        public Func<GSI.DAL.DataAccessLayer.Repositories.AdminRepository.IAdminRepository> AdminRepositoryFunc { get; set; }
        public ViewModelLayerSettings()
        {

        }

    }
}
