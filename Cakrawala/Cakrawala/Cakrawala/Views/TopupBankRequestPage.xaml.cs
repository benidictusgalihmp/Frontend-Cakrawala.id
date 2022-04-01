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
    [QueryProperty(nameof(Bank), "bank")]
    public partial class TopupBankRequestPage : ContentPage
    {
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
            string virtualAccount = "1234";
            string timeLimit = "12 April 2022";
            string nominal = this.nominal.Text;
            await Shell.Current.GoToAsync($"//topupbankstep?virtualAccount={virtualAccount}&timeLimit={timeLimit}&nominal={nominal}&bank={bank}");
        }
    }
}