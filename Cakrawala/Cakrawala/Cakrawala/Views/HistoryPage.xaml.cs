using Cakrawala.Models;
using System;
using System.Collections.Generic;
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
        public HistoryPage()
        {
            InitializeComponent();

            // ##### TESTING ONLY ###################
            User userA = new User(
                "0", 
                "userA", 
                "12345678",
                "userA",
                "userA@example.com",
                1000000, 
                50, 
                "GOLD", 
                0
            );
            User userB = new User(
                "0",
                "userB",
                "12345678",
                "userB",
                "userB@example.com",
                5000000,
                50,
                "SILVER",
                0
            );

            String payinuser = "payinuser.png";
            String payoutuser = "payoutuser.png";
            String payoutshop = "payoutshop.png";
            String topup = "topup.png";

            DateTime createdDate = new DateTime(2018, 6, 21);
            DateTime updatedAt = new DateTime(2020, 3, 5);


            List<TransactionHistory> transListView = new List<TransactionHistory>
            {
                new TransactionHistory(
                    0,
                    TransactionType.ISIULANG,
                    "Isi ulang",
                    topup,
                    createdDate,
                    updatedAt,
                    userA,
                    100000,
                    "BANK BCA",
                    TransferStatus.PENDING),
                new TransactionHistory(
                    1,
                    TransactionType.BAYARUSER,
                    "Pembayaran",
                    payoutuser,
                    createdDate,
                    updatedAt,
                    userA,
                    userB,
                    5000,
                    TransferStatus.PENDING)
            };
            // #######################################

            HistoryListView.ItemsSource = transListView;

        }

        private async void DetailHistoryPage_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//detailHistory");
        }
    }
}