﻿
//using AVFoundation;
using System.Windows.Input;
using System.Xml.Serialization;
using InicioSesion.Views;

namespace InicioSesion.ViewModels
{
    public class LoginViewModel : BaseViewModel
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

        public string Passowrd
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
        public ICommand LoginCommand { get; }
        public ICommand GoToSignUpCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
            GoToSignUpCommand = new Command(OnGoToSignUp);
        }

        private async void OnLogin()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Passowrd))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresa tus credenciales", "OK");
                return;
            }

            if (SignUpViewModel.UserCredentials.ContainsKey(Username) && SignUpViewModel.UserCredentials[Username] == Passowrd)
            {
                await Application.Current.MainPage.DisplayAlert("Exito", "Inicio de sesión exitoso", "OK");

                //Navega a la pagina de bienvenida
                await Application.Current.MainPage.Navigation.PushAsync(new Views.WelcomePage(Username));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Nombre de usuario o contraseña incorrecta", "OK");
            }
        }

        private async void OnGoToSignUp()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.SignUpPage());
                                                                              
        }
    }
}
