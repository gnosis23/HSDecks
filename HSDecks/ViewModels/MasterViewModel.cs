using FuckUWP1.Common;
using HSDecks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace HSDecks.ViewModels {
    public class MasterViewModel: BindableBase {
        private readonly string HOST = "10.0.0.4";

        List<AbstractCard> Cards;

        DeckViewModel _selectedDeck = null;
        public DeckViewModel SelectedDeck {
            get { return _selectedDeck; }
            set {
                _selectedDeck = value;
                if (value != null) {
                    HeroClass = value.playerClass.ToString();
                }
                else {
                    HeroClass = "All";
                }
            }
        }
        public ObservableCollection<DeckViewModel> Decks { get; set; }

        DeckItemViewModel _selectedCard = null;
        public DeckItemViewModel SelectedCard
        {
            get { return _selectedCard; }
            set
            {
                SetProperty(ref _selectedCard, value);
            }
        }

        int _page = 0;
        int _cost = 0;

        string _class = "All";
        public string HeroClass {
            get { return _class; }
            set { SetProperty(ref _class, value); }
        }

        public bool IsHeroClassEnabled => SelectedDeck == null;

        public MasterViewModel() {
            Cards = new List<AbstractCard>();
            Board = new ObservableCollection<DetailViewModel>();
            Decks = new ObservableCollection<DeckViewModel>();
        }

        public async void OnLoaded() {
            this.Downloads = new ObservableCollection<DownloadViewModel>();
            Downloads.Add(new DownloadViewModel("Basic") {
                FileName = "BASIC",
                FullFileName = "BASIC.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=BASIC.zip", HOST),
                Progress = 0,
                Size = 85,
            });
            Downloads.Add(new DownloadViewModel("BRM") {
                FileName = "BRM",
                FullFileName = "BRM.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=BRM.zip", HOST),
                Progress = 0,
                Size = 5,
            });
            Downloads.Add(new DownloadViewModel("GVG") {
                FileName = "GVG",
                FullFileName = "GVG.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=GVG.zip", HOST),
                Progress = 0,
                Size = 25,
            });
            Downloads.Add(new DownloadViewModel("LOE") {
                FileName = "LOE",
                FullFileName = "LOE.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=LOE.zip", HOST),
                Progress = 0,
                Size = 9,
            });
            Downloads.Add(new DownloadViewModel("NAX") {
                FileName = "NAX",
                FullFileName = "NAX.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=NAX.zip", HOST),
                Progress = 0,
                Size = 5,
            });
            Downloads.Add(new DownloadViewModel("OG") {
                FileName = "OG",
                FullFileName = "OG.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=OG.zip", HOST),
                Progress = 0,
                Size = 28,
            });
            Downloads.Add(new DownloadViewModel("AT") {
                FileName = "AT",
                FullFileName = "AT.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=AT.zip", HOST),
                Progress = 0,
                Size = 26,
            });
            foreach (var set in Downloads) {
                var s = await ApplicationData.Current.LocalFolder.TryGetItemAsync(set.FullFileName);
                if (s != null) {
                    set.Complete();
                }
            }

            await refreshPageAsync();
            await DeckInitializing();
        }

        public async Task DeckInitializing() {
            var str = await FileStuff.ReadFromFileAsync();
            var oldDeckList = await DeckSaver.StringToDeckListAsync(str);

            Decks.Clear();
            oldDeckList.ForEach(p => Decks.Add(p));
        }

        public ObservableCollection<DetailViewModel> Board { get; set; }

        public async Task refreshPageAsync() {
            if ((Cards.Count == 0)
                || (Cards[0].cost != _cost)
                || (Cards.Exists(p => p.playerClass != _class))) {
                Task t = CardData.GetCards(Cards, _cost, _class);
                await t;
            }

            AbstractCard empty = new AbstractCard();
            Board.Clear();

            for (int i = 0; i < Cards.Count; i++) {
                    Board.Add(new DetailViewModel(Cards[i]));
            }

            OnPropertyChanged(nameof(this.Board));
        }

        public async Task NextPage() {
            if (Cards.Count > 0 && (_page + 1) * 8 < Cards.Count) {
                _page++;
            }

            await refreshPageAsync();
        }

        public async Task PrevPage() {
            if (_page > 0) {
                _page--;
            }

            await refreshPageAsync();
        }

        public async Task Select(int cost, int page) {
            _cost = cost;
            _page = page;

            await refreshPageAsync();
        }

        public async Task SelectHero(string heroClass) {
            HeroClass = heroClass;

            await refreshPageAsync();
        }

        public async Task SaveDecks() {
            var str = DeckSaver.DeckListToString(new List<DeckViewModel>(Decks));
            await FileStuff.WriteToFileAsync(str);
        }

        public async Task SaveDecksAndExit() {
            await SaveDecks();
            HeroClass = "All";

            await refreshPageAsync();
        }

        bool _IsSharedPage = false;
        public bool IsSharedPage {
            get { return _IsSharedPage; }
            set { SetProperty(ref _IsSharedPage, value, nameof(IsSharedPage)); }
        }

        bool _IsDownloadPage = false;
        public bool IsDownloadPage {
            get { return _IsDownloadPage; }
            set { SetProperty(ref _IsDownloadPage, value, nameof(IsDownloadPage)); }
        }

        public void GotoSharedPage() {
            IsSharedPage = true;
        }
        public void GotoMainPage() {
            IsSharedPage = false;
        }

        // Download Page
        public ObservableCollection<DownloadViewModel> Downloads;
    }

    public class DetailViewModel {
        public AbstractCard card { get; }

        public DetailViewModel(AbstractCard c) {
            this.card = c;
            this.selected = false;
        }

        public BitmapImage CardImage { get { return this.card.image; } }
        public string Name { get { return this.card.name; } }
        public string Text { get { return this.card.text; } }
        public string Hero { get { return this.card.playerClass; } }
        public bool selected { get; set; }
        public string Id { get { return this.card.cardId; } }
    }
}
