using FuckUWP1.Common;
using HSDecks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using System.ComponentModel;
using HSDecks.Views;

namespace HSDecks.ViewModels {
    public class MasterViewModel: BindableBase {
        private readonly string HOST = "10.0.0.4";

        public List<AbstractCard> Cards { get; set; }

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
            this.Downloads = new List<DownloadViewModel>();
            Downloads.Add(new DownloadViewModel("Basic") {
                FileName = "BASIC",
                ChnName = "基础系列",
                FullFileName = "BASIC.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=BASIC.zip", HOST),
                Progress = 0,
                Size = 85,
                Mode = "standard",
            });
            Downloads.Add(new DownloadViewModel("BRM") {
                FileName = "BRM",
                ChnName = "黑石山的火焰",
                FullFileName = "BRM.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=BRM.zip", HOST),
                Progress = 0,
                Size = 5,
                Mode = "standard",
            });
            Downloads.Add(new DownloadViewModel("GVG") {
                FileName = "GVG",
                ChnName = "地精大战侏儒",
                FullFileName = "GVG.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=GVG.zip", HOST),
                Progress = 0,
                Size = 25,
                Mode = "wild",
            });
            Downloads.Add(new DownloadViewModel("LOE") {
                FileName = "LOE",
                ChnName = "探险者协会",
                FullFileName = "LOE.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=LOE.zip", HOST),
                Progress = 0,
                Size = 9,
                Mode = "standard",
            });
            Downloads.Add(new DownloadViewModel("NAX") {
                FileName = "NAX",
                ChnName = "纳克萨玛斯",
                FullFileName = "NAX.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=NAX.zip", HOST),
                Progress = 0,
                Size = 5,
                Mode = "wild",
            });
            Downloads.Add(new DownloadViewModel("OG") {
                FileName = "OG",
                ChnName = "古神的低语",
                FullFileName = "OG.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=OG.zip", HOST),
                Progress = 0,
                Size = 28,
                Mode = "standard",
            });
            Downloads.Add(new DownloadViewModel("AT") {
                FileName = "AT",
                ChnName = "冠军的试炼",
                FullFileName = "AT.zip",
                Address = string.Format("http://{0}/HSDecks/Home/Download?ImageName=AT.zip", HOST),
                Progress = 0,
                Size = 26,
                Mode = "standard",
            });
            foreach (var set in Downloads) {
                var s = await ApplicationData.Current.LocalFolder.TryGetItemAsync(set.FullFileName);
                if (s != null) {
                    set.Complete();
                }
            }
            StandardSet = Downloads.Where(p => p.Mode == "standard").ToList();
            WildSet = Downloads.Where(p => p.Mode == "wild").ToList();

            StandardSet.ForEach(p => p.PropertyChanged += this.NotifyStandardSetCount);
            WildSet.ForEach(p => p.PropertyChanged += this.NotifyWildSetCount);
            Downloads.ForEach(p => p.PropertyChanged += this.UpdateCardImage);

            await refreshPageAsync();
            await DeckInitializing();
        }

        private void UpdateCardImage(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DownloadViewModel.Ready))
            {
                foreach (var deck in Decks)
                {
                    foreach (var item in deck.items)
                    {
                        FindCardImage(item.card);
                    }
                }
            }
        }

        private void NotifyWildSetCount(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DownloadViewModel.Ready))
            {
                OnPropertyChanged(nameof(WildDownloadMsg));
            }
        }

        private void NotifyStandardSetCount(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DownloadViewModel.Ready))
            {
                OnPropertyChanged(nameof(StandardDownloadMsg));
            }
        }

        public async Task DeckInitializing() {
            var oldDeckList = await DeckDataSource.GetDecksAsync();

            Decks.Clear();
            foreach (var deck in oldDeckList)
            {
                foreach (var item in deck.items)
                {
                    FindCardImage(item.card);
                }
            }
            oldDeckList.ForEach(p => Decks.Add(p));
        }

        private void FindCardImage(AbstractCard card)
        {
            Uri uri;
            if (ImageSetReady(card.cardSetName))
            {
                uri = new Uri(String.Format("ms-appdata:///local/cards/{0}.png", card.cardId));
            }
            else
            {
                uri = new Uri(String.Format("ms-appx:///Assets/empty.png"));
            }
            card.image = new BitmapImage(uri);
        }

        public ObservableCollection<DetailViewModel> Board { get; set; }

        public async Task refreshPageAsync() {
            if ((Cards.Count == 0)
                || (Cards[0].cost != _cost)
                || (Cards.Exists(p => p.playerClass != _class)))
            {
                var option = new CardQueryOption();
                option.AddHero(_class);
                // Neutral
                option.AddHero("None");  
                option.AddCost(_cost);

                await CardData.GetCards(Cards, option);
            }

            AbstractCard empty = new AbstractCard();
            Board.Clear();

            for (int i = 0; i < Cards.Count; i++) {
                FindCardImage(Cards[i]);
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

            await DeckDataSource.SaveDeckAsync(Decks.ToList().Select(p => p.ToDeck()));
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
        public List<DownloadViewModel> Downloads;
        public List<DownloadViewModel> StandardSet;
        public List<DownloadViewModel> WildSet;

        public string StandardDownloadMsg => string.Format("已下载({0}/5)", StandardSet.Count(p => p.Ready == true));
        public string WildDownloadMsg => string.Format("已下载({0}/2)", WildSet.Count(p => p.Ready == true));

        
        public bool ImageSetReady(string setName)
        {
            return Downloads.First(p => p.FileName == setName).Ready ;
        }

        public DownloadViewModel SelectedExpansion { get; set; }

        public async Task FilterCardsAsync(string setName)
        {
            var option = new CardQueryOption();
            option.AddExpansion(setName);

            await CardData.GetCards(Cards, option);
            Cards.ForEach(p => FindCardImage(p));
        }

        public async Task FilterCardsAsync(CardQueryOption option)
        {
            await CardData.GetCards(Cards, option);
            Cards.ForEach(p => FindCardImage(p));
        }

        // Page CardOption
        public CardQueryOptionViewModel QueryViewModel = new CardQueryOptionViewModel();
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
