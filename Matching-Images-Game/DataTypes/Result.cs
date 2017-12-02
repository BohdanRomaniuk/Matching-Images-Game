using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matching_Images_Game.DataTypes
{
    [Serializable]
    public class Result: IComparable
    {
        public string GamerName { get; set; }
        public string FieldSize { get; set; }
        public uint BestTime { get; set; }
        public uint Points { get; set; }
        public Result()
        {
        }
        public Result(string _gamerName, string _fieldSize, uint _bestTime, uint _points)
        {
            GamerName = _gamerName;
            FieldSize = _fieldSize;
            BestTime = _bestTime;
            Points = _points;
        }

        public int CompareTo(object obj)
        {
            return (obj as Result).Points.CompareTo(Points);
        }
    }
}
