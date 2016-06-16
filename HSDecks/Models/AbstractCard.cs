using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace HSDecks.Models {
    public class AbstractCard {
        public string cardId { get; set; }
        public int cost { get; set; }
        public int? attack { get; set; }
        public int? health { get; set; }
        public bool? collectable { get; set; }

        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public string race { get; set; }
        public string rarity { get; set; }
        public string text { get; set; }
        public string playerClass { get; set; }

        public string img { get; set; }
        public string imgGold { get; set; }

        public BitmapImage image { get; set; }
        public BitmapImage sImage { get; set; }
    }
}
