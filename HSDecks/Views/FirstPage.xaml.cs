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
    public sealed partial class FirstPage : Page
    {
        public FirstPage()
        {
            this.InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DownloadListBoxItem.IsTapEnabled)
            {
                this.Frame.Navigate(typeof(DownloadPage));
            }
        }

        private void IconTextBlock_Click(object sender, RoutedEventArgs e)
        {
            MenuView.IsPaneOpen = !MenuView.IsPaneOpen;
        }

        private void DataBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DownloadPage));
        }

        private void DeckBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DeckMenu));
        }

        private void DeckImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DeckMenu));
        }

        private void DataImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DownloadPage));
        }
    }
}
