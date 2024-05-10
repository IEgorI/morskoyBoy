using System;
using System.Windows.Threading;
using System.Windows;
using System.Collections.ObjectModel;

namespace Battleship
{
    public partial class MainWindow
    {
        class BatleshipVM : ViewModelBase
        {
            Random rnd = new Random();

            string time = "";

            string sampleMap =
                @"
**********
*XXXX***X*
******X***
XX*XX***XX
******X***
*XXX******
*****XXX**
**********
*X********
**********
";
            
            public MapVM OurMap { get; private set; }
            public MapVM EnemyMap { get; private set; }

            public string Time {  
                get => time; 
                private set => Set(ref time, value); }
            DateTime startTime;
            DispatcherTimer timer;
            public BatleshipVM()
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += Timer_Tick;
                OurMap = new MapVM(0);
                OurMap.FillMap(0,4,3,2,1);
                EnemyMap = new MapVM(1);
                EnemyMap.FillMap(0,4,3,2,1);
            }
            private void Timer_Tick(object? sender, EventArgs e)
            {
                var now = DateTime.Now;
                var dt = now - startTime;
                Time = dt.ToString(@"mm\:ss");
            }
            void FillShips()
            {
                var ships = OurMap.Ships;
                ships.Add(new ShipVM {Rang = 4, Pos = (1,1) });
                ships.Add(new ShipVM {Rang = 3, Pos = (6,1), Direct = DirectionShip.Vertical });
                ships.Add(new ShipVM {Rang = 3, Pos = (8,1), Direct = DirectionShip.Vertical });
                ships.Add(new ShipVM {Rang = 2, Pos = (1,3) });
                ships.Add(new ShipVM {Rang = 2, Pos = (1,5) });
                ships.Add(new ShipVM {Rang = 2, Pos = (4,3), Direct = DirectionShip.Vertical});
                ships.Add(new ShipVM {Rang = 1, Pos = (1,9) });
                ships.Add(new ShipVM {Rang = 1, Pos = (2,7) });
                ships.Add(new ShipVM {Rang = 1, Pos = (4,7) });
                ships.Add(new ShipVM {Rang = 1, Pos = (8,9) });
            }

            public void Start() 
            { 
                timer.Start();
                startTime = DateTime.Now;
            }

            public void Stop() 
            {
                timer.Stop();
            }

            public void AliveCheck(ObservableCollection<ShipVM> listShips, CellVM cellVM)
            {
                for (int i = 0; i < listShips.Count; i++)
                {
                    if (listShips[i].Direct == DirectionShip.Horisont)
                    {
                        if ((listShips[i].Pos.Item1 <= cellVM.X && cellVM.X <= listShips[i].Pos.Item1 + listShips[i].Rang - 1) && listShips[i].Pos.Item2 == cellVM.Y)
                        {
                            listShips[i].CountSection += 1;
                        }
                    }
                    else
                    {
                        if ((listShips[i].Pos.Item2 <= cellVM.Y && cellVM.Y <= listShips[i].Pos.Item2 + listShips[i].Rang - 1) && listShips[i].Pos.Item1 == cellVM.X)
                        {
                            listShips[i].CountSection += 1;
                        }
                    }
                    if (listShips[i].CountSection == listShips[i].Rang)
                    {
                        listShips[i].Alive = Visibility.Visible;
                    }
                }
            }

            internal void ShotToOurMap()
            {
                var x = rnd.Next(10);
                var y = rnd.Next(10);
                if (App.FirstShot == false)
                {
                    while (OurMap[x, y].Shot == Visibility.Visible || OurMap[x, y].Miss == Visibility.Visible)
                    {
                        x = rnd.Next(10);
                        y = rnd.Next(10);
                    }
                }
                else
                {
                    App.FirstShot = false;
                }
                AliveCheck(OurMap.Ships, OurMap[x, y]);
                OurMap[x, y].ToShot();
                while (OurMap[x,y].Shot == Visibility.Visible)
                {
                    var newX = rnd.Next(10);
                    var newY = rnd.Next(10);
                    if (OurMap[newX, newY].Shot == Visibility.Collapsed && OurMap[newX, newY].Miss == Visibility.Collapsed)
                    {
                        x = newX;
                        y = newY;
                        AliveCheck(OurMap.Ships, OurMap[x, y]);
                        OurMap[x, y].ToShot();
                    }
                }
            }
        }
    }
}