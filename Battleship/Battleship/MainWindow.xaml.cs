using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battleship
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BatleshipVM bs = new BatleshipVM();
        Random rnd = new Random();
        public MainWindow()
        {
            DataContext = bs;
            InitializeComponent();
            bs.Start();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var brd = sender as Border;
            var cellVM = brd.DataContext as CellVM;
            if (cellVM.Shot == Visibility.Collapsed && cellVM.Miss == Visibility.Collapsed && cellVM.Party == 1)
            {
                var listEnemyShips = bs.EnemyMap.Ships;
                bs.AliveCheck(listEnemyShips, cellVM, 1);
                cellVM.ToShot();
                if (cellVM.Shot == Visibility.Collapsed)
                {
                    bs.ShotToOurMap(); 
                }
            }
        }

        private async void btnFleet_Click(object sender, RoutedEventArgs e)
        {
            bs.OurMap.Clear();
            bs.OurMap.FillMap(0, 0, 4, 3, 2, 1);
            (sender as Button).IsEnabled = false;
            await Task.Delay(2000);
            (sender as Button).IsEnabled = true;
        }

        private void btnGame_Click(object sender, RoutedEventArgs e)
        {
            bs.VisibilityGameBtn = Visibility.Collapsed;
            bs.DestroyedOurShips.Clear();
            bs.DestroyedEnemyShips.Clear();
            bs.UpdateShips();
            bs.OurMap.Clear();
            bs.EnemyMap.ClearMap();
            bs.OurMap.ClearMap();
            bs.EnemyMap.Clear();
            bs.EnemyMap.FillMap(1, 0, 4, 3, 2, 1);
            bs.VisibilityGameStatus = Visibility.Collapsed;
            bs.OurMap.BtnVisibility = Visibility.Visible;
            App.FirstShot = true;
            bs.Start();
        }
    }
}