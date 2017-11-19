using System.Windows;
using Matching_Images_Game.MVVM;

namespace Matching_Images_Game
{
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
            DataContext = new MainViewModel();
		}
	}
}
