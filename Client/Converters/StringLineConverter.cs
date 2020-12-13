using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Client.Converters
{
    public class StringLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var src = value as IEnumerable<string>; ;
            if (src == null)
                return null;
            return string.Join(Environment.NewLine, src);
        }
        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            var src = value as string;
            if (string.IsNullOrEmpty(src) == true)
                return new List<string>();
            return src.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }
    }
}
