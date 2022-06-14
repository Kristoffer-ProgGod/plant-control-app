using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PlantControlApp.Converters
{
    internal class PlantIdToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("http://20.4.59.10:9092/images/plants/");
            stringBuilder.Append(value.ToString());
            return stringBuilder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
