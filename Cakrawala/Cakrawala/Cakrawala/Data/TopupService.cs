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

        public async Task<bool> TopupAsync(string voucherCode)
        {
            Uri uri = new Uri(string.Format(Constants.LocalRestUrl + "topup", string.Empty));
            bool output = false;
            try
            {
                string json = JsonConvert.SerializeObject(new TopupRequest(voucherCode));
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Content = content;
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");
                HttpResponseMessage response = await client.SendAsync(request);

                // HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Success Topup");
                    output = true;
                }

                Debug.WriteLine(response.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return output;
        }

        private class TopupRequest
        {
            public string voucherCode { get; set; }

            public TopupRequest(string _voucherCode)
            {
                voucherCode = _voucherCode;
            }
        }
    }
}
