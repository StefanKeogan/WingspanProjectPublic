using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace WingspanPrototype1
{
    public partial class AddSponsorship : ContentPage
    {
        public AddSponsorship(string title)
        {
            InitializeComponent();
            Title = title; //gives the detail page the title passed from the main page

            // What device are we running on? 
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    addSponsorshipStackLayout.Margin = new Thickness(300, 20, 300, 20);
                    break;
                case Device.Android:
                    addSponsorshipStackLayout.Margin = new Thickness(5, 5, 5, 5);
                    break;
                default:
                    addSponsorshipStackLayout.Margin = new Thickness(300, 20, 300, 20);
                    break;
            }
        }

        //saves sponsorship to database?
        void OnLevelSelected (object sender, EventArgs e)
        {
            string sponsorshipLevel; //ready for entering to database (maybe make it a property?)

            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    sponsorshipLevel = "Wild Bird";
                    break;
                case 1:
                    sponsorshipLevel = "Captive Bird (Absolute)";
                    break;
                case 2:
                    sponsorshipLevel = "Captive Bird (Gold)";
                    break;
                case 3:
                    sponsorshipLevel = "Captive Bird (Silver)";
                    break;
                case 4:
                    sponsorshipLevel = "Captive Bird (Bronze)";
                    break;
            }

            //assign the string of the level to this sponsorship
        }

        //sorts out the sponsor selection
        async void OnSponsorSelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex == 0) //exisiting sponsor
            {
                //await Navigation.PushAsync(sponsor search page);
            }
            else if (selectedIndex == 1) // new sponsor
            {
                //pop up box for sponsor name
            }
        }


        //save button for sponsorship
        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation", "Would you like to save this sponsorship?", "Yes", "Cancel");

            if (answer == true)
            {
                //need to connect these things to the database as well
                await DisplayAlert("Success", "This sponsorship has been saved", "Ok");
            }
        }
    }
}
