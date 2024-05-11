using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship
{
    enum DirectionShip { Horisont, Vertical}
    class ShipVM : ViewModelBase
    {
        int rang = 1, x = 0, y = 0;
        (int x, int y) pos;
        Visibility alive = Visibility.Collapsed;
        DirectionShip dir = DirectionShip.Horisont;

        public ShipVM() { }
        public ShipVM(MapVM.Ship ship)
        {
            pos = (ship.X, ship.Y);
            rang = ship.Rang;
            dir = ship.Dir;
        }

        public int CountOfDestroyedShips { get; set; }

        public DirectionShip Direct
        {
            get => dir;
            set => Set(ref dir, value, "Angle");
        } 
        public int Rang { 
            get => rang; 
            set => Set(ref rang, value, "RangView"); } 

        public int RangView => Rang * App.CellSize - 5;
        public int CountSection { get; set; }
        public Visibility Alive { get => alive; set => Set(ref alive, value, "Alive"); }
        public int Angle => dir == DirectionShip.Horisont ? 0 : 90;
        public (int, int) Pos { 
            get =>pos;
            set => Set(ref pos, value, "X", "Y");
            
        }
        public int X => pos.x * App.CellSize + 3; 
        public int Y => pos.y * App.CellSize + 3; 
    }
}
