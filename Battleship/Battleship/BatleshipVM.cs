using System;
using System.Windows.Threading;

namespace Battleship
{
    public partial class MainWindow
    {
        class BatleshipVM : ViewModelBase
        {
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
                OurMap = new MapVM();
                OurMap.SetShips(
                    new ShipVM { Rang = 4, Pos = (1, 1) },
                    new ShipVM { Rang = 3, Pos = (6, 1), Direct = DirectionShip.Vertical },
                    new ShipVM { Rang = 3, Pos = (8, 1), Direct = DirectionShip.Vertical },
                    new ShipVM { Rang = 2, Pos = (1, 3) },
                    new ShipVM { Rang = 2, Pos = (1, 5) },
                    new ShipVM { Rang = 2, Pos = (4, 3), Direct = DirectionShip.Vertical },
                    new ShipVM { Rang = 1, Pos = (1, 9) },
                    new ShipVM { Rang = 1, Pos = (2, 7) },
                    new ShipVM { Rang = 1, Pos = (4, 7) },
                    new ShipVM { Rang = 1, Pos = (8, 9) }
                    );
                FillShips();
                EnemyMap = new MapVM();
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

            internal void ShotToOurMap(int x, int y)
            {
                OurMap[x,y].ToShot();
            }
        }
    }
}