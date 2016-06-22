using HSDecks.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HSDecks.Controls {
    public sealed partial class DeckItemBlock : UserControl {
        public DeckItemBlock() {
            this.InitializeComponent();
        }

        public DeckItemViewModel Item {
            get { return (DeckItemViewModel)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(DeckItemViewModel), typeof(DeckItemBlock), 
                new PropertyMetadata(null));
    }
}
