using HSDecks.Models;
using HSDecks.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SetCardDetailPage : Page
    {
        public MasterViewModel masterViewModel => App.Global.masterViewModel;

        public DownloadViewModel SelectedExpansion => masterViewModel.SelectedExpansion;

        public List<AbstractCard> Cards => masterViewModel.Cards;

        public SetCardDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void DeckList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var deckItem = (AbstractCard)e.ClickedItem;
            masterViewModel.SelectedCard = new DeckItemViewModel(deckItem);

            this.Frame.Navigate(typeof(CardDetailPage));
        }
    }
}
