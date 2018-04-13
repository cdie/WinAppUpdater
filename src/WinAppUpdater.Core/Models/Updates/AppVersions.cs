using System.Collections.Generic;

namespace WinAppUpdater.Core.Models.Updates
{
    /// <summary>
    /// Class that contains versions for a specific app.
    /// </summary>
    public class AppVersions
    {

        #region Properties

        /// <summary>
        /// Name of the application.
        /// </summary>
        public string AppName { get; private set; }
        /// <summary>
        /// List of all updates.
        /// </summary>
        public IEnumerable<UpdateVersion> Versions { get; private set; }

        #endregion

        #region Ctor

        private AppVersions()
        {

        }

        public AppVersions(string appName, IEnumerable<UpdateVersion> versions)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new System.ArgumentException("AppVersions.ctor() : The app name must be specified.", nameof(appName));
            }

            AppName = appName;
            Versions = versions;
        }

        #endregion

    }
}
