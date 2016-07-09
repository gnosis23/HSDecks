using FuckUWP1.Common;
using HSDecks.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Windows.UI.Xaml.Media.Imaging;

namespace HSDecks.Models {
    public class DeckViewModel : BindableBase {
        public int Id { get; set; }

        string _name;
        public string name {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public PlayerClass playerClass { get; set; }
        public ObservableCollection<DeckItemViewModel> items { get; set; }

        public BitmapImage hImage { get; set; }

        public DeckViewModel(int id, string name, PlayerClass playerClass) {
            this.Id = id;
            this._name = name;
            this.playerClass = playerClass;
            hImage = new BitmapImage(new Uri(String.Format("ms-appx:///Assets/control/{0}.jpg", 
                playerClass.ToString())));
            this.items = new ObservableCollection<DeckItemViewModel>();
        }

        public DeckViewModel(int id, string name, PlayerClass playerClass, List<DeckItemViewModel> xxx) {
            this.Id = id;
            this._name = name;
            this.playerClass = playerClass;
            hImage = new BitmapImage(new Uri(String.Format("ms-appx:///Assets/control/{0}.jpg", 
                playerClass.ToString())));
            this.items = new ObservableCollection<DeckItemViewModel>(xxx);
        }

        public Deck ToDeck()
        {
            List<DeckItem> items = this.items.Select(p => new DeckItem() {
                cardId = p.cardId,
                cardCount = p.cardCount,
            }).ToList();
            return new Deck() {
                id = this.Id,
                name = this.name,
                playerClass = this.playerClass,
                items = items,
            };
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

                OnPropertyChanged(nameof(cardCount));
            }
        }

        public void Remove(DeckItemViewModel item) {
            this.items.Remove(item);
            OnPropertyChanged(nameof(cardCount));
        }

        private void _Add(DeckItemViewModel item) {
            items.Add(item);
            OnPropertyChanged(nameof(cardCount));
        }

        public void Insert(int Index, DeckItemViewModel item) {
            items.Insert(Index, item);
            OnPropertyChanged(nameof(cardCount));
        }

        public int cardCount => items.Sum(p => p.cardCount);

    }
}


