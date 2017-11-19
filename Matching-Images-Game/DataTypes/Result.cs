using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matching_Images_Game.DataTypes
{
    [Serializable]
    public class Result
    {
        private uint number;
        public uint Number
        {
            get
            {
                return number;
            }
            set
            {
                if(number<0 || number>10)
                {
                    throw new ArgumentException("Номер результату повинен бути в межах [0,10]");
                }
                number = value;
            }
        }
        public string GamerName { get; set; }
        public string FieldSize { get; set; }
        public uint BestTime { get; set; }
        public uint Points { get; set; }
        public Result()
        {
        }
        public Result(uint _number, string _gamerName, string _fieldSize, uint _bestTime, uint _points)
        {
            Number = _number;
            GamerName = _gamerName;
            FieldSize = _fieldSize;
            BestTime = _bestTime;
            Points = _points;
        }
    }
}
