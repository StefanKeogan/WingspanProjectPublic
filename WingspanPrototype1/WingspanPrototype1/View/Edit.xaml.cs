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
                case "Edit Sponsors":
                    searchTitle.Text = "Find Sponsor";
                    searchSponsorForm.IsVisible = true;
                    break;
                case "Select Sponsor":
                    searchTitle.Text = "Find Sponsor";
                    searchSponsorForm.IsVisible = true;
                    break;
                case "Edit Sponsorships":
                    searchTitle.Text = "Find Sponsorship";
                    searchSponsorshipForm.IsVisible = true;
                    break;
                case "Edit Volunteers":
                    searchTitle.Text = "Edit Volunteers";
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
                        if (Validate.ContainsNumerOrSymbol(birdNameEntry.Text)) // Does the name feild contain an invalid symblo
                        {
                            DisplayAlert("Invalid Name Value", "The name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // If no feilds are invalid run the search
                    if (DeviceSize.ScreenArea() <= 783457) // If the device size is less than 7 inches push the mobile page
                    {
                        Navigation.PushAsync(new BirdResultsMobile1(SearchBirds()));
                    }
                    else
                    {
                        Navigation.PushAsync(new BirdResultsDesktop(SearchBirds()));
                    }
                    break;

                case "Select Bird":
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        Navigation.PushAsync(new SelectBirdResultsDesktop(SearchBirds()));
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
                        if (Validate.ContainsNumerOrSymbol(memberFirstNameEntry.Text))
                        {
                            DisplayAlert("Invalid First Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Is the last name feild filled in 
                    if (Validate.FeildPopulated(memberLastNameEntry.Text))
                    {
                        if (Validate.ContainsNumerOrSymbol(memberLastNameEntry.Text))
                        {
                            DisplayAlert("Invalid Last Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Is the salutaion name feild filled in 
                    if (Validate.FeildPopulated(salutationNameEntry.Text))
                    {
                        // Does this feild contaiin any numbers or special characters
                        if (Validate.ContainsNumerOrSymbol(salutationNameEntry.Text))
                        {
                            DisplayAlert("Invalid First Name Value", "The first name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }
                    
                    // Search Memebers
                    List<Member> results = SearchMembers.Search(memberFirstNameEntry.Text, memberLastNameEntry.Text, salutationNameEntry.Text);

                    if (results != null)
                    {
                        // Results have been returned push the results page 
                        if (DeviceSize.ScreenArea() <= 783457)
                        {
                            Navigation.PushAsync(new MemberResultsMobile1(results));
                        }
                        else
                        {
                            Navigation.PushAsync(new MemberResultsDesktop(results));
                        }
                        
                    }
                    else
                    {
                        DisplayAlert("No Members Found", "That member could been found", "OK");
                    }
                    break;

                case "Edit Sponsors":
         
                    if (Validate.AllFeildsEmpty(new string[] {sponsorIdEntry.Text, sponsorNameEntry.Text }))
                    {
                        DisplayAlert("All Feilds Empty", "Please fill in at least one serach feild to continue", "OK");
                        return;
                    }

                    // Has the sponsor name feild been poulated? 
                    if (Validate.FeildPopulated(sponsorNameEntry.Text))
                    {
                        // Does the sponsor name feild contain any numbers or symbols 
                        if (Validate.ContainsNumerOrSymbol(sponsorNameEntry.Text))
                        {
                            DisplayAlert("Invalid Sponsor Name Value", "The sponsor name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // If all feilds are valid run the search
                    if (DeviceSize.ScreenArea() <= 783457)
                    {
                        // TODO: Edit sponsor results desktop
                    }
                    else
                    {
                        Navigation.PushAsync(new EditSponsorResultsDesktop(SearchSponsors()));
                    }
                    break;

                case "Select Sponsor":
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        Navigation.PushAsync(new SelectSponsorResultsDesktop(SearchSponsors()));
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
                        if (Validate.ContainsNumerOrSymbol(sponsorshipWingspanIdEntry.Text))
                        {
                            DisplayAlert("Invalid Winspan Id Value", "The sponsor name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    // Has the sponsor name feild been poulated? 
                    if (Validate.FeildPopulated(sponsorshipSponsorEntry.Text))
                    {
                        // Does the sponsor name feild contain any numbers or symbols 
                        if (Validate.ContainsNumerOrSymbol(sponsorshipSponsorEntry.Text))
                        {
                            DisplayAlert("Invalid Sponsor Name Value", "The sponsor name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }


                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        Navigation.PushAsync(new EditSponsorshipResultsDesktop(SearchSponsorships()));
                    }
                    break;

                case "Edit Volunteers":

                    if (Validate.AllFeildsEmpty(new string[] {volunteerEmailEntry.Text, volunteerNameEntry.Text }))
                    {
                        DisplayAlert("All Feilds Empty", "Please fill in at least one serach feild to continue", "OK");
                        return;
                    }

                    if (Validate.FeildPopulated(volunteerEmailEntry.Text))
                    {
                        // TODO: valid email address                       
                    }

                    if (Validate.FeildPopulated(volunteerNameEntry.Text))
                    {
                        if (Validate.ContainsNumerOrSymbol(volunteerNameEntry.Text))
                        {
                            DisplayAlert("Invalid Volunteer Name Value", "The volunteer name feild can not contain numbers or symbols", "OK");
                            return;
                        }
                    }

                    if (DeviceSize.ScreenArea() <= 783457)
                    {
                        Navigation.PushAsync(new VolunteerResultsMobile1(SearchVolunteers.Search(volunteerEmailEntry.Text, volunteerNameEntry.Text)));
                    }
                    else
                    {
                        Navigation.PushAsync(new VolunteerResultsDesktop(SearchVolunteers.Search(volunteerEmailEntry.Text, volunteerNameEntry.Text)));
                    }

                    break;
            }
            
       
        }

        private ArrayList SearchBirds()
        {
            SearchBirds searchBirds = new SearchBirds(wingspanIdEntry.Text, birdNameEntry.Text, bandNumberEntry.Text);

            // Store found items 
            List<WildBird> wildBirds = searchBirds.SearchWildBirds();
            List<CaptiveBird> captiveBirds = searchBirds.SearchCaptiveBirds();

            // Stores items from both lists so that we can pass them through to the results pages
            ArrayList results = new ArrayList();

            // If wild birds were found add them to the array list
            if (wildBirds != null)
            {
                foreach (var bird in wildBirds)
                {
                    results.Add(bird);
                }
            }

            // If captive birds were found add them to the array list
            if (captiveBirds != null)
            {
                foreach (var bird in captiveBirds)
                {
                    results.Add(bird);
                }
            }
                                 
            // Hardcoded data
            //results.Add(new WildBird { WingspanId = "W15/003", Age = "Juvenile", MetalBandId = "H39851", BanderName = "Noel Hyde", DateBanded = DateTime.Now, Gps = "-38.163565, +176.27060", Location = "Nursery Road, Whakarewarewa Forest, Rotorua, Bay of Plenty", Sex = "Male", Species = "Falcon" });
            //results.Add(new WildBird { WingspanId = "W15/004", Age = "Adult", MetalBandId = "L40435", BanderName = "Dave Crip", DateBanded = DateTime.Now, Gps = "-38.714077, 176.371659", Location = "Kaingaroa Forest, Cpt 512", Sex = "Female", Species = "Falcon" });
            //results.Add(new WildBird { WingspanId = "W15/004", Age = "Juvenile", MetalBandId = "K10996", BanderName = "Heidi Stook", DateBanded = DateTime.Now, Gps = "-38.712070, 176.533579", Location = "Hill Road, Whakarewarewa Forest, Rotorua, Bay of Plenty", Sex = "Male", Species = "Barn Owl" });
            //results.Add(new WildBird { WingspanId = "W15/006", Age = "Adult", MetalBandId = "S-87486", BanderName = "Noel Hyde", DateBanded = DateTime.Now, Gps = "-35.106184, +173.30352", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            //results.Add(new CaptiveBird { WingspanId = "15/006", Age = "Juvenile", BandNo = "S-87486", Name = "Mr. Beaks", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            //results.Add(new CaptiveBird { WingspanId = "15/007", Age = "Adult", BandNo = "L40435", Name = "Hawk Eye", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            //results.Add(new CaptiveBird { WingspanId = "15/008", Age = "Juvenile", BandNo = "K10996", Name = "Professor Feathers", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            //results.Add(new CaptiveBird { WingspanId = "15/009", Age = "Adult", BandNo = "H39851", Name = "Batman", DateArrived = DateTime.Now, DateDeceased = DateTime.Now, Result = "Deceased", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });

            return results;
        }

        private ArrayList SearchSponsors()
        {

            //hardcoded examples
            ArrayList sponsors = new ArrayList();

            sponsors.Add(new Sponsor { SponsorID = "S001", SponsorName = "Homer Simpson", SponsorAddress = "Springfield, USA", SponsorPhone = "022 123 4567", SponsorEmail = "homer@gmail.com", SponsorNotes = null });
            sponsors.Add(new Sponsor { SponsorID = "S002", SponsorName = "Bruce Banner", SponsorAddress = "", SponsorPhone = "", SponsorEmail = "hulk@gmail.com", SponsorNotes = "You won't like him when he's angry." });
            sponsors.Add(new Sponsor { SponsorID = "S003", SponsorName = "Monty Burns", SponsorAddress = "Springfield, USA", SponsorPhone = "", SponsorEmail = "rich@gmail.com", SponsorNotes = null });

            return sponsors;
        }

        private ArrayList SearchSponsorships()
        {


            //dummy data
            ArrayList sponsorships = new ArrayList();

            return sponsorships;
        }

    }
}