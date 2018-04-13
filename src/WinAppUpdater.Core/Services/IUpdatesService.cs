using System;
using System.Collections.Generic;
using System.Text;
using WinAppUpdater.Core.Models.Updates;

namespace WinAppUpdater.Core.Services
{
    /// <summary>
    /// Contract interface for retrieving updates information.
    /// </summary>
    public interface IUpdatesService
    {

        #region Public methods

        /// <summary>
        /// Retrieve all updates of a specific app.
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns>Collection of update.</returns>
        IEnumerable<UpdateVersion> GetUpdates(string appName);

        #endregion

    }
}
