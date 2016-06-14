﻿using HSDecks.Models;
using System;
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

        private void Button_Click(object sender, RoutedEventArgs e) {
            Frame.GoBack(new EntranceNavigationTransitionInfo());
        }

        private void DeckList_ItemClick(object sender, ItemClickEventArgs e) {
            var deckItem = (DeckItem)e.ClickedItem;
            OneDeck.items.Remove(deckItem);
            DeckCountChanged();
        }

        private void DeckCountChanged() {
            // DeckTitle.Text = string.Format("Your Deck ({0})", Deck.Sum(p => p.cardCount));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e) {
            var str = DeckSaver.DeckToString(OneDeck.items.ToList());
            await FileStuff.WriteToFileAsync(str);

            ContentDialog saveDialog = new ContentDialog() {
                Title = "Save Deck",
                Content = "Deck saved!",
                PrimaryButtonText = "Ok"
            };

            // saveDialog.Content = str;

            await saveDialog.ShowAsync();
        }
    }
}
