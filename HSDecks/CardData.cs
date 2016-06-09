using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HSDecks {
    class CardData {
        public async static Task GetCards(List<BasicCard> cards, int cost) {
            var uri = new Uri("ms-appx:///Assets/cards.json");
            var sampleFile = await StorageFile.GetFileFromApplicationUriAsync(uri);

            UnicodeEncoding encoding = new UnicodeEncoding();
            string text = await FileIO.ReadTextAsync(sampleFile, Windows.Storage.Streams.UnicodeEncoding.Utf8);

            var readFromFile = text;

            var jsonMessage = readFromFile;


            var serializer = new DataContractJsonSerializer(typeof(CardSets));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            CardSets Fuck = (CardSets) serializer.ReadObject(ms);
            var selected = from p in Fuck.Basic
                           where p.cost == cost && p.img != null
                           orderby p.playerClass
                           select p;

            cards.Clear();
            foreach (var item in selected) {
                cards.Add(item);
            }
        }
    }

    public class Mechanic {
        public string name { get; set; }
    }

    public class BasicCard {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public string faction { get; set; }
        public string rarity { get; set; }
        public int cost { get; set; }
        public string text { get; set; }
        public string flavor { get; set; }
        public string artist { get; set; }
        public bool collectible { get; set; }
        public string playerClass { get; set; }
        public string howToGet { get; set; }
        public string howToGetGold { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
        public List<Mechanic> mechanics { get; set; }
        public int? attack { get; set; }
        public int? health { get; set; }
        public string race { get; set; }
        public string inPlayText { get; set; }
        public int? durability { get; set; }
    }

    public class Mechanic2 {
        public string name { get; set; }
    }

    public class ClassicCard {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public string faction { get; set; }
        public string rarity { get; set; }
        public int cost { get; set; }
        public int attack { get; set; }
        public int health { get; set; }
        public string text { get; set; }
        public string flavor { get; set; }
        public string artist { get; set; }
        public bool collectible { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
        public List<Mechanic2> mechanics { get; set; }
        public string race { get; set; }
        public int? durability { get; set; }
        public string playerClass { get; set; }
        public string inPlayText { get; set; }
    }

    public class Credit {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public string rarity { get; set; }
        public int cost { get; set; }
        public int attack { get; set; }
        public int health { get; set; }
        public string text { get; set; }
        public bool elite { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
    }

    public class Mechanic3 {
        public string name { get; set; }
    }

    public class Naxxrama {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public string rarity { get; set; }
        public int cost { get; set; }
        public string text { get; set; }
        public string flavor { get; set; }
        public string artist { get; set; }
        public bool collectible { get; set; }
        public string playerClass { get; set; }
        public string howToGet { get; set; }
        public string howToGetGold { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
        public List<Mechanic3> mechanics { get; set; }
        public int? attack { get; set; }
        public int? health { get; set; }
        public int? durability { get; set; }
        public string race { get; set; }
    }

    public class Mechanic4 {
        public string name { get; set; }
    }

    public class GoblinsVsGnome {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public int cost { get; set; }
        public string text { get; set; }
        public string artist { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
        public int? attack { get; set; }
        public int? health { get; set; }
        public string race { get; set; }
        public List<Mechanic4> mechanics { get; set; }
        public string rarity { get; set; }
        public string flavor { get; set; }
        public bool? collectible { get; set; }
        public string playerClass { get; set; }
    }

    public class Mechanic5 {
        public string name { get; set; }
    }

    public class Mission {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public string faction { get; set; }
        public string rarity { get; set; }
        public int cost { get; set; }
        public string text { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
        public int? attack { get; set; }
        public int? health { get; set; }
        public List<Mechanic5> mechanics { get; set; }
    }

    public class Promotion {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public string faction { get; set; }
        public string rarity { get; set; }
        public int cost { get; set; }
        public int attack { get; set; }
        public int health { get; set; }
        public string text { get; set; }
        public string inPlayText { get; set; }
        public string race { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
    }

    public class Mechanic6 {
        public string name { get; set; }
    }

    public class BlackrockMountain {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public int cost { get; set; }
        public int attack { get; set; }
        public int health { get; set; }
        public string text { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
        public List<Mechanic6> mechanics { get; set; }
        public string rarity { get; set; }
        public string artist { get; set; }
        public string race { get; set; }
        public string flavor { get; set; }
        public bool? collectible { get; set; }
        public string howToGet { get; set; }
        public string howToGetGold { get; set; }
        public int? durability { get; set; }
        public string playerClass { get; set; }
        public bool? elite { get; set; }
    }

    public class Mechanic7 {
        public string name { get; set; }
    }

    public class TavernBrawl {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public int cost { get; set; }
        public int attack { get; set; }
        public int durability { get; set; }
        public string text { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
        public int? health { get; set; }
        public List<Mechanic7> mechanics { get; set; }
        public string race { get; set; }
    }

    public class Mechanic8 {
        public string name { get; set; }
    }

    public class TheGrandTournament {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public string rarity { get; set; }
        public int cost { get; set; }
        public string text { get; set; }
        public string flavor { get; set; }
        public string artist { get; set; }
        public bool collectible { get; set; }
        public string playerClass { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
        public int? attack { get; set; }
        public int? health { get; set; }
        public List<Mechanic8> mechanics { get; set; }
        public string race { get; set; }
        public int? durability { get; set; }
    }

    public class Mechanic9 {
        public string name { get; set; }
    }

    public class TheLeagueOfExplorer {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public int cost { get; set; }
        public int attack { get; set; }
        public int health { get; set; }
        public string text { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
        public string rarity { get; set; }
        public int? durability { get; set; }
        public string flavor { get; set; }
        public string artist { get; set; }
        public bool? collectible { get; set; }
        public string playerClass { get; set; }
        public string howToGet { get; set; }
        public string howToGetGold { get; set; }
        public List<Mechanic9> mechanics { get; set; }
        public string race { get; set; }
        public bool? elite { get; set; }
    }

    public class Mechanic10 {
        public string name { get; set; }
    }

    public class WhispersOfTheOldGod {
        public string cardId { get; set; }
        public string name { get; set; }
        public string cardSet { get; set; }
        public string type { get; set; }
        public string rarity { get; set; }
        public int cost { get; set; }
        public int attack { get; set; }
        public int health { get; set; }
        public string text { get; set; }
        public string flavor { get; set; }
        public string artist { get; set; }
        public bool collectible { get; set; }
        public string playerClass { get; set; }
        public string img { get; set; }
        public string imgGold { get; set; }
        public string locale { get; set; }
        public List<Mechanic10> mechanics { get; set; }
        public string race { get; set; }
        public int? durability { get; set; }
        public bool? elite { get; set; }
    }

    [DataContract]
    public class CardSets {
        [DataMember]
        public List<BasicCard> Basic { get; set; }
        [DataMember]
        public List<ClassicCard> Classic { get; set; }
        [DataMember]
        public List<Credit> Credits { get; set; }
        [DataMember]
        public List<Naxxrama> Naxxramas { get; set; }
        [DataMember]
        public List<object> Debug { get; set; }
        [DataMember(Name = "Goblins vs Gnomes")]
        public List<GoblinsVsGnome> GoblinsvsGnomes { get; set; }
        [DataMember]
        public List<Mission> Missions { get; set; }
        [DataMember]
        public List<Promotion> Promotion { get; set; }
        [DataMember]
        public List<object> Reward { get; set; }
        [DataMember]
        public List<object> System { get; set; }
        [DataMember(Name = "Blackrock Mountain")]
        public List<BlackrockMountain> BlackrockMountain { get; set; }
        [DataMember(Name = "Hero Skins")]
        public List<object> HeroSkins { get; set; }
        [DataMember(Name = "Tavern Brawl")]
        public List<TavernBrawl> TavernBrawl { get; set; }
        [DataMember(Name = "The Grand Tournament")]
        public List<TheGrandTournament> TheGrandTournament { get; set; }
        [DataMember(Name = "The League of Explorers")]
        public List<TheLeagueOfExplorer> TheLeagueofExplorers { get; set; }
        [DataMember(Name = "Whispers of the Old Gods")]
        public List<WhispersOfTheOldGod> WhispersoftheOldGods { get; set; }
    }

}
