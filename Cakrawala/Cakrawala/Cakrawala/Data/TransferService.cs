using Cakrawala.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Cakrawala.Data.DashboardService;

namespace Cakrawala.Data
{
    public class TransferService
    {
        HttpClient client;

        public TransferService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TransferResponse> TransferAsync(uint senderId, uint receiverId, uint nominal)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "transactions", string.Empty));
            TransferResponse output = null;
            try
            {
                string json = JsonConvert.SerializeObject(new TransferRequest(senderId, receiverId, nominal));
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Content = content;
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");
                HttpResponseMessage response = await client.SendAsync(request);

                // HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Success Transfer");
                    string rawResp = await response.Content.ReadAsStringAsync();
                    output = JsonConvert.DeserializeObject<ResponseWrapper<TransferResponse>>(rawResp).data;
                }

                Debug.WriteLine(response.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return output;
        }

        public async Task<User> FindUserAsync(string username)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + $"users/check/{username}", string.Empty));
            User userResp = null;

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
                    userResp = JsonConvert.DeserializeObject<ResponseWrapper<User>>(rawResp).data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
                return null;
            }

            return userResp;
        }



        // BELOW IS CLASSES

        private class TransferRequest
        {
            [JsonProperty(PropertyName = "FromUserId")]
            public uint senderId { get; set; }

            [JsonProperty(PropertyName = "ToUserId")]
            public uint receiverId { get; set; }

            [JsonProperty(PropertyName = "amount")]
            public uint nominal { get; set; }

            public TransferRequest(uint _senderId, uint _receiverId, uint _nominal)
            {
                senderId= _senderId;
                receiverId = _receiverId;
                nominal = _nominal;
            }
        }

        public class TransferResponse
        {
            public class UserDTO
            {
                [JsonProperty(PropertyName = "id")]
                public uint Id { get; set; }

                [JsonProperty(PropertyName = "username")]
                public string UserName { get; set; } = string.Empty;

                [JsonProperty(PropertyName = "email")]
                public string Email { get; set; } = string.Empty;

                [JsonProperty(PropertyName = "previous_balance")]
                public uint PreviousBalance { get; set; }

                [JsonProperty(PropertyName = "current_balance")]
                public uint CurrentBalance { get; set; }
            }

            [JsonProperty(PropertyName = "id")]
            public uint Id { get; set; }

            [JsonProperty(PropertyName = "created_at")]
            public string CreatedAt { get; set; }

            [JsonProperty(PropertyName = "updated_at")]
            public string UpdatedAt { get; set; }

            [JsonProperty(PropertyName = "from")]
            public UserDTO From { get; set; }

            [JsonProperty(PropertyName = "to")]
            public UserDTO To { get; set; }

            [JsonProperty(PropertyName = "amount")]
            public int Amount { get; set; }

            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }
        }
    }
}
