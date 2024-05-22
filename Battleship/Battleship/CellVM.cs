using System.Windows;

namespace Battleship
{
    internal class CellVM : ViewModelBase
    {
        static Random rnd = new Random();
        public int Angle { get; } = rnd.Next(-5, 5);
        public int X { get; }
        public int Y { get; }
        public int AngleX { get; } = rnd.Next(-5, 5);
        public int AngleY { get; } = rnd.Next(-5, 5);
        public float ScaleX { get; } = 1 + rnd.Next(-15, 3) / 100.0f;
        public float ScaleY { get; } = 1 + rnd.Next(-15, 3) / 100.0f;
        public float ShiftX { get; } = 1 + rnd.Next(-20, 20) / 10.0f;
        public float ShiftY { get; } = 1 + rnd.Next(-20, 20) / 10.0f;
        public int Party { get; }
        bool ship, shot;
        public bool clear = false;

        public CellVM() { }
        public CellVM(int party, int x, int y)
        {
            this.Party = party;
            this.X = x; 
            this.Y = y;
        }
        public Visibility Miss => shot && !ship && !clear ? Visibility.Visible : Visibility.Collapsed;

        public Visibility Shot => shot && ship && !clear ? Visibility.Visible : Visibility.Collapsed;

        public void ToShot()
        {
            shot = true;
            Notify("Miss", "Shot");
        }
        public void ToShotUndo()
        {
            shot = false;
            Notify("Miss", "Shot");
        }
        public void ToShip()
        {
            ship = true;
        }
        public void ToMiss()
        {
            ship = false;
        }
        public void ToClear()
        {
            clear = true;
            Notify("Miss", "Shot");
            clear = false;
            Notify("Miss", "Shot");
        }
    }
}