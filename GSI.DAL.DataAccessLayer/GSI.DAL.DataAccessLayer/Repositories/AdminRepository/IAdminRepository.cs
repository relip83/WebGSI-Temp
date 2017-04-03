using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Device;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Zone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Repositories.AdminRepository
{
    public interface IAdminRepository
    {
       

        #region Setting

        GeneralSettings GeneralSettings_Get(string SN);
        bool GeneralSettings_Update(string SN, GeneralSettings GeneralSettings);

        FertilizerSetting FertilizerSetting_Get(string SN);
        bool FertilizerSetting_Update(string SN, FertilizerSetting FertilizerSetting);

        RestrictedDates[] IrrExceptionDates_Get(string SN);
        bool IrrExceptionDates_Update(string SN, RestrictedDates[] IrrgationSetting);
       
        MainPipeSettings MainPipeSettings_Get(string SN);
        bool MainPipeSettings_Update(string SN, MainPipeSettings MainPipeSettings);

        WaterMeterSetting WaterMeterSetting_Get(string SN);
        bool WaterMeterSetting_Update(string SN, WaterMeterSetting WaterMeterSetting);
        AlertsSetting[] AlertSettings_Get(string sN);
        bool AlertSettings_Update(string SN, AlertsSetting WaterMeterSetting);

        #endregion

        #region CRUD operations
        long? AddDevice(string SN, int ModelID);
        Models.Device.DeviceBase GetDevice(string SN);

        #endregion

        #region Zones
        ZoneSetting[] DeviceZoneSetting_Get(string sN);
        bool ZoneSetting_Update(string sN, ZoneSetting item);
        ZoneSetting ZoneSetting_Get(string sN, int zoneNumber);

        #endregion

        #region Program
        ProgramSetting[] DevicePrograms_Get(string sN);

        ProgramSetting ProgramSetting_Get(long ProgramID);

        bool ProgramSetting_Update(ProgramSetting Program);

        CyclicDayProgram CyclicDayProgram_Get( long ProgramID);

        bool CyclicDayProgram_Update( CyclicDayProgram Setting);

        WeeklyProgramSetting WeeklyProgramSetting_Get(long ProgramID);

        bool WeeklyProgramSetting_Update(WeeklyProgramSetting Setting);

        ExecutionHours[] ExecutionHoursProgram_Get(long ProgramID);

        bool ExecutionHoursProgram_Update(long ProgramID, ExecutionHours[] Times);

        ZoneInProgram[] ZonesInProgram_Get(long ProgramID);

        bool ZoneInProgram_Update( ZoneInProgram Zone);
        bool ZoneInProgram_Delete(byte zoneNumber, long programID);

        #endregion


    }
}
