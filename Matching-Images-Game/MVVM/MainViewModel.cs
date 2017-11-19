using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Matching_Images_Game.DataTypes;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Matching_Images_Game.MVVM
{
    public class MainViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Result> BestResults { get; private set; }
        public Dictionary<int, string> FieldSizes { get; set; }

        private KeyValuePair<int, string> fieldSize;
        private uint delayTime;
        private string gamerName;
        public KeyValuePair<int, string> FieldSize
        {
            get
            {
                return fieldSize;
            }
            set
            {
                fieldSize = value;
                OnPropertyChanged(nameof(FieldSize));
            }
        }
        public uint DelayTime
        {
            get
            {
                return delayTime;
            }
            set
            {
                delayTime = value;
                OnPropertyChanged(nameof(DelayTime));
            }
        }
        public string GamerName
        {
            get
            {
                return gamerName;
            }
            set
            {
                gamerName = value;
                OnPropertyChanged(nameof(GamerName));
            }
        }
        public ICommand StartGame { get; private set; }
        public MainViewModel()
        {
            DelayTime = 10;
            GamerName = "гість";
            StartGame = new RelayCommand(startGame);
            BestResults = new ObservableCollection<Result>();
            FieldSizes = new Dictionary<int, string>();
            FieldSizes[2] = "2x2";
            FieldSizes[4] = "4x4";
            FieldSizes[6] = "6x6";
            FieldSizes[8] = "8x8";
            FieldSize = new KeyValuePair<int,string>(2,"2x2");
            loadBestResults();
        }
        private void startGame(object obj)
        {
            GameWindow gameWin = new GameWindow(this, FieldSize.Key);
            gameWin.Show();
        }
        private void loadBestResults()
        {
            string fileName = "best-results.xml";
            List<Result> results = new List<Result>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Result>));
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                results = (List<Result>)serializer.Deserialize(reader);
            }
            BestResults.Clear();
            foreach(Result res in results)
            {
                BestResults.Add(res);
            }
        }
        private void saveBestResults()
        {
            string fileName = "best-results.xml";
            List<Result> results = new List<Result>();
            foreach (var res in BestResults)
            {
                results.Add(res);
            }
            using (Stream outputFile = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Result>));
                serializer.Serialize(outputFile, results);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
