using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InsurenceCar.Models
{
    public class NetManager
    {
        public static string URL = "http://arthuraipov-001-site1.atempurl.com/api/";
        public static HttpClient HttpClient = new HttpClient();

        static NetManager()
        {
            HttpClient = new HttpClient(new HttpClientHandler()
            {
                Credentials = new NetworkCredential("11177875", "60-dayfreetrial")
            });
        }

        public async static Task<T> Get<T>(string controller)
        {
            var response = await HttpClient.GetAsync(URL + controller);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }
        public async static Task<HttpResponseMessage> Post<T>(string controller, T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var response = await HttpClient.PostAsync(URL + controller, new StringContent(jsonData, Encoding.UTF8, "application/json"));
            return response;
        }
        public async static Task<HttpResponseMessage> Put<T>(string controller, T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var response = await HttpClient.PutAsync(URL + controller, new StringContent(jsonData, Encoding.UTF8, "application/json"));
            return response;
        }
        public async static Task<HttpResponseMessage> Delete<T>(string controller)
        {
            var response = await HttpClient.DeleteAsync(URL + controller);
            return response;
        }
    }
}