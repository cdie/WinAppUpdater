namespace WinAppUpdater.Models
{
    class LaunchParams
    {

        #region Properties

        public string URL { get; set; }
        public URLType URLType { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public LaunchMode Mode { get;  set; }
        public int PID{ get; set; }
        public string VersionFilePath { get; set; }

        #endregion

    }
}
