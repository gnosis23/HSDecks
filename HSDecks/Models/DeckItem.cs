using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

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

        public string strCardCount {
            get {
                if (this._count == 2) {
                    return "2";
                } else if (this.card.rarity == "Legendary") {
                    return "*";
                } else {
                    return "";
                }
            }
            set { this.OnPropertyChanged(); }
        }

        public Visibility visible {
            get {
                if (this.card.rarity == "Legendary" || this._count == 2) {
                    return Visibility.Visible;
                } else {
                    return Visibility.Collapsed;
                }
            }
            set { this.OnPropertyChanged(); }
        }

        public void addCard() {
            this.cardCount = 2;
            this.strCardCount = "";
            this.visible = Visibility.Visible;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
