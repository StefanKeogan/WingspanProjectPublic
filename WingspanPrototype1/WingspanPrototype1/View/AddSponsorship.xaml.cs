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

        //saves sponsorship to database?
        void OnLevelSelected (object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            switch (selectedIndex)
            {
                case 0: //wild
                    //assign
                    break;
                case 1: //absolute
                    break;
                case 2: //gold
                    break;
                case 3: //silver
                    break;
                case 4: //bronze
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
                //await Navigation.PushAsync(sponsor earch page);
            }
            else if (selectedIndex == 1) // new sponsor
            {
                //new page or pop up box for sponsor name
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
