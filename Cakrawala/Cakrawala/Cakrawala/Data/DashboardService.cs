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

        public async Task<DashboardResponse> DashboardAsync(string email, string password)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "users", string.Empty));
            DashboardResponse dashResp = new DashboardResponse();

            try
            {
                // string json = JsonConvert.SerializeObject(new TopupRequest(voucherCode));
                // StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string rawLogResp = await response.Content.ReadAsStringAsync();
                    dashResp = JsonConvert.DeserializeObject<DashboardResponse>(rawLogResp);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
                dashResp.userId = "error";
            }

            return dashResp;
        }

        public class DashboardResponse
        {
            public string userId { get; set; }
            public string name { get; set; }
            public int level { get; set; }
            public int exp { get; set; }
            public int credit { get; set; }

            public DashboardResponse()
            {
                this.userId = "";
                this.name = "";
                level = 0;
                exp = 0;
                credit = 0;
            }

            public DashboardResponse (string userId, string name, int level, int exp, int credit)
            {
                this.userId = userId;
                this.name = name;
                this.level = level;
                this.exp = exp;
                this.credit = credit;
            }
        }
    }
}
