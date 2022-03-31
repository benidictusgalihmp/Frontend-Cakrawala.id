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
    }
}