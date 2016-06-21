using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HSDecks.Controls {
    public sealed partial class InputDialog : ContentDialog {
        public bool Confirm {get; set;}
        public string MyInput => textBox.Text;

        public InputDialog(string text) {
            this.InitializeComponent();

            this.Confirm = false;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
            Confirm = true;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
        }
    }
}
