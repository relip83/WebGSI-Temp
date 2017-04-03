using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Setting= Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Setting;

namespace Galcon.GSI.Systems.GSIGroup.BL.ViewModelLayer.Base
{
    public abstract class BaseViewModelManager :  IDisposable
    {
        #region properties

        public Setting.ViewModelLayerSettings CurrentSettings { get; protected set; }

        #endregion

        #region ctor

        public BaseViewModelManager(Setting.ViewModelLayerSettings currentSettings)
        {
            CurrentSettings = currentSettings;
        }

        #endregion

        #region IDisposable members

        public void Dispose()
        {
            OnDispose();
        }

        #endregion

        #region abstract methods

        protected abstract void OnDispose();

        #endregion
    }

}
