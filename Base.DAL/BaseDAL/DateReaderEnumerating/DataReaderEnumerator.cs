using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

using System.Data;

namespace Galcon.DAL.BaseDAL
{
    public class DataReaderEnumerator : IDisposable
    {
        #region Properties

        public DataReaderRow CurrentRow { get; private set; }

        public bool LastResult { get; private set; }
        internal delegate DbDataReader RunProcedureDelegate(out bool Result);
        internal RunProcedureDelegate RunProcedureHandler;
        public bool IsDisposed { get; private set; }

        #endregion

        #region constructors

        public DataReaderEnumerator()
        {
            IsDisposed = false;
        }

        #endregion

        #region static

        public static void HandleDataReader(DbDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                reader.Close();
            }
        }

        #endregion

        #region public method

        public void Close()
        {
            Dispose();
        }

        public void Open()
        {
            if (CurrentRow == null)
            {
                bool result = false;
                DbDataReader reader = RunProcedureHandler(out result);

                if (!result || reader == null)
                {
                    HandleDataReader(reader);
                    CurrentRow = new EmptyDataReaderRow();
                }
                else
                {
                    CurrentRow = new DataReaderRow(reader);
                }
            }
        }

        public bool Read()
        {
            Open();

            LastResult =
                !IsDisposed
                            && CurrentRow != null
                            && CurrentRow.isValid
                            && CurrentRow.DataReader != null
                            && CurrentRow.DataReader.HasRows
                            && CurrentRow.DataReader.Read();

            if (!LastResult)
                Dispose();
            else
                CurrentRow.RowIndex = CurrentRow.RowIndex.HasValue ? CurrentRow.RowIndex++ : 0;

            return LastResult;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            IsDisposed = true;

            if (CurrentRow != null)
            {
                HandleDataReader(CurrentRow.DataReader);
                CurrentRow = null;
            }
        }

        #endregion
    }
}
