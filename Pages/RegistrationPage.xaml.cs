using InsurenceCar.Models;

namespace InsurenceCar.Pages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    private async void BSave_Clicked(object sender, EventArgs e)
    {
		var users = await NetManager.Get<List<User>>("Users");
		
		var errorMessage = "";
		if (string.IsNullOrWhiteSpace(ELogin.Text))
			errorMessage += "¬ведите Ћогин\n";
		if (string.IsNullOrWhiteSpace(EPassword.Text))
			errorMessage += "¬ведите ѕароль\n";
		
		var userInDB = users.FirstOrDefault(x => x.Login == ELogin.Text);
		if (userInDB != null)
			errorMessage += "¬веден некорректный логин";
		
		if (!string.IsNullOrWhiteSpace(errorMessage))
		{
			await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
			return;
		}

		var user = new User() { Login =  ELogin.Text, Password = EPassword.Text, RoleId = 2 };
		await NetManager.Post("Users", user);
		await Shell.Current.GoToAsync($"//LoginPage");

    }
}