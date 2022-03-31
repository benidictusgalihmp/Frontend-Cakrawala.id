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
    public partial class TopupVoucherPage : ContentPage
    {
        public TopupVoucherPage()
        {
            InitializeComponent();
        }

        private async void RedeemButton_Clicked(object sender, EventArgs e)
        {
            bool res = await App.topupService.TopupAsync(this.redeemCode.Text);
            if (res)
            {
                this.errorFindUserNote.IsVisible = false;
                await Shell.Current.GoToAsync("//dashboard");
            } else
            {
                this.errorFindUserNote.IsVisible = true;
            }
        }

        private async void BankButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//topupbank");
        }
    }
}