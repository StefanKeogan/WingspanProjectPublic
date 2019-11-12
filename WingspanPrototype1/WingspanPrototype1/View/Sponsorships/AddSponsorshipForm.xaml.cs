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
using System.Threading.Tasks;

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

        // Are we selecting a member / bird for the add or edit page
        public static bool thisAddingSponsorship;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (SponsorshipDetails.thisFirstName != null)
            {
                selectedSponsorNameValueLabel.Text = SponsorshipDetails.thisFirstName;
            }
            else if ((SponsorshipDetails.thisFirstName == null) && (SponsorshipDetails.thisCompany != null))
            {
                selectedSponsorNameValueLabel.Text = SponsorshipDetails.thisCompany;
            }

            if (SponsorshipDetails.thisWingspanID != null)
            {
                selectedWingspanIdValueLabel.Text = SponsorshipDetails.thisWingspanID;
            }

        }


        //sorts out the sponsor selection
        private async void OnSponsorSelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex == 0) //exisiting member
            {
                SponsorshipDetails.thisAddingSponsorship = true;
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
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            bool allFieldsValid = true;

            addingIndicator.IsRunning = true;
           
            saveButton.IsEnabled = false;

            //validation
            //sponsor
            if (sponsorSelector.SelectedIndex == -1)
            {
                selectNameError.IsVisible = true;
                allFieldsValid = false;
            }

            if ((SponsorshipDetails.thisFirstName == null) && (SponsorshipDetails.thisCompany == null)) 
            {
                //the member first name and/or company name is not displaying properly
                sponsorNameError.IsVisible = true;
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

            Task.Run(() =>
            {
                if (allFieldsValid)
                {
                    bool sponsorshipInserted = AddSponsorship.InsertSponsorshipDocument(new Sponsorship
                    {
                        WingspanId = selectedWingspanIdValueLabel.Text,
                        Category = levelSelector.SelectedItem.ToString(),
                        Notes = sponsorshipNotesEditor.Text,
                        Member_id = SponsorshipDetails.thisMember,
                        StartDate = startDateSelector.Date,
                        EndDate = endDateSelector.Date
                    });

                    if (sponsorshipInserted)
                    {
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            selectedSponsorNameValueLabel.Text = null;
                            //selectedSponsorLastNameValueLabel.Text = null;
                            //selectedSponsorCompanyValueLabel.Text = null;
                            selectedWingspanIdValueLabel.Text = null;
                            levelSelector.SelectedItem = null;
                            sponsorshipNotesEditor.Text = null;
                            sponsorSelector.SelectedIndex = -1;
                            sponsoredBirdSelector.SelectedIndex = -1;

                            //make 'global' variables null as well
                            SponsorshipDetails.thisFirstName = null;
                            SponsorshipDetails.thisLastName = null;
                            SponsorshipDetails.thisCompany = null;
                            SponsorshipDetails.thisWingspanID = null;

                            DisplayAlert("Sponsorship Added", "Sponsorship document inserted into the database", "OK");
                            
                            addingIndicator.IsRunning = false;

                            saveButton.IsEnabled = true;
                            
                        });

                        
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            DisplayAlert("Connenction Error", "Could not insert sponsorship record, please check connection and try again", "OK");

                            addingIndicator.IsRunning = false;

                            saveButton.IsEnabled = true;
                        });

                        
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        addingIndicator.IsRunning = false;
                        saveButton.IsEnabled = true;
                    });

                    allFieldsValid = true;
                }
            });

            
            
        }
    }
}
