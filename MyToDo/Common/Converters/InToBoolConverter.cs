using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MyToDo.Common.Converters
{
    public class InToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value != null && int.TryParse(value.ToString(), out int result))
            {  // 有了out关键字，这个int类型直接就有
                if (result == 0)
                    return false;
            }
            return true;
        }

        // 这个是UI返回来VM的(反向装换)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && bool.TryParse(value.ToString(), out bool result))
            {
                if (result)
                    return 1;
            }
            return 0;
        }
    }
}
