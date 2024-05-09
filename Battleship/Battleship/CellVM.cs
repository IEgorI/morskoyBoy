using System.Windows;

namespace Battleship
{
    internal class CellVM : ViewModelBase
    {
        static Random rnd = new Random();
        public int Angle { get; } = rnd.Next(-5, 5);
        public int AngleX { get; } = rnd.Next(-5, 5);
        public int AngleY { get; } = rnd.Next(-5, 5);
        public float ScaleX { get; } = 1 + rnd.Next(-15, 3) / 100.0f;
        public float ScaleY { get; } = 1 + rnd.Next(-15, 3) / 100.0f;
        public float ShiftX { get; } = 1 + rnd.Next(-20, 20) / 10.0f;
        public float ShiftY { get; } = 1 + rnd.Next(-20, 20) / 10.0f;
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