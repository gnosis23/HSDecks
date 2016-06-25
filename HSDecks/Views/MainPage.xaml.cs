using HSDecks.Models;
using HSDecks.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HSDecks {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MasterViewModel masterViewModel => App.Global.masterViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += (s,e) => { masterViewModel.OnLoaded(); };
            // TODO: auto two-way binding with the images
            masterViewModel.PropertyChanged += OnImageRefresh;

            ImageViewer.Visibility = Visibility.Collapsed;
        }

 

        private void IconTextBlock_Click(object sender, RoutedEventArgs e) {
            MenuView.IsPaneOpen = !MenuView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (StarListBoxItem.IsSelected) {
                masterViewModel.IsDownloadPage = true;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            DeckFrame.Navigate(typeof(DeckMenu));
        }


        private async void NextPageButton_Click(object sender, RoutedEventArgs e) {
            await masterViewModel.NextPage();
        }

        private  async void PrevPageButton_Click(object sender, RoutedEventArgs e) {
            await masterViewModel.PrevPage();
        }

        private async void Btn0_Click(object sender, RoutedEventArgs e) {
            var item = (Button)sender;
            int _cost = Int32.Parse(item.Content.ToString());
            int _page = 0;

            await masterViewModel.Select(_cost, _page);
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
                    masterViewModel.Board[count].selected = true;
                    break;
                }

                count++;
            }
        }

        private void ImageViewer_Tapped(object sender, TappedRoutedEventArgs e) {
            ImageViewer.Visibility = Visibility.Collapsed;
            // unselect all
            foreach (var item in masterViewModel.Board) {
                item.selected = false;
            }
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e) {
            var item = (MenuFlyoutItem)sender;
            string _class = item.Text;
            HeroMenu.Content = item.Text;

            await masterViewModel.SelectHero(_class);
        }

        private void Image0_Tapped_1(object sender, TappedRoutedEventArgs e) {
            if (masterViewModel.SelectedDeck == null) {
                return;
            }

            List<Image> currentPage = new List<Image>() {
                    Image0, Image1, Image2, Image3,
                    Image4, Image5, Image6, Image7
            };

            Image selectedItem = (Image)sender;

            var card = masterViewModel.Board.First(p => p.CardImage == selectedItem.Source).card;
            var item = new DeckItemViewModel(card);

            masterViewModel.SelectedDeck.Add(item);
        }

        public void OnImageRefresh(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(masterViewModel.Board)) {
                List<Image> currentPage = new List<Image>() {
                    Image0, Image1, Image2, Image3,
                    Image4, Image5, Image6, Image7
                };

                int count = 0;
                foreach (Image item in currentPage) {
                    item.Source = masterViewModel.Board[count].CardImage;

                    count++;
                }
            }
        }

 
    }

}
