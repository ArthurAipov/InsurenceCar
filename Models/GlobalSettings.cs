using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurenceCar.Models
{
    public static class GlobalSettings
    {
        public static User MainUser;
        public static List<Car> CarsOfUser;

        public static async Task<List<Car>> GetCars()
        {
            await DBConnection.RefreshEnums();
            await DBConnection.RefreshData();
            var user = GlobalSettings.MainUser;
            var driver = (await NetManager.Get<List<Driver>>("Drivers")).FirstOrDefault(x => x.UserId == user.Id);
            var cars = (await NetManager.Get<List<Car>>("Cars")).Where(x => x.DriverId == driver.Id).ToList();
            return cars;
        }

        public static void SaveUser()
        {
            DataManager.SaveLoggedUser(MainUser);
        } 

        public static void GetUser()
        {
            MainUser = DataManager.GetLoggedUser();
        }

    }
}
