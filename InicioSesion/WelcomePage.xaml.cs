namespace InicioSesion;

public partial class WelcomePage : ContentPage
{
	public WelcomePage(string usuername)
	{
		InitializeComponent();
        WelcomeLabel.Text = $"�Bienvenido, {usuername}!";

    }

	public async void OnLogoutButtonClicked(object sender, EventArgs e)
	{
		//Regresar a la pagina de inicio de sesi�n y limpiar la pila de navegaci�n
		await Navigation.PopToRootAsync();
	}
}