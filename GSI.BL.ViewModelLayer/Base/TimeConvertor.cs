using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base
{
    public static class TimeConvertor
    {
        public const long DATETIME_UNIX_1970_1JAN = 621355968000000000;

        public static long GetTicks(DateTime date)
        {
            return ((date.Ticks - DATETIME_UNIX_1970_1JAN) / TimeSpan.TicksPerMillisecond);
        }

        public static DateTime GetDateTime(long tikcs)
        {
            return new DateTime().AddMilliseconds(TimeSpan.FromTicks(DATETIME_UNIX_1970_1JAN).TotalMilliseconds + tikcs);
        }

    }
}
