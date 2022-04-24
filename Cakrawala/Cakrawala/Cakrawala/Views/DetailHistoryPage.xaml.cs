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
        private string HistoryId;
        private TransactionHistory transactionHistory;

        public DetailHistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RetrieveUserData();
        }

        public async void RetrieveUserData()
        {
            List<TransactionHistory> transListView = HistoryPage.transListView;
            Debug.WriteLine(transListView[0].transactionId);
            foreach (TransactionHistory history in transListView)
            {
                if (history.transactionId == HistoryId)
                {
                    this.transactionHistory = history;
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