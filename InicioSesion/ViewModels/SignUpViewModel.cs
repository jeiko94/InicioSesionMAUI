using System.Windows.Input;
using Microsoft.Maui.Controls;
using InicioSesion.Models;
using InicioSesion.Services;
using InicioSesion.Helpers;

namespace InicioSesion.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private string username;
        private string password;
        private readonly DatabaseService _databaseService;

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
            _databaseService = new DatabaseService();
            SignUpCommand = new Command(OnSingUp);
            GoToLoginCommand = new Command(OnGoToLogin);
        }


        private async void OnSingUp()
        {
            if(Username.Length < 4 || Username.Length > 20)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El Usuario debe tener enttre 4 y 20 caracteres", "OK");
                return;
            }

            //Validar fortaleza de la contraseña
            if (!IsPasswordStrong(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La contraseña debe tener al menos 8 caracteres, incluyendo mayúsculas, minúsculas, numeros y simbolos", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresa tus credenciales", "OK");
                return;
            }

    
            //Verigficar si el usuario ya existe
            var existingUser = await _databaseService.GetUserAsync(Username);
            if (existingUser != null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El usuario ya exite", "OK");
                return;
            }

            //Hashear la contraseña
            string passwordHash = PasswordHasher.HashPassword(Password);

            //Crear el nuevo usuario
            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash
            };
            
            await _databaseService.SaveUserAsync(user);

            await Application.Current.MainPage.DisplayAlert("Exito", "Cuenta creada exitosamente", "OK");

            //Navega a la pagina de inicio de sesión
            await Application.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
        }

        private async void OnGoToLogin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
        }

        private bool IsPasswordStrong(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUpper = false, hasLower = false, hasDigit = false, hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUpper = true;
                else if (char.IsLower(c))
                    hasLower = true;
                else if (char.IsDigit(c))
                    hasDigit = true;
                else if (!char.IsLetterOrDigit(c))
                    hasSpecial = true;
            }

            return hasUpper && hasLower && hasDigit && hasSpecial;
        }
    }
}
