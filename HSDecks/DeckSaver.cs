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

        public static List<DeckItem> StringToDeck(string code, List<AbstractCard> CardsPool) {
            List<DeckItem> deck = new List<DeckItem>();

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
    }
}
