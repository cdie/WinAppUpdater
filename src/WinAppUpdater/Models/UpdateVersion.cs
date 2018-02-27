using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppUpdater.Models
{
    class UpdateVersion
    {

        #region Properties

        public string Version { get; set; }
        public string Changelog { get; set; }
        public string PathToUpdateZip { get; set; }

        #endregion

    }
}
