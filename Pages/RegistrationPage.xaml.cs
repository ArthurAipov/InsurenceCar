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
        await DBConnection.RefreshEnums();
        await DBConnection.RefreshData();

        var errorMessage = "";
		if (string.IsNullOrWhiteSpace(ELogin.Text))
			errorMessage += "������� �����\n";
		if (string.IsNullOrWhiteSpace(EPassword.Text))
			errorMessage += "������� ������\n";
		
		var userInDB = users.FirstOrDefault(x => x.Login == ELogin.Text);
		if (userInDB != null)
			errorMessage += "������ ������������ �����";
		
		if (!string.IsNullOrWhiteSpace(errorMessage))
		{
			await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
			return;
		}

		var user = new User() { Login =  ELogin.Text, Password = EPassword.Text, RoleId = 3 };
		await NetManager.Post("Users", user);
		await Shell.Current.DisplayAlert("�������", "�� ������������������", "��");
		await Navigation.PopAsync();

    }
}