using InsurenceCar.Models;
using System.Text.RegularExpressions;

namespace InsurenceCar.Pages.ClientPages;

public partial class AddCarClientPage : ContentPage
{
    private Model currentModel = new Model();
	public AddCarClientPage()
	{
		InitializeComponent();
		InitPage();
	}

	private async void InitPage()
	{
        BindingContext = new Car();
		PBrend.ItemsSource = await NetManager.Get<List<Brand>>("Brands");
		Refresh();
	}

	private async void Refresh()
	{
		await DBConnection.RefreshEnums();
		await DBConnection.RefreshData();
		var models = await NetManager.Get<List<Model>>("Models");
		var brand = PBrend.SelectedItem as Brand;
		if (brand == null)
		{
			PModel.ItemsSource = null;
			return;
		}
		PModel.ItemsSource = models.Where(x => x.Make == brand.Name).ToList();
		
	}

    private void PBrend_SelectedIndexChanged(object sender, EventArgs e)
    {
		Refresh();
    }

    private void PModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //currentModel = (Model)PBrend.SelectedItem;
    }

    private async void BAdd_Clicked(object sender, EventArgs e)
    {
        await DBConnection.RefreshEnums();
        await DBConnection.RefreshData();
        var cars = await NetManager.Get<List<Car>>("Cars");
        var models = await NetManager.Get<List<Model>>("Models");
		var car = BindingContext as Car;
        if (car == null)
        {
            return;
        }
        var selectedModel = PModel.SelectedItem as Model;
        if (selectedModel == null)
        {
            await Shell.Current.DisplayAlert("������", "�������� ������", "��");
            return;
        }
        car.Driver = GlobalSettings.MainUser.Driver.First();
        car.DateOfPurchase = DateTime.Now;
        var model = models.FirstOrDefault(x => x.Id == selectedModel.Id);
        car.Model = model;
        var errorMessage = "";
        if (car.VIN == null)
            errorMessage += "������� ��� ����� \n";
        if (car.Number == null)
            errorMessage += "������� ����� \n";
        if (car.Horsepower.ToString() == null)
            errorMessage += "������� �������� \n";
        if (car.Passport == null)
            errorMessage += "������� ������� �� \n";
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            await Shell.Current.DisplayAlert("������", errorMessage, "��");
            return;
        }
        var checkNum = cars.FirstOrDefault(u => u.Number == car.Number);
        if (checkNum != null)
            errorMessage += "������ ����� ��� ��� ��������������� \n";
        string patternForNumber = @"^[������������]\d{3}(?<!000)[������������]{2}\d{2,3}$";
        if (!Regex.IsMatch(car.Number, patternForNumber) && car.Number != null)
            errorMessage += "������� ���������� ����� ������ \n";
        string patternForPassport = @"^[0-9]{2}[A-Z]{2}[0-9]{6}$";
        if (!Regex.IsMatch(car.Passport, patternForPassport) && car.Passport != null)
            errorMessage += "������� ���������� ������� �� \n";
        if (car.Horsepower <= 0)
            errorMessage += "������� �������� \n";
        string patternForVINNumber = @"^[A-Z0-9]{10}$";
        if (!Regex.IsMatch(car.VIN, patternForVINNumber) && car.VIN.Length != 17)
            errorMessage += "������� ���������� VIN ����� \n";
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            await Shell.Current.DisplayAlert("������", errorMessage, "��");
            return;
        }

        await NetManager.Post("Cars", car);
        var user = car.Driver.User;
        GlobalSettings.MainUser = user;
        GlobalSettings.SaveUser();
        await Navigation.PopAsync();

    }
}