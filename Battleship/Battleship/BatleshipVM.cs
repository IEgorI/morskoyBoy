using System.Windows.Threading;

namespace Battleship
{
    public partial class MainWindow
    {
        class BatleshipVM : ViewModelBase
        {
            string time = "";

            string ourMap =
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
                private set => Set( ref time, value); }
            DateTime startTime;
            DispatcherTimer timer;
            public BatleshipVM()
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += Timer_Tick;
                OurMap = new MapVM(ourMap);
                EnemyMap = new MapVM(ourMap);
            }
            private void Timer_Tick(object? sender, EventArgs e)
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

            internal void ShotToOurMap(int x, int y)
            {
                OurMap[x,y].ToShot();
            }
        }
    }
}