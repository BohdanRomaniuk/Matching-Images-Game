using System.Windows;
using Matching_Images_Game.MVVM;
using System.Windows.Data;
using System;
using System.Windows.Controls;
using System.Globalization;

namespace Matching_Images_Game
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(item);
            return (index+1).ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
            DataContext = new MainViewModel();
		}
	}
}
