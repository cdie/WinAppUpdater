using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppUpdater.Models
{
    class AppVersions
    {

        #region Properties

        public string AppName { get; set; }

        public List<UpdateVersion> Versions { get; set; }

        #endregion

    }
}
