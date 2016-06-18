using HSDecks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeckMenu : Page {
        public ObservableCollection<Deck> Decks;


        public DeckMenu() {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            Decks = (ObservableCollection<Deck>)e.Parameter;
        }



        private void ListView_ItemClick(object sender, ItemClickEventArgs e) {
            App.SelectedDeck = (Deck)e.ClickedItem;
            this.Frame.Navigate(typeof(DeckDetail), e.ClickedItem, new DrillInNavigationTransitionInfo());
        }

        private void MenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            PlayerClass _class = (PlayerClass)Enum.Parse(typeof(PlayerClass), item.Text, true);

            Deck deck = new Deck(1, "123", _class);

            App.Decks.Add(deck);
            App.SelectedDeck = deck;
            this.Frame.Navigate(typeof(DeckDetail), deck, new DrillInNavigationTransitionInfo());
        }
    }
}
