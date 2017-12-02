using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matching_Images_Game.DataTypes
{
    interface IResultSaver
    {
        void AddResult(Result newResult);
        void LoadBestResults(string fileName);
        void SaveBestResults(string fileName);
    }
}
