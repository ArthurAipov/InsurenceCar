using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurenceCar.Models
{
    public class DBConnection
    {
        public static List<User> Users { get; set; }
        public static List<Role> Roles { get; set; }
        public static List<Status> Statuses { get; set; }
        public static List<EmergencyApplication> EmergencyApplications { get; set; }
        public static List<Region> Regions { get; set; }
        public static List<Model> Models { get; set; }
        public static List<Driver> Drivers { get; set; }
        public static List<Casco> Cascos { get; set; }
        public static List<Osago> Osagos { get; set; }
        public static List<PhotoEmergency> PhotoEmergencies { get; set; }
        public static List<Car> Cars { get; set; }


        public static async Task InitializeDBConnection()
        {
            await RefreshEnums();
            await RefreshData();
        }
        public static async Task RefreshData()
        {
            Users = await NetManager.Get<List<User>>("Users");
            EmergencyApplications = await NetManager.Get<List<EmergencyApplication>>("EmergencyApplications");
            Models = await NetManager.Get<List<Model>>("Models");
            Drivers = await NetManager.Get<List<Driver>>("Drivers");
            Cars = await NetManager.Get<List<Car>>("Cars");
            Cascos = await NetManager.Get<List<Casco>>("Cascoes");
            Osagos = await NetManager.Get<List<Osago>>("Osagoes");
        }
        public static async Task RefreshEnums()
        {
            Roles = await NetManager.Get<List<Role>>("Roles");
            Statuses = await NetManager.Get<List<Status>>("Status");
            Regions = await NetManager.Get<List<Region>>("Regions");
            PhotoEmergencies = await NetManager.Get<List<PhotoEmergency>>("PhotoEmergencies");
        }

    }
}
