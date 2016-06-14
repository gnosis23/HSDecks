using HSDecks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeckMenu : Page {
        public ObservableCollection<Deck> Decks;

        private List<AbstractCard> _AllCards;

        public DeckMenu() {
            this.InitializeComponent();

            Decks = new ObservableCollection<Deck>();
            _AllCards = new List<AbstractCard>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e) {
            await CardData.GetCards(_AllCards, -1, "All");
            

            var deck = new Deck(1, "shit hunter", PlayerClass.Hunter);
            deck.items = await DeckInitializing(_AllCards);
            Decks.Add(deck);
        }

        private async Task<ObservableCollection<DeckItem>> DeckInitializing(List<AbstractCard> CardPool) {
            var str = await FileStuff.ReadFromFileAsync();
            var oldDeckList = DeckSaver.StringToDeck(str, CardPool);

            var ret = new ObservableCollection<DeckItem>();
            oldDeckList.ForEach(p => ret.Add(p));
            // DeckCountChanged();
            return ret;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e) {
            this.Frame.Navigate(typeof(DeckDetail), e.ClickedItem, new DrillInNavigationTransitionInfo());
        }
    }
}
