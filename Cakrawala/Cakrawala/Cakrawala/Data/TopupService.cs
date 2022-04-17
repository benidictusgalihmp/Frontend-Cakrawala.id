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
    public class TopupService
    {
        HttpClient client;

        public TopupService()
        {
            client = new HttpClient();
        }

        public async Task<VoucherTopupResponse> TopupVoucherAsync(uint userId, string voucherCode)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + $"topup/users/{userId}/voucher/use", string.Empty));
            VoucherTopupResponse output = null;
            try
            {
                string json = JsonConvert.SerializeObject(new TopupRequest(voucherCode));
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Content = content;
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Success Topup");
                    string rawResp = await response.Content.ReadAsStringAsync();
                    output = JsonConvert.DeserializeObject<ResponseWrapper<VoucherTopupResponse>>(rawResp).data;
                }

                Debug.WriteLine(response.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }

            return output;
        }


        private class TopupRequest
        {
            [JsonProperty(PropertyName = "voucher_code")]
            public string voucherCode { get; set; }

            public TopupRequest(string _voucherCode)
            {
                voucherCode = _voucherCode;
            }
        }

        public class VoucherTopupResponse
        {
            [JsonProperty(PropertyName = "amount")]
            public uint Amount { get; set; }
        }
    }
}
