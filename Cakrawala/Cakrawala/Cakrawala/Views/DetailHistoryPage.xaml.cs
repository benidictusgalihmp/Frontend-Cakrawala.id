using Cakrawala.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cakrawala.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(HistoryId), "historyId")]
    public partial class DetailHistoryPage : ContentPage
    {
        private string historyId;
        private TransactionHistory transactionHistory;

        public string HistoryId
        {
            get { return historyId; }
            set { historyId = value; 
                Debug.WriteLine("[SETTER]");
                Debug.WriteLine(HistoryId);
                RetrieveUserData();
            }
        }

        public DetailHistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("[BEFORE]");
            Debug.WriteLine(HistoryId);
            // RetrieveUserData();
            Debug.WriteLine("[AFTER]");
            Debug.WriteLine(HistoryId);
        }

        public void RetrieveUserData()
        {
            List<TransactionHistory> transListView = HistoryPage.transListView;
            foreach (TransactionHistory history in transListView)
            {
                Debug.WriteLine("[HISTORY TRANSACTION ID]");
                Debug.WriteLine(history.transactionId);
                Debug.WriteLine("[PAGE TRANSACTION ID]");
                Debug.WriteLine(HistoryId);
                if (history.transactionId == HistoryId)
                {
                    this.transactionHistory = new TransactionHistory(history);
                }
            }

            totalHeader.Text = this.transactionHistory.amount.ToString();
            detailHeader.Text = this.transactionHistory.transactionNote;
            timeHour.Text = this.transactionHistory.createdDate.ToString("hh:mm");
            timeDate.Text = this.transactionHistory.createdDate.ToString("dd MMM yyyy");
            idTransaction.Text = this.transactionHistory.transactionId;
            amount.Text = this.transactionHistory.amount.ToString();
            totalAmount.Text = this.transactionHistory.amount.ToString();
        }

        private async void BackTaptoRiwayat(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//history");
        }
    }
}