using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurenceCar.Models
{
    public static class DataManager
    {

        public static string LoggedUserPath { get => Path.Combine(FileSystem.Current.AppDataDirectory, "LoggedUser.txt"); }

        private static T GetData<T>(string fileName)
        {
            var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
            return data;
        }

        private static void SetData<T>(string fileName, T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(fileName, jsonData);
        }

        public static User GetLoggedUser()
        {
            var loggedUser = GetData<User>(LoggedUserPath);
            return loggedUser;
        }

        public static void SaveLoggedUser(User user)
        {
            SetData(LoggedUserPath, user);
        }

        public static async void InitDataFile(string outputFileName, string sourceFileName)
        {
            if (!File.Exists(outputFileName))
            {
                var file = File.Create(outputFileName);
                file.Close();
                if (!string.IsNullOrEmpty(sourceFileName))
                    File.WriteAllText(outputFileName, await ReadAsset(sourceFileName));
            }
        }

        private static async Task<string> ReadAsset(string assetPath)
        {
            using (var sourceStream = await FileSystem.OpenAppPackageFileAsync(assetPath))
            {
                using (var reader = new StreamReader(sourceStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }


    }
}
