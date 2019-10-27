using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using WingspanPrototype1.View;
using WingspanPrototype1.Functions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WingspanPrototype1.View.Volunteers;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Controller.Volunteers;
using WingspanPrototype1.Controller.Sponsorships;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit : ContentPage
    {
        // The detail page displays the search / add menu, the content of this page is determined by the title of 
        // the menu item selected
        public Edit(string title)
        {
            InitializeComponent();

            Title = title; // Set title 

            // Based on title value set feilds 
            switch (title)
            {
                case "Edit Birds":
                    searchTitle.Text = "Find Bird";
                    searchBirdForm.IsVisible = true;
                    break;
                case "Select Bird":
                    searchTitle.Text = "Find Bird";
                    searchBirdForm.IsVisible = true;
                    break;
                case "Edit Members":
                    searchTitle.Text = "Find Member";
                    searchMemberForm.IsVisible = true;
                    break;               
                case "Select Member":
                    searchTitle.Text = "Find Member";
                    searchMemberForm.IsVisible = true;
                    break;
                case "Edit Sponsorships":
                    searchTitle.Text = "Find Sponsorship";
                    searchSponsorshipForm.IsVisible = true;
                    break;
                case "Edit Volunteers":
                    searchTitle.Text = "Search Volunteers";
                    searchVolunteerForm.IsVisible = true;
                    break;
                default:
                    break;
            }

        }

        private void SearchBtn_Clicked(object sender, EventArgs e)
        {

            // What are we searching based on page title 
            switch (Title)
            {
                case "Edit Birds":
                    // Have all feilds been left empty
                    if (Validate.AllFeildsEmpty(new string[] {wingspanIdEntry.Text, birdNameEntry.Text, bandNumberEntry.Text }))
                    {
                        DisplayAlert("All feilds empty", "Please fill in at least one feild to continue", "OK");
                        return;
                    }

                    // Has the name feild been filled in  
                    if (Validate.FeildPopulated(birdNameEntry.Text)) 
                    {
                        if (Validate.ContainsNumberOrSymbol(birdNameEntry.Text)) // Does the name feild contain an invalid symblo
                        {
                            DisplayAlert("Invalid Name Value", "The name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Load animation
                    searchingIndicator.IsRunning = true;

                    // Run search on another thread
                    Task.Run(() => {

                        ArrayList birdResults = SearchBirds.Search(wingspanIdEntry.Text, birdNameEntry.Text, bandNumberEntry.Text);

                        // Run on main thread
                        Device.BeginInvokeOnMainThread(() => {

                            if ((birdResults != null) && (birdResults.Count > 0))
                            {
                                wingspanIdEntry.Text = null;
                                birdNameEntry.Text = null;
                                bandNumberEntry.Text = null;

                                // If no feilds are invalid run the search
                                if (DeviceSize.ScreenArea() <= 783457) // If the device size is less than 7 inches push the mobile page
                                {
                                    Navigation.PushAsync(new BirdResultsMobile1(birdResults));
                                }
                                else
                                {
                                    Navigation.PushAsync(new BirdResultsDesktop(birdResults));
                                }
                            }
                            else
                            {
                                DisplayAlert("No Birds Found", "That bird could not be found", "OK");
                            }

                            searchingIndicator.IsRunning = false;
                        });
                    });

                    break;

                case "Select Bird":
                    // Have all feilds been left empty
                    if (Validate.AllFeildsEmpty(new string[] { wingspanIdEntry.Text, birdNameEntry.Text, bandNumberEntry.Text }))
                    {
                        DisplayAlert("All feilds empty", "Please fill in at least one feild to continue", "OK");
                        return;
                    }

                    // Has the name feild been filled in  
                    if (Validate.FeildPopulated(birdNameEntry.Text))
                    {
                        if (Validate.ContainsNumberOrSymbol(birdNameEntry.Text)) // Does the name feild contain an invalid symblo
                        {
                            DisplayAlert("Invalid Name Value", "The name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }
                   
                    ArrayList selectBirdResults = SearchBirds.Search(wingspanIdEntry.Text, birdNameEntry.Text, bandNumberEntry.Text);                   

                    if ((selectBirdResults != null) && (selectBirdResults.Count > 0))
                    {
                        // If no feilds are invalid run the search
                        if (DeviceSize.ScreenArea() <= 783457) // If the device size is less than 7 inches push the mobile page
                        {
                            Navigation.PushAsync(new SelectBirdResultsMobile1(selectBirdResults));
                        }
                        else
                        {
                            Navigation.PushAsync(new SelectBirdResultsDesktop(selectBirdResults));
                        }
                    }
                    else
                    {
                        DisplayAlert("No Birds Found", "That bird could not be found", "OK");
                    }
                    break;


                case "Edit Members":

                    // Are all feilds left empty
                    if (Validate.AllFeildsEmpty(new string[]{memberFirstNameEntry.Text, memberLastNameEntry.Text, salutationNameEntry.Text }))
                    {
                        DisplayAlert("All Feilds Empty", "Please fill in at least one search feild to continue", "OK");
                        return;
                    }

                    // Is the first name feild filled in 
                    if (Validate.FeildPopulated(memberFirstNameEntry.Text))
                    {
                        if (Validate.ContainsNumberOrSymbol(memberFirstNameEntry.Text))
                        {
                            DisplayAlert("Invalid First Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Is the last name feild filled in 
                    if (Validate.FeildPopulated(memberLastNameEntry.Text))
                    {
                        if (Validate.ContainsNumberOrSymbol(memberLastNameEntry.Text))
                        {
                            DisplayAlert("Invalid Last Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Is the salutaion name feild filled in 
                    if (Validate.FeildPopulated(salutationNameEntry.Text))
                    {
                        // Does this feild contain any numbers or special characters
                        if (Validate.ContainsNumberOrSymbol(salutationNameEntry.Text))
                        {
                            DisplayAlert("Invalid First Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    searchingIndicator.IsRunning = true;
                    searchButton.IsVisible = false;

                    // Run search on another thread
                    Task.Run(() => {

                        // Search Members
                        List<Member> memberResults = SearchMembers.Search(memberFirstNameEntry.Text, memberLastNameEntry.Text, salutationNameEntry.Text);

                        // Run on main thread
                        Device.BeginInvokeOnMainThread(() => {

                            if ((memberResults != null) && (memberResults.Count > 0))
                            {
                                memberFirstNameEntry.Text = null;
                                memberLastNameEntry.Text = null;
                                salutationNameEntry.Text = null;

                                // Results have been returned push the results page 
                                if (DeviceSize.ScreenArea() <= 783457)
                                {
                                    Navigation.PushAsync(new MemberResultsMobile1(memberResults));
                                }
                                else
                                {
                                    Navigation.PushAsync(new MemberResultsDesktop(memberResults));
                                }

                            }
                            else
                            {
                                DisplayAlert("No Members Found", "That member could not be found", "OK");                               
                            }


                            searchingIndicator.IsRunning = false;
                            // searchButton.IsVisible = true;

                        });

                    });                                      
                    break;
               
                case "Select Member":

                    // Are all feilds left empty
                    if (Validate.AllFeildsEmpty(new string[] { memberFirstNameEntry.Text, memberLastNameEntry.Text, salutationNameEntry.Text }))
                    {
                        DisplayAlert("All Feilds Empty", "Please fill in at least one search feild to continue", "OK");
                        return;
                    }

                    // Is the first name feild filled in 
                    if (Validate.FeildPopulated(memberFirstNameEntry.Text))
                    {
                        if (Validate.ContainsNumberOrSymbol(memberFirstNameEntry.Text))
                        {
                            DisplayAlert("Invalid First Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Is the last name feild filled in 
                    if (Validate.FeildPopulated(memberLastNameEntry.Text))
                    {
                        if (Validate.ContainsNumberOrSymbol(memberLastNameEntry.Text))
                        {
                            DisplayAlert("Invalid Last Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Is the salutaion name feild filled in 
                    if (Validate.FeildPopulated(salutationNameEntry.Text))
                    {
                        // Does this feild contain any numbers or special characters
                        if (Validate.ContainsNumberOrSymbol(salutationNameEntry.Text))
                        {
                            DisplayAlert("Invalid First Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    searchingIndicator.IsRunning = true;

                    // Search Members
                    List<Member> selectMemberResults = SearchMembers.Search(memberFirstNameEntry.Text, memberLastNameEntry.Text, salutationNameEntry.Text);
                    searchingIndicator.IsRunning = false;

                    if ((selectMemberResults != null) && (selectMemberResults.Count > 0))
                    {
                        // Results have been returned push the results page 
                        if (DeviceSize.ScreenArea() <= 783457)
                        {
                            Navigation.PushAsync(new SelectMemberResultsMobile1(selectMemberResults));
                        }
                        else
                        {
                            Navigation.PushAsync(new SelectMemberResultsDesktop(selectMemberResults));
                        }

                    }
                    else
                    {
                        DisplayAlert("No Members Found", "That member could not be found", "OK");
                    }
                    break;

                case "Edit Sponsorships":

                    if (Validate.AllFeildsEmpty(new string[] { sponsorshipWingspanIdEntry.Text, sponsorshipFirstNameEntry.Text, sponsorshipLastNameEntry.Text, sponsorshipCompanyNameEntry.Text }))
                    {
                        DisplayAlert("All Feilds Empty", "Please fill in at least one search feild to continue", "OK");
                        return;
                    }                    

                    // Has the first name feild been populated? 
                    if (Validate.FeildPopulated(sponsorshipFirstNameEntry.Text))
                    {
                        // Does the sponsor name feild contain any numbers or symbols 
                        if (Validate.ContainsNumberOrSymbol(sponsorshipFirstNameEntry.Text))
                        {
                            DisplayAlert("Invalid First Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Has the last name feild been populated? 
                    if (Validate.FeildPopulated(sponsorshipLastNameEntry.Text))
                    {
                        // Does the sponsor name feild contain any numbers or symbols 
                        if (Validate.ContainsNumberOrSymbol(sponsorshipLastNameEntry.Text))
                        {
                            DisplayAlert("Invalid Last Name Value", "The last name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Has the company name feild been populated? 
                    if (Validate.FeildPopulated(sponsorshipCompanyNameEntry.Text))
                    {
                        // Does the sponsor name feild contain any numbers or symbols 
                        if (Validate.ContainsNumberOrSymbol(sponsorshipCompanyNameEntry.Text))
                        {
                            DisplayAlert("Invalid Company Name Value", "The company name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    searchingIndicator.IsRunning = true;
                    // searchButton.IsVisible = false;

                    Task.Run(() => {

                        // Search sponsorships
                        List<Sponsorship> sponsorshipResults = SearchSponsorships.Search(sponsorshipWingspanIdEntry.Text, sponsorshipFirstNameEntry.Text, sponsorshipLastNameEntry.Text, sponsorshipCompanyNameEntry.Text);

                        Device.BeginInvokeOnMainThread(() => {

                            if ((sponsorshipResults != null) && (sponsorshipResults.Count > 0))
                            {
                                // Results have been returned push the results page 
                                if (DeviceSize.ScreenArea() <= 783457)
                                {
                                    Navigation.PushAsync(new EditSponsorshipResultsMobile1(sponsorshipResults));
                                }
                                else
                                {
                                    Navigation.PushAsync(new EditSponsorshipResultsMobile1(sponsorshipResults));
                                }
                            }
                            else
                            {
                                DisplayAlert("No Sponsorships Found", "That sponsorship could not be found", "OK");
                            }

                            searchingIndicator.IsRunning = false;
                            // searchButton.IsVisible = true;

                        });

                    });

                    break;

                case "Edit Volunteers":

                    if (Validate.AllFeildsEmpty(new string[] {volunteerEmailEntry.Text, volunteerFirstNameEntry.Text, volunteerLastNameEntry.Text }))
                    {
                        DisplayAlert("All Feilds Empty", "Please fill in at least one search feild to continue", "OK");
                        return;
                    }

                    if (Validate.FeildPopulated(volunteerEmailEntry.Text))
                    {
                        if (!Validate.EmailFormatValid(volunteerEmailEntry.Text))
                        {
                            DisplayAlert("Invalid Email Format","Please enter a valid email address", "OK");
                        }                     
                    }

                    if (Validate.FeildPopulated(volunteerFirstNameEntry.Text))
                    {
                        if (Validate.ContainsNumberOrSymbol(volunteerFirstNameEntry.Text))
                        {
                            DisplayAlert("Invalid Volunteer Name Value", "The volunteer name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    if (Validate.FeildPopulated(volunteerLastNameEntry.Text))
                    {
                        if (Validate.ContainsNumberOrSymbol(volunteerLastNameEntry.Text))
                        {
                            DisplayAlert("Invalid Volunteer Name Value", "The volunteer name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    searchingIndicator.IsRunning = true;
                    // searchButton.IsVisible = false;


                    Task.Run(() => {

                        List<Volunteer> volunteerResults = SearchVolunteers.Search(volunteerEmailEntry.Text, volunteerFirstNameEntry.Text, volunteerLastNameEntry.Text);

                        Device.BeginInvokeOnMainThread(() => {

                            if ((volunteerResults != null) && (volunteerResults.Count > 0))
                            {
                                volunteerFirstNameEntry.Text = null;
                                volunteerLastNameEntry.Text = null;
                                volunteerEmailEntry.Text = null;

                                if (DeviceSize.ScreenArea() <= 783457)
                                {
                                    Navigation.PushAsync(new VolunteerResultsMobile1(volunteerResults));
                                }
                                else
                                {
                                    Navigation.PushAsync(new VolunteerResultsDesktop(volunteerResults));
                                }
                            }
                            else
                            {
                                DisplayAlert("No Members Found", "That member could not be found", "OK");
                            }

                            searchingIndicator.IsRunning = false;
                            // searchButton.IsVisible = true;

                        });

                    });

                    break;
            }
                  
        }              
    }
}