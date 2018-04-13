namespace WinAppUpdater.Core.Models.Updates
{
    class Configuration
    {

        #region Properties

        public string URL { get; set; }
        public URLType URLType { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }

        #endregion

    }
}
