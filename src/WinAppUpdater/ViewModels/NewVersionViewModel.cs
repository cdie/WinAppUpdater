using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WinAppUpdater.Models;

namespace WinAppUpdater.ViewModels
{
    class NewVersionViewModel : INotifyPropertyChanged
    {

        #region Members

        private readonly UpdateVersion _version;
        private readonly Process _appProcess;
        private readonly string _versionFilePath;
        private readonly MetroWindow _holderWindow;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties


        public string AppName
        {
            get => _appName;
            set => Set(ref _appName, value);
        }
        private string _appName;

        public string AppVersion
        {
            get => _appVersion;
            set => Set(ref _appVersion, value);
        }
        private string _appVersion;

        public string NewVersion
        {
            get => _newVersion;
            set => Set(ref _newVersion, value);
        }
        private string _newVersion;

        public string Changelog
        {
            get => _changelog;
            set => Set(ref _changelog, value);
        }
        private string _changelog;


        public bool Installing
        {
            get => _installing;
            set => Set(ref _installing, value);
        }
        private bool _installing;


        public string CurrentFile
        {
            get => _currentFile;
            set => Set(ref _currentFile, value);
        }
        private string _currentFile;

        public ICommand InstallNewVersionCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        #endregion

        #region Ctor

        public NewVersionViewModel(MetroWindow window, string appName, string appVersion, Process appProcess, string versionFilePath, UpdateVersion version)
        {
            AppVersion = appVersion;
            AppName = appName;
            _version = version;

            InstallNewVersionCommand = new DelegateCommand(InstallNewVersion);
            CancelCommand = new DelegateCommand(window.Close);

            _appProcess = appProcess;
            _versionFilePath = versionFilePath;
            window.Loaded += (s, e) =>
            {
                NewVersion = _version.Version;
                if (!_version.Changelog.Contains("<html>"))
                {
                    Changelog = $"<html><body><span>{_version.Changelog}</span></body></html>";
                }
                else
                {
                    Changelog = _version.Changelog;
                }
            };
            _holderWindow = window;
        }

        #endregion

        #region Public methods

        public async void InstallNewVersion()
        {
            if (!File.Exists(_version.PathToUpdateZip))
            {
                MessageBox.Show($"The file {_version.PathToUpdateZip} which contains update data was not found.", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            string directory = Path.GetDirectoryName(_appProcess.MainModule.FileName);
            string exePath = _appProcess.MainModule.FileName;
            try
            {
                _appProcess.Kill();
                _appProcess.WaitForExit(1500);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Cannot kill executable to update. {Environment.NewLine} {e}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(3);
            }
            Installing = true;
            try
            {
                await Task.Run(() =>
                {
                    using (ZipArchive archive = ZipFile.OpenRead(_version.PathToUpdateZip))
                    {
                        var fullExtractPath = string.Empty;
                        int i = 1;
                        int total = archive.Entries.Count;
                        foreach (var entry in archive.Entries)
                        {
                            fullExtractPath = Path.Combine(directory, entry.FullName);
                            if (String.IsNullOrEmpty(entry.Name))
                            {
                                CurrentFile = $"Extracting directory {fullExtractPath} ({i}/{total})";
                                Directory.CreateDirectory(fullExtractPath);   
                            }
                            else
                            {
                                CurrentFile = $"Extracting file {entry.Name}  ({i}/{total})";
                                entry.ExtractToFile(fullExtractPath, true);
                            }
                            i++;
                        }
                    }
                });
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error when trying to update app. {Environment.NewLine} {e}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            File.WriteAllText(_versionFilePath, _version.Version);
            MessageBox.Show($"The app '{AppName}' has been updated ! It will restart now.", "Update complete !",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            Process.Start(exePath);
            Environment.Exit(0);
        }

        #endregion

        #region Private methods

        private bool Set<T>(ref T member, T value, [CallerMemberName] string memberName = "")
        {
            if (EqualityComparer<T>.Default.Equals(member, value))
                return false;

            member = value;
            RaisePropertyChanged(memberName);
            return true;
        }

        private void RaisePropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion


    }
}
