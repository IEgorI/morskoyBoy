using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Battleship
{
    internal class ViewModelBase :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T field, T value, [CallerMemberName] string propName = ""){
            if (!field.Equals(value))
            {
                field = value;
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        protected void Notify(params string[] names)
        {
            foreach (string name in names)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}