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
    public partial class TransferPage : ContentPage
    {
        public TransferPage()
        {
            InitializeComponent();
        }

        private void NominalButton_Clicked(object sender, EventArgs e)
        {
            this.nominal.Text = (sender as Button).Text.Trim('R','p');
        }

        private async void LanjutButton_Clicked(object sender, EventArgs e)
        {
            User recvUser = await App.transferService.FindUserAsync(this.receiverId.Text);
            if (recvUser != null)
            {
                // this.errorFindUserNote.IsVisible = false;
                await Shell.Current.GoToAsync($"//transferconfirmation?receiverId={this.receiverId.Text}&nominal={this.nominal.Text}&receiverName={recvUser.userName}");
            } else
            {
                await DisplayAlert("Error", "Tidak dapat menemukan Pengguna dengan ID terkait", "Ok");
            }
        }
    }
}