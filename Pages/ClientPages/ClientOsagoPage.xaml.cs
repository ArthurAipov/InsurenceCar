using InsurenceCar.Models;

namespace InsurenceCar.Pages.ClientPages;

public partial class ClientOsagoPage : ContentPage
{
    public User MainUser = GlobalSettings.MainUser;
    public Driver MainDriver = GlobalSettings.MainUser.Driver.First();
    public double TotalPrice = 0;
    public List<CountOfUser> countList = new List<CountOfUser>();
    public ClientOsagoPage()
    {
        InitializeComponent();
        InitPage();
    }

    private async void InitPage()
    {
        var oneUser = new CountOfUser { Id = 1, Name = "Один" };
        var twoUser = new CountOfUser { Id = 2, Name = "Два" };
        var Unlimited = new CountOfUser { Id = 3, Name = "Безгранично" };
        countList.Add(oneUser);
        countList.Add(twoUser);
        countList.Add(Unlimited);
        PAmountOfDrivers.ItemsSource = countList.ToList();
        var cars = await GlobalSettings.GetCars();
        var regions = (await NetManager.Get<List<Models.Region>>("Regions")).ToList();
        PRegion.ItemsSource = regions;
        PCar.ItemsSource = cars;
        BindingContext = new Osago();



    }

    public async void Refresh()
    {
        double fullPrice = 5900;
        var selectedCar = PCar.SelectedItem as Car;
        var selectsdRegion = PRegion.SelectedItem as Models.Region;
        if (selectsdRegion != null)
        {
            var coefficient = selectsdRegion.CoefficientForOsago;
            fullPrice = fullPrice * coefficient;
        }
        var experience = DateTime.Now.Year - MainDriver.Experience.Year;
        var birth = DateTime.Now.Year - MainDriver.DateOfBirth.Year;
        var selectedCount = PAmountOfDrivers.SelectedItem as CountOfUser;
        if (selectedCar != null)
        {
            if (selectedCar.Driver.Osago.Where(u => u.CarId == selectedCar.Id).ToList().Count != 0)
            {
                var osagoSelectedCar = selectedCar.Driver.Osago.Last();
                if (osagoSelectedCar.DateEnd > DateTime.Now.Date)
                {
                    await Shell.Current.DisplayAlert("Ошибка", "На данную машину уже было оформленно ОСАГО и оно пока не истекло", "Ок");
                    BCreate.IsEnabled = false;
                    return;
                }
            }
            else
            {
                BCreate.IsEnabled = true;
            }
        }
        if (selectedCount != null)
        {
            if (selectedCount.Id == 2)
                fullPrice = fullPrice * 1.3;
            if (selectedCount.Id == 3)
                fullPrice = fullPrice * 2.6;
        }
        if (selectedCar != null)
        {
            var horsePower = selectedCar.Horsepower;
            if (horsePower <= 50)
            {
                fullPrice = fullPrice * 0.6;
            }
            if (50 < horsePower && horsePower <= 70)
            {
                fullPrice = fullPrice * 1.0;
            }
            if (70 < horsePower && horsePower <= 100)
            {
                fullPrice = fullPrice * 1.1;
            }
            if (100 < horsePower && horsePower <= 120)
            {
                fullPrice = fullPrice * 1.2;
            }
            if (120 < horsePower && horsePower <= 150)
            {
                fullPrice = fullPrice * 1.4;
            }
            if (150 <= horsePower)
            {
                fullPrice = fullPrice * 1.6;
            }

        }
        if (experience == 0)
        {
            if (birth >= 16 && birth <= 21)
                fullPrice = fullPrice * 2.27;
            if (birth >= 22 && birth <= 24)
                fullPrice = fullPrice * 1.88;
            if (birth >= 25 && birth <= 29)
                fullPrice = fullPrice * 1.72;
            if (birth >= 30 && birth <= 34)
                fullPrice = fullPrice * 1.56;
            if (birth >= 35 && birth <= 39)
                fullPrice = fullPrice * 1.54;
            if (birth >= 40 && birth <= 49)
                fullPrice = fullPrice * 1.50;
            if (birth >= 50 && birth <= 59)
                fullPrice = fullPrice * 1.46;
            if (birth > 59)
                fullPrice = fullPrice * 1.43;
        }
        if (experience == 1)
        {
            if (birth >= 16 && birth <= 21)
                fullPrice = fullPrice * 1.92;
            if (birth >= 22 && birth <= 24)
                fullPrice = fullPrice * 1.72;
            if (birth >= 25 && birth <= 29)
                fullPrice = fullPrice * 1.60;
            if (birth >= 30 && birth <= 34)
                fullPrice = fullPrice * 1.50;
            if (birth >= 35 && birth <= 39)
                fullPrice = fullPrice * 1.47;
            if (birth >= 40 && birth <= 49)
                fullPrice = fullPrice * 1.44;
            if (birth >= 50 && birth <= 59)
                fullPrice = fullPrice * 1.40;
            if (birth > 59)
                fullPrice = fullPrice * 1.36;

        }
        if (experience == 2)
        {
            if (birth >= 16 && birth <= 21)
                fullPrice = fullPrice * 1.84;
            if (birth >= 22 && birth <= 24)
                fullPrice = fullPrice * 1.71;
            if (birth >= 25 && birth <= 29)
                fullPrice = fullPrice * 1.54;
            if (birth >= 30 && birth <= 34)
                fullPrice = fullPrice * 1.48;
            if (birth >= 35 && birth <= 39)
                fullPrice = fullPrice * 1.46;
            if (birth >= 40 && birth <= 49)
                fullPrice = fullPrice * 1.43;
            if (birth >= 50 && birth <= 59)
                fullPrice = fullPrice * 1.39;
            if (birth > 59)
                fullPrice = fullPrice * 1.35;

        }
        if (experience == 3 || experience == 4)
        {
            if (birth >= 16 && birth <= 21)
                fullPrice = fullPrice * 1.65;
            if (birth >= 22 && birth <= 24)
                fullPrice = fullPrice * 1.13;
            if (birth >= 25 && birth <= 29)
                fullPrice = fullPrice * 1.09;
            if (birth >= 30 && birth <= 34)
                fullPrice = fullPrice * 1.05;
            if (birth >= 35 && birth <= 39)
                fullPrice = fullPrice * 1.00;
            if (birth >= 40 && birth <= 49)
                fullPrice = fullPrice * 0.96;
            if (birth >= 50 && birth <= 59)
                fullPrice = fullPrice * 0.93;
            if (birth > 59)
                fullPrice = fullPrice * 0.91;

        }
        if (experience == 5 || experience == 6)
        {
            if (birth >= 16 && birth <= 21)
                fullPrice = fullPrice * 1.62;
            if (birth >= 22 && birth <= 24)
                fullPrice = fullPrice * 1.10;
            if (birth >= 25 && birth <= 29)
                fullPrice = fullPrice * 1.08;
            if (birth >= 30 && birth <= 34)
                fullPrice = fullPrice * 1.04;
            if (birth >= 35 && birth <= 39)
                fullPrice = fullPrice * 0.97;
            if (birth >= 40 && birth <= 49)
                fullPrice = fullPrice * 0.95;
            if (birth >= 50 && birth <= 59)
                fullPrice = fullPrice * 0.92;
            if (birth > 59)
                fullPrice = fullPrice * 0.90;

        }
        if (experience >= 7 && experience <= 9)
        {
            if (birth >= 22 && birth <= 24)
                fullPrice = fullPrice * 1.09;
            if (birth >= 25 && birth <= 29)
                fullPrice = fullPrice * 1.07;
            if (birth >= 30 && birth <= 34)
                fullPrice = fullPrice * 1.01;
            if (birth >= 35 && birth <= 39)
                fullPrice = fullPrice * 0.95;
            if (birth >= 40 && birth <= 49)
                fullPrice = fullPrice * 0.94;
            if (birth >= 50 && birth <= 59)
                fullPrice = fullPrice * 0.91;
            if (birth > 59)
                fullPrice = fullPrice * 0.89;
        }
        if (experience >= 10 && experience <= 14)
        {
            if (birth >= 25 && birth <= 29)
                fullPrice = fullPrice * 1.02;
            if (birth >= 30 && birth <= 34)
                fullPrice = fullPrice * 0.97;
            if (birth >= 35 && birth <= 39)
                fullPrice = fullPrice * 0.94;
            if (birth >= 40 && birth <= 49)
                fullPrice = fullPrice * 0.93;
            if (birth >= 50 && birth <= 59)
                fullPrice = fullPrice * 0.90;
            if (birth > 59)
                fullPrice = fullPrice * 0.88;
        }
        if (experience > 14)
        {
            if (birth >= 30 && birth <= 34)
                fullPrice = fullPrice * 0.95;
            if (birth >= 35 && birth <= 39)
                fullPrice = fullPrice * 0.93;
            if (birth >= 40 && birth <= 49)
                fullPrice = fullPrice * 0.91;
            if (birth >= 50 && birth <= 59)
                fullPrice = fullPrice * 0.86;
            if (birth > 59)
                fullPrice = fullPrice * 0.83;
        }
        LTotalPrice.Text = "Полная цена:" + fullPrice.ToString();
        TotalPrice = fullPrice;
    }

    private async void BCreate_Clicked(object sender, EventArgs e)
    {
        var osago = BindingContext as Osago;
        osago.DriverId = MainDriver.Id;
        osago.DateStart = DateTime.Now.Date;
        osago.DateEnd = DateTime.Now.Date.AddYears(1);
        var erroMessage = "";
        var selectedCar = PCar.SelectedItem as Car;
        var selectsdRegion = PRegion.SelectedItem as Models.Region;
        var selectedCount = PAmountOfDrivers.SelectedItem as CountOfUser;
        if (selectedCar == null)
            erroMessage += "Выберите машину \n";
        if (selectsdRegion == null)
            erroMessage += "Выберите регион \n";
        if (selectedCount == null)
            erroMessage += "выберите количество возможных водителей \n";
        if (!string.IsNullOrWhiteSpace(erroMessage))
        {
            await Shell.Current.DisplayAlert("Ошибка", erroMessage, "Ok");
            return;
        }
        osago.RegionId = selectsdRegion.Id;
        osago.Price = TotalPrice;
        osago.CarId = selectedCar.Id;
        await NetManager.Post("Osagoes", osago);

        var user = osago.Car.Driver.User;
        GlobalSettings.MainUser = user;
        GlobalSettings.SaveUser();
        await Shell.Current.DisplayAlert("Оформление", "Успешно", "Ok");
        await Navigation.PopAsync();
    }

    private async void BBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void PRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        Refresh();
    }

    private void PAmountOfDrivers_SelectedIndexChanged(object sender, EventArgs e)
    {
        Refresh();
    }

    private void PCar_SelectedIndexChanged(object sender, EventArgs e)
    {
        Refresh();
    }
}