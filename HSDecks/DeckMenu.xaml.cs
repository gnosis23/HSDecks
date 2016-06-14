using HSDecks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

            Decks = new ObservableCollection<Deck>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            for (int i = 0; i < 5; i++) {
                Decks.Add(new Deck(1, "shit hunter", PlayerClass.Hunter));
                Decks.Add(new Deck(1, "shit druid", PlayerClass.Druid));
                Decks.Add(new Deck(1, "shit mage", PlayerClass.Mage));
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e) {

        }
    }
}
