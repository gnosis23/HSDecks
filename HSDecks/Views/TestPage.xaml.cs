using System;
using HSDecks.Models;
using HSDecks.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using HSDecks.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestPage : Page {
        public MasterViewModel masterViewModel => App.Global.masterViewModel;
        public DeckViewModel Deck => masterViewModel.SelectedDeck;

        public TestPage() {
            this.InitializeComponent();

        }


        private void OnLoaded() {
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            masterViewModel.GotoMainPage();
        }
    }
}
