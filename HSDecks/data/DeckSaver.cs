using HSDecks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDecks {
    public class DeckSaver {
        public static string DeckToString(List<DeckItem> deck) {
            string str = "";
            foreach (var item in deck) {
                str += String.Format("{0}^{1} ", item.card.cardId, item.cardCount);
            }

            return str.Trim();
        }

        public static string DeckListToString(List<Deck> deckList) {
            string str = "";
            foreach (var deck in deckList) {
                str += String.Format("{0}+{1}+{2}+{3};", 
                    deck.Id, deck.name, (int)deck.playerClass, 
                    DeckToString(deck.items.ToList()));
            }

            return str.Trim(new char[] { ' ', ';' });
        }

        public static List<DeckItem> StringToDeck(string code, List<AbstractCard> CardsPool) {
            List<DeckItem> deck = new List<DeckItem>();
            if (code == "") return deck;

            foreach (var name in code.Split(' ')) {
                var pair = name.Split('^');
                string id = pair[0];
                int count = Int32.Parse(pair[1]);

                var selected = CardsPool.First(p => p.cardId == id);
                var t = new DeckItem(selected, count);
                deck.Add(t);
            }

            return deck;
        }

        public async static Task<List<Deck>> StringToDeckListAsync(string code) {
            List<Deck> deckList = new List<Deck>();
            List<AbstractCard> CardsPool = new List<AbstractCard>();
            await CardData.GetCards(CardsPool, -1, "All");

            foreach (var deckStr in code.Split(';')) {
                var pair = deckStr.Split('+');
                int Id = int.Parse(pair[0]);
                string Name = pair[1];
                PlayerClass pc =  (PlayerClass)int.Parse(pair[2]);
                List<DeckItem> items = StringToDeck(pair[3], CardsPool);

                Deck dk = new Deck(Id, Name, pc, items);
                deckList.Add(dk);
            }

            return deckList;
        }
    }
}
