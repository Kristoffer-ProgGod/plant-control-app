using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PlantControlApp.Converters
{
    internal class PlantIdToImageSourceConverter : IValueConverter
    {

        /// <summary>
        /// Converts the object id of plants to image source url of the corresponding image.
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>ImageSource url as a string</returns>
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
