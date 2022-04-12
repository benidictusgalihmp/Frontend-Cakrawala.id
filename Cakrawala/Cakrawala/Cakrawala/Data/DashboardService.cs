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
    public class DashboardService
    {
        HttpClient client;
        
        public DashboardService()
        {
            client = new HttpClient();
        }

        public async Task<User> DashboardAsync(string userId)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "users/" + userId, string.Empty));
            User dashResp = null;

            try
            {
                // string json = JsonConvert.SerializeObject(new TopupRequest(voucherCode));
                // StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string rawResp = await response.Content.ReadAsStringAsync();
                    dashResp = JsonConvert.DeserializeObject<User>(rawResp);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
                return null;
            }

            return dashResp;
        }

    }
}
