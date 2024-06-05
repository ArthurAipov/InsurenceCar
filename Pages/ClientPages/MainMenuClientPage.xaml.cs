using InsurenceCar.Models;
using System.Linq.Expressions;

namespace InsurenceCar.Pages.ClientPages;

public partial class MainMenuClientPage : ContentPage
{
    public MainMenuClientPage()
    {
        InitializeComponent();
    }

    private async void BExite_Clicked(object sender, EventArgs e)
    {
        GlobalSettings.MainUser = new User();
        GlobalSettings.SaveUser();
        await Navigation.PushAsync(new LoginPage());
    }

    private async void BOsago_Clicked(object sender, EventArgs e)
    {
        var user = GlobalSettings.MainUser;
        var drivers = await NetManager.Get<List<Driver>>("Drivers");
        var driver = drivers.FirstOrDefault(x => x.UserId == GlobalSettings.MainUser.Id);
        if (driver == null)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Заполните свой профиль", "Ок");
            return;
        }
        var cars = (await NetManager.Get<List<Car>>("Cars")).Where(x => x.DriverId == driver.Id).ToList();
        if (cars.Count == 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Добавьте машины", "Ок");
            return;
        }

        await Navigation.PushAsync(new ClientOsagoPage());

    }

    private async void BCars_Clicked(object sender, EventArgs e)
    {
        var user = GlobalSettings.MainUser;
        var drivers = await NetManager.Get<List<Driver>>("Drivers");
        var driver = drivers.FirstOrDefault(x => x.UserId == GlobalSettings.MainUser.Id);
        if (driver == null)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Заполните свой профиль", "Ок");
            return;
        }

        GlobalSettings.CarsOfUser = await GlobalSettings.GetCars();
        await Navigation.PushAsync(new ClientCarsPage());

    }

    private async void BProfile_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClientProfilePage());
    }

    private async void BIssue_Clicked(object sender, EventArgs e)
    {
        var user = GlobalSettings.MainUser;
        var drivers = await NetManager.Get<List<Driver>>("Drivers");
        var driver = drivers.FirstOrDefault(x => x.UserId == GlobalSettings.MainUser.Id);
        if (driver == null)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Заполните свой профиль", "Ок");
            return;
        }
        var cars = (await NetManager.Get<List<Car>>("Cars")).Where(x => x.DriverId == driver.Id).ToList();
        if (cars.Count == 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Добавьте машины", "Ок");
            return;
        }

        await Navigation.PushAsync(new ClientIssuePage());
    }
}