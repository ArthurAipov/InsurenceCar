using InsurenceCar.Models;

namespace InsurenceCar.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        var user = DataManager.GetLoggedUser();
        if (user != null)
        {

        }

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

        DataManager.SaveLoggedUser(user);

        switch(user.RoleId)
        {
            case 1:
                await Shell.Current.DisplayAlert("Error", "admin", "ok");
                break;
            case 2:
                await Shell.Current.DisplayAlert("Error", "admin", "ok");
                break;
            case 3:
                await Shell.Current.DisplayAlert("Error", "client", "ok");
                break;
            case 4:
                await Shell.Current.DisplayAlert("Error", "admin", "ok");
                break;
        }

    }

    private async void BRegistration_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
    }
}