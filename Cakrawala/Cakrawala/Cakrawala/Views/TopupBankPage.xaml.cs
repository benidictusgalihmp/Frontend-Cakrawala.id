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
    public partial class TopupBankPage : ContentPage
    {
        public TopupBankPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }

        private async void Init()
        {
            BankListView.ItemsSource = await App.bankService.GetBankList();
        }

        private async void VoucherButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//topupvoucher");
        }

        private async void BankButton_Clicked(object sender, EventArgs e)
        {
            string bankName = (sender as Button).Text;
            Debug.WriteLine("Hello" + bankName);
            await Shell.Current.GoToAsync("//topupbankrequest?bank=" + bankName);
        }

        private async void BankListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string bankName = (e.Item as Bank).Name;
            int bankId = (e.Item as Bank).Id;
            Debug.WriteLine("Hello" + bankName);
            await Shell.Current.GoToAsync($"//topupbankrequest?bank={bankName}&bankId={bankId}");
        }
    }
}