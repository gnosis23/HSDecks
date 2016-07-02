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
            // TODO: auto two-way binding with the images

        }

 

        private async void BackBtn_Click(object sender, RoutedEventArgs e) {
            // save before quit
            await masterViewModel.SaveDecks();
            this.Frame.GoBack();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
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

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e) {
            var item = (MenuFlyoutItem)sender;
            string _class = item.Text;

            await masterViewModel.SelectHero(_class);
        }

        private void Card_Tapped(object sender, TappedRoutedEventArgs e) {

        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (masterViewModel.SelectedDeck == null) {
                return;
            }

            var selectedItem = e.ClickedItem as DetailViewModel;

            masterViewModel.SelectedDeck.Add(new DeckItemViewModel(selectedItem.card));
        }

        private void QueryBtn_Click(object sender, RoutedEventArgs e)
        {
            DeckView.IsPaneOpen = !DeckView.IsPaneOpen;
        }

        private void CurrentDeckView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ClickedItem = e.ClickedItem as DeckItemViewModel;
            masterViewModel.SelectedDeck.Remove(ClickedItem);
        }
    }

}
