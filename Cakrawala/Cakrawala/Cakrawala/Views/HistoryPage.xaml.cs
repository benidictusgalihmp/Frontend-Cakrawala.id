using Cakrawala.Models;
using MvvmHelpers;
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
        public ObservableRangeCollection<Grouping<string, TransactionHistory>> transHistoryGroup { get; set; } = new ObservableRangeCollection<Grouping<string, TransactionHistory>>();
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

            List<TopupHistory> listTopupHistory = await App.transactionHistoryService.TopupHistoryAsync(userId);

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
            var items = from transHistory in HistoryPage.transListView
                        orderby transHistory.headerDate
                        group transHistory by transHistory.headerDate into transGroup
                        select new Grouping<string, TransactionHistory>(transGroup.Key, transGroup);

            Debug.WriteLine("[LISTVIEW GROUPING]");
            int i = 0;
            foreach (var item in items)
            {
                Debug.WriteLine(item.Key);
                transHistoryGroup.Add(item);
                i++;
            }

            HistoryListView.ItemsSource = transHistoryGroup;
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

            todayDateAllTime.Text = todayDate.ToString("dd MMM yyyy");
        }

        private async void DetailHistoryPage_Tapped(object sender, ItemTappedEventArgs e)
        {
            string transaksiId = (e.Item as TransactionHistory).transactionId;
            Debug.WriteLine("[TRANSAKSI ID]");
            Debug.WriteLine(transaksiId);
            await Shell.Current.GoToAsync($"//detailHistory?historyId={transaksiId}");
        }

        public void changeHistoryTransactionsWeekAgo(object sender, EventArgs args)
        {
            var todayDate = DateTime.Today;

            // radio button week ago
            DateTime from = todayDate.AddDays(-7);
            DateTime to = todayDate;

            ObservableRangeCollection<Grouping<string, TransactionHistory>> list = new ObservableRangeCollection<Grouping<string, TransactionHistory>>();
            list = GetListTransactionbyDate(from, to);
            if(list.Count > 0)
            {
                HistoryListView.ItemsSource = GetListTransactionbyDate(from, to);
            } 
            else
            {
                Debug.WriteLine("Listview is null or empty");
            }

            Debug.WriteLine("Week ago");
        }

        public void changeHistoryTransactionsMonthAgo(object sender, EventArgs args)
        {
            var todayDate = DateTime.Today;

            DateTime from = todayDate.AddMonths(-1);
            DateTime to = todayDate;

            ObservableRangeCollection<Grouping<string, TransactionHistory>> list = new ObservableRangeCollection<Grouping<string, TransactionHistory>>();
            list = GetListTransactionbyDate(from, to);
            if (list.Count > 0)
            {
                HistoryListView.ItemsSource = GetListTransactionbyDate(from, to);
            }
            else
            {
                Debug.WriteLine("Listview is null or empty");
            }

            Debug.WriteLine("Month ago");
        }

        public void changeHistoryTransactionsAllTime(object sender, EventArgs args)
        {
            HistoryListView.ItemsSource = transHistoryGroup;
        }

        private ObservableRangeCollection<Grouping<string, TransactionHistory>> GetListTransactionbyDate(DateTime from, DateTime to)
        {
            List<TransactionHistory> list = new List<TransactionHistory>();
            ObservableRangeCollection<Grouping<string, TransactionHistory>> transHistory = new ObservableRangeCollection<Grouping<string, TransactionHistory>>();
            foreach (TransactionHistory transactionHistory in HistoryPage.transListView)
            {
                int dateFromState = DateTime.Compare(transactionHistory.createdDate, from);
                int dateToState = DateTime.Compare(transactionHistory.createdDate, to);

                if (dateFromState >= 0 &&
                    dateToState <= 0)
                {
                    list.Add(transactionHistory);
                }
            }

            var items = from transHistoryGet in list
                        orderby transHistoryGet.headerDate
                        group transHistoryGet by transHistoryGet.headerDate into transGroupGet
                        select new Grouping<string, TransactionHistory>(transGroupGet.Key, transGroupGet);

            int i = 0;
            foreach (var item in items)
            {
                Debug.WriteLine(item.Key);
                transHistory.Add(item);
                i++;
            }

            return transHistory;
        }
    }
}