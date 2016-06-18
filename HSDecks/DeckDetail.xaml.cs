using HSDecks.Models;
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
        private static DependencyProperty s_deckProperty =
            DependencyProperty.Register("OneDeck", typeof(Deck), typeof(DeckDetail), new PropertyMetadata(null));

        public static DependencyProperty DeckProperty {
            get { return s_deckProperty; }
        }

        public Deck OneDeck {
            get { return (Deck)GetValue(s_deckProperty); }
            set { SetValue(s_deckProperty, value); }
        }

        public DeckDetail() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            OneDeck = (Deck)e.Parameter;
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            var str = DeckSaver.DeckListToString(new List<Deck>(App.Decks));
            await FileStuff.WriteToFileAsync(str);

            //ContentDialog saveDialog = new ContentDialog() {
            //    Title = "Save Deck",
            //    Content = "Deck saved!",
            //    PrimaryButtonText = "Ok"
            //};
            //await saveDialog.ShowAsync();

            Frame.GoBack(new EntranceNavigationTransitionInfo());
        }

        private void DeckList_ItemClick(object sender, ItemClickEventArgs e) {
            var deckItem = (DeckItem)e.ClickedItem;
            OneDeck.Remove(deckItem);
        }

    }
}
