using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDecks.Models {
    public class DeckItem {
        public AbstractCard card { get; set; }
        public int cardCount { get; set; }

        public DeckItem(AbstractCard card) {
            this.card = card;
            this.cardCount = 1;
        }
    }
}
