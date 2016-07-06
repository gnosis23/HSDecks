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
    public sealed partial class SingleCard : Page
    {
        public MasterViewModel masterViewModel => App.Global.masterViewModel;
        public List<DownloadViewModel> Downloads => masterViewModel.Downloads;
        public SingleCard()
        {
            this.InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var Selected = (DownloadViewModel)e.ClickedItem;
            masterViewModel.SelectedExpansion = Selected; 
            await masterViewModel.FilterCardsAsync(Selected.FileName);

            Frame.Navigate(typeof(SetCardDetailPage));
        }

        private void QueryBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CardOptionsPage));
        }
    }

}
