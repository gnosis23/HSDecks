using HSDecks.Models;
using HSDecks.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeckSharingPage : Page {
        public MasterViewModel masterViewModel => App.Global.masterViewModel;
        public DeckViewModel Deck => masterViewModel.SelectedDeck;

        public DeckSharingPage() {
            this.InitializeComponent();

            // this.Loaded += (s, e) => { this.OnLoaded(); };
        }

        private void OnLoaded() {
            List<UserControl> items = new List<UserControl> {
                Item0, Item1, Item2, Item3, Item4,
                Item5, Item6, Item7, Item8, Item9,
                Item10, Item11, Item12, Item13, Item15,
                Item15, Item16, Item17, Item18, Item19,
                Item20, Item21, Item22, Item23, Item24,
                Item25, Item26, Item27, Item28, Item29,
            };

            int Index = 0;
            for (Index = 0; Index < 30; Index++ ) {
                items[Index].DataContext = Deck.items[0];
            }
        }
    }
}
