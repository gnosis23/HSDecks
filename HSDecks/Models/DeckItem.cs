using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HSDecks.Models {
    public class DeckItem : INotifyPropertyChanged {
        public AbstractCard card { get; set; }
        
        private int _count = 0;

        public DeckItem(AbstractCard card) {
            this.card = card;
            this._count = 1;
        }
        public DeckItem(AbstractCard card, int _count) {
            this.card = card;
            this._count = _count;
        }

        public int cardCount {
            get { return this._count; }
            set {
                this._count = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
