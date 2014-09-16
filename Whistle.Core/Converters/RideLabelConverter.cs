using Cirrious.CrossCore.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whistle.Core.Modal;

namespace Whistle.Core.Converters
{
    public class RideLabelConverter
        : MvxValueConverter<string, string>
    {
        protected override string Convert(string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value))
                return base.Convert(value, targetType, parameter, culture);
            int index = 0;
            var lables = Resources.RideLables.Split(';');

            for (; index < RideTypeConstants.All.Length; index++)
            {
                if (RideTypeConstants.All[index] == value)
                {
                    return lables[index];
                }
            }
            return base.Convert(value, targetType, parameter, culture);
        }
    }
}
