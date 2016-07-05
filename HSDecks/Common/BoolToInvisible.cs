using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HSDecks.Common {
    public class BoolToInVisibility : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            Visibility result = Visibility.Visible;
            if (value != null) {
                bool isTrue = false;
                bool.TryParse(value.ToString(), out isTrue);
                if (isTrue) {
                    result = Visibility.Collapsed;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
