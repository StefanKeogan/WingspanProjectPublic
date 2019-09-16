using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace WingspanPrototype1.View
{
    public partial class AddSponsorship : ContentPage
    {
        public AddSponsorship(string title)
        {
            InitializeComponent();
            Title = title; //gives the detail page the title passed from the main page
        }

        //sorts out 
        async void OnBirdSelected (object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex == 0)
            {
                await Navigation.PushAsync()
            }
            else if (selectedIndex == 1)
            {

            }
        }


        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation", "Would you like to save this sponsorship?", "Yes", "No");

            if (answer == true)
            {
                //need to connect these things to the database as well
                await DisplayAlert("Saved", "This sponsorship is now active", "Ok");
            }
            //DisplayAlert("Sponsorship Confirmed", "This bird is now being sponsored", "Ok");
        }
    }
}
