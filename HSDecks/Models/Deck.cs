using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HSDecks.Models
{
    [DataContract]
    public class Deck
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public PlayerClass playerClass { get; set; }

        [DataMember]
        public List<DeckItem> items;

        public Deck()
        {
            items = new List<DeckItem>();
        }
    }

    [DataContract]
    public class DeckItem
    {
        [DataMember]
        public string cardId { get; set; }
        [DataMember]
        public int cardCount { get; set; }
    }
}
