using InsurenceCar.Models;
using InsurenceCar.Pages.AdminPages;
using InsurenceCar.Pages.ClientPages;

namespace InsurenceCar.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        var user = DataManager.GetLoggedUser();
        if (user.Id != 0)
        {
            GlobalSettings.MainUser = user;
            Navigate(user);
        }

    }

    private async void Navigate(User user)
    {
        if (user.RoleId == 3)
        {
            await Navigation.PushAsync(new MainMenuClientPage());
            return;
        }
        await Navigation.PushAsync(new MainMenuAdminPage());
    }


    private async void BLogin_Clicked(object sender, EventArgs e)
    {
        var users = await NetManager.Get<List<User>>("Users");
        await DBConnection.RefreshEnums();
        await DBConnection.RefreshData();
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

        GlobalSettings.MainUser = user;
        GlobalSettings.SaveUser();

        Navigate(user);

    }

    private async void BRegistration_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
    }
}