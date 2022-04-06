using Cakrawala.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cakrawala.Data
{
    public class ProfileService
    {
        HttpClient client;

        public ProfileService()
        {
            client = new HttpClient();
        }

        public async Task<User> ViewProfileAsync(string userId)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "users/" + userId, string.Empty));
            User viewProfileResp = new User();

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string rawResp = await response.Content.ReadAsStringAsync();
                    viewProfileResp = JsonConvert.DeserializeObject<User>(rawResp);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
                viewProfileResp.userId = "error";
            }

            return viewProfileResp;
        }
    }
}
