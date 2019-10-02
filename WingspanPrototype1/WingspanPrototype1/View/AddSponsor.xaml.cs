using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddSponsor : ContentPage
	{
		public AddSponsor (string title)
		{
			InitializeComponent ();
            Title = title;
		}

        //save new sponsor button
        private async void SaveNewSponsorButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("New sponsor added", "", "OK");
            await Navigation.PopAsync();
        }
    }
}