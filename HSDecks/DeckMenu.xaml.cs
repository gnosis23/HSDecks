using HSDecks.Models;
using HSDecks.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeckMenu : Page {
        public MasterViewModel masterViewModel => App.Global.masterViewModel;
        public ObservableCollection<Deck> Decks => masterViewModel.Decks;

        public DeckMenu() {
            this.InitializeComponent();

        }


        private void ListView_ItemClick(object sender, ItemClickEventArgs e) {
            masterViewModel.SelectedDeck = (Deck)e.ClickedItem;
            this.Frame.Navigate(typeof(DeckDetail), null, new DrillInNavigationTransitionInfo());
        }

        private void MenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            PlayerClass _class = (PlayerClass)Enum.Parse(typeof(PlayerClass), item.Text, true);

            Deck deck = new Deck(1, "123", _class);

            masterViewModel.Decks.Add(deck);
            masterViewModel.SelectedDeck = deck;
            this.Frame.Navigate(typeof(DeckDetail), null, new DrillInNavigationTransitionInfo());
        }
    }
}
