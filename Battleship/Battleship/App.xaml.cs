using System.Configuration;
using System.Data;
using System.Windows;

namespace Battleship
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const int CellSize = 30;
        public static bool FirstShot = true;
        public static int CountOfDestroyEnemyShips = 0;
        public static int CountOfDestroyOurShips = 0;
    }

}
