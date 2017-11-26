using System.Windows;
using Matching_Images_Game.MVVM;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Matching_Images_Game
{
    public class GridHelpers
    {
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.RegisterAttached(
                "RowCount", typeof(int), typeof(GridHelpers),
                new PropertyMetadata(-1, RowCountChanged));
        public static int GetRowCount(DependencyObject obj)
        {
            return (int)obj.GetValue(RowCountProperty);
        }
        public static void SetRowCount(DependencyObject obj, int value)
        {
            obj.SetValue(RowCountProperty, value);
        }
        public static void RowCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)obj;
            grid.RowDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
                grid.RowDefinitions.Add(
                    new RowDefinition() { Height = GridLength.Auto });

            SetStarRows(grid);
        }
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.RegisterAttached(
                "ColumnCount", typeof(int), typeof(GridHelpers),
                new PropertyMetadata(-1, ColumnCountChanged));
        public static int GetColumnCount(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnCountProperty);
        }
        public static void SetColumnCount(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnCountProperty, value);
        }
        public static void ColumnCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)obj;
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
                grid.ColumnDefinitions.Add(
                    new ColumnDefinition() { Width = GridLength.Auto });

            SetStarColumns(grid);
        }
        public static readonly DependencyProperty StarRowsProperty =
            DependencyProperty.RegisterAttached(
                "StarRows", typeof(string), typeof(GridHelpers),
                new PropertyMetadata(string.Empty, StarRowsChanged));
        public static string GetStarRows(DependencyObject obj)
        {
            return (string)obj.GetValue(StarRowsProperty);
        }
        public static void SetStarRows(DependencyObject obj, string value)
        {
            obj.SetValue(StarRowsProperty, value);
        }
        public static void StarRowsChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || string.IsNullOrEmpty(e.NewValue.ToString()))
                return;

            SetStarRows((Grid)obj);
        }
        public static readonly DependencyProperty StarColumnsProperty =
            DependencyProperty.RegisterAttached(
                "StarColumns", typeof(string), typeof(GridHelpers),
                new PropertyMetadata(string.Empty, StarColumnsChanged));
        public static string GetStarColumns(DependencyObject obj)
        {
            return (string)obj.GetValue(StarColumnsProperty);
        }
        public static void SetStarColumns(DependencyObject obj, string value)
        {
            obj.SetValue(StarColumnsProperty, value);
        }
        public static void StarColumnsChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || string.IsNullOrEmpty(e.NewValue.ToString()))
                return;

            SetStarColumns((Grid)obj);
        }
        private static void SetStarColumns(Grid grid)
        {
            string[] starColumns =
                GetStarColumns(grid).Split(',');

            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                if (starColumns.Contains(i.ToString()))
                    grid.ColumnDefinitions[i].Width =
                        new GridLength(1, GridUnitType.Star);
            }
        }
        private static void SetStarRows(Grid grid)
        {
            string[] starRows =
                GetStarRows(grid).Split(',');

            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                if (starRows.Contains(i.ToString()))
                    grid.RowDefinitions[i].Height =
                        new GridLength(1, GridUnitType.Star);
            }
        }
    }
    public partial class GameWindow : Window
	{
        private Image firstImage = null;
        private Image secondImage = null;
        private DateTime startTime;
        Random randomizer = new Random();
        private uint imageCount = 0;
        private uint realImageCount;
        public GameWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            int fieldSize = viewModel.FieldSize.Key * 100;
            Height = fieldSize + 40;
            Width = fieldSize + 30;
            MainGrid.Height = fieldSize;
            MainGrid.Width = fieldSize;
            int size = viewModel.FieldSize.Key;
            uint delay = viewModel.DelayTime;
            realImageCount = (uint)(size * size);
            images.RemoveRange(size * size, images.Count - size * size);

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    Image img = CreateImage();

                    StackPanel imgWraper = new StackPanel();
                    imgWraper.Orientation = Orientation.Horizontal;
                    imgWraper.Children.Add(img);

                    Button btn = new Button();
                    btn.Height = 98;
                    btn.Width = 98;
                    btn.Margin = new Thickness(1);
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    btn.Content = imgWraper;
                    //btn.Click += new RoutedEventHandler(ShowImage);
                    MainGrid.Children.Add(btn);
                }
            }
           
            DispatcherTimer delayTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(delay) };
            delayTimer.Start();
            delayTimer.Tick += (sender, args) =>
            {
                startTime = DateTime.Now;
                delayTimer.Stop();
                HideAllImages();
            };
        }

        private void HideAllImages()
        {
            UIElementCollection allImages = MainGrid.Children;
            foreach(Button elem in allImages)
            {
                elem.Click += new RoutedEventHandler(ShowImage);
                (elem.Content as StackPanel).Children[0].Visibility = Visibility.Hidden;
            }
        }

        private void ShowImage(object sender, EventArgs args)
        {
            Image imageInside = (((sender as Button).Content as StackPanel).Children[0] as Image);
            if(firstImage==null && imageInside.Visibility == Visibility.Hidden)
            {
                firstImage = imageInside;
                imageInside.Visibility = Visibility.Visible;
                ++imageCount;
            }
            else if(imageInside.Visibility == Visibility.Hidden)
            {
                secondImage = imageInside;
                if (secondImage.Uid == firstImage.Uid)
                {
                    imageInside.Visibility = Visibility.Visible;
                    if (++imageCount == realImageCount)
                    {
                        CheckResult();
                    }
                    firstImage = null;
                    secondImage = null;
                }
            }
        }

        private void CheckResult()
        {
            string size = (DataContext as MainViewModel).FieldSize.Value;
            TimeSpan elapsedTime = DateTime.Now - startTime;
            uint points = (uint)((DataContext as MainViewModel).FieldSize.Key * elapsedTime.Seconds);
            string gamer = (DataContext as MainViewModel).GamerName;
            
            if(points>(DataContext as MainViewModel).BestResults.Last().Points)
            {
                uint position = 1;
                foreach(var elem in (DataContext as MainViewModel).BestResults)
                {
                    if(points>=elem.Points)
                    {
                        break;
                    }
                    ++position;
                }
                (DataContext as MainViewModel).BestResults.Add(new DataTypes.Result(position, gamer, size, (uint)elapsedTime.TotalSeconds, points));
            }
            if (MessageBox.Show(String.Format("Вітаємо {0}! Ви набрали {1} очків!\nСумарний час даної спроби {2} сек.", gamer, points, elapsedTime.Seconds), "Перемога") == MessageBoxResult.OK)
            {
                Close();
            }
        }

        private Image CreateImage()
        {
            Image result = new Image();
            int index = randomizer.Next(images.Count);
            result = images[index];
            images.RemoveAt(index);
            return result;
        }

        
        private List<Image> images = new List<Image>()
        {
            new Image() { Width=100, Height=100, Uid="1", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/leleka.jpg")) },
            new Image() { Width=100, Height=100, Uid="1", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/leleka.jpg")) },
            new Image() { Width=100, Height=100, Uid="2", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/flower1.png")) },
            new Image() { Width=100, Height=100, Uid="2", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/flower1.png")) },
            new Image() { Width=100, Height=100, Uid="3", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/sliva.jpg")) },
            new Image() { Width=100, Height=100, Uid="3", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/sliva.jpg")) },
            new Image() { Width=100, Height=100, Uid="4", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/bird1.jpg")) },
            new Image() { Width=100, Height=100, Uid="4", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/bird1.jpg")) },
            new Image() { Width=100, Height=100, Uid="5", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/bird2.jpg")) },
            new Image() { Width=100, Height=100, Uid="5", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/bird2.jpg")) },
            new Image() { Width=100, Height=100, Uid="6", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/butterfly1.jpg")) },
            new Image() { Width=100, Height=100, Uid="6", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/butterfly1.jpg")) },
            new Image() { Width=100, Height=100, Uid="7", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/flower2.jpg")) },
            new Image() { Width=100, Height=100, Uid="7", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/flower2.jpg")) },
            new Image() { Width=100, Height=100, Uid="8", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/rose.png")) },
            new Image() { Width=100, Height=100, Uid="8", Source=new BitmapImage(new Uri("D:/Хмара/ЛНУ/3 курс/Навчальна практика (Бардила)/Matching-Images-Game/Matching-Images-Game/bin/Debug/images/rose.png")) },
        };
    }
}
