﻿using FuckUWP1.Common;
using HSDecks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace HSDecks {
    class CardData {
        public readonly static string CARD_VER = "cards";
        public static bool _IsDownloading = false;

        public async static Task GetCards(List<AbstractCard> cards, int cost, string heroClass) {
            bool ImageFolderExists = true;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try {
                await localFolder.GetFolderAsync(CARD_VER);
            } catch (FileNotFoundException) {
                ImageFolderExists = false;
            }

            if (!ImageFolderExists && !_IsDownloading) {
#pragma warning disable CS4014 // 
                Task.Factory.StartNew(() => StartDownload(BackgroundTransferPriority.Default, false));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }

            var uri = new Uri("ms-appx:///Assets/cards.json");
            var sampleFile = await StorageFile.GetFileFromApplicationUriAsync(uri);

            UnicodeEncoding encoding = new UnicodeEncoding();
            string text = await FileIO.ReadTextAsync(sampleFile, Windows.Storage.Streams.UnicodeEncoding.Utf8);

            var readFromFile = text;

            var jsonMessage = readFromFile;


            var serializer = new DataContractJsonSerializer(typeof(CardSets));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            CardSets Fuck = (CardSets) serializer.ReadObject(ms);

            cards.Clear();
            List<AbstractCard> tempCards = new List<AbstractCard>();

            Fuck.Basic.ForEach(p => {
                AbstractCard aCard = new AbstractCard();
                aCard.cardId = p.cardId;
                aCard.collectable = p.collectible;
                aCard.cost = p.cost;
                aCard.attack = p.attack;
                aCard.health = p.health;
                aCard.name = p.name;
                aCard.cardSet = p.cardSet;
                aCard.type = p.type;
                aCard.race = p.race;
                aCard.rarity = p.rarity;
                aCard.text = p.text;
                aCard.playerClass = p.playerClass;
                aCard.img = p.img;
                tempCards.Add(aCard);
            });
            Fuck.Classic.ForEach(p => {
                AbstractCard aCard = new AbstractCard();
                aCard.cardId = p.cardId;
                aCard.collectable = p.collectible;
                aCard.cost = p.cost;
                aCard.attack = p.attack;
                aCard.health = p.health;
                aCard.name = p.name;
                aCard.cardSet = p.cardSet;
                aCard.type = p.type;
                aCard.race = p.race;
                aCard.rarity = p.rarity;
                aCard.text = p.text;
                aCard.playerClass = p.playerClass;
                aCard.img = p.img;
                tempCards.Add(aCard);
            });
            Fuck.Naxxramas.ForEach(p => {
                AbstractCard aCard = new AbstractCard();
                aCard.cardId = p.cardId;
                aCard.collectable = p.collectible;
                aCard.cost = p.cost;
                aCard.attack = p.attack;
                aCard.health = p.health;
                aCard.name = p.name;
                aCard.cardSet = p.cardSet;
                aCard.type = p.type;
                aCard.race = p.race;
                aCard.rarity = p.rarity;
                aCard.text = p.text;
                aCard.playerClass = p.playerClass;
                aCard.img = p.img;
                tempCards.Add(aCard);
            });
            Fuck.GoblinsvsGnomes.ForEach(p => {
                AbstractCard aCard = new AbstractCard();
                aCard.cardId = p.cardId;
                aCard.collectable = p.collectible;
                aCard.cost = p.cost;
                aCard.attack = p.attack;
                aCard.health = p.health;
                aCard.name = p.name;
                aCard.cardSet = p.cardSet;
                aCard.type = p.type;
                aCard.race = p.race;
                aCard.rarity = p.rarity;
                aCard.text = p.text;
                aCard.playerClass = p.playerClass;
                aCard.img = p.img;
                tempCards.Add(aCard);
            });
            Fuck.BlackrockMountain.ForEach(p => {
                AbstractCard aCard = new AbstractCard();
                aCard.cardId = p.cardId;
                aCard.collectable = p.collectible;
                aCard.cost = p.cost;
                aCard.attack = p.attack;
                aCard.health = p.health;
                aCard.name = p.name;
                aCard.cardSet = p.cardSet;
                aCard.type = p.type;
                aCard.race = p.race;
                aCard.rarity = p.rarity;
                aCard.text = p.text;
                aCard.playerClass = p.playerClass;
                aCard.img = p.img;
                tempCards.Add(aCard);
            });
            Fuck.TheGrandTournament.ForEach(p => {
                AbstractCard aCard = new AbstractCard();
                aCard.cardId = p.cardId;
                aCard.collectable = p.collectible;
                aCard.cost = p.cost;
                aCard.attack = p.attack;
                aCard.health = p.health;
                aCard.name = p.name;
                aCard.cardSet = p.cardSet;
                aCard.type = p.type;
                aCard.race = p.race;
                aCard.rarity = p.rarity;
                aCard.text = p.text;
                aCard.playerClass = p.playerClass;
                aCard.img = p.img;
                tempCards.Add(aCard);
            });
            Fuck.TheLeagueofExplorers.ForEach(p => {
                AbstractCard aCard = new AbstractCard();
                aCard.cardId = p.cardId;
                aCard.collectable = p.collectible;
                aCard.cost = p.cost;
                aCard.attack = p.attack;
                aCard.health = p.health;
                aCard.name = p.name;
                aCard.cardSet = p.cardSet;
                aCard.type = p.type;
                aCard.race = p.race;
                aCard.rarity = p.rarity;
                aCard.text = p.text;
                aCard.playerClass = p.playerClass;
                aCard.img = p.img;
                tempCards.Add(aCard);
            });
            Fuck.WhispersoftheOldGods.ForEach(p => {
                AbstractCard aCard = new AbstractCard();
                aCard.cardId = p.cardId;
                aCard.collectable = p.collectible;
                aCard.cost = p.cost;
                aCard.attack = p.attack;
                aCard.health = p.health;
                aCard.name = p.name;
                aCard.cardSet = p.cardSet;
                aCard.type = p.type;
                aCard.race = p.race;
                aCard.rarity = p.rarity;
                aCard.text = p.text;
                aCard.playerClass = p.playerClass;
                aCard.img = p.img;
                tempCards.Add(aCard);
            });

            if (heroClass == "All") {
                if (cost == -1) {
                    var selected = from p in tempCards
                                   where p.img != null && p.collectable == true
                                   && p.type != "Hero"
                                   orderby p.name
                                   select p;

                    cards.AddRange(selected);
                }
                else if (cost == 7) {
                    var selected = from p in tempCards
                                   where p.cost >= 7 && p.img != null && p.collectable == true
                                   && p.type != "Hero"
                                   orderby p.name
                                   select p;

                    cards.AddRange(selected);
                } else {
                    var selected = from p in tempCards
                                   where p.cost == cost && p.img != null && p.collectable == true
                                   && p.type != "Hero"
                                   orderby p.name
                                   select p;

                    cards.AddRange(selected);
                }
            } 
            else {
                if (cost == 7) {
                    var selected = from p in tempCards
                                   where p.cost >= 7 && p.img != null && p.collectable == true
                                   && p.type != "Hero"
                                   && (p.playerClass == heroClass || p.playerClass == null)
                                   orderby p.playerClass descending, p.name
                                   select p;

                    cards.AddRange(selected);
                } else {
                    var selected = from p in tempCards
                                   where p.cost == cost && p.img != null && p.collectable == true
                                   && p.type != "Hero"
                                   && (p.playerClass == heroClass || p.playerClass == null)
                                   orderby p.playerClass descending, p.name
                                   select p;

                    cards.AddRange(selected);
                }
            }

            cards.ForEach(p => {
                if (ImageFolderExists) {
                    uri = new Uri(String.Format("ms-appdata:///local/{1}/{0}.png", p.cardId, CARD_VER));
                } else {
                    uri = new Uri(p.img);
                }
                BitmapImage img = new BitmapImage(uri);
                p.image = img;

                uri = new Uri(String.Format("ms-appx:///Assets/bars/{0}.png", p.cardId));
                img = new BitmapImage(uri);
                p.sImage = img;
            });
        }

        private static async Task StartDownload(BackgroundTransferPriority priority, bool requestUnconstrainedDownload) {
            _IsDownloading = true;
            var ZipFileUrl = String.Format("http://10.0.0.4/HSDecks/Home/Download?ImageName={0}.zip", CARD_VER);

            // The URI is validated by calling Uri.TryCreate() that will return 'false' for strings that are not valid URIs. 
            // Note that when enabling the text box users may provide URIs to machines on the intrAnet that require 
            // the "Home or Work Networking" capability. 
            Uri source;
            if (!Uri.TryCreate(ZipFileUrl, UriKind.Absolute, out source)) {
                // NotifyUser("Invalid URI.", NotifyType.ErrorMessage);
                return;
            }
            StorageFolder destinationFolder = ApplicationData.Current.LocalFolder;


            try {
                StorageFile localFile = await destinationFolder.CreateFileAsync(CARD_VER + ".zip",
                    CreationCollisionOption.GenerateUniqueName);
                BackgroundDownloader downloader = new BackgroundDownloader();
                DownloadOperation download = downloader.CreateDownload(source, localFile);
                download.Priority = priority;
                // In this sample, we do not show how to request unconstrained download. 
                // For more information about background transfer, please refer to the SDK Background transfer sample: 
                // http://code.msdn.microsoft.com/windowsapps/Background-Transfer-Sample-d7833f61 
                if (!requestUnconstrainedDownload) {
                    // Attach progress and completion handlers. 
                    await HandleDownloadAsync(download, true);
                    StorageFolder unzipFolder =
                        await destinationFolder.CreateFolderAsync(Path.GetFileNameWithoutExtension(localFile.Name),
                        CreationCollisionOption.GenerateUniqueName);
                    await ZipHelper.UnZipFileAsync(localFile, unzipFolder);
                    return;
                }
            } catch (Exception ) {
                // LogStatus(ex.Message, NotifyType.ErrorMessage);
            }
        }

        private static async Task HandleDownloadAsync(DownloadOperation download, bool v) {
            await download.StartAsync();
        }

    }

    public enum CardClass {
        Basic, Classic,
        Naxxramas, GoblinsvsGnomes,
        BlackrockMountain,
        TavernBrawl,
        TheGrandTournament,
        TheLeagueofExplorers,
        WhispersoftheOldGods
    }
    public enum PlayerClass {
        Druid, Rogue, Mage,
        Shaman, Paladin, Warlock,
        Warrior, Priest, Hunter
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
