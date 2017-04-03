using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using Galcon.DAL.BaseDAL;
using System.Data;
using System.Data.Common;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Zone;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Repositories.AdminRepository.T_SQL
{
    public class TSQLAdminRepository : BaseConnector, IAdminRepository
    {
        #region CONSTANTS

        public const string DEFAULT_STRING_CONNECTION = "GSI-Group_AdminDB";

        #endregion

        #region ctor

        public TSQLAdminRepository()
            : base(DEFAULT_STRING_CONNECTION)
        {

        }

        public TSQLAdminRepository(string providerName, string stringConnection)
            : base(providerName, stringConnection)
        {

        }
        public TSQLAdminRepository(string stringConnectionSectionName)
            : base(stringConnectionSectionName)
        {

        }

        public TSQLAdminRepository(Connection connection)
            : base(connection)
        {

        }

        #endregion

        private static IEnumerable<SqlDataRecord> CreateSqlMetaDataItem(RestrictedDates[] items)
        {
            SqlMetaData[] metaData = new SqlMetaData[1];
            metaData[0] = new SqlMetaData("Date", SqlDbType.DateTime);

            return items
                .Select(r =>
                {
                    var record = new SqlDataRecord(metaData);
                    record.SetDateTime(0, r.ExceptionDate);
                    return record;
                });
        }


        private static IEnumerable<SqlDataRecord> CreateSqlMetaDataItem(ExecutionHours[] items)
        {
            SqlMetaData[] metaData = new SqlMetaData[1];
            metaData[0] = new SqlMetaData("ExecTime", SqlDbType.Int);

            return items
                .Select(r =>
                {
                    var record = new SqlDataRecord(metaData);
                    record.SetInt32(0, r.ExecTime);
                    return record;
                });
        }

        #region Device

        #region Setting

        public GeneralSettings GeneralSettings_Get(string SN)
        {
            return Connector.GetEntity<Models.Device.GeneralSettings>(this.Connector.CreateProcedureEnumerator("Config.GeneralSettings_Get",
                                                                      new IDataParameter[] {
                                                                      Connector.CreateParameter("SN",SN)
                                                                     }));
        }

        public bool GeneralSettings_Update(string SN, GeneralSettings GeneralSettings)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.GeneralSettings_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("SN",SN),
                                                                        Connector.CreateParameter("IsMetric",GeneralSettings.IsMetric),
                                                                        Connector.CreateParameter("LandTypeID",GeneralSettings.LandTypeID),
                                                                         Connector.CreateParameter("AdvancedFert",GeneralSettings.Flag_AdvancedFert),
                                                                        Connector.CreateParameter("CustomTimeZoneID",GeneralSettings.CustomTimeZoneID),
                                                                        Connector.CreateParameter("StatusID",GeneralSettings.Status),
                                                                        Connector.CreateParameter("ModelID",GeneralSettings.ModelID),
                                                                        Connector.CreateParameter("Name",GeneralSettings.Name)
                                                                         },
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }


        public FertilizerSetting FertilizerSetting_Get(string SN)
        {
            return Connector.GetEntity<Models.Device.FertilizerSetting>(this.Connector.CreateProcedureEnumerator("Config.FertilizerSetting_Get",
                                                                      new IDataParameter[] {
                                                                      Connector.CreateParameter("SN",SN)
                                                                     }));
        }

        public bool FertilizerSetting_Update(string SN, FertilizerSetting FertilizerSetting)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.FertilizerSetting_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("SN",SN),
                                                                        Connector.CreateParameter("ContinuousFert",FertilizerSetting.ContinuousFert),
                                                                        Connector.CreateParameter("FerlizerFaillureTime",FertilizerSetting.FerlizerFaillureTime),
                                                                        Connector.CreateParameter("FlowTypeID",FertilizerSetting.FlowTypeID),
                                                                        Connector.CreateParameter("IsEnabled",FertilizerSetting.IsEnabled),
                                                                        Connector.CreateParameter("Leakage",FertilizerSetting.Leakage),
                                                                        Connector.CreateParameter("NominalFlow",FertilizerSetting.NominalFlow),
                                                                        Connector.CreateParameter("OutputNumber",FertilizerSetting.OutputNumber),
                                                                        Connector.CreateParameter("PulseSize",FertilizerSetting.PulseSize),
                                                                        Connector.CreateParameter("PulseTime",FertilizerSetting.PulseTime),
                                                                        Connector.CreateParameter("PulseTypeID",FertilizerSetting.PulseTypeID),
                                                                        Connector.CreateParameter("TypeID",FertilizerSetting.TypeID),
                                                                        },
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }

        public RestrictedDates[] IrrExceptionDates_Get(string SN)
        {
            return Connector.GetEntities<RestrictedDates>(this.Connector.CreateProcedureEnumerator("Config.IrrExceptionDates_Get",
                                                                      new IDataParameter[] {
                                                                      Connector.CreateParameter("SN",SN),
                                                                     })).ToArray();
        }

        public bool IrrExceptionDates_Update(string SN, RestrictedDates[] IrrgationSetting)
        {
            var Parameter = Connector.CreateParameter("Items", CreateSqlMetaDataItem(IrrgationSetting)) as SqlParameter;
            Parameter.SqlDbType = SqlDbType.Structured;
            Parameter.TypeName = "config.TimeValue";
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.IrrExceptionDates_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("SN",SN),
                                                                        Parameter,
                                                                        },
                                                                        out rowsAffected,
                                                                        out Result);
            return Result;

        }

        public MainPipeSettings MainPipeSettings_Get(string SN)
        {
            return Connector.GetEntity<Models.Device.MainPipeSettings>(this.Connector.CreateProcedureEnumerator("Config.MainPipeSettings_Get",
                                                                      new IDataParameter[] {
                                                                      Connector.CreateParameter("SN",SN)
                                                                     }));
        }

        public bool MainPipeSettings_Update(string SN, MainPipeSettings MainPipeSettings)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.MainPipeSettings_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("SN",SN),
                                                                        Connector.CreateParameter("RainDetectorEnabled",MainPipeSettings.RainDetectorEnabled),
                                                                        Connector.CreateParameter("RainDetectorNC",MainPipeSettings.RainDetectorNC),
                                                                        Connector.CreateParameter("RainyDaysStopper",MainPipeSettings.RainyDaysStopper),
                                                                        Connector.CreateParameter("UseMaster",MainPipeSettings.UseMaster),
                                                                        Connector.CreateParameter("OverlapTime",MainPipeSettings.OverlapTime),
                                                                        Connector.CreateParameter("DSundayState",MainPipeSettings.DSundayState),
                                                                        Connector.CreateParameter("DMondayState",MainPipeSettings.DMondayState),
                                                                        Connector.CreateParameter("DTuesdayState",MainPipeSettings.DTuesdayState),
                                                                        Connector.CreateParameter("DWednesdayState",MainPipeSettings.DWednesdayState),
                                                                        Connector.CreateParameter("DThursdayState",MainPipeSettings.DThursdayState),
                                                                        Connector.CreateParameter("DFridayState",MainPipeSettings.DFridayState),
                                                                        Connector.CreateParameter("DSaturdayState",MainPipeSettings.DSaturdayState),
                                                                        Connector.CreateParameter("ProgramsAsQueue",MainPipeSettings.ProgramsAsQueue),
                                                                        Connector.CreateParameter("IsLocalSequenceActive",MainPipeSettings.IsLocalSequenceActive),
                                                                        Connector.CreateParameter("WaterFactor",MainPipeSettings.WaterFactor)
                                                                        },
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }

        public WaterMeterSetting WaterMeterSetting_Get(string SN)
        {
            return Connector.GetEntity<Models.Device.WaterMeterSetting>(this.Connector.CreateProcedureEnumerator("Config.WaterMeterSetting_Get",
                                                                     new IDataParameter[] {
                                                                      Connector.CreateParameter("SN",SN)
                                                                    }));
        }

        public bool WaterMeterSetting_Update(string SN, WaterMeterSetting WaterMeterSetting)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.WaterMeterSetting_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("SN",SN),
                                                                        Connector.CreateParameter("IsEnabled",WaterMeterSetting.IsEnabled),
                                                                        Connector.CreateParameter("FlowTypeID",WaterMeterSetting.FlowTypeID),
                                                                        Connector.CreateParameter("LeakageLimit",WaterMeterSetting.LeakageLimit),
                                                                        Connector.CreateParameter("MeterTypeID",WaterMeterSetting.MeterTypeID),
                                                                        Connector.CreateParameter("NoWaterPulseDelay",WaterMeterSetting.NoWaterPulseDelay),
                                                                        Connector.CreateParameter("PulseSize",WaterMeterSetting.PulseSize),
                                                                        Connector.CreateParameter("PulseTypeID",WaterMeterSetting.PulseTypeID)
                                                                        },
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }

        public AlertsSetting[] AlertSettings_Get(string SN)
        {
            return Connector.GetEntities<Models.Device.AlertsSetting>(this.Connector.CreateProcedureEnumerator("Config.AlertDeviceSettings_Get",
                                                                     new IDataParameter[] {
                                                                      Connector.CreateParameter("SN",SN)
                                                                    })).ToArray();
        }

        public bool AlertSettings_Update(string SN, AlertsSetting alertsSetting)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.AlertSettings_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("SN",SN),
                                                                        Connector.CreateParameter("IsEnable",alertsSetting.IsEnable),
                                                                        Connector.CreateParameter("SendEmail",alertsSetting.SendEmail),
                                                                        Connector.CreateParameter("SendSMS",alertsSetting.SendSMS),
                                                                        Connector.CreateParameter("AlertCode",alertsSetting.AlertCode)

                                                                        }
                                                                       ,
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }
        #endregion

        #region CRUD operations
        public long? AddDevice(string SN, int ModelID)
        {
            bool Result = false;
            int rowsAffected = 0;
            long DeviceID = Connector.GetProcedureResultInt64("Device.AddDevice", new IDataParameter[] {
                                                                        Connector.CreateParameter("ModelID",ModelID),
                                                                        Connector.CreateParameter("SN",SN)
                                                                    }, out rowsAffected, out Result);
            return DeviceID;

        }
        public Models.Device.DeviceBase GetDevice(string SN)
        {
            return Connector.GetEntity<Models.Device.DeviceBase>(this.Connector.CreateProcedureEnumerator("Device.GetDevice",
                                                                   new IDataParameter[] {
                                                                      Connector.CreateParameter("SN",SN)
                                                                    }));
        }
        #endregion

        #region Zone

        public ZoneSetting[] DeviceZoneSetting_Get(string SN)
        {
            return Connector.GetEntities<Models.Zone.ZoneSetting>(this.Connector.CreateProcedureEnumerator("Config.DeviceZonesSetting_Get",
                                                                     new IDataParameter[] {
                                                                      Connector.CreateParameter("SN",SN)
                                                                    })).ToArray();
        }
        public bool ZoneSetting_Update(string SN, ZoneSetting item)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.ZoneSetting_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("SN",SN),
                                                                        Connector.CreateParameter("IrrigrationArea",item.IrrigrationArea),
                                                                        Connector.CreateParameter("FertilizerConnected",item.FertilizerConnected),
                                                                        Connector.CreateParameter("HighFlowDeviation",item.HighFlowDeviation),
                                                                        Connector.CreateParameter("HighFlowFaultDelay",item.HighFlowFaultDelay),
                                                                        Connector.CreateParameter("LowFlowDeviation",item.LowFlowDeviation),
                                                                        Connector.CreateParameter("LowFlowFaultDelay",item.LowFlowFaultDelay),
                                                                        Connector.CreateParameter("Name",item.Name),
                                                                        Connector.CreateParameter("OutputNumber",item.OutputNumber),
                                                                        Connector.CreateParameter("PrecipitationRate",item.PrecipitationRate),
                                                                        Connector.CreateParameter("SetupNominalFlow",item.SetupNominalFlow),
                                                                        Connector.CreateParameter("StopOnFertFailure",item.StopOnFertFailure),
                                                                        Connector.CreateParameter("TypeID",item.TypeID),
                                                                        Connector.CreateParameter("StatusID",item.StatusID),
                                                                        Connector.CreateParameter("TimeFillDelay",item.TimeFillDelay),
                                                                        Connector.CreateParameter("ZoneColor ",item.ZoneColor),
                                                                        }
                                                                       ,
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }

        public ZoneSetting ZoneSetting_Get(string SN, int zoneNumber)
        {
            return Connector.GetEntity<Models.Zone.ZoneSetting>(this.Connector.CreateProcedureEnumerator("Config.ZoneSetting_Get",
                                                                     new IDataParameter[] {
                                                                      Connector.CreateParameter("SN",SN),
                                                                      Connector.CreateParameter("OutputNumber",zoneNumber)
                                                                    }));
        }

        #endregion

        #region Program

        public ProgramSetting[] DevicePrograms_Get(string SN)
        {
            return Connector.GetEntities<ProgramSetting>(this.Connector.CreateProcedureEnumerator("Config.DevicePrograms_Get",
                                                                     new IDataParameter[] {
                                                                      Connector.CreateParameter("SN",SN)
                                                                    })).ToArray();
        }

        public ProgramSetting ProgramSetting_Get( long ProgramID)
        {
            return Connector.GetEntity<Models.Program.ProgramSetting>(this.Connector.CreateProcedureEnumerator("Config.ProgramSetting_Get",
                                                                      new IDataParameter[] {
                                                                      Connector.CreateParameter("ProgramID",ProgramID)
                                                                     }));
        }

        public bool ProgramSetting_Update(ProgramSetting Program)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.ProgramSetting_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("DaysExecutionsTypeID",Program.DaysExecutionsTypeID),
                                                                        Connector.CreateParameter("FertUnitID",Program.FertUnitID),
                                                                        Connector.CreateParameter("FinalEndDateLimit",Program.FinalEndDateLimit),
                                                                        Connector.CreateParameter("FinalStartDateLimit",Program.FinalStartDateLimit),
                                                                        Connector.CreateParameter("FinalStartHoursLimit",Program.FinalStartHoursLimit),
                                                                        Connector.CreateParameter("FinalStopHoursLimit",Program.FinalStopHoursLimit),
                                                                        Connector.CreateParameter("HourlyCyclesPerDay",Program.HourlyCyclesPerDay),
                                                                        Connector.CreateParameter("Name",Program.Name),
                                                                        Connector.CreateParameter("HourlyCyclesStartTime",Program.HourlyCyclesStartTime),
                                                                        Connector.CreateParameter("HourlyCyclesStopTime",Program.HourlyCyclesStopTime),
                                                                        Connector.CreateParameter("HourlyCycleTime",Program.HourlyCycleTime),
                                                                        Connector.CreateParameter("HoursExecutionsTypeID",Program.HoursExecutionsTypeID),
                                                                        Connector.CreateParameter("IsFertilizerON",Program.IsFertilizerON),
                                                                        Connector.CreateParameter("StatusID",Program.StatusID),
                                                                        Connector.CreateParameter("Priority",Program.Priority),
                                                                        Connector.CreateParameter("ProgramID",Program.ProgramID),
                                                                        Connector.CreateParameter("WaterFactor",Program.WaterFactor),
                                                                        Connector.CreateParameter("WaterUnitID ",Program.WaterUnitID),
                                                                        }
                                                                       ,
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }

        public CyclicDayProgram CyclicDayProgram_Get( long ProgramID)
        {
            return Connector.GetEntity<Models.Program.CyclicDayProgram>(this.Connector.CreateProcedureEnumerator("Config.CyclicDayProgram_Get",
                                                                     new IDataParameter[] {
                                                                      Connector.CreateParameter("ProgramID",ProgramID)
                                                                    }));
        }

        public bool CyclicDayProgram_Update(CyclicDayProgram Setting)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.CyclicDayProgram_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("ProgramID",Setting.ProgramID),
                                                                        Connector.CreateParameter("DaysInterval",Setting.DaysInterval),
                                                                        Connector.CreateParameter("StartDate",Setting.StartDate),
                                                                        }
                                                                       ,
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }

        public WeeklyProgramSetting WeeklyProgramSetting_Get(long ProgramID)
        {
            return Connector.GetEntity<Models.Program.WeeklyProgramSetting>(this.Connector.CreateProcedureEnumerator("Config.WeeklyProgramSetting_Get",
                                                                     new IDataParameter[] {
                                                                      Connector.CreateParameter("ProgramID",ProgramID)
                                                                    }));
        }

        public bool WeeklyProgramSetting_Update(WeeklyProgramSetting Setting)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.WeeklyProgramSetting_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("ProgramID",Setting.ProgramID),
                                                                        Connector.CreateParameter("Sunday",Setting.Sunday),
                                                                        Connector.CreateParameter("Monday",Setting.Monday),
                                                                        Connector.CreateParameter("Tuesday",Setting.Tuesday),
                                                                        Connector.CreateParameter("Wednesday",Setting.Wednesday),
                                                                        Connector.CreateParameter("Thursday",Setting.Thursday),
                                                                        Connector.CreateParameter("Friday",Setting.Friday),
                                                                        Connector.CreateParameter("Saturday",Setting.Saturday)
                                                                        }
                                                                       ,
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }

        public ExecutionHours[] ExecutionHoursProgram_Get(long ProgramID)
        {
            return Connector.GetEntities<ExecutionHours>(this.Connector.CreateProcedureEnumerator("Config.ExecutionHoursProgram_Get",
                                                                      new IDataParameter[] {
                                                                      Connector.CreateParameter("ProgramID",ProgramID)
                                                                     })).ToArray();
        }

        public bool ExecutionHoursProgram_Update(long ProgramID, ExecutionHours[] Times)
        {
            var Result = false;
            int rowsAffected = 0;
            var Parameter = Connector.CreateParameter("Items", CreateSqlMetaDataItem(Times)) as SqlParameter;
            Parameter.SqlDbType = SqlDbType.Structured;
            Parameter.TypeName = "[Config].ExecutionHours";
            Connector.GetProcedureResultInt32("config.ExecutionHoursProgram_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("ProgramID",ProgramID),
                                                                        Parameter,
                                                                        }
                                                                       ,
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }


        public ZoneInProgram[] ZonesInProgram_Get(long ProgramID)
        {
            return Connector.GetEntities<ZoneInProgram>(this.Connector.CreateProcedureEnumerator("Config.ZonesInProgram_Get",
                                                                      new IDataParameter[] {
                                                                      Connector.CreateParameter("ProgramID",ProgramID)
                                                                     })).ToArray();
        }

        public bool ZoneInProgram_Update(ZoneInProgram Zone)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.ZoneInProgram_Update",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("ProgramID",Zone.ProgramID),
                                                                        Connector.CreateParameter("FertQuant",Zone.FertQuant),
                                                                        Connector.CreateParameter("FertTime",Zone.FertTime),
                                                                        Connector.CreateParameter("OrderNumber",Zone.OrderNumber),
                                                                        Connector.CreateParameter("WaterAfter",Zone.WaterAfter),
                                                                        Connector.CreateParameter("WaterBefore",Zone.WaterBefore),
                                                                        Connector.CreateParameter("WaterQuantity",Zone.WaterQuantity),
                                                                        Connector.CreateParameter("WaterDuration",Zone.WaterDuration),
                                                                        Connector.CreateParameter("WaterPrecipitation_Duration",Zone.WaterPrecipitation_Duration),
                                                                        Connector.CreateParameter("WaterPrecipitation_Quantity",Zone.WaterPrecipitation_Quantity),
                                                                        Connector.CreateParameter("ZoneNumber",Zone.ZoneNumber)
                                                                        }
                                                                       ,
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }


        public bool ZoneInProgram_Delete(byte zoneNumber, long programID)
        {
            var Result = false;
            int rowsAffected = 0;
            Connector.GetProcedureResultInt32("config.ZoneInProgram_Delete",
                                                          new IDataParameter[] {
                                                                        Connector.CreateParameter("ProgramID",programID),
                                                                        Connector.CreateParameter("ZoneNumber",zoneNumber)
                                                                        }
                                                                       ,
                                                                        out rowsAffected,
                                                                        out Result);

            return Result;
        }



        #endregion

        #endregion
    }
}
