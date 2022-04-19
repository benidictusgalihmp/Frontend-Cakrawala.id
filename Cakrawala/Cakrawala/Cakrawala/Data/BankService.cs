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
    public class BankService
    {
        HttpClient client;

        public BankService()
        {
            client = new HttpClient();
        }

        public async Task<List<Bank>> GetBankList ()
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "banks/", string.Empty));
            List<Bank> bankList = new List<Bank>();

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
                    bankList = JsonConvert.DeserializeObject<ResponseWrapper<BankListResponse>>(rawResp).data.Banks;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
                return null;
            }

            return bankList;
        }

        private class BankListResponse
        {
            [JsonProperty(PropertyName = "banks")]
            public List<Bank> Banks { get; set; }
        }
    }
}
