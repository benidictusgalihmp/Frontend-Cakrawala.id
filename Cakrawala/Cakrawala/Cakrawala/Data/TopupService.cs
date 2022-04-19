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
            Uri uri = new Uri(string.Format(Constants.RestUrl + $"topup/voucher", string.Empty));
            VoucherTopupResponse output = null;
            try
            {
                string json = JsonConvert.SerializeObject(new TopupVoucherRequest(userId, voucherCode));
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

        public async Task<TopupBankResponse> TopupBankAsync(uint userId, uint amount, uint bankId)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + $"topup/bank", string.Empty));
            TopupBankResponse output = null;
            try
            {
                string json = JsonConvert.SerializeObject(new TopupBankRequest(userId, amount, bankId));
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Content = content;
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Success Topup");
                    string rawResp = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(rawResp);
                    output = JsonConvert.DeserializeObject<ResponseWrapper<TopupBankResponse>>(rawResp).data;
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


        private class TopupVoucherRequest
        {
            [JsonProperty(PropertyName = "user_id")]
            public uint userId { get; set; }

            [JsonProperty(PropertyName = "voucher_code")]
            public string voucherCode { get; set; }

            public TopupVoucherRequest(uint _userId, string _voucherCode)
            {
                userId = _userId;
                voucherCode = _voucherCode;
            }
        }

        public class VoucherTopupResponse
        {
            [JsonProperty(PropertyName = "amount")]
            public uint Amount { get; set; }
        }

        private class TopupBankRequest
        {
            [JsonProperty(PropertyName = "user_id")]
            public uint userId { get; set; }

            [JsonProperty(PropertyName = "amount")]
            public uint amount { get; set; }

            [JsonProperty(PropertyName = "bank_id")]
            public uint bankId { get; set; }

            public TopupBankRequest(uint _userId, uint _amount, uint _bankId)
            {
                userId = _userId;
                amount = _amount;
                bankId = _bankId;
            }
        }

        public class TopupBankResponse
        {
            [JsonProperty(PropertyName = "account_number")]
            public uint AccountNumber { get; set; }

            [JsonProperty(PropertyName = "expired_date")]
            // [JsonConverter(typeof(DateTime))]
            public DateTime ExpirationDate { get; set; }
        }
    }
}
