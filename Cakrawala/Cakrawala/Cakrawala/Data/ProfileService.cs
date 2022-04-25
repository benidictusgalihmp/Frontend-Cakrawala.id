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
                    viewProfileResp = JsonConvert.DeserializeObject<ResponseWrapper<User>>(rawResp).data;
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

            bool output = false;

            try
            {
                string json = JsonConvert.SerializeObject(new UpdateUsernameFormat(newUsername));
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                Debug.WriteLine(json);

                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), uri);
                request.Content = content;
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");
                HttpResponseMessage response = await client.SendAsync(request);

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

        public async Task<int> UpdatePasswordAsync(
            string userId,
            string oldUpdatePassword,
            string newUpdatePassword)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "users/" + userId, string.Empty));
            int output = 0;

            try
            {
                string json = JsonConvert.SerializeObject(new UpdatePasswordFormat(newUpdatePassword, oldUpdatePassword));
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), uri);
                request.Content = content;
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");
                HttpResponseMessage response = await client.SendAsync(request);

                Debug.WriteLine(json);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Success Change Password");
                    output = 1;
                } else
                {
                    string responseResult = response.Content.ReadAsStringAsync().Result;
                    bool oldPasswordCorrect = responseResult.Contains("Incorrect old password");

                    Debug.WriteLine("[RESPONSES]");
                    Debug.WriteLine(oldPasswordCorrect);
                    Debug.WriteLine("Gagal mengubah password");
                    output = oldPasswordCorrect ? 2 : 0;

                    return output;
                }

                Debug.WriteLine(response.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
            }
            return output;
        }

        private class UpdateUsernameFormat
        {
            [JsonProperty(PropertyName = "new_display_name")]
            public string newUsername { get; set; }

            public UpdateUsernameFormat(
                string newUsername)
            {
                this.newUsername = newUsername;
            }
        }

        private class UpdatePasswordFormat
        {
            [JsonProperty(PropertyName = "new_password")]
            public string newUpdatePassword { get; set; }

            [JsonProperty(PropertyName = "old_password")]
            public string oldUpdatePassword { get; set; }

            public UpdatePasswordFormat(
                string newUpdatePassword,
                string oldUpdatePassword)
            {
                this.newUpdatePassword = newUpdatePassword;
                this.oldUpdatePassword = oldUpdatePassword;
            }
        }
      
    }
}
