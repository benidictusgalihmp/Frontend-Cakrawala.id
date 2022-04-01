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
    public partial class PayHistoryPage : ContentPage
    {
        public PayHistoryPage()
        {
            InitializeComponent();
        }

        private async void BackTaptoRiwayat(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//history");
        }
    }
}