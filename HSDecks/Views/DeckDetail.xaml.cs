using HSDecks.Controls;
using HSDecks.Models;
using HSDecks.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeckDetail : Page {
        public MasterViewModel masterViewModel => App.Global.masterViewModel;

        public DeckViewModel OneDeck => masterViewModel.SelectedDeck;

        public DeckDetail() {
            this.InitializeComponent();
            this.Loaded += async (s, e) => { await masterViewModel.refreshPageAsync(); };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            await masterViewModel.SaveDecksAndExit();

            Frame.GoBack(new EntranceNavigationTransitionInfo());
        }

        private void DeckList_ItemClick(object sender, ItemClickEventArgs e) {
            var deckItem = (DeckItemViewModel)e.ClickedItem;
            OneDeck.Remove(deckItem);
        }

        private async void RenameBtn_Click(object sender, RoutedEventArgs e) {
            InputDialog dlg = new InputDialog(OneDeck.name);
            dlg.DataContext = OneDeck;
            await dlg.ShowAsync();

            if (dlg.Confirm) {
                OneDeck.name = dlg.MyInput;
            }
        }
    }
}
