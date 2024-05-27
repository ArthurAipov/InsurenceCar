using InsurenceCar.Models;

namespace InsurenceCar
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            DataManager.InitDataFile(DataManager.LoggedUserPath, string.Empty);
            


        }
    }
}
