using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Device.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Setting;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Zone;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program
{
    public class ProgramModelManager : BaseViewModelManager
    {
        private Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Repositories.AdminRepository.IAdminRepository _AdminRepository = null;

        public ProgramModelManager(ViewModelLayerSettings currentSettings) : base(currentSettings)
        {
            _AdminRepository = currentSettings.AdminRepositoryFunc();
        }

        #region Privte function
        private int ConvertQuantityToDuration(double WaterQuantity, WaterMeter_FlowType? FlowType, decimal? FlowValue, WaterMeter_PulseType UnitPulseID)
        {
            if (!FlowValue.HasValue || FlowType == null)
            {
                return 1800;// 30 min
            }

            double quantPerSecond = 0;
            switch (FlowType)
            {
                case WaterMeter_FlowType.LPH:
                case WaterMeter_FlowType.LPH_AS_M3PH_X10:
                case WaterMeter_FlowType.LPH_AS_M3PH_X100:
                    quantPerSecond = ((double)FlowValue / 3600);
                    break;
                case WaterMeter_FlowType.M3PH:
                    if (UnitPulseID != WaterMeter_PulseType.M3_AS_M3)
                    {
                        quantPerSecond = ((double)(FlowValue * 1000) / 3600);
                    }
                    else
                    {
                        quantPerSecond = ((double)(FlowValue) / 3600);
                    }

                    break;
                case WaterMeter_FlowType.GPM:
                    quantPerSecond = ((double)FlowValue / 60);
                    break;
                case WaterMeter_FlowType.GPH:
                    quantPerSecond = ((double)FlowValue / 3600);
                    break;
            }
            return (int)(WaterQuantity / quantPerSecond);
        }

        private int ConvertPrecipitationToDuration(decimal? PrecipitationRate, decimal WaterPrecipitation)
        {
            if (PrecipitationRate.HasValue && WaterPrecipitation > 0)
            {
                return (int)( WaterPrecipitation *3600 /PrecipitationRate.Value);
            }
            return 0;
        }

        public double ConvertPrecipitationToQuantity(decimal? irrigationArea, decimal precipitationValue)
        {
            if (irrigationArea.HasValue && irrigationArea > 0)
            {
                return (double)(precipitationValue * irrigationArea.Value);
            }
            return 0;
        }

        public int GetDurationTime(WaterMeter_FlowType? FlowTypeID,  decimal? FlowValue, WaterMeter_PulseType PulseTypeID, WaterProgramType WaterUnitID, ZoneSetting zoneSetting, ZoneInProgram Zoneitem)
        {
            int WaterDuration_Seconds = 0;
            switch (WaterUnitID)
            {
                case WaterProgramType.Duration:
                    WaterDuration_Seconds = Zoneitem.WaterDuration;
                    break;
                case WaterProgramType.Quantity:
                    WaterDuration_Seconds = ConvertQuantityToDuration(Zoneitem.WaterQuantity, FlowTypeID, FlowValue,PulseTypeID);
                    break;
                case WaterProgramType.Quantity_Precipitation:
                    var  WaterQuantity = ConvertPrecipitationToQuantity(zoneSetting.IrrigrationArea, Zoneitem.WaterPrecipitation_Quantity);
                    WaterDuration_Seconds = ConvertQuantityToDuration(WaterQuantity, FlowTypeID, FlowValue, PulseTypeID);
                    break;
                case WaterProgramType.Duration_Precipitation:
                    WaterDuration_Seconds = ConvertPrecipitationToDuration(zoneSetting.PrecipitationRate, Zoneitem.WaterPrecipitation_Duration);
                    break;
            }

            return WaterDuration_Seconds;
        }
        #endregion

        #region Scheduled
        public ScheduledIrrigationView[] GetScheduled(string sN, long StartTicks, long EndTicks)
        {
            var sDate = TimeConvertor.GetDateTime(StartTicks);
            DateTime Start = new DateTime(sDate.Year, sDate.Month, sDate.Day, 0, 0, 0);
            var eDate = TimeConvertor.GetDateTime(EndTicks);
            DateTime End = new DateTime(eDate.Year, eDate.Month, eDate.Day, 23, 59, 59);

            var ScheduledIrrigation = new List<ScheduledIrrigationView>();
            var ProgramList = _AdminRepository.DevicePrograms_Get(sN).Where(p => (!p.FinalEndDateLimit.HasValue || p.FinalEndDateLimit.Value > End) &&
                                                                                 (!p.FinalStartDateLimit.HasValue || p.FinalStartDateLimit.Value < Start) &&
                                                                                 (StatusType)p.StatusID == StatusType.Active).ToArray();

            var MainPipe = _AdminRepository.MainPipeSettings_Get(sN);
            var device_WaterFactor = MainPipe != null ? MainPipe.WaterFactor : 100;


            var ZoneSetting = _AdminRepository.DeviceZoneSetting_Get(sN);
            foreach (var program in ProgramList)
            {
                var PeriodDate = new List<DateTime>();
                var ExecutionTimes = new List<ExecutionTime>();

                decimal WaterFactor = (program.WaterFactor != 100 ? program.WaterFactor : device_WaterFactor) * 0.01m;

                #region Weekly - Get the allow watering days

                if ((DaysExecutionType)program.DaysExecutionsTypeID == DaysExecutionType.Weekly)
                {
                    var weeklyDays = _AdminRepository.WeeklyProgramSetting_Get(program.ProgramID);
                    var tempStart = Start;
                    while (tempStart < End)
                    {
                        switch (tempStart.DayOfWeek)
                        {
                            case DayOfWeek.Sunday:
                                if (weeklyDays.Sunday)
                                {
                                    PeriodDate.Add(tempStart);
                                }
                                break;
                            case DayOfWeek.Monday:
                                if (weeklyDays.Monday)
                                {
                                    PeriodDate.Add(tempStart);
                                }
                                break;
                            case DayOfWeek.Tuesday:
                                if (weeklyDays.Tuesday)
                                {
                                    PeriodDate.Add(tempStart);
                                }
                                break;
                            case DayOfWeek.Wednesday:
                                if (weeklyDays.Wednesday)
                                {
                                    PeriodDate.Add(tempStart);
                                }
                                break;
                            case DayOfWeek.Thursday:
                                if (weeklyDays.Thursday)
                                {
                                    PeriodDate.Add(tempStart);
                                }
                                break;
                            case DayOfWeek.Friday:
                                if (weeklyDays.Friday)
                                {
                                    PeriodDate.Add(tempStart);
                                }
                                break;
                            case DayOfWeek.Saturday:
                                if (weeklyDays.Saturday)
                                {
                                    PeriodDate.Add(tempStart);
                                }
                                break;
                        }

                        tempStart = tempStart.AddDays(1);
                    }

                }
                #endregion

                #region CyclicDayProgram -StartDate and DaysInterval
                else if ((DaysExecutionType)program.DaysExecutionsTypeID == DaysExecutionType.Cyclic)
                {
                    var CyclicSetting = _AdminRepository.CyclicDayProgram_Get(program.ProgramID);
                    var tempStart = CyclicSetting.StartDate > Start ? CyclicSetting.StartDate : Start;
                    tempStart = new DateTime(tempStart.Year, tempStart.Month, tempStart.Day, 0, 0, 0);
                    while (tempStart < End)
                    {
                        PeriodDate.Add(tempStart);
                        tempStart = tempStart.AddDays(CyclicSetting.DaysInterval);
                    }
                }
                #endregion

                #region Fixed Time - By Execution Hours

                if ((HoursExecutionType)program.HoursExecutionsTypeID == HoursExecutionType.Fixed)
                {
                    var ExecuTimes = _AdminRepository.ExecutionHoursProgram_Get(program.ProgramID);
                    foreach (var item_Date in PeriodDate)
                    {
                        foreach (var startTime in ExecuTimes)
                        {
                            var initialStart = item_Date.AddSeconds(startTime.ExecTime);
                            ExecutionTimes.Add(new ExecutionTime() { StartDate = initialStart });
                        }

                    }
                }
                #endregion

                #region Interval time  - StartTime ,Cycl and Interval Spent time
                else if ((HoursExecutionType)program.HoursExecutionsTypeID == HoursExecutionType.Interval)
                {
                    foreach (var item_Date in PeriodDate)
                    {
                        var CyclesPerDay = program.HourlyCyclesPerDay;
                        var Counter = 0;
                        var StartTime = program.HourlyCyclesStartTime;
                        while (Counter < CyclesPerDay)
                        {
                            var item_Date1 = item_Date.AddSeconds(StartTime + Counter * program.HourlyCycleTime);
                            ExecutionTimes.Add(new ExecutionTime() { StartDate = item_Date1, Interval = Counter });
                            Counter++;
                        }
                    }
                }
                #endregion

                #region ExecutionTimes over ZoneInProgram 

                var ZoneInProgram = _AdminRepository.ZonesInProgram_Get(program.ProgramID);

                foreach (var item_time in ExecutionTimes)
                {

                    ScheduledIrrigationView item_Scheduled = new ScheduledIrrigationView()
                    {
                        ProgramID = program.ProgramID,
                        StartDate = item_time.StartDate,
                        Interval = item_time.Interval,
                        Zones = new List<ZoneScheduledItemView>(),
                        Name = program.Name,
                        IsFertilizerON = program.IsFertilizerON,
                    };
                    ScheduledIrrigation.Add(item_Scheduled);

                    var startTime = item_time.StartDate;
                    DateTime endTime = new DateTime();
                    var midnightTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0).AddDays(1);
                    var totalTime = 0;

                    foreach (var Zoneitem in ZoneInProgram)
                    {
                        var zoneSetting = ZoneSetting.Where(z => z.OutputNumber == Zoneitem.ZoneNumber).FirstOrDefault();

                        if (zoneSetting != null)
                        {
                            zoneSetting = new GSI.DAL.DataAccessLayer.Models.Zone.ZoneSetting()
                            {
                                ZoneColor = 15658734,
                                OutputNumber = Zoneitem.ZoneNumber,
                                Name = "Name" + Zoneitem.ZoneNumber
                            };
                        }


                        var WaterMeterSetting = _AdminRepository.WaterMeterSetting_Get(sN);
                        var FlowTypeID = WaterMeterSetting != null ? (WaterMeter_FlowType?)WaterMeterSetting.FlowTypeID : null;
                        var FlowValue = zoneSetting.SetupNominalFlow != null ? zoneSetting.SetupNominalFlow : zoneSetting.LastFlow;
                        var PulseTypeID = WaterMeterSetting != null ? (WaterMeter_PulseType)WaterMeterSetting.PulseTypeID : WaterMeter_PulseType.LITER_AS_LITER;
                        int WaterDuration_Seconds = GetDurationTime(FlowTypeID, FlowValue, PulseTypeID, (WaterProgramType)program.WaterUnitID, zoneSetting, Zoneitem);


                        WaterDuration_Seconds = (int)(WaterDuration_Seconds * WaterFactor);

                        endTime = startTime.AddSeconds(WaterDuration_Seconds);
                        totalTime += WaterDuration_Seconds;
                        if (midnightTime < endTime)
                        {
                            item_Scheduled = new ScheduledIrrigationView()
                            {
                                StartDate = midnightTime,
                                ProgramID = item_Scheduled.ProgramID,
                                Interval = item_time.Interval,
                                Zones = new List<ZoneScheduledItemView>(),
                                Name = program.Name,
                                IsFertilizerON = program.IsFertilizerON,
                            };
                            midnightTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, 0, 0, 0).AddDays(1);
                            ScheduledIrrigation.Add(item_Scheduled);
                        }

                        item_Scheduled.Zones.Add(new ZoneScheduledItemView()
                        {
                            Duration = Zoneitem.WaterDuration,
                            FertQuant = Zoneitem.FertQuant,
                            FertTime = Zoneitem.FertTime,
                            WaterAfter = Zoneitem.WaterAfter,
                            WaterBefore = Zoneitem.WaterBefore,
                            Zone = new ZoneScheduledInfoView()
                            {
                                Color = zoneSetting.ZoneColor,
                                ZoneNum = zoneSetting.OutputNumber,
                                Name = zoneSetting.Name
                            },
                            StartTime = startTime,
                            EndTime = endTime
                        });

                        startTime = endTime;
                    }
                    item_Scheduled.EndDate = endTime;

                }

                #endregion

            }
            return ScheduledIrrigation.ToArray();
        }

        #endregion

        #region Settings
        public ProgramSettingMainView GetSettings(string SN, long programID)
        {

            return new ProgramSettingMainView()
            {
                MainSettings = new Device.DeviceSettingTitleView(_AdminRepository.FertilizerSetting_Get(SN),
                                                                _AdminRepository.WaterMeterSetting_Get(SN),
                                                                _AdminRepository.GeneralSettings_Get(SN),
                                                                _AdminRepository.MainPipeSettings_Get(SN),
                                                                _AdminRepository.DeviceZoneSetting_Get(SN)),

                Program = new ProgramSettingView(_AdminRepository.ProgramSetting_Get(programID),
                                                _AdminRepository.WeeklyProgramSetting_Get(programID),
                                                _AdminRepository.CyclicDayProgram_Get(programID),
                                                _AdminRepository.ExecutionHoursProgram_Get(programID),
                                                _AdminRepository.ZonesInProgram_Get(programID))
            };


        }

        public bool SaveSettings(string sN, long programID, ProgramSettingView setting)
        {
            var result = false;

            #region ProgramSetting

            var psetting = new ProgramSetting()
            {
                Name = setting.Name,
                WaterUnitID = (byte)setting.ProgramType,
                ProgramID = programID,
                ProgramNumber = setting.ProgramNumber,
                Priority = setting.Priority,
                IsFertilizerON = setting.IsFertilizerON,
                StatusID = (byte)setting.Status,
                FertUnitID = (byte)setting.FertExecutionType,
                WaterFactor = setting.WaterFactor,
                HoursExecutionsTypeID = (byte)setting.HoursExecution,
                DaysExecutionsTypeID = (byte)setting.DaysExecutionType,
                FinalEndDateLimit = setting.Advanced.FinalEndDateLimit.HasValue ? (DateTime?)TimeConvertor.GetDateTime(setting.Advanced.FinalEndDateLimit.Value) : null,
                FinalStartDateLimit = setting.Advanced.FinalStartDateLimit.HasValue ? (DateTime?)TimeConvertor.GetDateTime(setting.Advanced.FinalStartDateLimit.Value) : null,
                FinalStartHoursLimit = setting.Advanced.FinalStartHoursLimit,
                FinalStopHoursLimit = setting.Advanced.FinalStopHoursLimit,
                HourlyCyclesPerDay = setting.HourlyCycle.HourlyCyclesPerDay,
                HourlyCyclesStartTime = setting.HourlyCycle.HourlyCyclesStartTime,
                HourlyCyclesStopTime = setting.HourlyCycle.HourlyCycleTime,
                HourlyCycleTime = setting.HourlyCycle.HourlyCyclesStopTime

            };

            result = _AdminRepository.ProgramSetting_Update(psetting);

            if (!result)
                return result;
            #endregion

            #region cyclicsetting

            var cyclicsetting = new CyclicDayProgram()
            {
                ProgramID = programID,
                DaysInterval = setting.CyclicDayProgram.DaysInterval,
                StartDate = TimeConvertor.GetDateTime(setting.CyclicDayProgram.StartDate)
            };

            result = _AdminRepository.CyclicDayProgram_Update(cyclicsetting);

            if (!result)
                return result;
            #endregion

            #region ExecutionHours

            var ExecutionHours = setting.ExecutionHours.Select(e => new ExecutionHours { ExecTime = e }).ToArray();

            result = _AdminRepository.ExecutionHoursProgram_Update(programID, ExecutionHours);

            if (!result)
                return result;

            #endregion

            #region WeeklyDaysProgram

            var w_days = setting.WeeklyDaysProgram.Days;
            var WeeklyDaysProgram = new WeeklyProgramSetting()
            {
                ProgramID = programID,
                Sunday = w_days[0].IsEnabled,
                Monday = w_days[1].IsEnabled,
                Tuesday = w_days[2].IsEnabled,
                Wednesday = w_days[3].IsEnabled,
                Thursday = w_days[4].IsEnabled,
                Friday = w_days[5].IsEnabled,
                Saturday = w_days[6].IsEnabled
            };

            result = _AdminRepository.WeeklyProgramSetting_Update(WeeklyDaysProgram);

            if (!result)
                return result;

            #endregion

            #region ZoneInProgram
            //GET All zone in program from DB
            var zone_dbList = _AdminRepository.ZonesInProgram_Get(programID).ToList();


            for (int i = 0; i < setting.ZoneInProgram.Length; i++)
            {
                var zone = setting.ZoneInProgram[i];
                zone_dbList.RemoveAll(s => s.ZoneNumber == zone.ZoneNumber);
                var zone_db = new ZoneInProgram()
                {
                    FertQuant = zone.FertQuant,
                    FertTime = zone.FertTime,
                    OrderNumber = zone.OrderNumber,
                    ProgramID = programID,
                    WaterAfter = zone.WaterAfter,
                    WaterBefore = zone.WaterBefore,
                    WaterDuration = zone.WaterDuration,
                    WaterPrecipitation_Duration = zone.Precipitation_Duration,
                    WaterPrecipitation_Quantity = zone.Precipitation_Quantity,
                    WaterQuantity = zone.WaterQuantity,
                    ZoneNumber = zone.ZoneNumber
                };

                result = _AdminRepository.ZoneInProgram_Update(zone_db);
                if (!result)
                    return result;
            }

            if (zone_dbList.Count > 0)
            {
                foreach (var item in zone_dbList)
                {
                    result = _AdminRepository.ZoneInProgram_Delete(item.ZoneNumber, programID);
                    if (!result)
                        return result;
                }

            }

            #endregion

            return true;
        }

        protected override void OnDispose()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
