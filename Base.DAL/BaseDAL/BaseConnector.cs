using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galcon.DAL.BaseDAL
{
    public class BaseConnector : IDisposable
    {
        #region public properties

        public BaseDALConnector Connector { get; private set; }
        public bool ThrowExceptions { get; set; }

        #endregion

        #region ctor (s)

        private BaseConnector()
        {
            ThrowExceptions = false;
        }

        public BaseConnector(Connection connection)
            : this()
        {
            Connector = BaseDALConnector.Create(connection.ProviderName, connection.ConnectionString);
            Connector.ThrowExceptions = this.ThrowExceptions;
        }

        public BaseConnector(string providerName, string stringConnection)
            : this()
        {
            Connector = BaseDALConnector.Create(providerName, stringConnection);
            Connector.ThrowExceptions = this.ThrowExceptions;
        }

        public BaseConnector(string stringConnectionSectionName)
            : this()
        {
            Connector = BaseDALConnector.Create(stringConnectionSectionName);
            Connector.ThrowExceptions = this.ThrowExceptions;
        }

        #endregion

        #region IDisposable

        public virtual void Dispose()
        {
            if (Connector != null && !Connector.IsDisposed)
            {
                Connector.Dispose();
            }
        }

        #endregion
    }
}
