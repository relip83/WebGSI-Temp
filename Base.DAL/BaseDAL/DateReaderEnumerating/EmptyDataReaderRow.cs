﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Galcon.DAL.BaseDAL
{
    public class EmptyDataReaderRow : DataReaderRow
    {
        public EmptyDataReaderRow()
            : base(null)
        {

        }
    }
}
