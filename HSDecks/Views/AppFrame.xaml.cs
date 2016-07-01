using System;
using System.ComponentModel;
using HSDecks.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppPage : Page {
        public MasterViewModel masterViewModel => App.Global.masterViewModel;

        public AppPage() {
            this.InitializeComponent();
            this.Loaded += (s, e) => { masterViewModel.OnLoaded(); };
            masterViewModel.PropertyChanged += this.OnPageNavigation;

            AppFrame.Navigate(typeof(FirstPage));
        }

        private void OnPageNavigation(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(masterViewModel.IsSharedPage)) {
                if (masterViewModel.IsSharedPage) {
                    AppFrame.Navigate(typeof(DeckSharingPage));
                }
                else {
                    AppFrame.GoBack();
                }
            }
            if (e.PropertyName == nameof(masterViewModel.IsDownloadPage)) {
                AppFrame.Navigate(typeof(DownloadPage));
            }
        }
    }
}
