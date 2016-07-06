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
    public sealed partial class CardOptionsPage : Page
    {
        public MasterViewModel masterViewModel => App.Global.masterViewModel;
        public CardQueryOptionViewModel Query => masterViewModel.QueryViewModel;

        public CardOptionsPage()
        {
            this.InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            CardQueryOption query = Query.toModel();
            await masterViewModel.FilterCardsAsync(query);

            Frame.Navigate(typeof(SetCardDetailPage));
        }
    }


    public class CardQueryOptionViewModel
    {
        public string name { get; set; }
        public bool[] heroClassList { get; set; }
        public bool[] costList { get; set; }
        public bool[] cardTypeList { get; set; }
        public bool[] cardRarityList { get; set; }
        public bool[] modeList { get; set; }
        public bool[] expansionList { get; set; }

        public CardQueryOptionViewModel()
        {
            name = "";
            heroClassList = new bool[10];
            costList = new bool[8];
            cardTypeList = new bool[3];
            cardRarityList = new bool[5];
            modeList = new bool[2];
            expansionList = new bool[7];
        }

        private static readonly string[] heroClassString = new string[] {
                "Druid",
                "Mage",
                "Hunter",
                "Priest",
                "Rogue",
                "Shaman",
                "Paladin",
                "Warlock",
                "Warrior",
                "None",
        };
        private static readonly string[] costListString = new string[] {
                 "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
            };
        private static readonly string[] cardTypeListString = new string[] {
                 "Spell",
                 "Minion",
                 "Weapon",
            };
        private static readonly string[] cardRarityListString = new string[] {
                 "Free",
                 "Common",
                 "Rare",
                 "Epic",
                 "Legendary",
            };
        private static readonly string[] modeListString = new string[] {
                 "Standard",
                 "Wild"
            };
        private static readonly string[] expansionListString = new string[] {
                 "BASIC",
                 "NAX",
                 "GVG",
                 "AT",
                 "BRM",
                 "LOE",
                 "OG",
            };

        public CardQueryOption toModel()
        {
            CardQueryOption result = new CardQueryOption();
            result.name = name;
            for (int i = 0; i < heroClassList.Count(); i++)
            {
                if (heroClassList[i]) result.heroClassList.Add(heroClassString[i]);
            }
            for (int i = 0; i < costList.Count(); i++)
            {
                if (costList[i]) result.costList.Add(costListString[i]);
            }
            for (int i = 0; i < cardTypeList.Count(); i++)
            {
                if (cardTypeList[i]) result.cardTypeList.Add(cardTypeListString[i]);
            }
            for (int i = 0; i < cardRarityList.Count(); i++)
            {
                if (cardRarityList[i]) result.cardRarityList.Add(cardRarityListString[i]);
            }
            for (int i = 0; i < modeList.Count(); i++)
            {
                if (modeList[i]) result.modeList.Add(modeListString[i]);
            }
            for (int i = 0; i < expansionList.Count(); i++)
            {
                if (expansionList[i]) result.expansionList.Add(expansionListString[i]);
            }
            return result;
        }
    }

    public class NullableBooleanToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool?)
            {
                return (bool)value;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
                return (bool)value;
            return false;
        }
    }
}
