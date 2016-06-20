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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            await masterViewModel.SaveDecks();

            Frame.GoBack(new EntranceNavigationTransitionInfo());
        }

        private void DeckList_ItemClick(object sender, ItemClickEventArgs e) {
            var deckItem = (DeckItemViewModel)e.ClickedItem;
            OneDeck.Remove(deckItem);
        }

    }
}
