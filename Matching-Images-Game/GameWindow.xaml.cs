using System.Windows;
using Matching_Images_Game.MVVM;

namespace Matching_Images_Game
{
    public partial class GameWindow : Window
	{
        public GameWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
	}
}
