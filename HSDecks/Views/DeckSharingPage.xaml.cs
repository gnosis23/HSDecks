
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

using HSDecks.Controls;
using HSDecks.Models;
using HSDecks.ViewModels;
using System.Collections.Generic;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using System;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;

namespace HSDecks.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeckSharingPage : Page {
        public MasterViewModel masterViewModel => App.Global.masterViewModel;
        public DeckViewModel Deck => masterViewModel.SelectedDeck;

        public DeckSharingPage() {
            this.InitializeComponent();

            this.Loaded += (s, e) => { this.OnLoaded(); };
        }

        private void OnLoaded() {
            List<DeckItemBlock> items = new List<DeckItemBlock> {
                Item0, Item1, Item2, Item3, Item4,
                Item5, Item6, Item7, Item8, Item9,
                Item10, Item11, Item12, Item13, Item14,
                Item15, Item16, Item17, Item18, Item19,
                Item20, Item21, Item22, Item23, Item24,
                Item25, Item26, Item27, Item28, Item29,
            };

            int _count = 0;
            for (int Index = 0; Index < Deck.items.Count; Index++ ) {
                DeckItemViewModel t = new DeckItemViewModel(Deck.items[Index].card);
                items[_count++].DataContext = t;
                if (Deck.items[Index].cardCount > 1) {
                    items[_count++].DataContext = t;
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) {
            masterViewModel.GotoMainPage();
        }


        public async void SaveBtn_Click(object sender, RoutedEventArgs e) {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(ElementToRender);
            IBuffer pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

            var savePicker = new FileSavePicker();
            savePicker.DefaultFileExtension = ".png";
            savePicker.FileTypeChoices.Add(".png", new List<string> { ".png" });
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            savePicker.SuggestedFileName = "snapshot.png";

            // Prompt the user to select a file
            var saveFile = await savePicker.PickSaveFileAsync();

            // Verify the user selected a file
            if (saveFile == null)
                return;

            // Encode the image to the selected file on disk
            using (var fileStream = await saveFile.OpenAsync(FileAccessMode.ReadWrite)) {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, fileStream);

                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    pixelBuffer.ToArray());

                await encoder.FlushAsync();
            }
        }
    }
}
