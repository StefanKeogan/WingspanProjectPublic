using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using WingspanPrototype1.View;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSponsorship : ContentPage
    {
        public AddSponsorship(string title)
        {
            InitializeComponent();
            Title = title;           
        }

        //sorts out the sponsor selection
        async void OnSponsorSelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex == 0) //exisiting member
            {
                await Navigation.PushAsync(new Edit("Select Member"));
            }
            else if (selectedIndex == 1) //new member
            {
                await DisplayAlert("Create new member", "Enter new member details first", "OK");
                await Navigation.PushAsync(new AddMemberForm("New Member"));
            }
        }

        //search the birds
        private async void SelectSponsorship_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex == 0)
            {
                await Navigation.PushAsync(new Edit("Select Bird"));
            }
        }      

        //saves sponsorship to database?
        void OnLevelSelected (object sender, EventArgs e)
        {
            //string sponsorshipLevel; //ready for entering to database (maybe make it a property?)

            //var picker = (Picker)sender;
            //int selectedIndex = picker.SelectedIndex;
            //switch (selectedIndex)
            //{
            //    case 0:
            //        sponsorshipLevel = "Wild Bird";
            //        break;
            //    case 1:
            //        sponsorshipLevel = "Captive Bird (Absolute)";
            //        break;
            //    case 2:
            //        sponsorshipLevel = "Captive Bird (Gold)";
            //        break;
            //    case 3:
            //        sponsorshipLevel = "Captive Bird (Silver)";
            //        break;
            //    case 4:
            //        sponsorshipLevel = "Captive Bird (Bronze)";
            //        break;
            //    case 5:
            //        sponsorshipLevel = "Other";
            //        break;
            //}

            ////assign the string of the level to this sponsorship
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
            else
            {
                return;
            }
        }
    }
}
