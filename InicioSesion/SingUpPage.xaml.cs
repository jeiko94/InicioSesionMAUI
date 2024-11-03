namespace InicioSesion;

public partial class SingUpPage : ContentPage
{
	//Diccionaria estatico para almacenar el usuario y contraseña en memoria
	private Dictionary<string, string> userCredentials = new Dictionary<string, string>();
	public SingUpPage()
	{
		InitializeComponent();
	}

	public async void OnSingUpButtonClicked(object sender, EventArgs e)
	{
		string username = UsernameEntry.Text?.Trim();
		string password = PasswordEntry.Text?.Trim();

		if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
		{
			await DisplayAlert("Error", "Ingresa tus crendenciales", "OK");
			return;
		}

		if (userCredentials.ContainsKey(username))
		{
			await DisplayAlert("Error", "El usuario ya existe", "OK");
			return;
		}

		//Guardar las credenciales en memoria
		userCredentials[username] = password;
		await DisplayAlert("Exito", "Usuario creado con exito", "OK");

		//Navegar a la pagina de inicio de sesión
		await Navigation.PushAsync(new LoginPage(userCredentials));
	}

	public async void OnGoToLoginButtonClicked(object sender, EventArgs e)
	{
		//Navegar a la pagina de inicio de sesión
		await Navigation.PushAsync(new LoginPage(userCredentials));
	}
}