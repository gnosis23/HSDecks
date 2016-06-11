using HSDecks.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HSDecks
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<AbstractCard> Cards;
        public ObservableCollection<AbstractCard> Board;

        int _page = 0;
        int _cost = 0;

        public MainPage()
        {
            this.InitializeComponent();

            Cards = new List<AbstractCard>();
            Board = new ObservableCollection<AbstractCard>();
        }

        private void IconTextBlock_Click(object sender, RoutedEventArgs e) {
            MenuView.IsPaneOpen = !MenuView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            refreshPageAsync();
        }

        private async void refreshPageAsync() {
            Progress.IsActive = true;
            Progress.Visibility = Visibility.Visible;

            if ((Cards.Count == 0) 
                || (Cards[0].cost != _cost)) {
                Task t = CardData.GetCards(Cards, _cost);
                await t;
            }

            List<Image> currentPage = new List<Image>() {
                    Image0, Image1, Image2, Image3,
                    Image4, Image5, Image6, Image7
            };

            if (Cards.Count >= 8) {
                AbstractCard empty = new AbstractCard();
                Board.Clear();

                for (int i = 0; i < 8; i++) {
                    if (_page * 8 + i < Cards.Count) {
                        Board.Add(Cards.ElementAt(_page * 8 + i));
                    } 
                    else {
                        empty.img = "ms-appx:///Assets/yaoming.jpg";
                        Board.Add(empty);
                    }
                }

                int count = 0;
                foreach (Image item in currentPage) {
                    item.Source = Board[count].image;

                    count++;
                }
            } 
            else {
                int count = 0;
                foreach (Image item in currentPage) {
                    Uri uri = new Uri("ms-appx:///Assets/yaoming.jpg");
                    BitmapImage image = new BitmapImage(uri);

                    item.Source = image;
                    count++;
                }
            }

            Progress.Visibility = Visibility.Collapsed;
            Progress.IsActive = false;
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e) {
            if (Cards.Count > 0 && (_page + 1) * 8 < Cards.Count) {
                _page++;
            }

            refreshPageAsync();
        }

        private void PrevPageButton_Click(object sender, RoutedEventArgs e) {
            if (_page > 0) {
                _page--;
            }

            refreshPageAsync();
        }

        private void Btn0_Click(object sender, RoutedEventArgs e) {
            var item = (Button)sender;
            _cost = Int32.Parse(item.Content.ToString());
            _page = 0;

            refreshPageAsync();
        }

    }
}
