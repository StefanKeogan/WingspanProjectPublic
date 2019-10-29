﻿using System;
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
    public static class SponsorshipInfo
    {
        public static string thisFirstName;
        public static string thisLastName;
        public static string thisCompany;
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

            //assign selected sponsor name fields
            selectedSponsorFirstNameValueLabel.Text = SponsorshipInfo.thisFirstName;
            selectedSponsorLastNameValueLabel.Text = SponsorshipInfo.thisLastName;
            selectedSponsorCompanyValueLabel.Text = SponsorshipInfo.thisCompany;

            //check that at least one field is filled in
            if ((selectedSponsorFirstNameValueLabel.Text == null) && (selectedSponsorLastNameValueLabel.Text == null) && (selectedSponsorCompanyValueLabel.Text == null))
            {
                sponsorNameError1.IsVisible = true;
                sponsorNameError2.IsVisible = true;
                sponsorNameError3.IsVisible = true;
                allFieldsValid = false;
            }


            //to be sponsored
            if (sponsoredBirdSelector.SelectedIndex == -1)
            {
                selectBirdError.IsVisible = true;
                allFieldsValid = false;
            }

            //assign Wingspan ID of selected bird, if it is not already "Other"
            if (!selectedWingspanIdValueLabel.Text.Equals("Other"))
            {
                selectedWingspanIdValueLabel.Text = SponsorshipInfo.thisWingspanID;
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
                    SponsorshipCategory = levelSelector.SelectedItem.ToString(),
                    SponsorshipNotes = sponsorshipNotesEditor.Text,
                    FirstName = selectedSponsorFirstNameValueLabel.Text,
                    LastName = selectedSponsorLastNameValueLabel.Text,
                    Company = selectedSponsorCompanyValueLabel.Text,
                    SponsorshipStart = startDateSelector.Date,
                    SponsorshipEnd = endDateSelector.Date
                });

                if (sponsorshipInserted)
                {
                    selectedSponsorFirstNameValueLabel.Text = null;
                    selectedSponsorLastNameValueLabel.Text = null;
                    selectedSponsorCompanyValueLabel.Text = null;
                    selectedWingspanIdValueLabel.Text = null;
                    levelSelector.SelectedItem = null;
                    sponsorshipNotesEditor.Text = null;

                    //make 'global' variables null as well
                    SponsorshipInfo.thisFirstName = null;
                    SponsorshipInfo.thisLastName = null;
                    SponsorshipInfo.thisCompany = null;
                    SponsorshipInfo.thisWingspanID = null;

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