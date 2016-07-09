using FuckUWP1.Common;
using HSDecks.Models;
using System.Runtime.Serialization;

namespace HSDecks.ViewModels {
    public class DeckItemViewModel : BindableBase {
        public AbstractCard card { get; set; }
        

        public DeckItemViewModel(AbstractCard card) {
            this.card = card;
            this._count = 1;
            this.cardId = card.cardId;
        }
        public DeckItemViewModel(AbstractCard card, int _count) {
            this.card = card;
            this._count = _count;
            this.cardId = card.cardId;
        }

        int _count = 0;
        public int cardCount {
            get { return this._count; }
            set {
                if (SetProperty(ref _count, value)) {
                    OnPropertyChanged(nameof(strCardCount));
                    OnPropertyChanged(nameof(visible));
                }
            }
        }

        public string cardId { get; set; }

        public string strCardCount => (_count == 2 ? "2" : "*");

        public bool visible => 
                (this.card.rarity == "Legendary" || this._count == 2);
        public bool twoNumberVisible => (this._count == 2);

        public void addCard() {
            this.cardCount = 2;
        }

    }
}
