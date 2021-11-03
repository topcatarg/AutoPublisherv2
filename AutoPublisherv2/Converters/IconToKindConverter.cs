using System;
using System.Globalization;
using System.Windows.Data;
using MahApps.Metro.IconPacks;

namespace AutoPublisherv2.Converters
{
    class IconToKindConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? PackIconPicolIconsKind.Accept : (object)PackIconPicolIconsKind.Cancel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return null;
        }
    }
}
