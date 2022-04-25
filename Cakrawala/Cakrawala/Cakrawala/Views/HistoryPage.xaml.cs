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
    public partial class HistoryPage : ContentPage
    {
        public static List<TransactionHistory> transListView { get; set; }
        public int lenTransListView;

        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RetrieveUserData();
            resetState();
        }

        public async void RetrieveUserData()
        {
            string userId = Application.Current.Properties["userId"].ToString();

            List<TransferHistory> listTransferHistory = await App.transactionHistoryService.TransferHistoryAsync(userId);
            // Debug.WriteLine("[TRANSFER HISTORY]", listTransferHistory[0].transactionId);

            List<TopupHistory> listTopupHistory = await App.transactionHistoryService.TopupHistoryAsync(userId);
            // Debug.WriteLine("[TOPUP HISTORY]", listTopupHistory);

            // renew data list transaction from backend
            HistoryPage.transListView = new List<TransactionHistory>();
            int idTrans = 0;

            foreach (TransferHistory transferHistory in listTransferHistory)
            {
                TransactionType transactionType = TransactionType.TERIMAUSER;
                string transactionNote = "Diterima dari User";
                string urlImage = "payinuser.png";
                string idFrom = transferHistory.from;
                string idTo = transferHistory.to;

                if (idFrom == userId)
                {
                    transactionType = TransactionType.BAYARUSER;
                    transactionNote = "Dibayar ke User";
                    urlImage = "payoutuser.png";
                }

                User userFrom = await App.profileService.ViewProfileAsync(idFrom);
                User userTo = await App.profileService.ViewProfileAsync(idTo);

                HistoryPage.transListView.Add(new TransactionHistory(
                    (idTrans).ToString(),
                    transactionType,
                    transactionNote,
                    urlImage,
                    transferHistory.createdAt.ToString("ddd, dd MMM yyyy"),
                    transferHistory.createdAt,
                    transferHistory.updatedAt,
                    userFrom,
                    userTo,
                    transferHistory.baseValue,
                    transferHistory.status));
                idTrans += 1;
            }

            foreach (TopupHistory topupHistory in listTopupHistory)
            {
                TransactionType transactionType = TransactionType.ISIULANG;
                string transactionNote = "Isi Ulang Saldo";
                string urlImage = "topup.png";

                HistoryPage.transListView.Add(new TransactionHistory(
                    (idTrans).ToString(),
                    transactionType,
                    transactionNote,
                    urlImage,
                    topupHistory.createdAt.ToString("ddd, dd MMM yyyy"),
                    topupHistory.createdAt,
                    topupHistory.updatedAt,
                    topupHistory.value,
                    topupHistory.method,
                    topupHistory.status));
                idTrans += 1;
            }
            HistoryListView.ItemsSource = HistoryPage.transListView;
        }

        private void resetState()
        {
            var todayDate = DateTime.Today;

            // radio button week ago
            todayDateWeek.Text = todayDate.ToString("dd MMM yyyy");
            oneWeekDate.Text = todayDate.AddDays(-7).ToString("dd MMM yyyy");

            // radio button month ago
            todayDateMonth.Text = todayDate.ToString("dd MMM yyyy");
            oneMonthDate.Text = todayDate.AddMonths(-1).ToString("dd MMM yyyy");

            // radio button custom date
            DatePickerFrom.Date = todayDate;
            DatePickerFrom.MinimumDate = todayDate.AddMonths(-6).Date;
            DatePickerFrom.MaximumDate = todayDate.Date;

            DatePickerTo.Date = todayDate;
            DatePickerTo.MinimumDate = todayDate.AddMonths(-6).Date;
            DatePickerTo.MaximumDate = todayDate.Date;
        }

        private async void DetailHistoryPage_Tapped(object sender, ItemTappedEventArgs e)
        {
            string transaksiId = (e.Item as TransactionHistory).transactionId;
            Debug.WriteLine("[TRANSAKSI ID]");
            Debug.WriteLine(transaksiId);
            await Shell.Current.GoToAsync($"//detailHistory?historyId={transaksiId}");
        }
    }
}