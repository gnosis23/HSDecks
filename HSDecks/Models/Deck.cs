using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace HSDecks.Models {
    public class Deck {
        public int Id { get; set; }
        public ObservableCollection<DeckItem> items { get; set; }
        public string name { get; set; }
        public PlayerClass playerClass { get; set; }

        public BitmapImage hImage { get; set; }

        public Deck(int id, string name, PlayerClass playerClass) {
            this.Id = id;
            this.name = name;
            this.playerClass = playerClass;
            hImage = new BitmapImage(new Uri(String.Format("ms-appx:///Assets/control/{0}.jpg", 
                playerClass.ToString())));
        }
    }
}


