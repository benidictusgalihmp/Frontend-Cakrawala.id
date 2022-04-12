﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Cakrawala.Data.TopupService;

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
            VoucherTopupResponse res = await App.topupService.TopupAsync(this.redeemCode.Text);

            /*
            if (res != null)
            {
                this.errorFindUserNote.IsVisible = false;
                await Shell.Current.GoToAsync("//dashboard");
            } else
            {
                this.errorFindUserNote.IsVisible = true;
            }
            */

            if (res == null)
            {
                await DisplayAlert("Error", "Tidak dapat tersambung ke internet", "Ok");
            } else if (res.IsUsed == true)
            {
                await DisplayAlert("Expired", "Kode voucher telah dipakai", "Ok");
            } else
            {
                await DisplayAlert("Sukses", $"Saldo anda telah bertambah sebanyak Rp{res.Amount}", "Ok");
                await Shell.Current.GoToAsync("//dashboard");
            }
        }

        private async void BankButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//topupbank");
        }
    }
}