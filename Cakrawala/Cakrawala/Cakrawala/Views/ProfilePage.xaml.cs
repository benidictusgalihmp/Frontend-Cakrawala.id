using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cakrawala.Views
{
    public partial class ProfilePage : ContentPage
    {
        //[QueryProperty(nameof(namavariabel), namastringpenyimpan)]
        public ProfilePage()
        {
            InitializeComponent();
        }

        private async void onClick(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//update");
            // BindingContext -- ngisi return variabel

        }
    }
}