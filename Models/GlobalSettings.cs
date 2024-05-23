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
