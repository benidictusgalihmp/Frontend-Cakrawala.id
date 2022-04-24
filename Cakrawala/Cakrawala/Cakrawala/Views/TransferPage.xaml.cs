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
            User recvUser = await App.transferService.FindUserAsync(this.receiverUsername.Text);
            if (!(recvUser is null)) // For now cannot retrieve other data
            {
                // this.errorFindUserNote.IsVisible = false;
                await Shell.Current.GoToAsync($"//transferconfirmation?receiverId={recvUser.userId}&nominal={this.nominal.Text}&receiverName={recvUser.displayName}");
            } else
            {
                await DisplayAlert("Error", "Tidak dapat menemukan Pengguna dengan Username terkait", "Ok");
            }
        }
    }
}