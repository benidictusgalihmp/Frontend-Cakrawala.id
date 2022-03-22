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

        void LoadNominal(string value)
        {
            var v = value.Trim();
            nominal = v;
            this.nominalLabel.Text = "Rp"+v;
        }

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine($"recvId: {receiverId}, nominal: {nominal}");
        }
    }
}