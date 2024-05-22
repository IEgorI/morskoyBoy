using System;
using System.Windows.Threading;
using System.Windows;
using System.Collections.ObjectModel;
using System.Media;
using System.Linq;

namespace Battleship
{
    public partial class MainWindow
    {
        class BatleshipVM : ViewModelBase
        {
            static string path = $"{Environment.CurrentDirectory}\\Sound\\explosion.wav";
            static string pathWin = $"{Environment.CurrentDirectory}\\Sound\\game-won.wav";
            static string pathLose = $"{Environment.CurrentDirectory}\\Sound\\lose.wav";
            SoundPlayer SoundPlayerExplosion = new SoundPlayer(path);
            SoundPlayer SoundPlayerWin = new SoundPlayer(pathWin);
            SoundPlayer SoundPlayerLose = new SoundPlayer(pathLose);
            Random rnd = new Random();

            string time = "";
            string statusGame = "";
            Visibility visibilityGameStatus = Visibility.Collapsed;
            Visibility visibilityGameBtn = Visibility.Collapsed;
            public string StatusGame { get => statusGame; set => Set(ref statusGame, value); }
            public Visibility VisibilityGameStatus { get => visibilityGameStatus; set => Set(ref visibilityGameStatus, value); }
            public Visibility VisibilityGameBtn { get => visibilityGameBtn; set => Set(ref visibilityGameBtn, value); }
            public List<ShipVM> ourShip = new List<ShipVM>();
            public List<ShipVM> enemyShip = new List<ShipVM>();
            public List<ShipVM> DestroyedOurShips { 
                get => ourShip; 
                set => Set(ref ourShip, value); }
            public List<ShipVM> DestroyedEnemyShips { 
                get => enemyShip; 
                set => Set(ref enemyShip, value); 
            }
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
                OurMap.FillMap(0,0,4,3,2,1);
                EnemyMap = new MapVM(1);
                EnemyMap.FillMap(1,0,4,3,2,1);
            }
            private void Timer_Tick(object sender, EventArgs e)
            {
                var now = DateTime.Now;
                var dt = now - startTime;
                Time = dt.ToString(@"mm\:ss");
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
            public void UpdateShips()
            {
                Notify("DestroyedOurShips", "DestroyedEnemyShips");
            }

            public void AliveCheck(ObservableCollection<ShipVM> listShips, CellVM cellVM, int side)
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
                    if (listShips[i].CountSection == listShips[i].Rang && !DestroyedOurShips.Contains(listShips[i]) && !DestroyedEnemyShips.Contains(listShips[i]))
                    {
                        SoundPlayerExplosion.Play();
                        if (DestroyedEnemyShips == null || DestroyedOurShips == null || !DestroyedOurShips.Contains(listShips[i]) || !DestroyedEnemyShips.Contains(listShips[i]))
                        {
                            if (side == 0)
                            {
                                ourShip.Add(listShips[i]);
                                Notify("DestroyedOurShips");
                                if (DestroyedOurShips.Count == 10)
                                {
                                    Stop();
                                    SoundPlayerLose.Play();
                                    StatusGame = "Поражение!";
                                    VisibilityGameStatus = Visibility.Visible;
                                    VisibilityGameBtn = Visibility.Visible;
                                }
                            }
                            else
                            {
                                enemyShip.Add(listShips[i]);
                                Notify("DestroyedEnemyShips");
                                if (DestroyedEnemyShips.Count == 10)
                                {
                                    Stop();
                                    SoundPlayerWin.Play();
                                    StatusGame = "Победа!";
                                    VisibilityGameStatus = Visibility.Visible;
                                    VisibilityGameBtn = Visibility.Visible;
                                }
                            }
                        }
                        listShips[i].Alive = Visibility.Visible;
                    }
                }
            }

            internal async void ShotToOurMap()
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
                    OurMap.BtnVisibility = Visibility.Collapsed;
                }
                await Task.Delay(1000);
                if (ourShip.Count < 10)
                {
                    OurMap[x, y].ToShot();
                }
                AliveCheck(OurMap.Ships, OurMap[x, y], 0);
                while (OurMap[x,y].Shot == Visibility.Visible)
                {
                    if (ourShip.Count < 10)
                    {
                        var newX = rnd.Next(10);
                        var newY = rnd.Next(10);
                        if (OurMap[newX, newY].Shot == Visibility.Collapsed && OurMap[newX, newY].Miss == Visibility.Collapsed)
                        {
                            x = newX;
                            y = newY;
                            await Task.Delay(1000);
                            OurMap[x, y].ToShot();
                            AliveCheck(OurMap.Ships, OurMap[x, y], 0);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}