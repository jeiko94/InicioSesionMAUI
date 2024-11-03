using System.Windows.Input;
using Microsoft.Maui.Controls;
using InicioSesion.Models;

namespace InicioSesion.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private string username;
        private string password;

        //Propiedades para el enlace de datos
        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        //Comandos
        public ICommand SignUpCommand { get; }
        public ICommand GoToLoginCommand { get; }

        public SignUpViewModel()
        {
            SignUpCommand = new Command(OnSingUp);
            GoToLoginCommand = new Command(OnGoToLogin);
        }

        //Diccionario estatico para almacenar credenciales
        public static Dictionary<string, string> UserCredentials { get; } = new Dictionary<string, string>();

        private async void OnSingUp()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresa tus crdenciales", "OK");
                return;
            }

            if (UserCredentials.ContainsKey(Username))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El usuario ya exite", "OK");
                return;
            }

            UserCredentials[Username] = Password;
            await Application.Current.MainPage.DisplayAlert("Exito", "Cuenta creada exitosamente", "OK");

            //Navega a la pagina de inicio de sesión
            await Application.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
        }

        private async void OnGoToLogin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
        }
    }
}
