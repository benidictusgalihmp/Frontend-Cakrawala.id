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
    [QueryProperty(nameof(ReceiverId), "receiverId")]
    [QueryProperty(nameof(Nominal), "nominal")]
    [QueryProperty(nameof(ReceiverName), "receiverName")]
    public partial class TransferConfirmationPage : ContentPage
    {
        private string receiverId;
        public string ReceiverId
        {
            get { return receiverId; }
            set { LoadRecvId(value); }
        }

        private string nominal;
        public string Nominal 
        { 
            get { return nominal; }
            set { LoadNominal(value); }
        }

        private string receiverName;
        public string ReceiverName
        {
            get { return receiverName; }
            set { LoadRecvName(value); }
        }

        public TransferConfirmationPage()
        {
            InitializeComponent();
            //this.nominalLabel.Text = "Rp" + Nominal;
            //this.receiverIdLabel.Text = ReceiverId;
        }

        void LoadParam(string value)
        {
            Debug.WriteLine("Value: " + value);
            BindingContext = value;
        }

        void LoadRecvId(string value)
        {
            Debug.WriteLine("Value: " + value);
            receiverId = value;
            this.receiverIdLabel.Text = "#"+value;
        }

        void LoadRecvName(string value)
        {
            receiverName = value;
            this.receiverNameLabel.Text = value;
        }

        void LoadNominal(string value)
        {
            var v = value.Trim();
            nominal = v;
            this.nominalLabel.Text = "Rp"+v;
        }

        private async void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            bool res = await App.transferService.TransferAsync(receiverId, Int32.Parse(nominal));
            Debug.WriteLine($"recvId: {receiverId}, nominal: {nominal}");
            if (!res)
            {
                await DisplayAlert("Error", "Failed to transfer", "Ok");
            } else
            {
                await Shell.Current.GoToAsync("//dashboard");
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//transfer");
        }
    }
}