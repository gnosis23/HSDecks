﻿using HSDecks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HSDecks {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<AbstractCard> Cards;
        public ObservableCollection<DetailViewModel> Board;

        int _page = 0;
        int _cost = 0;
        string _class = "All";

        public MainPage()
        {
            this.InitializeComponent();

            Cards = new List<AbstractCard>();
            Board = new ObservableCollection<DetailViewModel>();

            ImageViewer.Visibility = Visibility.Collapsed;
        }

        private async Task<ObservableCollection<Deck>> DeckInitializing() {
            var str = await FileStuff.ReadFromFileAsync();
            var oldDeckList = await DeckSaver.StringToDeckListAsync(str);

            var ret = new ObservableCollection<Deck>();
            oldDeckList.ForEach(p => ret.Add(p));
            // DeckCountChanged();
            return ret;
        }

        private void IconTextBlock_Click(object sender, RoutedEventArgs e) {
            MenuView.IsPaneOpen = !MenuView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e) {
            await refreshPageAsync();

            App.Decks = await DeckInitializing();

            DeckFrame.Navigate(typeof(DeckMenu), App.Decks);
        }

        private async Task refreshPageAsync() {
            Progress.IsActive = true;
            Progress.Visibility = Visibility.Visible;

            if ((Cards.Count == 0) 
                || (Cards[0].cost != _cost)
                || (Cards.Exists(p => p.playerClass != _class))) 
            {
                Task t = CardData.GetCards(Cards, _cost, _class);
                await t;
            }

            List<Image> currentPage = new List<Image>() {
                    Image0, Image1, Image2, Image3,
                    Image4, Image5, Image6, Image7
            };

            AbstractCard empty = new AbstractCard();
            Board.Clear();

            for (int i = 0; i < 8; i++) {
                if (_page * 8 + i < Cards.Count) {
                    Board.Add(new DetailViewModel(Cards.ElementAt(_page * 8 + i)));
                } else {
                    empty.img = null;
                    Board.Add(new DetailViewModel(empty));
                }
            }

            int count = 0;
            foreach (Image item in currentPage) {
                item.Source = Board[count].CardImage;

                count++;
            }


            Progress.Visibility = Visibility.Collapsed;
            Progress.IsActive = false;
        }

        private async void NextPageButton_Click(object sender, RoutedEventArgs e) {
            if (Cards.Count > 0 && (_page + 1) * 8 < Cards.Count) {
                _page++;
            }

            await refreshPageAsync();
        }

        private  async void PrevPageButton_Click(object sender, RoutedEventArgs e) {
            if (_page > 0) {
                _page--;
            }

            await refreshPageAsync();
        }

        private async void Btn0_Click(object sender, RoutedEventArgs e) {
            var item = (Button)sender;
            _cost = Int32.Parse(item.Content.ToString());
            _page = 0;

            await refreshPageAsync();
        }

        private void Image0_Tapped(object sender, RightTappedRoutedEventArgs e) {
            // Data source.
            ImageViewer.Visibility = Visibility.Visible;

            List<Image> currentPage = new List<Image>() {
                    Image0, Image1, Image2, Image3,
                    Image4, Image5, Image6, Image7
            };

            Image selectedItem = (Image)sender;

            var count = 0;
            foreach (var item in currentPage) {
                if (item == selectedItem) {
                    itemFlipView.SelectedIndex = count;
                    Board[count].selected = true;
                    break;
                }

                count++;
            }
            var id = Board.First(p => p.selected).Id;
        }

        private void ImageViewer_Tapped(object sender, TappedRoutedEventArgs e) {
            ImageViewer.Visibility = Visibility.Collapsed;
            // unselect all
            foreach (var item in Board) {
                item.selected = false;
            }
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e) {
            var item = (MenuFlyoutItem)sender;
            _class = item.Text;
            HeroMenu.Content = item.Text;

            await refreshPageAsync();
        }

        private void Image0_Tapped_1(object sender, TappedRoutedEventArgs e) {
            List<Image> currentPage = new List<Image>() {
                    Image0, Image1, Image2, Image3,
                    Image4, Image5, Image6, Image7
            };

            Image selectedItem = (Image)sender;

            var card = Board.First(p => p.CardImage == selectedItem.Source).card;
            var item = new DeckItem(card);

            // NOTE: deck card logic
            if (App.SelectedDeck.cardCount < 30 && _class != "All") {
                App.SelectedDeck.Add(item);
            }

        }
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
        public string  Id { get { return this.card.cardId; } }
    }
}
