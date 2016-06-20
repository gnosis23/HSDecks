using HSDecks.Models;
using HSDecks.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDecks {
    public class DeckSaver {
        public static string DeckToString(List<DeckItemViewModel> deck) {
            string str = "";
            foreach (var item in deck) {
                str += String.Format("{0}^{1} ", item.card.cardId, item.cardCount);
            }

            return str.Trim();
        }

        public static string DeckListToString(List<DeckViewModel> deckList) {
            string str = "";
            foreach (var deck in deckList) {
                str += String.Format("{0}+{1}+{2}+{3};", 
                    deck.Id, deck.name, (int)deck.playerClass, 
                    DeckToString(deck.items.ToList()));
            }

            return str.Trim(new char[] { ' ', ';' });
        }

        public static List<DeckItemViewModel> StringToDeck(string code, List<AbstractCard> CardsPool) {
            List<DeckItemViewModel> deck = new List<DeckItemViewModel>();
            if (code == "") return deck;

            foreach (var name in code.Split(' ')) {
                var pair = name.Split('^');
                string id = pair[0];
                int count = Int32.Parse(pair[1]);

                var selected = CardsPool.First(p => p.cardId == id);
                var t = new DeckItemViewModel(selected, count);
                deck.Add(t);
            }

            return deck;
        }

        public async static Task<List<DeckViewModel>> StringToDeckListAsync(string code) {
            List<DeckViewModel> deckList = new List<DeckViewModel>();
            List<AbstractCard> CardsPool = new List<AbstractCard>();
            await CardData.GetCards(CardsPool, -1, "All");

            foreach (var deckStr in code.Split(';')) {
                var pair = deckStr.Split('+');
                int Id = int.Parse(pair[0]);
                string Name = pair[1];
                PlayerClass pc =  (PlayerClass)int.Parse(pair[2]);
                List<DeckItemViewModel> items = StringToDeck(pair[3], CardsPool);

                DeckViewModel dk = new DeckViewModel(Id, Name, pc, items);
                deckList.Add(dk);
            }

            return deckList;
        }
    }
}
