using HSDecks.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using System.Threading;
using FuckUWP1.Common;
using HSDecks.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DownloadPage : Page {
        public ObservableCollection<DownloadViewModel> Downloads;

        private CancellationTokenSource cts;

        public DownloadPage() {
            cts = new CancellationTokenSource();

            this.Downloads = new ObservableCollection<DownloadViewModel>();
            Downloads.Add(new DownloadViewModel("Basic") {
                FileName = "BASIC",
                FullFileName = "BASIC.zip",
                Address = "http://localhost/HSDecks/Home/Download?ImageName=BASIC.zip",
                Progress = 0,
                Size = 85,
            });
            Downloads.Add(new DownloadViewModel("BRM") {
                FileName = "BRM",
                FullFileName = "BRM.zip",
                Address = "http://localhost/HSDecks/Home/Download?ImageName=BRM.zip",
                Progress = 0,
                Size = 5,
            });
            Downloads.Add(new DownloadViewModel("GVG") {
                FileName = "GVG",
                FullFileName = "GVG.zip",
                Address = "http://localhost/HSDecks/Home/Download?ImageName=GVG.zip",
                Progress = 0,
                Size = 25,
            });
            Downloads.Add(new DownloadViewModel("LOE") {
                FileName = "LOE",
                FullFileName = "LOE.zip",
                Address = "http://localhost/HSDecks/Home/Download?ImageName=LOE.zip",
                Progress = 0,
                Size = 9,
            });
            Downloads.Add(new DownloadViewModel("NAX") {
                FileName = "NAX",
                FullFileName = "NAX.zip",
                Address = "http://localhost/HSDecks/Home/Download?ImageName=NAX.zip",
                Progress = 0,
                Size = 5,
            });
            Downloads.Add(new DownloadViewModel("OG") {
                FileName = "OG",
                FullFileName = "OG.zip",
                Address = "http://localhost/HSDecks/Home/Download?ImageName=OG.zip",
                Progress = 0,
                Size = 28,
            });
            Downloads.Add(new DownloadViewModel("AT") {
                FileName = "AT",
                FullFileName = "AT.zip",
                Address = "http://localhost/HSDecks/Home/Download?ImageName=AT.zip",
                Progress = 0,
                Size = 26,
            });
            this.Loaded += (s,e) => this.OnLoaded();

            this.InitializeComponent();
        }

        private async void OnLoaded() {
            foreach (var set in Downloads) {
                var s = await ApplicationData.Current.LocalFolder.TryGetItemAsync(set.FullFileName);
                if (s != null) {
                    var props = await s.GetBasicPropertiesAsync();
                    set.Status = "Ready";
                    set.Progress = 100;
                }
            }
        }

        private async void StartDownload_Click(object sender, RoutedEventArgs e) {
            DownloadViewModel SelectedSets = (sender as Button).DataContext as DownloadViewModel;

            Uri source = new Uri(SelectedSets.Address);
            StorageFile destinationFile;
            try {
                destinationFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                    SelectedSets.FullFileName,
                    CreationCollisionOption.ReplaceExisting);
            } catch (FileNotFoundException ) {
                return;
            }

            BackgroundDownloader downloader = new BackgroundDownloader();
            DownloadOperation download = downloader.CreateDownload(source, destinationFile);

            SelectedSets.guid = download.Guid;

            await HandleDownloadAsync(download, true);
        }

        private async Task HandleDownloadAsync(DownloadOperation download, bool start) {
            try {
                // LogStatus("Running: " + download.Guid, NotifyType.StatusMessage);

                // Store the download so we can pause/resume.
                // activeDownloads.Add(download);

                Progress<DownloadOperation> progressCallback = new Progress<DownloadOperation>(DownloadProgress);
                if (start) {
                    // Start the download and attach a progress handler.
                    await download.StartAsync().AsTask(cts.Token, progressCallback);
                } else {
                    // The download was already running when the application started, re-attach the progress handler.
                    await download.AttachAsync().AsTask(cts.Token, progressCallback);
                }

                ResponseInformation response = download.GetResponseInformation();

                // GetResponseInformation() returns null for non-HTTP transfers (e.g., FTP).
                string statusCode = response != null ? response.StatusCode.ToString() : String.Empty;

                // Completed
                var Selected = Downloads.First(p => p.guid == download.Guid);
                Selected.Status = "Ready";

                // Unzip 
                var zipFile = await ApplicationData.Current.LocalFolder.GetFileAsync(Selected.FullFileName);
                var unzipFolder = await FileHelper.GetFolderNotNullAsync(
                    ApplicationData.Current.LocalFolder, "cards");
                await ZipHelper.UnZipFileAsync(zipFile, unzipFolder);
            } catch (TaskCanceledException) {
                // LogStatus("Canceled: " + download.Guid, NotifyType.StatusMessage);
            } catch (Exception ) {
                // if (!IsExceptionHandled("Execution error", ex, download)) {
                //     throw;
                // }
            } finally {
                // activeDownloads.Remove(download);
            }

        }

        // Note that this event is invoked on a background thread, so we cannot access the UI directly.
        private void DownloadProgress(DownloadOperation download) {
            var Selected = Downloads.First(p => p.guid == download.Guid);
            Selected.Status = "Downloading";

            // DownloadOperation.Progress is updated in real-time while the operation is ongoing. Therefore,
            // we must make a local copy at the beginning of the progress handler, so that we can have a consistent
            // view of that ever-changing state throughout the handler's lifetime.
            BackgroundDownloadProgress currentProgress = download.Progress;

            // MarshalLog(String.Format(CultureInfo.CurrentCulture, "Progress: {0}, Status: {1}", download.Guid,
            //     currentProgress.Status));

            double percent = 100;
            if (currentProgress.TotalBytesToReceive > 0) {
                percent = currentProgress.BytesReceived * 100 / currentProgress.TotalBytesToReceive;
            }
            Selected.Progress = percent;

            // MarshalLog(String.Format(
            //     CultureInfo.CurrentCulture,
            //     " - Transfered bytes: {0} of {1}, {2}%",
            //     currentProgress.BytesReceived,
            //     currentProgress.TotalBytesToReceive,
            //     percent));
            Selected.Size = currentProgress.TotalBytesToReceive / 1024 / 1024;

            if (currentProgress.HasRestarted) {
                // MarshalLog(" - Download restarted");
            }

            if (currentProgress.HasResponseChanged) {
                // We have received new response headers from the server.
                // Be aware that GetResponseInformation() returns null for non-HTTP transfers (e.g., FTP).
                ResponseInformation response = download.GetResponseInformation();
                int headersCount = response != null ? response.Headers.Count : 0;

                // MarshalLog(" - Response updated; Header count: " + headersCount);

                // If you want to stream the response data this is a good time to start.
                // download.GetResultStreamAt(0);
            }
        }

        private async void RemoveButton_Click(object sender, RoutedEventArgs e) {
            var SelectedSet = (sender as Button).DataContext as DownloadViewModel;

            var file = await ApplicationData.Current.LocalFolder.TryGetItemAsync(SelectedSet.FullFileName);
            if (file != null) {
                await file.DeleteAsync();
            }

            // Init;
            SelectedSet.Status = "";
            SelectedSet.Progress = 0;
        }
    }
}
