using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InicioSesion.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string porpertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(porpertyName));
            }
        }
    }
}
