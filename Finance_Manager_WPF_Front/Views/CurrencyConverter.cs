using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Finance_Manager_WPF_Front.Views;

public class CurrencyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return "";

        if (!(value is IFormattable formattable))
            return value.ToString() ?? "";

        CultureInfo ci;
        try
        {
            ci = CurrencyCultureProvider.GetCultureFromUserPrimaryCurrency();
        }
        catch
        {
            ci = CultureInfo.InvariantCulture;
        }

        return formattable.ToString("C", ci);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException("Reverse transformation is not supported");
    }
}


