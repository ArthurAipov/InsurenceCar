using InsurenceCar.Models;

namespace InsurenceCar.Pages.ClientPages;

public partial class ClientCarsPage : ContentPage
{
    public ClientCarsPage()
    {
        InitializeComponent();
    }

    private async void Refresh()
    {
        await DBConnection.RefreshEnums();
        await DBConnection.RefreshData();
        CVCars.ItemsSource = await GlobalSettings.GetCars();
    }

    private async void BAddCar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddCarClientPage());
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Refresh();
    }

    private async void BBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}