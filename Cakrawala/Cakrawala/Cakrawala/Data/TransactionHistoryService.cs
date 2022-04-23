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
    public class TransactionHistoryService
    {
        HttpClient client;

        public TransactionHistoryService()
        {
            client = new HttpClient();
        }

        public async Task<List<TopupHistory>> TopupHistoryAsync(string userId)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "topup/users/" + userId, string.Empty));
            List<TopupHistory> listTopupHistory = new List<TopupHistory>();

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string rawResp = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(rawResp);
                    listTopupHistory = JsonConvert.DeserializeObject<ResponseWrapper<List<TopupHistory>>>(rawResp).data;
                    Debug.WriteLine(listTopupHistory);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
                listTopupHistory[0].topupId = "error";
            }

            return listTopupHistory;
        }

        public async Task<List<TransferHistory>> TransferHistoryAsync(string userId)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "transactions/users/" + userId, string.Empty));
            List<TransferHistory> listTransferHistory = new List<TransferHistory>();

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Add("Authorization", $"Bearer {Application.Current.Properties["token"]}");
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string rawResp = await response.Content.ReadAsStringAsync();
                    listTransferHistory = JsonConvert.DeserializeObject<ResponseWrapper<List<TransferHistory>>>(rawResp).data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
                listTransferHistory[0].transactionId = "error";
            }

            return listTransferHistory;
        }
    }
}
