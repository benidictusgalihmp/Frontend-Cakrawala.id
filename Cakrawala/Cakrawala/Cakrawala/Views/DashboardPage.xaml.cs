using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cakrawala.Views
{
    [QueryProperty(nameof(Email), "email")]
    public partial class DashboardPage : ContentPage
    {
        public string Email
        {
            set
            {
                OnChangeEmail(value);
            }
            get { return Email; }
        }
        public DashboardPage()
        {
            InitializeComponent();
        }

        public void OnChangeEmail(string value) 
        {
            if (value != null) MainLabel.Text = "Selamat Datang, " + value.ToString();
            BindingContext = value;
        }

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Login");
        }
    }
}