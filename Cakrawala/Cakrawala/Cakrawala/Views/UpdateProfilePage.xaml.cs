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
    public partial class UpdateProfilePage : ContentPage
    {
        public UpdateProfilePage()
        {
            InitializeComponent();
        }

        private async void confirmClick(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//profile");

        }

        private async void cancelClick(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//profile");
        }
    }
}