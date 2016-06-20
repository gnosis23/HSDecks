using HSDecks.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace HSDecks.Models {
    public class Deck : INotifyPropertyChanged {
        public int Id { get; set; }
        public string name { get; set; }
        public PlayerClass playerClass { get; set; }
        public int _count = 0;
        public ObservableCollection<DeckItemViewModel> _items;

        public BitmapImage hImage { get; set; }

        public Deck(int id, string name, PlayerClass playerClass) {
            this.Id = id;
            this.name = name;
            this.playerClass = playerClass;
            hImage = new BitmapImage(new Uri(String.Format("ms-appx:///Assets/control/{0}.jpg", 
                playerClass.ToString())));
            this.items = new ObservableCollection<DeckItemViewModel>();
        }

        public Deck(int id, string name, PlayerClass playerClass, List<DeckItemViewModel> xxx) {
            this.Id = id;
            this.name = name;
            this.playerClass = playerClass;
            hImage = new BitmapImage(new Uri(String.Format("ms-appx:///Assets/control/{0}.jpg", 
                playerClass.ToString())));
            this.items = new ObservableCollection<DeckItemViewModel>(xxx);
        }

        public void Add(DeckItemViewModel item) {
            if (cardCount < 30) {
                var prevCard = items.FirstOrDefault(p => p.card.cardId == item.card.cardId);
                if (prevCard == null) {
                    // insert item order by cost
                    var nextCard = items.FirstOrDefault(p => p.card.cost >= item.card.cost);
                    if (nextCard == null) {
                        _Add(item);
                    } else {
                        var index = items.IndexOf(nextCard);
                        Insert(index, item);
                    }
                } else if (prevCard.cardCount < 2 && prevCard.card.rarity != "Legendary") {
                    prevCard.addCard();
                }

                this.cardCount = items.Sum(p => p.cardCount);
            }
        }

        public void Remove(DeckItemViewModel item) {
            this._items.Remove(item);
            this.cardCount = items.Sum(p => p.cardCount);
        }

        private void _Add(DeckItemViewModel item) {
            _items.Add(item);
            this.cardCount++;
        }

        public void Insert(int Index, DeckItemViewModel item) {
            _items.Insert(Index, item);
            this.cardCount++;
        }

        public int cardCount {
            get { return this._count; }
            set {
                this._count = value;
                this.OnPropertyChanged();
            }
        }


        public ObservableCollection<DeckItemViewModel> items {
            get { return _items; }
            set {
                this._items = value;
                this.cardCount = _items.Sum(p => p.cardCount);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


