using HSDecks.Models;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeckDetail : Page {
        private static DependencyProperty s_deckProperty =
            DependencyProperty.Register("OneDeck", typeof(Deck), typeof(DeckDetail), new PropertyMetadata(null));

        public static DependencyProperty DeckProperty {
            get { return s_deckProperty; }
        }

        public Deck OneDeck {
            get { return (Deck)GetValue(s_deckProperty); }
            set { SetValue(s_deckProperty, value); }
        }

        public DeckDetail() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            OneDeck = (Deck)e.Parameter;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Frame.GoBack(new EntranceNavigationTransitionInfo());
        }
    }
}
