using HSDecks.Common;
using HSDecks.Models;
using HSDecks.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HSDecks {
    public class DeckDataSource {
 
        public static async Task SaveDeckAsync(IEnumerable<Deck> p)
        {
            await SaveAsync(p);
        }

        private static async Task SaveAsync<T> (T p)
        {
            var file = await ApplicationData.Current.LocalFolder
                .CreateFileAsync("deck.dat", CreationCollisionOption.ReplaceExisting);
            byte[] array = Serializer.Serialize(p);
            await FileIO.WriteBytesAsync(file, array);
        }

        public static async Task<List<DeckViewModel>> GetDecksAsync()
        {
            IEnumerable<Deck> decks = await GetAsync<IEnumerable<Deck>>();

            var deckList = new List<DeckViewModel>();
            var CardsPool = new List<AbstractCard>();
            await CardData.GetCards(CardsPool, new CardQueryOption());

            if (decks == null)
            {
                return deckList;
            }

            foreach (var deck in decks)
            {
                var items = new List<DeckItemViewModel>();

                foreach (var item in deck.items)
                {
                    string id = item.cardId;
                    int count = item.cardCount;

                    var selected = CardsPool.FirstOrDefault(p => p.cardId == id);
                    if (selected != null)
                    {
                        var t = new DeckItemViewModel(selected, count);
                        items.Add(t);
                    }
                }


                DeckViewModel dk = new DeckViewModel(deck.id, deck.name, deck.playerClass, items);
                deckList.Add(dk);
            }

            return deckList;
        }

        private static async Task<T> GetAsync<T>()
        {
            T obj = default(T);
            var file = await ApplicationData.Current.LocalFolder.TryGetItemAsync("deck.dat") as StorageFile;
            if (file != null)
            {
                var bytes = (await FileIO.ReadBufferAsync(file)).ToArray();
                obj = Serializer.Deserialize<T>(bytes);
            }

            return obj;
        }
    }
}
