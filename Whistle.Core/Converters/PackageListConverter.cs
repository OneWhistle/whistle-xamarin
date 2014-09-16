using Cirrious.CrossCore.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Whistle.Core.Converters
{
    public class PackageListConverter
        : MvxValueConverter<IList<int>, string>
    {
        protected override string Convert(IList<int> value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.Count == 0)
                return base.Convert(value, targetType, parameter, culture);

            var lables = Resources.PackageLabels.Split(';');

            StringBuilder result = new StringBuilder();
            result.Append(lables[value[0]]);
            for (int i = 1; i < value.Count; i++)
            {
                result.Append("+").Append(lables[value[i]]);
            }
            return result.ToString();
        }
    }
}
