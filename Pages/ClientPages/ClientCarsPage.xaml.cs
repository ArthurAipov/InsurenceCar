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
        CVCars.ItemsSource = GlobalSettings.CarsOfUser;
    }

    private void BAddCar_Clicked(object sender, EventArgs e)
    {

    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Refresh();
    }
}