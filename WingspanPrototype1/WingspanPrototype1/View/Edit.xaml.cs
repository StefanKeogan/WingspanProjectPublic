using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
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
                    searchTitle.Text = "Find Bird"; // Heading text
                    // Add feilds search box by adding them to list 
                    searchFeilds.Add(new SearchFeild { FeildName = "nameInput", LabelName = "Name", LabelText = "Bird Name: ", FeildType = typeof(Entry) });
                    searchFeilds.Add(new SearchFeild { FeildName = "idInput", LabelName = "ID", LabelText = "ID: ", FeildType = typeof(Entry) });
                    searchFeilds.Add(new SearchFeild { FeildName = "speciesInput", LabelName = "Species", LabelText = "Species: ", FeildType = typeof(Entry) });
                    searchFeilds.Add(new SearchFeild { FeildName = "statusInput", LabelName = "Status", LabelText = "Status: ", FeildType = typeof(Picker) });
                    break;
                case "Edit Members":
                    searchTitle.Text = "Find Member";
                    searchFeilds.Add(new SearchFeild { FeildName = "nameInput", LabelName = "Name", LabelText = "Member Name: ", FeildType = typeof(Entry) });
                    searchFeilds.Add(new SearchFeild { FeildName = "idInput", LabelName = "ID", LabelText = "ID: ", FeildType = typeof(Entry) });
                    break;
                default:
                    searchFeilds.Add(new SearchFeild { FeildName = "Error", LabelName = "Error", LabelText = "Error" });
                    break;
            }

            SetSearchFeilds(searchFeilds);            

        }

        private void SearchBtn_Clicked(object sender, EventArgs e)
        {
            // Search database 

            // TODO: Get DB connection working 
            // AccessDatabase accessDatabase = new AccessDatabase();
            // List<BsonDocument> results = accessDatabase.SearchCollection("Name", "Mr. Beaks");

            // Hardcoded data
            ArrayList results = new ArrayList();

            //results.Add(new WildBird { WingspanId = "W15/003", Age = "Juvenile", MetalBandId = "H39851", BanderName = "Noel Hyde", DateBanded = DateTime.Now, Gps = "-38.163565, +176.27060", Location = "Nursery Road, Whakarewarewa Forest, Rotorua, Bay of Plenty", Sex = "Male", Species = "Falcon" });
            //results.Add(new WildBird { WingspanId = "W15/004", Age = "Adult", MetalBandId = "L40435", BanderName = "Dave Crip", DateBanded = DateTime.Now, Gps = "-38.714077, 176.371659", Location = "Kaingaroa Forest, Cpt 512", Sex = "Female", Species = "Falcon" });
            //results.Add(new WildBird { WingspanId = "W15/004", Age = "Juvenile", MetalBandId = "K10996", BanderName = "Heidi Stook", DateBanded = DateTime.Now, Gps = "-38.712070, 176.533579", Location = "Hill Road, Whakarewarewa Forest, Rotorua, Bay of Plenty", Sex = "Male", Species = "Barn Owl" });
            //results.Add(new WildBird { WingspanId = "W15/006", Age = "Adult", MetalBandId = "S-87486", BanderName = "Noel Hyde", DateBanded = DateTime.Now, Gps = "-35.106184, +173.30352", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            
            results.Add(new CaptiveBird { WingspanId = "15/006", Age = "Juvenile", BandNo = "S-87486", Name = "Mr. Beaks", DateArrived = DateTime.Now,  Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            results.Add(new CaptiveBird { WingspanId = "15/007", Age = "Adult", BandNo = "L40435", Name = "Hawk Eye", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            results.Add(new CaptiveBird { WingspanId = "15/008", Age = "Juvenile", BandNo = "K10996", Name = "Professor Feathers", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            results.Add(new CaptiveBird { WingspanId = "15/009", Age = "Adult", BandNo = "H39851", Name = "Batman", DateArrived = DateTime.Now, DateDeceased= DateTime.Now, Result = "Deceased", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });




            // What results page do we need to display ? 
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    Navigation.PushAsync(new BirdResultsDesktop(results, typeof(CaptiveBird)));
                    break;
                case Device.Android:
                    // Navigation.PushAsync(new ResultsMobile1(results));
                    break;
                default:
                    Navigation.PushAsync(new BirdResultsDesktop(results, typeof(WildBird)));
                    break;
            }
            
        }

        public void SetSearchFeilds(List<SearchFeild> searchFeilds)
        {
            int rowIndex = 0;

            foreach (SearchFeild searchFeild in searchFeilds)
            {
                editGrid.RowDefinitions.Add(new RowDefinition());

                editGrid.Children.Add(new Label { Text = searchFeild.LabelText, FontSize = 20,  VerticalOptions = LayoutOptions.Center}, 0, rowIndex);

                if (searchFeild.FeildType == typeof(Entry))
                {
                    editGrid.Children.Add(new Entry { VerticalOptions = LayoutOptions.Center,}, 1, rowIndex);
                }
                else if (searchFeild.FeildType == typeof(Picker))
                {
                    Picker picker = new Picker();

                    switch (searchFeild.LabelName)
                    {
                        case "Status": picker.ItemsSource = new string[] { "Captive", "Wild" };
                            break;
                        default: picker.ItemsSource = new string[] { "Error" };
                            break;
                    }

                    editGrid.Children.Add(picker, 1, rowIndex);
                }

                rowIndex++;
            }
        }

    }
}