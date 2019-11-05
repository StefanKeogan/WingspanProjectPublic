using System;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using WingspanPrototype1.View;
using WingspanPrototype1.Model;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Controller.Sponsorships;


namespace WingspanPrototype1
{
    //public variables for the select member/bird classes to access and set
    public static class SponsorshipDetails
    {
        public static string thisFirstName;
        public static string thisLastName;
        public static string thisCompany;
        public static ObjectId thisMember;
        public static string thisWingspanID;
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSponsorshipForm : ContentPage
    {
        public AddSponsorshipForm(string title)
        {
            InitializeComponent();
            Title = title;

            // What type of device are we running on 
            if (DeviceSize.ScreenArea() <= 783457)
            {
                sponsorshipMargin1.Width = 0;
                sponsorshipMargin2.Width = 0;
            }
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
            else if (selectedIndex == 1)
            {
                selectedWingspanIdValueLabel.Text = "Other";
                selectedWingspanIdValueLabel.IsVisible = true;
            }
        }



        //save button for sponsorship
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            bool allFieldsValid = true;

            //validation
            //sponsor
            if (sponsorSelector.SelectedIndex == -1)
            {
                selectNameError.IsVisible = true;
                allFieldsValid = false;
            }


            //check whether the sponser has a first name or company name to display
            if (SponsorshipDetails.thisFirstName != null)
            {
                selectedSponsorNameValueLabel.Text = SponsorshipDetails.thisFirstName;
            }
            else if ((SponsorshipDetails.thisFirstName == null) && (SponsorshipDetails.thisCompany != null)) 
            {
                selectedSponsorNameValueLabel.Text = SponsorshipDetails.thisCompany;
            }
            else
            {
                //the member first name and/or company name is not displaying properly
                sponsorNameError.IsVisible = true;
                allFieldsValid = false;
            }

            //DELETE THESE
            ////assign selected sponsor name fields
            //selectedSponsorFirstNameValueLabel.Text = SponsorshipDetails.thisFirstName;
            //selectedSponsorLastNameValueLabel.Text = SponsorshipDetails.thisLastName;
            //selectedSponsorCompanyValueLabel.Text = SponsorshipDetails.thisCompany;
            
            ////check that at least one field is filled in
            //if ((selectedSponsorFirstNameValueLabel.Text == null) && (selectedSponsorLastNameValueLabel.Text == null) && (selectedSponsorCompanyValueLabel.Text == null))
            //{
            //    sponsorNameError1.IsVisible = true;
            //    sponsorNameError2.IsVisible = true;
            //    sponsorNameError3.IsVisible = true;
            //    allFieldsValid = false;
            //}


            //to be sponsored
            if (sponsoredBirdSelector.SelectedIndex == -1)
            {
                selectBirdError.IsVisible = true;
                allFieldsValid = false;
            }

            //assign Wingspan ID of selected bird, if it is not already "Other"
            if (!selectedWingspanIdValueLabel.Text.Equals("Other"))
            {
                selectedWingspanIdValueLabel.Text = SponsorshipDetails.thisWingspanID;
            }

            //make sure there is a Wingspan ID or "Other" displayed 
            if (selectedWingspanIdValueLabel.Text == null)
            {
                selectBirdIDError.IsVisible = true;
                allFieldsValid = false;
            }


            //sponsorship level
            if (levelSelector.SelectedIndex == -1)
            {
                selectLevelError.IsVisible = true;
                allFieldsValid = false;
            }

            
            if (allFieldsValid)
            {
                bool sponsorshipInserted = AddSponsorship.InsertSponsorshipDocument(new Sponsorship
                {
                    WingspanId = selectedWingspanIdValueLabel.Text,
                    Category = levelSelector.SelectedItem.ToString(),
                    SponsorshipNotes = sponsorshipNotesEditor.Text,
                    Member_id = SponsorshipDetails.thisMember,
                    SponsorshipStart = startDateSelector.Date,
                    SponsorshipEnd = endDateSelector.Date
                });

                if (sponsorshipInserted)
                {
                    selectedSponsorNameValueLabel.Text = null;
                    //selectedSponsorLastNameValueLabel.Text = null;
                    //selectedSponsorCompanyValueLabel.Text = null;
                    selectedWingspanIdValueLabel.Text = null;
                    levelSelector.SelectedItem = null;
                    sponsorshipNotesEditor.Text = null;

                    //make 'global' variables null as well
                    SponsorshipDetails.thisFirstName = null;
                    SponsorshipDetails.thisLastName = null;
                    SponsorshipDetails.thisCompany = null;
                    SponsorshipDetails.thisWingspanID = null;

                    await DisplayAlert("Sponsorship Added", "Sponsorship document inserted into the database", "OK");
                }
                else
                {
                    await DisplayAlert("Connenction Error", "Could not insert sponsorship record, please check connection and try again", "OK");
                }
            }
            else
            {
                allFieldsValid = true;
            }
        }
    }
}
