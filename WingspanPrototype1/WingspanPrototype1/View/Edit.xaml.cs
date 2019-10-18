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

                    ArrayList birdResults = SearchBirds.Search(wingspanIdEntry.Text, birdNameEntry.Text, bandNumberEntry.Text);

                    // TODO: Try Catch
                    if ((birdResults != null) && (birdResults.Count > 0))
                    {
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
                        DisplayAlert("No Birds Found", "That bird could been found", "OK");
                    }
                    
                    break;

                case "Select Bird":
                    ArrayList selectBirdResults = SearchBirds.Search(wingspanIdEntry.Text, birdNameEntry.Text, bandNumberEntry.Text);
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        Navigation.PushAsync(new SelectBirdResultsDesktop(selectBirdResults));
                    }                 
                    break;

                case "Edit Members":
                    // Are all feilds left empty
                    if (Validate.AllFeildsEmpty(new string[]{memberFirstNameEntry.Text, memberLastNameEntry.Text, salutationNameEntry.Text }))
                    {
                        DisplayAlert("All Feilds Empty", "Please fill in at least one serach feild to continue", "OK");
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
                        // Does this feild contaiin any numbers or special characters
                        if (Validate.ContainsNumberOrSymbol(salutationNameEntry.Text))
                        {
                            DisplayAlert("Invalid First Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }
                    
                    // Search Members
                    List<Member> memberResults = SearchMembers.Search(memberFirstNameEntry.Text, memberLastNameEntry.Text, salutationNameEntry.Text);

                    if ((memberResults != null) && (memberResults.Count > 0))
                    {
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
                        DisplayAlert("No Members Found", "That member could been found", "OK");
                    }
                    break;
               

                case "Select Member":
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        //Navigation.PushAsync(new SelectSponsorResultsDesktop(SearchMembers()));
                    }
                    break;

                case "Edit Sponsorships":

                    if (Validate.AllFeildsEmpty(new string[] { sponsorshipWingspanIdEntry.Text, sponsorshipSponsorEntry.Text }))
                    {
                        DisplayAlert("All Feilds Empty", "Please fill in at least one serach feild to continue", "OK");
                        return;
                    }

                    // Has the wingspan Id feild been poulated? 
                    if (Validate.FeildPopulated(sponsorshipWingspanIdEntry.Text))
                    {
                        // Does the wingspan Id feild contain any numbers or symbols 
                        if (Validate.ContainsNumberOrSymbol(sponsorshipWingspanIdEntry.Text))
                        {
                            DisplayAlert("Invalid Winspan Id Value", "The sponsor name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Has the sponsor name feild been poulated? 
                    if (Validate.FeildPopulated(sponsorshipSponsorEntry.Text))
                    {
                        // Does the sponsor name feild contain any numbers or symbols 
                        if (Validate.ContainsNumberOrSymbol(sponsorshipSponsorEntry.Text))
                        {
                            DisplayAlert("Invalid Sponsor Name Value", "The sponsor name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }


                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        //Navigation.PushAsync(new EditSponsorshipResultsDesktop(SearchSponsorships()));
                    }
                    break;



                case "Edit Volunteers":

                    if (Validate.AllFeildsEmpty(new string[] {volunteerEmailEntry.Text, volunteerFirstNameEntry.Text, volunteerLastNameEntry.Text }))
                    {
                        DisplayAlert("All Feilds Empty", "Please fill in at least one serach feild to continue", "OK");
                        return;
                    }

                    if (Validate.FeildPopulated(volunteerEmailEntry.Text))
                    {
                        // TODO: valid email address                       
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

                    List<Volunteer> volunteerResults = SearchVolunteers.Search(volunteerEmailEntry.Text, volunteerFirstNameEntry.Text, volunteerLastNameEntry.Text);

                    if ((volunteerResults != null) && (volunteerResults.Count > 0))
                    {
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
                        DisplayAlert("No Members Found", "That member could been found", "OK");
                    }
                    break;
            }
                  
        }              
    }
}