using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace HSDecks.Common
{
    class RarityToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color c = Colors.Black;
            if (value != null)
            {
                switch (value.ToString())
                {
                    case "Free":
                        c =  Colors.Gray;
                        break;

                    case "Common":
                        c =  Colors.WhiteSmoke;
                        break;

                    case "Rare":
                        c =  Colors.Blue;
                        break;

                    case "Epic":
                        c =  Colors.Purple;
                        break;

                    case "Legendary":
                        c =  Colors.Orange;
                        break;
                }
            }

            return new SolidColorBrush(c);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
