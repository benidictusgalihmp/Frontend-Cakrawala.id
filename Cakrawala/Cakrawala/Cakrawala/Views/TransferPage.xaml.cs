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
            Debug.WriteLine(receiverId.Text);
            await Shell.Current.GoToAsync($"//transferconfirmation?receiverId={this.receiverId.Text}&nominal={this.nominal.Text}");
        }
    }
}