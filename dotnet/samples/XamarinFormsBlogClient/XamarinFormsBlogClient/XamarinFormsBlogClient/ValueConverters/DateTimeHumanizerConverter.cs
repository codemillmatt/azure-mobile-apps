﻿using System;
using System.Globalization;
using Humanizer;
using Xamarin.Forms;

namespace XamarinFormsBlogClient.ValueConverters
{
    public class DateTimeHumanizerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is DateTime) ? value : ((DateTime)value).Humanize();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}