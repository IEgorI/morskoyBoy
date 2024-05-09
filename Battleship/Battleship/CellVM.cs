using System.Windows;

namespace Battleship
{
    internal class CellVM : ViewModelBase
    {
        bool ship, shot;

        public CellVM(char state = '*')
        {
            ship = state == 'X';
        }
    
        public Visibility Miss => shot && !ship ? Visibility.Visible : Visibility.Collapsed;

        public Visibility Shot => shot && ship ? Visibility.Visible : Visibility.Collapsed;

        public void ToShot()
        {
            shot = true;
            Notify("Miss", "Shot");
        }
        public void ToShip()
        {
            ship = true;
        }

    }
}