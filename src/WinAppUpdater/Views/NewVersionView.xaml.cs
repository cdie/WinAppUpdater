using CQELight.MVVM.MahApps;
using MahApps.Metro.Controls;
using System.Diagnostics;
using WinAppUpdater.Models;
using WinAppUpdater.ViewModels;

namespace WinAppUpdater.Views
{

    public partial class NewVersionView : CQEMetroWindow
    {

        #region Ctor

        internal NewVersionView(string appName, string appVersion, Process appProcess, string versionFilePath)//, UpdateVersion version)
        {
            InitializeComponent();
            DataContext = new NewVersionViewModel(this, appName, appVersion, appProcess, versionFilePath);//, version);
        }

        #endregion
    }
}
