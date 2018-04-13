namespace WinAppUpdater.Models
{
    class CommandLine  
    {

        #region Properties

        public LaunchMode Mode { get; set; }
        public int PID { get; set; }
        public string VersionFilePath { get; set; }

        #endregion

    }
}
