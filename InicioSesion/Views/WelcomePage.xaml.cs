namespace InicioSesion.Views;

public partial class WelcomePage : ContentPage
{
	public WelcomePage(string username)
	{
		InitializeComponent();
		WelcomeLabel.Text = $"�Bienvenido , {username}!";
	}

    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
		//Regresar al inicio y limpiar la pila de navegaci�n
		await Application.Current.MainPage.Navigation.PopToRootAsync();
    }
}