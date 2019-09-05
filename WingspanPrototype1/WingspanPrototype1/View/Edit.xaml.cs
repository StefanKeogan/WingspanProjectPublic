using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            List<SearchFeild> searchFeilds = new List<SearchFeild>(); // Stores the feilds we want to display

            // Based on title value set feilds 
            switch (title)
            {
                case "Edit Birds":
                    BoxTitle.Text = "Find Bird"; // Heading text

                    // Add feilds search box by adding them to list 
                    searchFeilds.Add(new SearchFeild { FeildName = "NameInput", LabelName = "Name", LabelText = "Name: " });
                    searchFeilds.Add(new SearchFeild { FeildName = "IDInput", LabelName = "ID", LabelText = "ID: " });
                    searchFeilds.Add(new SearchFeild { FeildName = "BreedInput", LabelName = "Breed", LabelText = "Breed: " });
                    break;
                default:
                    searchFeilds.Add(new SearchFeild { FeildName = "Error", LabelName = "Error", LabelText = "Error" });
                    break;
            }

            // Set source of search feilds list view to the list we added our feilds to 
            SearchFeildsList.ItemsSource = searchFeilds;

        }

        private void SearchBtn_Clicked(object sender, EventArgs e)
        {
            // Search database 

            // TODO: Get DB connection working 
            // AccessDatabase accessDatabase = new AccessDatabase();
            // List<BsonDocument> results = accessDatabase.SearchCollection("Name", "Mr. Beaks");

            // Hardcoded data
            List<WildBird> results = new List<WildBird>();
            results.Add(new WildBird {WingspanId = "W15/003", Age = "Juvenile", MetalBandId = "H39851", BanderName = "Noel Hyde", DateBanded = DateTime.Now, Gps = "-38.163565, +176.27060" , Location = "Nursery Road, Whakarewarewa Forest, Rotorua, Bay of Plenty", Sex = "Male", Species = "Falcon" });
            results.Add(new WildBird {WingspanId = "W15/004", Age = "Adult", MetalBandId = "L40435", BanderName = "Dave Crip", DateBanded = DateTime.Now, Gps = "-38.714077, 176.371659", Location = "Kaingaroa Forest, Cpt 512", Sex = "Female", Species = "Falcon" });
            results.Add(new WildBird {WingspanId = "W15/004", Age = "Juvenile", MetalBandId = "K10996", BanderName = "Heidi Stook", DateBanded = DateTime.Now, Gps = "-38.712070, 176.533579", Location = "Hill Road, Whakarewarewa Forest, Rotorua, Bay of Plenty", Sex = "Male", Species = "Barn Owl" });
            results.Add(new WildBird {WingspanId = "W15/006", Age = "Adult", MetalBandId = "S-87486", BanderName = "Noel Hyde", DateBanded = DateTime.Now, Gps = "-35.106184, +173.30352", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });



            // What results page do we need to display ? 
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    Navigation.PushAsync(new BirdResultsDesktop(results, typeof(WildBird)));
                    break;
                case Device.Android:
                    // Navigation.PushAsync(new ResultsMobile1(results));
                    break;
                default:
                    Navigation.PushAsync(new BirdResultsDesktop(results, typeof(WildBird)));
                    break;
            }
            
        }


    }
}