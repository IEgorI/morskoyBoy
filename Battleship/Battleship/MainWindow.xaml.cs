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

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var brd = sender as Border;
            var cellVM = brd.DataContext as CellVM;
            cellVM.ToShot();
            var x = rnd.Next(10);
            var y = rnd.Next(10);
            bs.ShotToOurMap(x,y); 
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            var brd = sender as Border;
            var cellVM = brd.DataContext as CellVM;
            cellVM.ToShot();
            var x = rnd.Next(10);
            var y = rnd.Next(10);
            bs.ShotToOurMap(x, y);
        }
    }
}