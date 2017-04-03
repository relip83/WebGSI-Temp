using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Program;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Setting;
using Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Types;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Program;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Repositories.AdminRepository.T_SQL;
using Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Models.Zone;

namespace GSI.BL.ViewModelLayer.Test
{
    [TestClass]
    public class ViewModelLayer_Test
    {
        private Galcon.GSI.Systems.GSI.DAL.DataAccessLayer.Repositories.AdminRepository.IAdminRepository _AdminRepository = null;

        public ViewModelLayer_Test()
        {
            _AdminRepository = new TSQLAdminRepository("GSI-Group_AdminDB");
        }

        [TestMethod]
        public void TestDurationTime()
        {
            ViewModelLayerSettings setting = new ViewModelLayerSettings();

            setting.AdminRepositoryFunc = () =>
            {
                return _AdminRepository;
            };

            ProgramModelManager p = new ProgramModelManager(setting);

            #region Quantity

            #region liter in flow & liter in program 

            int[] Quantity_liter = { 1000, 2000, 3000, 4000, 5000 };
            int[] Flow_l = { 4000, 5000, 6000, 7000, 8000 };
            double[] result_minute = { 15, 24, 30, 34.28571429, 37.5 };

            for (int i = 0; i < Quantity_liter.Length; i++)
            {
                var Quantity = Quantity_liter[i];
                var flow = Flow_l[i];
                var time = p.GetDurationTime(WaterMeter_FlowType.LPH, flow, WaterMeter_PulseType.LITER_AS_LITER, WaterProgramType.Quantity, null, new ZoneInProgram { WaterQuantity = Quantity });
                Assert.IsTrue((int)time / 60 == (int)result_minute[i]);
            }

            #endregion

            #region m3 in flow & liter in program 

            int[] Flow_m3 = { 4, 5, 6, 7, 8 };

            for (int i = 0; i < Quantity_liter.Length; i++)
            {
                var Quantity = Quantity_liter[i];
                var flow = Flow_m3[i];
                var time = p.GetDurationTime(WaterMeter_FlowType.M3PH, flow, WaterMeter_PulseType.LITER_AS_LITER, WaterProgramType.Quantity, null, new ZoneInProgram { WaterQuantity = Quantity });
                Assert.IsTrue((int)time / 60 == (int)result_minute[i]);
            }

            #endregion

            int[] Quantity_m3 = { 3, 4, 5, 6, 7 };
            double[] result_minute2 = { 45, 48, 50, 51.42857143, 52.5 };

            #region M3 in flow & m3 in program

            for (int i = 0; i < Quantity_m3.Length; i++)
            {
                var Quantity = Quantity_m3[i];
                var flow = Flow_m3[i];
                var time = p.GetDurationTime(WaterMeter_FlowType.M3PH, flow, WaterMeter_PulseType.M3_AS_M3, WaterProgramType.Quantity, null, new ZoneInProgram { WaterQuantity = Quantity });
                Assert.IsTrue((int)time / 60 == (int)result_minute2[i]);
            }

            #endregion
            #endregion

            #region  Quantity Precipitation 

            int[] Quantity_Precipitation = { 10, 11, 12, 13, 14 };
            double[] result_minute3 = { 195,184.8,180,178.2857143,210};
            int[] IrrigrationArea = { 1300,1400,1500,1600,2000};

            #region LandType 0

            for (int i = 0; i < Quantity_Precipitation.Length; i++)
            {
                var Quantity = Quantity_Precipitation[i];
                var flow = Flow_l[i];
                var time = p.GetDurationTime(WaterMeter_FlowType.LPH, flow, WaterMeter_PulseType.LITER_AS_LITER, WaterProgramType.Quantity_Precipitation,
                                              new ZoneSetting() { IrrigrationArea = IrrigrationArea [i]}, 
                                              new ZoneInProgram {WaterPrecipitation_Quantity = Quantity });
                Assert.IsTrue((int)time / 60 == (int)result_minute3[i]);
            }


            #endregion

            #endregion

            #region  Duration Precipitation

            int[] Duration_Precipitation = { 10, 11, 12, 13, 14 };
            double[] result_minute4 = { 150, 132, 120, 111.4285714, 105 };
            int[] Precipitation = { 4, 5, 6, 7, 8 };

           

            for (int i = 0; i < Duration_Precipitation.Length; i++)
            {
                var Duration = Duration_Precipitation[i];
                var time = p.GetDurationTime(WaterMeter_FlowType.LPH, 0, WaterMeter_PulseType.LITER_AS_LITER, WaterProgramType.Duration_Precipitation,
                                              new ZoneSetting() { PrecipitationRate = Precipitation [i]},
                                              new ZoneInProgram { WaterPrecipitation_Duration = Duration });
                Assert.IsTrue((int)time / 60 == (int)result_minute4[i]);
            }


           
            #endregion

        }
    }
}
