using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using WingspanPrototype1.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
                case "Edit Members":
                    searchTitle.Text = "Find Member";
                    searchMemberForm.IsVisible = true;
                    break;
                case "Edit Sponsors":
                    searchTitle.Text = "Find Sponsor";
                    searchSponsorForm.IsVisible = true;
                    break;
                case "Edit Sponsorships":
                    searchTitle.Text = "Find Sponsorship";
                    searchSponsorshipForm.IsVisible = true;
                    break;
                default:
                    break;
            }

        }

        private void SearchBtn_Clicked(object sender, EventArgs e)
        {
            // TODO: Get feild data
            // TODO: Wirte screen size awareness class to determine results page

            // What results page are we pushing based on title and runtime platform 

            switch (Title)
            {
                case "Edit Birds":
                    switch (Device.RuntimePlatform)
                    {
                        case Device.UWP:
                            Navigation.PushAsync(new BirdResultsDesktop(SearchBirds()));
                            break;
                        case Device.Android:
                            Navigation.PushAsync(new BirdResultsMobile1(SearchBirds()));
                            break;
                        default: DisplayAlert("Error","Could not get runtime platform", "OK");
                            break;
                    }
                    
                    break;
                case "Edit Members":
                    switch (Device.RuntimePlatform)
                    {
                        case Device.UWP:
                            Navigation.PushAsync(new MemberResultsDesktop(Searchmembers()));
                            break;
                        case Device.Android:
                            Navigation.PushAsync(new MemberResultsMobile1(Searchmembers()));
                            break;
                        default:
                            break;
                    }
                   
                    break;
                case "Edit Sponsors":
                    switch (Device.RuntimePlatform)
                    {
                        case Device.UWP:
                            Navigation.PushAsync(new SponsorResultsDesktop(SearchSponsors()));
                            break;
                        default:
                            break;
                    }

                    break;
                case "Edit Sponsorships":
         

                    break;
                default:
                    break;
            }

        }

        private ArrayList SearchBirds()
        {
            // TODO: Get DB connection working 
            // AccessDatabase accessDatabase = new AccessDatabase();
            // List<BsonDocument> results = accessDatabase.SearchCollection("Name", "Mr. Beaks");

            // Hardcoded data
            ArrayList results = new ArrayList();

            results.Add(new WildBird { WingspanId = "W15/003", Age = "Juvenile", MetalBandId = "H39851", BanderName = "Noel Hyde", DateBanded = DateTime.Now, Gps = "-38.163565, +176.27060", Location = "Nursery Road, Whakarewarewa Forest, Rotorua, Bay of Plenty", Sex = "Male", Species = "Falcon" });
            results.Add(new WildBird { WingspanId = "W15/004", Age = "Adult", MetalBandId = "L40435", BanderName = "Dave Crip", DateBanded = DateTime.Now, Gps = "-38.714077, 176.371659", Location = "Kaingaroa Forest, Cpt 512", Sex = "Female", Species = "Falcon" });
            results.Add(new WildBird { WingspanId = "W15/004", Age = "Juvenile", MetalBandId = "K10996", BanderName = "Heidi Stook", DateBanded = DateTime.Now, Gps = "-38.712070, 176.533579", Location = "Hill Road, Whakarewarewa Forest, Rotorua, Bay of Plenty", Sex = "Male", Species = "Barn Owl" });
            results.Add(new WildBird { WingspanId = "W15/006", Age = "Adult", MetalBandId = "S-87486", BanderName = "Noel Hyde", DateBanded = DateTime.Now, Gps = "-35.106184, +173.30352", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            results.Add(new CaptiveBird { WingspanId = "15/006", Age = "Juvenile", BandNo = "S-87486", Name = "Mr. Beaks", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            results.Add(new CaptiveBird { WingspanId = "15/007", Age = "Adult", BandNo = "L40435", Name = "Hawk Eye", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            results.Add(new CaptiveBird { WingspanId = "15/008", Age = "Juvenile", BandNo = "K10996", Name = "Professor Feathers", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            results.Add(new CaptiveBird { WingspanId = "15/009", Age = "Adult", BandNo = "H39851", Name = "Batman", DateArrived = DateTime.Now, DateDeceased = DateTime.Now, Result = "Deceased", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });

            return results;
        }

        private ArrayList Searchmembers()
        {
            // TODO: Get DB connection working 

            // Hardcoded data
            ArrayList members = new ArrayList();

            members.Add(new Member { MemberID = "1",
                FirstName = "Member",
                LastName = "Mc Person",
                SaluationName = "Member",
                Address1 = "12 Random St",
                City = "Rotorua",
                Postcode = "6039",
                JoinDate = DateTime.Today
            });

            return members;

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

    }
}