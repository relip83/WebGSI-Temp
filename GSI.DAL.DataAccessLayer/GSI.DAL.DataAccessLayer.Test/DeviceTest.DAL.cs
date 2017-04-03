using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Repositories.AdminRepository;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Repositories.AdminRepository.T_SQL;
using System.Linq;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Zone;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program;
using System.Collections.Generic;

namespace GSI.DAL.DataAccessLayer.Test
{
    [TestClass]
    public class DeviceTest
    {
        private IAdminRepository Repository = null;

        public DeviceTest()
        {
            Repository = new TSQLAdminRepository("GSI-Group_AdminDB");
        }

        private bool EqualsDate(DateTime date1, DateTime date2)
        {
            return (date1.Year == date2.Year
                   && date1.Month == date2.Month
                   && date1.Day == date2.Day);
        }

        private DeviceBase CreateDevice(string GUI_ID = null)
        {
            if (string.IsNullOrEmpty(GUI_ID))
            {
                GUI_ID = Guid.NewGuid().ToString();
            }
            var sn = String.Format("{0}", GUI_ID, GUI_ID);
            var result = Repository.AddDevice(sn, 1);
            Assert.IsTrue(result != null && result != -1);
            var devcie = Repository.GetDevice(sn);
            Assert.IsTrue(devcie != null && devcie.DeviceID == result.Value);
            return devcie;
        }

        #region setting Device

        [TestMethod]
        public void TestAllSetting()
        {
            var device = CreateDevice();

            #region GeneralSettings
            {
                var setting = new GeneralSettings()
                {
                    IsMetric = true,
                    LandTypeID = 1,
                    CustomTimeZoneID = 150,
                    ModelID = 1,
                    Name = "device00",
                    Status = 0,
                    Flag_AdvancedFert = true
                };

                Assert.IsTrue(Repository.GeneralSettings_Update(device.SN, setting));
                var s_db = Repository.GeneralSettings_Get(device.SN);
                Assert.IsTrue(s_db != null);
                Assert.IsTrue(s_db.IsMetric == setting.IsMetric);
                Assert.IsTrue(s_db.CustomTimeZoneID == setting.CustomTimeZoneID);
                Assert.IsTrue(s_db.LandTypeID == setting.LandTypeID);
                Assert.IsTrue(s_db.Flag_AdvancedFert == setting.Flag_AdvancedFert);
                Assert.IsTrue(s_db.Status == setting.Status);
                Assert.IsTrue(s_db.ModelID == setting.ModelID);
                Assert.IsTrue(s_db.Name == setting.Name);

            }

            #endregion

            #region MainPipeSettings
            {
                var setting = new MainPipeSettings()
                {
                    UseMaster = false,
                    RainyDaysStopper = 10,
                    RainDetectorNC = true,
                    IsLocalSequenceActive = true,
                    ProgramsAsQueue = false,
                    RainDetectorEnabled = true,
                    OverlapTime = 13,
                    DSundayState = true,
                    DMondayState = false,
                    DTuesdayState = true,
                    DWednesdayState = false,
                    DThursdayState = true,
                    DFridayState = false,
                    DSaturdayState = true,
                    WaterFactor = 99
                };

                for (int i = 0; i < 2; i++)
                {
                    Assert.IsTrue(Repository.MainPipeSettings_Update(device.SN, setting));
                    var s_db = Repository.MainPipeSettings_Get(device.SN);
                    Assert.IsTrue(s_db != null);
                    Assert.IsTrue(s_db.UseMaster == setting.UseMaster);
                    Assert.IsTrue(s_db.WaterFactor == setting.WaterFactor);
                    Assert.IsTrue(s_db.RainyDaysStopper == setting.RainyDaysStopper);
                    Assert.IsTrue(s_db.RainDetectorNC == setting.RainDetectorNC);
                    Assert.IsTrue(s_db.RainDetectorEnabled == setting.RainDetectorEnabled);
                    Assert.IsTrue(s_db.ProgramsAsQueue == setting.ProgramsAsQueue);
                    Assert.IsTrue(s_db.OverlapTime == setting.OverlapTime);
                    Assert.IsTrue(s_db.IsLocalSequenceActive == setting.IsLocalSequenceActive);
                    Assert.IsTrue(s_db.DSundayState == setting.DSundayState);
                    Assert.IsTrue(s_db.DMondayState == setting.DMondayState);
                    Assert.IsTrue(s_db.DTuesdayState == setting.DTuesdayState);
                    Assert.IsTrue(s_db.DWednesdayState == setting.DWednesdayState);
                    Assert.IsTrue(s_db.DThursdayState == setting.DThursdayState);
                    Assert.IsTrue(s_db.DFridayState == setting.DFridayState);
                    Assert.IsTrue(s_db.DSaturdayState == setting.DSaturdayState);


                    setting = new MainPipeSettings()
                    {
                        UseMaster = true,
                        RainyDaysStopper = 11,
                        RainDetectorNC = false,
                        IsLocalSequenceActive = true,
                        ProgramsAsQueue = false,
                        RainDetectorEnabled = true,
                        OverlapTime = 13,
                        DSundayState = true,
                        DMondayState = false,
                        DTuesdayState = true,
                        DWednesdayState = false,
                        DThursdayState = false,
                        DFridayState = true,
                        DSaturdayState = false,
                        WaterFactor = 33
                    };

                }




            }
            #endregion

            #region IrrExceptionDates
            {
                var now = DateTime.Now;

                var setting = new RestrictedDates[]
                {
                   new RestrictedDates { ExceptionDate = now } ,
                   new RestrictedDates { ExceptionDate = now.AddDays(1) },
                   new RestrictedDates { ExceptionDate = now.AddMonths(1) }
                };
                for (int i = 0; i < 3; i++)
                {
                    Assert.IsTrue(Repository.IrrExceptionDates_Update(device.SN, setting));
                    var s_db = Repository.IrrExceptionDates_Get(device.SN);
                    Assert.IsTrue(s_db != null);
                    Assert.IsTrue(s_db.Length == setting.Length);
                    for (int j = 0; j < s_db.Length; j++)
                    {
                        EqualsDate(s_db[j].ExceptionDate, setting[j].ExceptionDate);
                    }

                    now = DateTime.Now.AddMonths(i);
                    setting = new RestrictedDates[]
                    {
                       new RestrictedDates { ExceptionDate = now } ,
                       new RestrictedDates { ExceptionDate = now.AddDays(1) },
                       new RestrictedDates { ExceptionDate = now.AddMonths(1) }
                    };
                }

            }

            #endregion

            #region FertilizerSetting
            {
                var setting = new FertilizerSetting()
                {
                    ContinuousFert = true,
                    FerlizerFaillureTime = 11,
                    FlowTypeID = 2,
                    IsEnabled = true,
                    Leakage = 33,
                    NominalFlow = 2,
                    OutputNumber = 1,
                    PulseSize = 2,
                    PulseTime = 3,
                    PulseTypeID = 1,
                    TypeID = 1
                };

                for (int i = 0; i < 2; i++)
                {
                    Assert.IsTrue(Repository.FertilizerSetting_Update(device.SN, setting));
                    var s_db = Repository.FertilizerSetting_Get(device.SN);
                    Assert.IsTrue(s_db != null);
                    Assert.IsTrue(s_db.ContinuousFert == setting.ContinuousFert);
                    Assert.IsTrue(s_db.FerlizerFaillureTime == setting.FerlizerFaillureTime);
                    Assert.IsTrue(s_db.FlowTypeID == setting.FlowTypeID);
                    Assert.IsTrue(s_db.IsEnabled == setting.IsEnabled);
                    Assert.IsTrue(s_db.Leakage == setting.Leakage);
                    Assert.IsTrue(s_db.NominalFlow == setting.NominalFlow);
                    Assert.IsTrue(s_db.OutputNumber == setting.OutputNumber);
                    Assert.IsTrue(s_db.PulseSize == setting.PulseSize);
                    Assert.IsTrue(s_db.PulseTime == setting.PulseTime);
                    Assert.IsTrue(s_db.PulseTypeID == setting.PulseTypeID);
                    Assert.IsTrue(s_db.TypeID == setting.TypeID);

                    setting = new FertilizerSetting()
                    {
                        ContinuousFert = false,
                        FerlizerFaillureTime = 12,
                        FlowTypeID = 1,
                        IsEnabled = false,
                        Leakage = 22,
                        NominalFlow = 3,
                        OutputNumber = 0,
                        PulseSize = 1,
                        PulseTime = 2,
                        PulseTypeID = 0,
                        TypeID = 2
                    };
                }


            }
            #endregion

            #region WaterMeterSetting
            {
                var setting = new WaterMeterSetting()
                {
                    FlowTypeID = 1,
                    IsEnabled = true,

                    LeakageLimit = 1,
                    MeterTypeID = 0,
                    NoWaterPulseDelay = 11,
                    PulseSize = 10,
                    PulseTypeID = 1
                };

                for (int i = 0; i < 2; i++)
                {
                    Assert.IsTrue(Repository.WaterMeterSetting_Update(device.SN, setting));
                    var s_db = Repository.WaterMeterSetting_Get(device.SN);
                    Assert.IsTrue(s_db != null);
                    Assert.IsTrue(s_db.FlowTypeID == setting.FlowTypeID);
                    Assert.IsTrue(s_db.IsEnabled == setting.IsEnabled);
                    Assert.IsTrue(s_db.LeakageLimit == setting.LeakageLimit);
                    Assert.IsTrue(s_db.MeterTypeID == setting.MeterTypeID);
                    Assert.IsTrue(s_db.NoWaterPulseDelay == setting.NoWaterPulseDelay);
                    Assert.IsTrue(s_db.PulseSize == setting.PulseSize);
                    Assert.IsTrue(s_db.PulseTypeID == setting.PulseTypeID);

                    setting = new WaterMeterSetting()
                    {
                        FlowTypeID = 2,
                        IsEnabled = false,
                        LeakageLimit = 11,
                        MeterTypeID = 1,
                        NoWaterPulseDelay = 22,
                        PulseSize = 1,
                        PulseTypeID = 2
                    };
                }
            }
            #endregion
        }

        [TestMethod]
        public void AlertsSetting_Test()
        {
            var device = CreateDevice();
            var setting = Repository.AlertSettings_Get(device.SN);

            for (int i = 0; i < 4; i++)
            {
                foreach (var item in setting)
                {
                    item.IsEnable = i % 2 == 0;
                    item.SendEmail = !(i % 2 == 0);
                    item.SendSMS = i % 2 == 0;
                    Repository.AlertSettings_Update(device.SN, item);
                }

                var setting_db = Repository.AlertSettings_Get(device.SN);

                foreach (var item_db in setting_db)
                {
                    var item = setting.Where(a => a.AlertCode == item_db.AlertCode).FirstOrDefault();
                    Assert.IsTrue(item != null);
                    Assert.IsTrue(item.IsEnable == item_db.IsEnable);
                    Assert.IsTrue(item.SendEmail == item_db.SendEmail);
                    Assert.IsTrue(item.SendSMS == item_db.SendSMS);
                }

            }
        }

        #endregion

        #region zones
        [TestMethod]
        public void ZoneSetting_Test()
        {
            var device = CreateDevice();
            for (int i = 0; i < 4; i++)
            {
                var zoneList = Repository.DeviceZoneSetting_Get(device.SN);
                foreach (var item in zoneList)
                {
                    item.IrrigrationArea = i % 2 == 0 ? 1 : 0;
                    item.StatusID = (byte)(i % 2 == 0 ? 1 : 0);
                    item.ZoneColor = (i % 2 == 0 ? 1008430830 : 1008430830);
                    item.FertilizerConnected = i % 2 == 0;
                    item.HighFlowDeviation = (byte)(i % 2 == 0 ? 11 : 10);
                    item.HighFlowFaultDelay = (short)(i % 2 == 0 ? 12 : 13);
                    item.LowFlowDeviation = (byte)(i % 2 == 0 ? 15 : 16);
                    item.LowFlowFaultDelay = (short)(i % 2 == 0 ? 13 : 17);
                    item.PrecipitationRate = (i % 2 == 0 ? 18 : 19);
                    item.StatusID = (byte)(i % 2 == 0 ? 1 : 0);
                    item.Name = "Name_" + i.ToString();
                    item.SetupNominalFlow = (i % 2 == 0 ? 1.1M : 0.2M);
                    item.StopOnFertFailure = i % 2 == 0;
                    item.TypeID = (byte)(i % 2 == 0 ? 1 : 0);
                    Assert.IsTrue(Repository.ZoneSetting_Update(device.SN, item));
                }
                var zoneList_db = Repository.DeviceZoneSetting_Get(device.SN);
                foreach (var item_db in zoneList_db)
                {
                    var count = 0;
                    ZoneSetting item = null;
                    while (count < 2)
                    {
                        if (count == 0)
                            item = zoneList.Where(z => z.OutputNumber == item_db.OutputNumber).FirstOrDefault();
                        else
                            Repository.ZoneSetting_Get(device.SN, item_db.OutputNumber);

                        count++;
                        Assert.IsTrue(item != null);
                        Assert.IsTrue(item.IrrigrationArea == item_db.IrrigrationArea);
                        Assert.IsTrue(item.OutputNumber == item_db.OutputNumber);
                        Assert.IsTrue(item.ConfigID == item_db.ConfigID);
                        Assert.IsTrue(item.StatusID == item_db.StatusID);
                        Assert.IsTrue(item.ZoneColor == item_db.ZoneColor);
                        Assert.IsTrue(item.FertilizerConnected == item_db.FertilizerConnected);
                        Assert.IsTrue(item.HighFlowDeviation == item_db.HighFlowDeviation);
                        Assert.IsTrue(item.HighFlowFaultDelay == item_db.HighFlowFaultDelay);
                        Assert.IsTrue(item.PrecipitationRate == item_db.PrecipitationRate);
                        Assert.IsTrue(item.StatusID == item_db.StatusID);
                        Assert.IsTrue(item.Name == item_db.Name);
                        Assert.IsTrue(item.SetupNominalFlow == item_db.SetupNominalFlow);
                        Assert.IsTrue(item.StopOnFertFailure == item_db.StopOnFertFailure);
                        Assert.IsTrue(item.TypeID == item_db.TypeID);
                        Assert.IsTrue(item.LowFlowDeviation == item_db.LowFlowDeviation);
                        Assert.IsTrue(item.LowFlowFaultDelay == item_db.LowFlowFaultDelay);
                        Assert.IsTrue(item.TimeFillDelay == item_db.TimeFillDelay);
                    }
                }

            }

        }

        #endregion Program

        private void isProgramEqual(ProgramSetting p1, ProgramSetting p2)
        {
            Assert.IsTrue(p1.DaysExecutionsTypeID == p2.DaysExecutionsTypeID);
            Assert.IsTrue(p1.FertUnitID == p2.FertUnitID);
            Assert.IsTrue(EqualsDate(p1.FinalEndDateLimit.Value, p2.FinalEndDateLimit.Value));
            Assert.IsTrue(EqualsDate(p1.FinalStartDateLimit.Value, p2.FinalStartDateLimit.Value));
            Assert.IsTrue(p1.FinalStartHoursLimit == p2.FinalStartHoursLimit);
            Assert.IsTrue(p1.FinalStopHoursLimit == p2.FinalStopHoursLimit);
            Assert.IsTrue(p1.HourlyCyclesPerDay == p2.HourlyCyclesPerDay);
            Assert.IsTrue(p1.HourlyCyclesStartTime == p2.HourlyCyclesStartTime);
            Assert.IsTrue(p1.HourlyCyclesStopTime == p2.HourlyCyclesStopTime);
            Assert.IsTrue(p1.HourlyCycleTime == p2.HourlyCycleTime);
            Assert.IsTrue(p1.HoursExecutionsTypeID == p2.HoursExecutionsTypeID);
            Assert.IsTrue(p1.IsFertilizerON == p2.IsFertilizerON);
            Assert.IsTrue(p1.Name == p2.Name);
            Assert.IsTrue(p1.Priority == p2.Priority);
            Assert.IsTrue(p1.ProgramID == p2.ProgramID);
            Assert.IsTrue(p1.ProgramNumber == p2.ProgramNumber);
            Assert.IsTrue(p1.StatusID == p2.StatusID);
            Assert.IsTrue(p1.WaterFactor == p2.WaterFactor);
            Assert.IsTrue(p1.WaterUnitID == p2.WaterUnitID);
        }


        [TestMethod]
        public void Program_Test()
        {
            var SN = "0b257467-640b-47db-a";
            if (string.IsNullOrEmpty(SN))
            {
                var device = CreateDevice();
                SN = device.SN;
            }

            var plist = Repository.DevicePrograms_Get(SN);
            Assert.IsTrue(plist != null && plist.Length > 0);
            for (int i = 0; i < plist.Length; i++)
            {
                var p_fromList = plist[i];
                var ProgramID = p_fromList.ProgramID;

                var program = Repository.ProgramSetting_Get(ProgramID);
                isProgramEqual(program, p_fromList);

                var now = DateTime.Now;
                var CyclicDayProgram = new CyclicDayProgram() { ProgramID = ProgramID };
                var WeeklyDaysProgram = new WeeklyProgramSetting() { ProgramID = ProgramID };


                for (int j = 0; j < 2; j++)
                {
                    #region setting

                    program.DaysExecutionsTypeID = (byte)(i % 2 == 0 ? 0 : 1);
                    program.FinalEndDateLimit = now.AddMonths(i);
                    program.FinalStartDateLimit = now.AddMonths(-i);
                    program.FinalStartHoursLimit = i % 2 == 0 ? 10 : 100;
                    program.FinalStopHoursLimit = i % 2 == 0 ? 2 : 234;
                    program.HourlyCyclesPerDay = (byte)(i % 2 == 0 ? 11 : 12);
                    program.HourlyCyclesStartTime = i % 2 == 0 ? 12 : 34;
                    program.HourlyCyclesStopTime = i % 2 == 0 ? 34 : 345;
                    program.HourlyCycleTime = i % 2 == 0 ? 34 : 67;
                    program.HoursExecutionsTypeID = (byte)(i % 2 == 0 ? 1 : 0);
                    program.IsFertilizerON = i % 2 == 0;
                    program.Name = "name _" + i.ToString();
                    program.Priority = (byte)(i % 2 == 0 ? 17 : 0);
                    program.StatusID = (byte)(i % 2 == 0 ? 1 : 0);
                    program.WaterFactor = (byte)(i % 2 == 0 ? 45 : 78);
                    program.WaterUnitID = (byte)(i % 2 == 0 ? 0 : 1);

                    Assert.IsTrue(Repository.ProgramSetting_Update(program));
                    var program_db = Repository.ProgramSetting_Get(ProgramID);

                    isProgramEqual(program, program_db);

                    #endregion

                    #region CyclicDayProgram

                    CyclicDayProgram.DaysInterval = (byte)(2);
                    CyclicDayProgram.StartDate = now.AddDays(i);
                    Assert.IsTrue(Repository.CyclicDayProgram_Update(CyclicDayProgram));
                    var c_db = Repository.CyclicDayProgram_Get(ProgramID);
                    Assert.IsTrue(CyclicDayProgram.DaysInterval == c_db.DaysInterval);
                    Assert.IsTrue(EqualsDate(CyclicDayProgram.StartDate, c_db.StartDate));

                    #endregion

                    #region  WeeklyDaysProgram

                    WeeklyDaysProgram.Sunday = (i % 2 == 0);
                    WeeklyDaysProgram.Monday = !(i % 2 == 0);
                    WeeklyDaysProgram.Thursday = (i % 2 == 0);
                    WeeklyDaysProgram.Wednesday = !(i % 2 == 0);
                    WeeklyDaysProgram.Thursday = (i % 2 == 0);
                    WeeklyDaysProgram.Friday = !(i % 2 == 0);
                    WeeklyDaysProgram.Saturday = (i % 2 == 0);

                    Assert.IsTrue(Repository.WeeklyProgramSetting_Update(WeeklyDaysProgram));
                    var w_db = Repository.WeeklyProgramSetting_Get(ProgramID);
                    Assert.IsTrue(WeeklyDaysProgram.Sunday == w_db.Sunday);
                    Assert.IsTrue(WeeklyDaysProgram.Monday == w_db.Monday);
                    Assert.IsTrue(WeeklyDaysProgram.Tuesday == w_db.Tuesday);
                    Assert.IsTrue(WeeklyDaysProgram.Wednesday == w_db.Wednesday);
                    Assert.IsTrue(WeeklyDaysProgram.Thursday == w_db.Thursday);
                    Assert.IsTrue(WeeklyDaysProgram.Friday == w_db.Friday);
                    Assert.IsTrue(WeeklyDaysProgram.Saturday == w_db.Saturday);

                    #endregion

                    #region ExecutionHours

                    var ExecutionHours = new ExecutionHours[] {
                                                                new ExecutionHours() { ExecTime = i  },
                                                                new ExecutionHours() { ExecTime = 10800 },
                                                                new ExecutionHours() { ExecTime = 28800 },
                                                                new ExecutionHours() { ExecTime = 50400 }
                                                            };

                    Assert.IsTrue(Repository.ExecutionHoursProgram_Update(ProgramID, ExecutionHours));

                    var e_db = Repository.ExecutionHoursProgram_Get(ProgramID);
                    Assert.IsTrue(e_db.Length == ExecutionHours.Length);
                    foreach (var item_db in e_db)
                    {
                        var item = ExecutionHours.Where(e => e.ExecTime == item_db.ExecTime).FirstOrDefault();
                        Assert.IsTrue(item != null);
                    }

                    #endregion

                    #region ZoneInProgram
                    var list_zone = new List<ZoneInProgram>();

                    for (int x = 0; x < 4; x++)
                    {
                        var zone = new ZoneInProgram()
                        {
                            FertQuant = (i % 2 == 0 ? 0.9m : 1.1m),
                            FertTime = (i % 2 == 0 ? 11 : 12),
                            OrderNumber = x,
                            ProgramID = ProgramID,
                            WaterAfter = (i % 2 == 0 ? 3 : 4),
                            WaterBefore = (i % 2 == 0 ? 2 : 5),
                            WaterDuration = (i % 2 == 0 ? 23 : 34),
                            WaterPrecipitation_Duration = (i % 2 == 0 ? 3 : 4),
                            WaterPrecipitation_Quantity = (i % 2 == 0 ? 6 : 8),
                            WaterQuantity = (i % 2 == 0 ? 7 : 99),
                            ZoneNumber = (byte)x,
                        };
                        list_zone.Add(zone);
                        Assert.IsTrue(Repository.ZoneInProgram_Update(zone));
                    }

                    var list_zone_db = Repository.ZonesInProgram_Get(ProgramID);

                    Assert.IsTrue(list_zone_db.Length == list_zone.Count);

                    foreach (var item_db in list_zone_db)
                    {
                        var item = list_zone.Where(s => s.ZoneNumber == item_db.ZoneNumber).FirstOrDefault();
                        Assert.IsTrue(item != null);
                        Assert.IsTrue(item.FertQuant == item_db.FertQuant);
                        Assert.IsTrue(item.FertTime == item_db.FertTime);
                        Assert.IsTrue(item.OrderNumber == item_db.OrderNumber);
                        Assert.IsTrue(item.WaterAfter == item_db.WaterAfter);
                        Assert.IsTrue(item.WaterBefore == item_db.WaterBefore);
                        Assert.IsTrue(item.WaterDuration == item_db.WaterDuration);
                        Assert.IsTrue(item.WaterPrecipitation_Duration == item_db.WaterPrecipitation_Duration);
                        Assert.IsTrue(item.WaterPrecipitation_Quantity == item_db.WaterPrecipitation_Quantity);
                        Assert.IsTrue(item.WaterQuantity == item_db.WaterQuantity);
                    }


                    #endregion
                }
            }
        }


        [TestMethod]
        public void ProgramStory1_Test() //one program 
                                         //Weekly type , 
                                         //2  exec time - one cross-midnight

        {
            var SN = "0da20764-60b2-4081-8";
            if (string.IsNullOrEmpty(SN))
            {
                var device = CreateDevice();
                SN = device.SN;
            }

            var plist = Repository.DevicePrograms_Get(SN);
            Assert.IsTrue(plist != null && plist.Length > 0);
            // set only one to be active
            for (int i = 1; i < plist.Length; i++)
            {
                var p_fromList = plist[i];
                var ProgramID = p_fromList.ProgramID;

                var program = Repository.ProgramSetting_Get(ProgramID);
                program.StatusID = 0;
                Assert.IsTrue(Repository.ProgramSetting_Update(program));
            }

            var Program = plist[0];
            Program.DaysExecutionsTypeID = 0;

            Repository.ProgramSetting_Update(Program);
            var WeeklyDaysProgram = new WeeklyProgramSetting() { ProgramID = Program.ProgramID };
            WeeklyDaysProgram.Sunday = true;
            WeeklyDaysProgram.Monday = false;
            WeeklyDaysProgram.Thursday = true;
            WeeklyDaysProgram.Wednesday = false;
            WeeklyDaysProgram.Thursday = true;
            WeeklyDaysProgram.Friday = false;
            WeeklyDaysProgram.Saturday = true;

            Assert.IsTrue(Repository.WeeklyProgramSetting_Update(WeeklyDaysProgram));

            var ExecutionHours = new ExecutionHours[] {
                                                                new ExecutionHours() { ExecTime = 28800 }, //8 am
                                                                new ExecutionHours() { ExecTime = 82800 }  //11 pm 23:00
                                                                                                           //{ ExecTime = 64800 } 6 pm ,18:00
                                                            };

            Assert.IsTrue(Repository.ExecutionHoursProgram_Update(Program.ProgramID, ExecutionHours));

            var list_zone = new List<ZoneInProgram>();

            for (int x = 0; x < 4; x++)
            {
                var zone = new ZoneInProgram()
                {
                    FertQuant = 1.1m,
                    FertTime = 12,
                    OrderNumber = x,
                    ProgramID = Program.ProgramID,
                    WaterAfter = 3,
                    WaterBefore = 5,
                    WaterDuration = 3600,
                    WaterPrecipitation_Duration = 4,
                    WaterPrecipitation_Quantity = 3,
                    WaterQuantity = 6,
                    ZoneNumber = (byte)x,
                };
                list_zone.Add(zone);
                Assert.IsTrue(Repository.ZoneInProgram_Update(zone));
            }

        }


        [TestMethod]
        public void ProgramStory2_Test() //one program 
                                         //Weekly type , 
                                         //Interval in day 2 
                                         //StartTime at 7 am
                                         //Interval time 3Hour

        {
            var SN = "ff852d37-cda7-4295-a";
            if (string.IsNullOrEmpty(SN))
            {
                var device = CreateDevice();
                SN = device.SN;
            }

            var plist = Repository.DevicePrograms_Get(SN);
            Assert.IsTrue(plist != null && plist.Length > 0);
            // set only one to be active
            for (int i = 1; i < plist.Length; i++)
            {
                var p_fromList = plist[i];
                var ProgramID = p_fromList.ProgramID;

                var program = Repository.ProgramSetting_Get(ProgramID);
                program.StatusID = 0;
                Assert.IsTrue(Repository.ProgramSetting_Update(program));
            }

            var Program = plist[0];
            Program.DaysExecutionsTypeID = 0;
            Program.HoursExecutionsTypeID = 1;
            Program.HourlyCyclesPerDay = 2;
            Program.HourlyCyclesStartTime = 25200;//7 am
            Program.HourlyCycleTime = 10800;//Interval time 3  Hour
            Repository.ProgramSetting_Update(Program);
            var WeeklyDaysProgram = new WeeklyProgramSetting() { ProgramID = Program.ProgramID };
            WeeklyDaysProgram.Sunday = true;
            WeeklyDaysProgram.Monday = false;
            WeeklyDaysProgram.Thursday = true;
            WeeklyDaysProgram.Wednesday = false;
            WeeklyDaysProgram.Thursday = true;
            WeeklyDaysProgram.Friday = false;
            WeeklyDaysProgram.Saturday = true;

            Assert.IsTrue(Repository.WeeklyProgramSetting_Update(WeeklyDaysProgram));

            var list_zone = new List<ZoneInProgram>();

            for (int x = 0; x < 4; x++)
            {
                var zone = new ZoneInProgram()
                {
                    FertQuant = 1.1m,
                    FertTime = 12,
                    OrderNumber = x,
                    ProgramID = Program.ProgramID,
                    WaterAfter = 3,
                    WaterBefore = 5,
                    WaterDuration = 3600,
                    WaterPrecipitation_Duration = 4,
                    WaterPrecipitation_Quantity = 3,
                    WaterQuantity = 6,
                    ZoneNumber = (byte)x,
                };
                list_zone.Add(zone);
                Assert.IsTrue(Repository.ZoneInProgram_Update(zone));
            }

        }

        [TestMethod]
        public void ProgramStory3_Test() //one program 
                                         //CyclicDay type every3 day, 
                                          //2  exec time - one cross-midnight

        {
            var SN = "c32ad0de-9610-419c-8";
            if (string.IsNullOrEmpty(SN))
            {
                var device = CreateDevice();
                SN = device.SN;
            }

            var plist = Repository.DevicePrograms_Get(SN);
            Assert.IsTrue(plist != null && plist.Length > 0);
            // set only one to be active
            for (int i = 1; i < plist.Length; i++)
            {
                var p_fromList = plist[i];
                var ProgramID = p_fromList.ProgramID;

                var program = Repository.ProgramSetting_Get(ProgramID);
                program.StatusID = 0;
                Assert.IsTrue(Repository.ProgramSetting_Update(program));
            }

            var Program = plist[0];
            Program.DaysExecutionsTypeID = 1;
            var CyclicDay = new CyclicDayProgram()
            {
                DaysInterval = 3,
                ProgramID = Program.ProgramID,
                StartDate = new DateTime(2016,9,18)
            };
            Repository.CyclicDayProgram_Update(CyclicDay);

            var ExecutionHours = new ExecutionHours[] {
                                                                new ExecutionHours() { ExecTime = 28800 }, //8 am
                                                                new ExecutionHours() { ExecTime = 82800 }  //11 pm 23:00
                                                                                                           //{ ExecTime = 64800 } 6 pm ,18:00
                                                            };

            Assert.IsTrue(Repository.ExecutionHoursProgram_Update(Program.ProgramID, ExecutionHours));
            Repository.ProgramSetting_Update(Program);
           

            var list_zone = new List<ZoneInProgram>();

            for (int x = 0; x < 4; x++)
            {
                var zone = new ZoneInProgram()
                {
                    FertQuant = 1.1m,
                    FertTime = 12,
                    OrderNumber = x,
                    ProgramID = Program.ProgramID,
                    WaterAfter = 3,
                    WaterBefore = 5,
                    WaterDuration = 3600,
                    WaterPrecipitation_Duration = 4,
                    WaterPrecipitation_Quantity = 3,
                    WaterQuantity = 6,
                    ZoneNumber = (byte)x,
                };
                list_zone.Add(zone);
                Assert.IsTrue(Repository.ZoneInProgram_Update(zone));
            }

        }

        [TestMethod]
        public void ProgramStory4_Test() //one program 
                                         //Interval in day 2 
                                         //StartTime at 7 am
                                         //Interval time 3Hour

        {
            var SN = "33560133-c6eb-4d66-a";
            if (string.IsNullOrEmpty(SN))
            {
                var device = CreateDevice();
                SN = device.SN;
            }

            var plist = Repository.DevicePrograms_Get(SN);
            Assert.IsTrue(plist != null && plist.Length > 0);
            // set only one to be active
            for (int i = 1; i < plist.Length; i++)
            {
                var p_fromList = plist[i];
                var ProgramID = p_fromList.ProgramID;

                var program = Repository.ProgramSetting_Get(ProgramID);
                program.StatusID = 0;
                Assert.IsTrue(Repository.ProgramSetting_Update(program));
            }

            var Program = plist[0];
            Program.DaysExecutionsTypeID = 1;
            var CyclicDay = new CyclicDayProgram()
            {
                DaysInterval = 3,
                ProgramID = Program.ProgramID,
                StartDate = new DateTime(2016, 9, 18)
            };
            Repository.CyclicDayProgram_Update(CyclicDay);

            Program.HoursExecutionsTypeID = 1;
            Program.HourlyCyclesPerDay = 2;
            Program.HourlyCyclesStartTime = 25200;//7 am
            Program.HourlyCycleTime = 10800;//Interval time 3  Hour
            Repository.ProgramSetting_Update(Program);


            var list_zone = new List<ZoneInProgram>();

            for (int x = 0; x < 4; x++)
            {
                var zone = new ZoneInProgram()
                {
                    FertQuant = 1.1m,
                    FertTime = 12,
                    OrderNumber = x,
                    ProgramID = Program.ProgramID,
                    WaterAfter = 3,
                    WaterBefore = 5,
                    WaterDuration = 3600,
                    WaterPrecipitation_Duration = 4,
                    WaterPrecipitation_Quantity = 3,
                    WaterQuantity = 6,
                    ZoneNumber = (byte)x,
                };
                list_zone.Add(zone);
                Assert.IsTrue(Repository.ZoneInProgram_Update(zone));
            }

        }
    }
}
