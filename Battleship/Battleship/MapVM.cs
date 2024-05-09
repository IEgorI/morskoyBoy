using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class MapVM : ViewModelBase
    {
        CellVM[,] map;
        public CellVM this[int x,int y] => map[y,x];
        public IReadOnlyCollection<IReadOnlyCollection<CellVM>> Map
        {
            get
            {
                var map = new List<List<CellVM>>();
                for (int i = 0; i < 10; i++)
                {
                    map.Add(new List<CellVM>());
                    for (int j = 0; j < 10; j++)
                    {
                        map[i].Add(this.map[i, j]);
                    }
                }
                return map;
            }
        }
        public MapVM(string str) : this() {
            var mp = str.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i,j].ToShip();
                }
            }
        }
        public MapVM()
        {
            map = new CellVM[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = new CellVM();
                }
            }
        }

        private void FillMap()
        {
            var ps = new Dictionary<int, int>{
                [4] = 1,
                [3] = 2,
                [2] = 3,
                [1] = 4,};
            for (int p = 4; p > 0; p--)
            {
                int k = ps[p];
            }
        }
    }
}
