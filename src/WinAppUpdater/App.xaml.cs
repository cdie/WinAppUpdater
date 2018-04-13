using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using WinAppUpdater.Core;
using WinAppUpdater.Models;
using WinAppUpdater.Views;
//using Microsoft.

namespace WinAppUpdater
{
    public partial class App : Application
    {

        #region Consts

        private const string CONST_ALIAS_MODE = "mode";
        private const string CONST_ALIAS_URL = "url";
        private const string CONST_ALIAS_URLTYPE = "urlType";
        private const string CONST_ALIAS_APP_VERSION = "appVersion";
        private const string CONST_ALIAS_APP_NAME = "appName";
        private const string CONST_ALIAS_LAUNCH_PROCESS_ID = "pid";
        private const string CONST_ALIAS_VERSION_FILEPATH = "versionFilePath";
        private const string CONST_ALIAS_SILENT_MODE = "silentMode";

        #endregion

        #region Members

        private bool _silentMode;

        #endregion

        #region Properties
        

        #endregion

        #region Ctor

        public App()
        {
            

            var commandLines = Environment.GetCommandLineArgs();
            if (commandLines.Length <= 1)
            {
                Environment.Exit(1);
            }

            var launchParams = ParseCommandLineArgs(commandLines);
            ActAsParams(launchParams);

            AppDomain.CurrentDomain.UnhandledException += (s, e)
                => File.WriteAllText("UpdaterExc.txt", e.ExceptionObject?.ToString());
            CosturaUtility.Initialize();
        }

        #endregion

        #region Private methods

        private CommandLine ParseCommandLineArgs(string[] commandLines)
        {
            var launchParams = new CommandLine();
            foreach (var item in commandLines)
            {
                try
                {
                    var alias = item.Substring(0, item.IndexOf(':'));
                    var commandData = item.Substring(item.IndexOf(':') + 1);
                    switch (alias)
                    {
                        //case CONST_ALIAS_URL:
                        //    launchParams.URL = commandData;
                        //    break;
                        //case CONST_ALIAS_URLTYPE:
                        //    launchParams.URLType = (URLType)System.Enum.Parse(typeof(URLType), commandData);
                        //    break;
                        //case CONST_ALIAS_APP_NAME:
                        //    launchParams.AppName = commandData;
                        //    break;
                        //case CONST_ALIAS_APP_VERSION:
                        //    launchParams.AppVersion = commandData;
                            //break;
                        case CONST_ALIAS_MODE:
                            launchParams.Mode = (LaunchMode)System.Enum.Parse(typeof(LaunchMode), commandData);
                            break;
                        case CONST_ALIAS_LAUNCH_PROCESS_ID:
                            launchParams.PID = int.Parse(commandData);
                            break;
                        case CONST_ALIAS_VERSION_FILEPATH:
                            launchParams.VersionFilePath = commandData;
                            break;
                        case CONST_ALIAS_SILENT_MODE:
                            _silentMode = commandData == "1";
                            break;
                    }
                }
                catch (Exception)
                {
                    if (!_silentMode)
                    {
                        MessageBox.Show($"Command-line arg {item} could not be parsed.");
                    }

                }
            }
            return launchParams;
        }

        private void ActAsParams(CommandLine @params)
        {
            if (@params.Mode == LaunchMode.Check)
            {
                //if (!string.IsNullOrWhiteSpace(@params.URL))
                //{
                //    if (@params.URLType == URLType.Local)
                //    {
                //        if (!File.Exists(@params.URL))
                //        {
                //            MessageBox.Show($"L'URL {@params.URL} vers le fichier des infos de MAJ n'existe pas.");
                //            Environment.Exit(2);
                //        }
                //        try
                //        {
                //            var fileData = JsonConvert.DeserializeObject<IEnumerable<AppVersions>>(File.ReadAllText(@params.URL));
                //            var appData = fileData.FirstOrDefault(a => a.AppName == @params.AppName);
                //            if (appData != null)
                //            {
                //                var lastVersion = appData.Versions.OrderByDescending(d => d.Version).FirstOrDefault();
                //                if (lastVersion != null)
                //                {
                //                    if (string.Compare(lastVersion.Version, @params.AppVersion, true) > 0)
                //                    {
                //                        var processInfo = Process.GetProcessById(@params.PID);
                //                        var newVersion = new NewVersionView(@params.AppName, @params.AppVersion, processInfo,
                //                            @params.VersionFilePath, lastVersion);
                //                        Application.Current.MainWindow = newVersion;
                //                        newVersion.Show();
                //                    }
                //                }
                //            }
                //        }
                //        catch (Exception e)
                //        {
                //            MessageBox.Show($"Erreur lors du traitement. {Environment.NewLine}{e}");
                //        }

                //    }
                //    else
                //    {

                //    }
                //}
                //else
                //{
                //    MessageBox.Show("L'URL de récupération des infos de mise à jour n'a pas été fournie.");
                //}
            }
            else if (@params.Mode == LaunchMode.Edition)
            {
                MessageBox.Show("Le mode édition n'est pas encore supporté.");
                Environment.Exit(0);
            }

            if (Application.Current.Windows.Count <= 0)
            {
                Environment.Exit(0);
            }
        }

        #endregion

    }
}
