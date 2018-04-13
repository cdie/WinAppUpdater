using System;

namespace WinAppUpdater.Core.Models.Updates
{
    /// <summary>
    /// Specific version of update.
    /// </summary>
    public class UpdateVersion
    {

        #region Properties

        /// <summary>
        /// Version number.
        /// </summary>
        public string Version { get; private set; }
        /// <summary>
        /// Changelog of the version.
        /// </summary>
        public string Changelog { get; private set; }

        #endregion

        #region Ctor

        private UpdateVersion()
        {

        }

        public UpdateVersion(string version, string changelog)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                throw new ArgumentException("UpdateVersion.ctor() : The version must be specified.", nameof(version));
            }

            Version = version;
            Changelog = changelog;
        }

        #endregion

    }
}
