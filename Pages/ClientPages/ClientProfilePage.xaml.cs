using InsurenceCar.Models;
using System.Text.RegularExpressions;

namespace InsurenceCar.Pages.ClientPages;

public partial class ClientProfilePage : ContentPage
{
    public bool IsSave;
    public bool IsAdd;
    public ClientProfilePage()
    {
        InitializeComponent();
        var driver = new Driver();
        IsSave = false;
        var mainUser = GlobalSettings.MainUser;
        var dateNow = DateTime.Now.Date;
        DPExperience.MaximumDate = dateNow;
        DPBirthDate.MaximumDate = dateNow.AddYears(-18);
        if (mainUser.Driver.Count != 0)
        {
            driver = mainUser.Driver.First();
            IsAdd = false;
            DPBirthDate.IsEnabled = false;
            DPExperience.IsEnabled = false;
            //BSave.IsEnabled = false;
            //BSave.IsVisible = false;
        }
        else
        {
            BSave.IsEnabled = true;
            BSave.IsVisible = true;
            DPExperience.IsEnabled = false;
            driver.Experience = dateNow;
            driver.DateOfBirth = dateNow.AddYears(-18);
            IsAdd = true;
        }
        BindingContext = driver;
    }

    public void Refresh()
    {
        var selectedDateBirth = DPBirthDate.Date;
        if (selectedDateBirth != null)
        {
            DPExperience.MinimumDate = selectedDateBirth.AddYears(18);
            DPExperience.IsEnabled = true;
            DPExperience.Date = selectedDateBirth.AddYears(18);
        }
    }

    private async void BSave_Clicked(object sender, EventArgs e)
    {
        await DBConnection.RefreshData();
        await DBConnection.RefreshEnums();

        var drivers = await NetManager.Get<List<Driver>>("Drivers");


        var driver = BindingContext as Driver;
        if (driver == null)
        {
            return;
        }
        driver.UserId = GlobalSettings.MainUser.Id;
        var errorMessage = "";
        if (driver.Experience == null)
            errorMessage += "Выберите дату получения водительского удостоверения \n";
        if (driver.DateOfBirth == null)
            errorMessage += "Выберите дату рождения \n";
        if (driver.DriverLicense == null)
            errorMessage += "Введите водительское удостоверение\n";
        if (driver.Surname == null)
            errorMessage += "Введите фамилию\n";
        if (driver.Name == null)
            errorMessage += "Введите имя\n";
        if (driver.Patronic == null)
            errorMessage += "Введите отчество\n";
        if (driver.PassportData == null)
            errorMessage += "Введите Паспортные данные\n";
        if (driver.Email == null)
            errorMessage += "Введите Email\n";
        if (driver.Phone == null)
            errorMessage += "Введите номер телефона \n";
        string patternForPhone = @"^[0-9]{10}$";
        if (driver.Phone != null)
        {
            if (!Regex.IsMatch(driver.Phone, patternForPhone))
                errorMessage += "Введите корректный номер телефона \n";
        }
        string patternForPassportData = @"^[0-9]{10}$";
        if (driver.PassportData != null)
        {
            if (!Regex.IsMatch(driver.PassportData, patternForPassportData))
                errorMessage += "Введите корректные данные паспорта \n";
        }
        var patternForEmail = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        if (driver.Email != null)
        {
            if (!Regex.IsMatch(driver.Email, patternForEmail))
                errorMessage += "Введите корректный Email \n";
        }
        if (driver.DriverLicense != null)
        {
            string patternForLicense = @"[0-9]{10}$";
            if (!Regex.IsMatch(driver.DriverLicense, patternForLicense))
                errorMessage += "Введите корректное водительское удостоверение \n";
        }
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            await Shell.Current.DisplayAlert("Ошибка", errorMessage, "Ок");
            return;
        }
        if (IsAdd)
        {
            driver.BlackList = false;
            var messageAboutError = "";
            var driverLicense = drivers.FirstOrDefault(u => u.DriverLicense == driver.DriverLicense);
            if (driverLicense != null)
                messageAboutError += "Это водительское удостоверение уже было занято \n";
            var passport = drivers.FirstOrDefault(u => u.PassportData == driver.PassportData);
            if (passport != null)
                messageAboutError += "Эти паспортные данные уже были заняты \n";
            if (!string.IsNullOrWhiteSpace(messageAboutError))
            {
                await Shell.Current.DisplayAlert("Ошибка", messageAboutError, "Ок");
                return;
            }
            await NetManager.Post("Drivers", driver);
        }
        else
        {
            var driverId = driver.Id;
            await NetManager.Put($"Drivers/{driverId}", driver);
        }
        IsSave = true;
        await Shell.Current.DisplayAlert("Успешно", "Все успешно сохранено", "Ok");
        var user = await NetManager.Get<User>($"Users/{driver.UserId}");
        GlobalSettings.MainUser = user;
        GlobalSettings.SaveUser();
        BindingContext = new Driver();
        await Navigation.PopAsync();
    }

    private async void BBAck_Clicked(object sender, EventArgs e)
    {
        BindingContext = new Driver();
        await Navigation.PopAsync();
    }

    private void DPExperience_DateSelected(object sender, DateChangedEventArgs e)
    {
        Refresh();
    }

    private void DPBirthDate_DateSelected(object sender, DateChangedEventArgs e)
    {
        Refresh();
    }
}