using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static System.Math;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship
{
    internal class MapVM : ViewModelBase
    {
        static Random rnd = new Random();   
        CellVM[,] map; // y, x
        public ObservableCollection<ShipVM> Ships { get; } = new ObservableCollection<ShipVM>();
        public CellVM this[int x,int y] => map[y,x];

        public string Text { get; set; }

        public IReadOnlyCollection<IReadOnlyCollection<CellVM>> Map
        {
            get
            {
                var viewMap = new List<List<CellVM>>();
                for (int y = 0; y < 10; y++)
                {
                    viewMap.Add(new List<CellVM>());
                    for (int x = 0; x < 10; x++)
                    {
                        viewMap[y].Add(this.map[x, y]);
                    }
                }
                return viewMap;
            }
        }
        public MapVM(int party)
        {
            map = new CellVM[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = new CellVM(party, i, j);
                }
            }
            if (party == 0)
            {
                Text = "Твоё поле";
            }
            else
            {
                Text = "Поле соперника";

            }
        }
        //FillMap(0,4,3,2,1)
        private List<Ship> fillMap(List<Ship> ships, params int[] navy)
        {
            var p = 0;
            while (p < navy.Length && navy[p] == 0) p++;
            if (p == navy.Length)
            {
                return ships;
            }
            else
            {
                var ship = new Ship(0, 0, p, DirectionShip.Horisont);
                navy[p]--;
                int k = 0;
                while (k < 10)
                {
                    ship.Dir = rnd.Next(2) == 0 ? DirectionShip.Horisont : DirectionShip.Vertical;
                    if (ship.Dir == DirectionShip.Horisont)
                    {
                        ship.X = rnd.Next(11 - p);
                        ship.Y = rnd.Next(10);
                    }
                    else
                    {
                        ship.X = rnd.Next(10);
                        ship.Y = rnd.Next(11 - p);
                    }
                    int count = 0;
                    for (int i = 0; i < ships.Count; i++)
                    {
                        if (!ship.Croos(ships[i]))
                        {
                            count++;
                            break;
                        }
                    }
                    if (count == 0)
                    {
                        ships.Add(ship);
                        var result = fillMap(ships, navy);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
                return null;
            }
        }

        public void FillMap(int side, params int[] navy)
        {
            List<Ship> ships = null;
            while (ships == null)
            {
                ships = fillMap(new List<Ship>(), navy);
            }
            foreach (var ship in ships) {
                if (ship.Dir == DirectionShip.Horisont)
                {
                    for (int x = ship.X; x < ship.X + ship.Rang; x++)
                    {
                        map[x, ship.Y].ToShip();
                    }
                }
                else
                {
                    for (int y = ship.Y; y < ship.Y + ship.Rang; y++)
                    {
                        map[ship.X, y].ToShip();
                    }
                }
            }
            foreach (var ship in ships)
            {
                Ships.Add(new ShipVM(ship, side));
            }
        }
        public struct Ship
        {
            public int X, Y, Rang;
            public DirectionShip Dir;
            public Ship(int x, int y, int rang, DirectionShip dir)
            {
                this.Dir = dir;
                this.X = x; 
                this.Y = y; 
                this.Rang = rang;
            }

            public bool Croos(Ship other)
            {
                (int,int)[,] arrayXY = new (int, int)[3+Rang-1,3];
                for (int i = 0; i < arrayXY.GetLength(0); i++)
                {
                    for (int j = 0; j < arrayXY.GetLength(1); j++)
                    {
                        if (Dir == DirectionShip.Horisont)
                        {
                            arrayXY[i, j] = (X-1+i,Y-1+j);
                        }
                        else
                        {
                            arrayXY[i, j] = (X + 1 - j, Y - 1 + i);
                        }
                    }
                }
                for (int i = 0; i < arrayXY.GetLength(0); i++)
                {
                    for (int j = 0; j < arrayXY.GetLength(1); j++)
                    {
                        for (int rank = 0; rank < other.Rang; rank++)
                        {
                            if (other.Dir == DirectionShip.Horisont)
                            {
                                if (arrayXY[i, j] == (other.X + rank, other.Y))
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (arrayXY[i, j] == (other.X, other.Y + rank))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                return true;
            }

        }
    }
}
