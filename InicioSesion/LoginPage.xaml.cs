namespace InicioSesion;

public partial class LoginPage : ContentPage
{
	private Dictionary<string, string> userCredentials;

	public LoginPage(Dictionary<string, string> credentials)
	{
		InitializeComponent();
		userCredentials = credentials;
	}

	public async void OnLoginButtonClicked(object sender, EventArgs e)
	{
        string username = UsernameEntry.Text?.Trim();
        string password = PasswordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
		{
			await DisplayAlert("Error", "Debes ingresar tus credenciales", "Ok");
			return;
		}

		if (userCredentials.ContainsKey(username) && userCredentials[username] == password)
		{
			await DisplayAlert("Exito", "Inicio de sesión exitoso", "OK");

			//Navegar a la pagina de bienvenida
			await Navigation.PushAsync(new WelcomePage(username));
		}
		else
		{
			await DisplayAlert("Error", "Usuario o contraseña incorrecto", "Error");

		}

    }

	public async void OnGoToSingUpButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new SingUpPage());
	}
}