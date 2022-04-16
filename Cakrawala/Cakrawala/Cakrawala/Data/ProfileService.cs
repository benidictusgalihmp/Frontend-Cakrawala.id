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

        public async Task<bool> UpdateUsernameAsync(
            string userId,
            string newUsername)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "users/" + userId, string.Empty));
            Task<User> user = ViewProfileAsync(userId);
            bool output = false;

            String oldUpdatePassword = user.password;
            String newUpdatePassword = oldUpdatePassword;

            try
            {
                string json = JsonConvert.SerializeObject(new UpdateProfileFormat(newUsername, newUpdatePassword, oldUpdatePassword));
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success Change Username");
                    output = true;
                }

                Debug.WriteLine(response.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
            }
            return output;
        }

        public async Task<bool> UpdatePasswordAsync(
            string userId,
            string newUpdatePassword)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "users/" + userId, string.Empty));
            Task<User> user = ViewProfileAsync(userId);
            bool output = false;

            String newUsername = user.userName;
            String oldUpdatePassword = user.password;

            try
            {
                string json = JsonConvert.SerializeObject(new UpdateProfileFormat(newUsername, newUpdatePassword, oldUpdatePassword));
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success Change Password");
                    output = true;
                }

                Debug.WriteLine(response.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
            }
            return output;
        }

        private class UpdateProfileFormat
        {
            [JsonProperty(PropertyName = "newUsername")]
            public string newUsername { get; set; }

            [JsonProperty(PropertyName = "newUpdatePassword")]
            public string newUpdatePassword { get; set; }

            [JsonProperty(PropertyName = "oldUpdatePassword")]
            public string oldUpdatePassword { get; set; }

            public UpdateProfileFormat(
                string newUsername, 
                string newUpdatePassword, 
                string oldUpdatePassword)
            {
                this.newUsername = newUsername;
                this.newUpdatePassword = newUpdatePassword;
                this.oldUpdatePassword = oldUpdatePassword;
            }
        }
    }
}
