using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Cakrawala.Data.TopupService;

namespace Cakrawala.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Bank), "bank")]
    [QueryProperty(nameof(BankId), "bankId")]
    public partial class TopupBankRequestPage : ContentPage
    {
        private uint bankId;
        public uint BankId
        {
            get { return bankId; }
            set { bankId = value; Debug.WriteLine(value); }
        }

        private string bank;
        public string Bank
        {
            get { return bank; }
            set { LoadBank(value); }
        }

        public TopupBankRequestPage()
        {
            InitializeComponent();
        }

        void LoadBank(string value)
        {
            bank = value;
            this.bankLabel.Text = value;
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//topupbank");
        }

        private async void RequestButton_Clicked(object sender, EventArgs e)
        {
            uint userId = UInt32.Parse(Application.Current.Properties["userId"].ToString());
            uint amount = uint.Parse(this.nominal.Text);
            TopupBankResponse response = await App.topupService.TopupBankAsync(userId, amount, BankId);
            if (response == null)
            {
                await DisplayAlert("Error", "Cannot complete topup request", "Ok");
                return;
            }
            uint bankNumber = response.AccountNumber;
            string timeLimit = response.ExpirationDate.ToString();
            string nominal = this.nominal.Text;
            await Shell.Current.GoToAsync($"//topupbankstep?bankNumber={bankNumber}&timeLimit={timeLimit}&nominal={nominal}&bank={bank}");
        }
    }
}