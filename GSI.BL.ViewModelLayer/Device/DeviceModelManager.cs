using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Zone;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Zone;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device
{
    public class DeviceModelManager : BaseViewModelManager
    {
        private Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Repositories.AdminRepository.IAdminRepository _AdminRepository = null;

        #region ctor


        public DeviceModelManager(ViewModelLayer.Setting.ViewModelLayerSettings currentSettings)
            : base(currentSettings)
        {
            _AdminRepository = currentSettings.AdminRepositoryFunc();
        }

        #endregion 

        #region ZoneSetting
        public ZoneSettingView[] GetZoneSetting(string SN)
        {
            return _AdminRepository.DeviceZoneSetting_Get(SN).Select(z => new ZoneSettingView(z)).ToArray();
        }

        public bool SaveZoneSetting(string SN, BL.ViewModelLayer.Zone.ZoneSettingView[] _ZoneSettingList)
        {
            var result = false;
            foreach (var item in _ZoneSettingList)
            {
                var item_toDB = new ZoneSetting()
                {
                    Name = item.Name,
                    OutputNumber = item.OutputNumber,
                    StatusID = (byte)item.Status,
                    LowFlowDeviation = item.LowFlowDeviation,
                    LowFlowFaultDelay = item.LowFlowDelay,
                    HighFlowDeviation = item.HighFlowDeviation,
                    HighFlowFaultDelay = item.HighFlowDelay,
                    TimeFillDelay = item.LineFillTime,
                    FertilizerConnected = item.FertilizerConnected,
                    TypeID = (byte)item.TypeID,
                    LastFlow = item.LastFlow,
                    LastFlow_FlowTypeID = item.LastFlow_FlowTypeID,
                    LastFlow_Date = item.LastFlow_Date,
                    PrecipitationRate = item.PrecipitationRate,
                    ZoneColor = item.ZoneColor,
                    IrrigrationArea = item.IrrigrationArea,
                    SetupNominalFlow = item.SetupNominalFlow,
                    StopOnFertFailure = item.StopOnFertFailure,
                };

                result = _AdminRepository.ZoneSetting_Update(SN, item_toDB);

                if (!result)
                    return result;
            }
            return true;
        }

        #endregion

        #region Settings
        public bool SaveSettings(string SN, SettingsView setting)
        {
            var result = false;

            #region  GeneralSettings

            if (setting.AdvancedSettings != null
                && setting.DeviceInfo != null
                && setting.GeneralSetting != null)
            {

                var gSetting = new GeneralSettings()
                {
                    IsMetric = setting.AdvancedSettings.IsMetric,
                    LandTypeID = (byte)setting.AdvancedSettings.LandType,
                    Name = setting.DeviceInfo.Name,
                    Status = (byte)setting.GeneralSetting.Status,
                    CustomTimeZoneID = setting.GeneralSetting.TimeZoneID

                };
                result = _AdminRepository.GeneralSettings_Update(SN, gSetting);

                if (!result)
                    return result;
            }
            #endregion

            #region MainPipe

            if (setting.RainSensor != null
                && setting.IrrgationSetting != null
                && setting.GeneralSetting != null)
            {
                var mainpSetting = new MainPipeSettings()
                {
                    RainDetectorEnabled = setting.RainSensor.Active,
                    RainyDaysStopper = setting.RainSensor.SuspendDays,
                    RainDetectorNC = setting.RainSensor.NC,
                    IsLocalSequenceActive = setting.IrrgationSetting.IsLocalSequenceActive,
                    ProgramsAsQueue = setting.IrrgationSetting.ProgramsAsQueue,
                    UseMaster = setting.GeneralSetting.UseMaster
                };

                var days = setting.GeneralSetting.ValidDays;
                if (days != null && days.Days != null)
                {
                    mainpSetting.DSundayState = days.Days[0].IsEnabled;
                    mainpSetting.DMondayState = days.Days[1].IsEnabled;
                    mainpSetting.DTuesdayState = days.Days[2].IsEnabled;
                    mainpSetting.DWednesdayState = days.Days[3].IsEnabled;
                    mainpSetting.DThursdayState = days.Days[4].IsEnabled;
                    mainpSetting.DFridayState = days.Days[5].IsEnabled;
                    mainpSetting.DSaturdayState = days.Days[6].IsEnabled;
                }

                result = _AdminRepository.MainPipeSettings_Update(SN, mainpSetting);

                if (!result)
                    return result;
            }


            #endregion

            #region Fertilizer

            if (setting.Fertilizer != null)
            {
                var fertSetting = new FertilizerSetting()
                {
                    OutputNumber = setting.Fertilizer.OutputNumber,
                    TypeID = (byte)setting.Fertilizer.FertilizerType,
                    ContinuousFert = setting.Fertilizer.ContinuousFert,
                    PulseSize = setting.Fertilizer.PulseSize,
                    PulseTime = setting.Fertilizer.PulseTime,
                    FerlizerFaillureTime = setting.Fertilizer.FerlizerFaillureTime,
                    Leakage = setting.Fertilizer.Leakage,
                    PulseTypeID = (byte)setting.Fertilizer.PulseTypeID,
                    FlowTypeID = (byte)setting.Fertilizer.FlowTypeID,
                    IsEnabled = setting.Fertilizer.IsEnabled,
                    NominalFlow = setting.Fertilizer.NominalFlow
                };
                result = _AdminRepository.FertilizerSetting_Update(SN, fertSetting);

                if (!result)
                    return result;
            }
            #endregion

            #region WaterMeter

            if (setting.WaterMeter != null)
            {

                var wMeterSetting = new WaterMeterSetting()
                {
                    MeterTypeID = (byte)setting.WaterMeter.MeterType,
                    PulseSize = setting.WaterMeter.PulseSize,
                    IsEnabled = setting.WaterMeter.IsEnabled,
                    PulseTypeID = (byte)setting.WaterMeter.PulseTypeID,
                    FlowTypeID = (byte)setting.WaterMeter.FlowTypeID,
                    NoWaterPulseDelay = setting.WaterMeter.NoWaterPulseDelay,
                    LeakageLimit = setting.WaterMeter.LeakageLimit
                };

                result = _AdminRepository.WaterMeterSetting_Update(SN, wMeterSetting);

                if (!result)
                    return result;
            }
            #endregion

            #region IrrExceptionDates

            if (setting.IrrgationSetting.RestrictedDates != null)
            {
                RestrictedDates[] IrrExceptionDates = setting.IrrgationSetting.RestrictedDates.Select(d => new RestrictedDates() { ExceptionDate = d }).ToArray();
                result = _AdminRepository.IrrExceptionDates_Update(SN, IrrExceptionDates);

                if (!result)
                    return result;
            }

            #endregion

            return result;

        }
        public SettingsView GetSettingsView(String SN)
        {
            var pipe = _AdminRepository.MainPipeSettings_Get(SN);
            var gSetting = _AdminRepository.GeneralSettings_Get(SN);
            SettingsView s = new SettingsView()
            {
                AdvancedSettings = new AdvancedSettingsView(gSetting),
                DeviceInfo = new DeviceInfoView(gSetting),
                Fertilizer = new FertilizerSettingView(_AdminRepository.FertilizerSetting_Get(SN)),
                IrrgationSetting = new IrrgationSettingView(_AdminRepository.IrrExceptionDates_Get(SN), pipe),
                RainSensor = new RainSensorView(pipe),
                WaterMeter = new WaterMeterSettingView(_AdminRepository.WaterMeterSetting_Get(SN)),
                GeneralSetting = new GeneralSettingView(gSetting, pipe),
                Types = DeviceTypes.DeviceTypesSetting

            };
            return s;
        }

        #endregion

        #region Alert Settings
        public AlertSettingsView[] GetAlertsSettings(string SN, bool SendDefaults)
        {
            return _AdminRepository.AlertSettings_Get(SN).Select(u => new AlertSettingsView(u, SendDefaults)).ToArray();
        }

        public bool SaveAlertsSettings(string SN, DeviceAlertSettingsView[] Alertlist)
        {
            var result = false;

            foreach (var item in Alertlist)
            {
                result = _AdminRepository.AlertSettings_Update(SN, new AlertsSetting()
                {
                    AlertCode = item.AlertCode,
                    IsEnable = item.IsEnable,
                    SendEmail = item.SendEmail,
                    SendSMS = item.SendSMS
                });
                if (!result)
                    return result;
            }

            return result;
        }

        #endregion

        #region ProgramList

        public ProgramTitleView[] GetProgramList(string sN)
        {
            return _AdminRepository.DevicePrograms_Get(sN).Select(p => new ProgramTitleView(p)).ToArray();
        }

        protected override void OnDispose()
        {
        }

        #endregion
    }
}
