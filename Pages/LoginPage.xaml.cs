using InsurenceCar.Models;

namespace InsurenceCar.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();


    }


    private async void BLogin_Clicked(object sender, EventArgs e)
    {
        var users = await NetManager.Get<List<User>>("Users");

        var errorMessage = "";
        if (string.IsNullOrWhiteSpace(ELogin.Text))
            errorMessage += "Введите Логин\n";
        if (string.IsNullOrWhiteSpace(EPassword.Text))
            errorMessage += "Введите Пароль\n";
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
            return;
        }

        var user = users.FirstOrDefault(x => x.Login == ELogin.Text && x.Password == EPassword.Text);
        if (user == null)
        {
            await Shell.Current.DisplayAlert("Error", "Неправильный логин или пароль", "Ok");
            return;
        }

        switch(user.RoleId)
        {
            case 1:
                await Shell.Current.DisplayAlert("Error","admin","ok");
                break;
            case 2:
                await Shell.Current.GoToAsync($"/");
                break;
        }

    }

    private async void BRegistration_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//RegistrationPage");
    }
}