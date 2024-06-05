using InsurenceCar.Models;

namespace InsurenceCar.Pages.ClientPages;

public partial class ClientIssuePage : ContentPage
{
	public ClientIssuePage()
	{
		InitializeComponent();
		InitPage();
	}

	private async void InitPage()
	{
		await DBConnection.RefreshData();
		await DBConnection.RefreshEnums();
		var osagos = await NetManager.Get<List<Osago>>("Osagoes");
		var cars = (osagos.Where(x => x.DriverId == GlobalSettings.MainUser.Driver.First().Id).ToList()).Select(x => x.Car).ToList();
		PCars.ItemsSource = cars;
		BindingContext = new EmergencyApplication();
	}

    private async void BIssue_Clicked(object sender, EventArgs e)
    {
		var issue = BindingContext as EmergencyApplication;
		if (issue == null)
		{
			return;
		}
		issue.DateEmergency = DateTime.Now.Date;
		issue.StatusId = 3;
		issue.DriverId = GlobalSettings.MainUser.Driver.First().Id;
		if(issue.Car == null)
		{
			await Shell.Current.DisplayAlert("Ошибка", "Вы не выбрали машину", "Ок");
			return;
		}
		issue.ApplicationType = 1;
		issue.Price = 0;

		await NetManager.Post("EmergencyApplications", issue);
		var user = issue.Driver.User;
		GlobalSettings.MainUser = user;
		GlobalSettings.SaveUser();
        await Shell.Current.DisplayAlert("Успешно", "Заявка оформлена", "Ок");
        await Navigation.PopAsync();



    }
}