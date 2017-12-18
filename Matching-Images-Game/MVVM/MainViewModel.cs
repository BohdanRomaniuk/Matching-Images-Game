using System.Collections.Generic;
using System.Linq;
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
    public class MainViewModel: INotifyPropertyChanged, IResultSaver
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
                LoadBestResults("best-results"+value.Key+".xml");
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
                if(value <= 60)
                {
                    delayTime = value;
                }
                else
                {
                    MessageBox.Show("Час затримки не може перевищувати 60 секунд!", "Помилка");
                    delayTime = 60;
                }
                OnPropertyChanged(nameof(DelayTime));
            }
        }
        public string GamerName
        {
            get
            {
                if(string.IsNullOrEmpty(gamerName))
                {
                    MessageBox.Show("Імя користувача не задано","Помилка");
                }
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
            GamerName = "Гість";    
            StartGame = new RelayCommand(startGame);
            BestResults = new ObservableCollection<Result>();
            FieldSizes = new Dictionary<int, string>();
            FieldSizes[4] = "4x4";
            FieldSizes[6] = "6x6";
            FieldSizes[8] = "8x8";
            FieldSize = new KeyValuePair<int,string>(4,"4x4");
        }
        private void startGame(object obj)
        {
            GameWindow gameWin = new GameWindow(this);
            gameWin.Show();
        }
        public void AddResult(Result newResult)
        {
            List<Result> sortedResults = new List<Result>(BestResults);
            sortedResults.Add(newResult);
            sortedResults.Sort();
            if (BestResults.Count<10)
            {
                BestResults.Clear();
                foreach (Result res in sortedResults)
                {
                    BestResults.Add(res);
                }
            }
            else
            {
                BestResults.Clear();
                for (int i=0; i<10; ++i)
                {
                    BestResults.Add(sortedResults[i]);
                }
            }
            SaveBestResults("best-results" + FieldSize.Key + ".xml");
        }
        public void LoadBestResults(string fileName)
        {
            if (File.ReadAllLines(fileName).Count() != 0)
            {
                List<Result> results = new List<Result>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<Result>));
                using (XmlReader reader = XmlReader.Create(fileName))
                {
                    results = (List<Result>)serializer.Deserialize(reader);
                }
                BestResults.Clear();
                foreach (Result res in results)
                {
                    BestResults.Add(res);
                }
            }
        }
        public void SaveBestResults(string fileName)
        {
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
