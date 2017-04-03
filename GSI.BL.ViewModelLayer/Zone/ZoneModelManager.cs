using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Setting;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Zone
{
    public class ZoneModelManager : BaseViewModelManager
    {
        #region member

        private Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Repositories.AdminRepository.IAdminRepository _AdminRepository = null;

        #endregion

        #region ctor
        public ZoneModelManager(ViewModelLayerSettings currentSettings) : base(currentSettings)
        {
            _AdminRepository = currentSettings.AdminRepositoryFunc();
        }

        #endregion

        public ZoneSettingView GetZoneSetting(string sN, int zoneNumber)
        {
            return new ZoneSettingView(_AdminRepository.ZoneSetting_Get(sN, zoneNumber));
        }

        public bool SaveZoneSetting(string sN, int zoneNumber, ZoneSettingView setting)
        {
            return _AdminRepository.ZoneSetting_Update(sN, new GSI.DAL.DataAccessLayer.Models.Zone.ZoneSetting()
            {
                FertilizerConnected = setting.FertilizerConnected,
                HighFlowDeviation = setting.HighFlowDeviation,
                HighFlowFaultDelay = setting.HighFlowDelay,
                IrrigrationArea = setting.IrrigrationArea,
                LowFlowDeviation = setting.LowFlowDeviation,
                Name = setting.Name,
                OutputNumber = (byte)zoneNumber,
                PrecipitationRate = setting.PrecipitationRate,
                SetupNominalFlow = setting.SetupNominalFlow,
                StatusID = (byte)setting.Status,
                TypeID = (byte)setting.TypeID,
                TimeFillDelay = setting.LineFillTime,
                ZoneColor = setting.ZoneColor,
                StopOnFertFailure = setting.StopOnFertFailure,
                LowFlowFaultDelay = setting.LowFlowDelay
            });
        }

        protected override void OnDispose()
        {
            throw new NotImplementedException();
        }
    }
}
