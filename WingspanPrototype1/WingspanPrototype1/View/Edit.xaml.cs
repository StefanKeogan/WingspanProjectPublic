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

            // Based on title value set feilds 
            switch (title)
            {
                case "Edit Birds":
                    searchTitle.Text = "Find Bird"; // Heading text                   

                    // Add grid rows
                    editGrid.RowDefinitions.Add(new RowDefinition());
                    editGrid.RowDefinitions.Add(new RowDefinition());
                    editGrid.RowDefinitions.Add(new RowDefinition());
                    editGrid.RowDefinitions.Add(new RowDefinition());

                    // Add feilds to grid
                    // Bird name
                    editGrid.Children.Add(new Label { Text = "Bird Name", FontSize = 20, VerticalOptions = LayoutOptions.Center }, 0, 0);
                    editGrid.Children.Add(new Entry { VerticalOptions = LayoutOptions.Center }, 1, 0);

                    // Wingspan Number 
                    editGrid.Children.Add(new Label { Text = "Wingspan Number", FontSize = 20, VerticalOptions = LayoutOptions.Center }, 0, 1);
                    editGrid.Children.Add(new Entry { VerticalOptions = LayoutOptions.Center }, 1, 1);

                    // Species
                    editGrid.Children.Add(new Label { Text = "Species", FontSize = 20, VerticalOptions = LayoutOptions.Center }, 0, 2);
                    editGrid.Children.Add(new Entry { VerticalOptions = LayoutOptions.Center }, 1, 2);

                    // Status
                    editGrid.Children.Add(new Label { Text = "Status", FontSize = 20, VerticalOptions = LayoutOptions.Center }, 0, 3);
                    editGrid.Children.Add(new Picker { VerticalOptions = LayoutOptions.Center, ItemsSource = new string[] {"Captive", "Wild" } }, 1, 3);

                    break;
                case "Edit Members":
                    searchTitle.Text = "Find Member";                  
                    break;
                case "Edit Volunteers":
                    searchTitle.Text = "Find Volunteer";                 
                    break;
                default:
                    break;
            }

        }

        public ArrayList SearchBirdData(List<SearchParameter> searchParameters, string birdCategory)
        {
            // TODO: Get DB connection working 
            // AccessDatabase accessDatabase = new AccessDatabase();
            // List<BsonDocument> results = accessDatabase.SearchCollection("Name", "Mr. Beaks");

            // Hardcoded data
            ArrayList results = new ArrayList();

            if (birdCategory == "Captive")
            {
                results.Add(new CaptiveBird { WingspanId = "15/006", Age = "Juvenile", BandNo = "S-87486", Name = "Mr. Beaks", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
                results.Add(new CaptiveBird { WingspanId = "15/007", Age = "Adult", BandNo = "L40435", Name = "Hawk Eye", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
                results.Add(new CaptiveBird { WingspanId = "15/008", Age = "Juvenile", BandNo = "K10996", Name = "Professor Feathers", DateArrived = DateTime.Now, Result = "Captive", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
                results.Add(new CaptiveBird { WingspanId = "15/009", Age = "Adult", BandNo = "H39851", Name = "Batman", DateArrived = DateTime.Now, DateDeceased = DateTime.Now, Result = "Deceased", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            }
            else if (birdCategory == "Wild")
            {
                results.Add(new WildBird { WingspanId = "W15/003", Age = "Juvenile", MetalBandId = "H39851", BanderName = "Noel Hyde", DateBanded = DateTime.Now, Gps = "-38.163565, +176.27060", Location = "Nursery Road, Whakarewarewa Forest, Rotorua, Bay of Plenty", Sex = "Male", Species = "Falcon" });
                results.Add(new WildBird { WingspanId = "W15/004", Age = "Adult", MetalBandId = "L40435", BanderName = "Dave Crip", DateBanded = DateTime.Now, Gps = "-38.714077, 176.371659", Location = "Kaingaroa Forest, Cpt 512", Sex = "Female", Species = "Falcon" });
                results.Add(new WildBird { WingspanId = "W15/004", Age = "Juvenile", MetalBandId = "K10996", BanderName = "Heidi Stook", DateBanded = DateTime.Now, Gps = "-38.712070, 176.533579", Location = "Hill Road, Whakarewarewa Forest, Rotorua, Bay of Plenty", Sex = "Male", Species = "Barn Owl" });
                results.Add(new WildBird { WingspanId = "W15/006", Age = "Adult", MetalBandId = "S-87486", BanderName = "Noel Hyde", DateBanded = DateTime.Now, Gps = "-35.106184, +173.30352", Location = "436 Church Road, Kaitaia", Sex = "Female", Species = "Falcon" });
            }
            else
            {
                // TODO: Error message
            }

            return results;
        }

        private void SearchBtn_Clicked(object sender, EventArgs e)
        {
            // Iterate through grid children and get feild data 
            List<SearchParameter> searchParameters = new List<SearchParameter>();
            string category = "";

            int i = 0;
            foreach (var child in editGrid.Children)
            {
                if (child.GetType() == typeof(Label))
                {
                    var childLabel = child as Label;
                    var childFeildType = editGrid.Children[i + 1].GetType();

                    if (childFeildType == typeof(Entry))
                    {
                        var childEntry = editGrid.Children[i + 1] as Entry;

                        if ((childEntry.Text != null)
                            || (childEntry.Text == string.Empty)
                            || (childEntry.Text == ""))
                        {
                            searchParameters.Add(new SearchParameter { Key = childLabel.Text, Value = childEntry.Text });
                        }

                    }
                    else if (childFeildType == typeof(Picker))
                    {
                        var categoryPicker = editGrid.Children[i + 1] as Picker;

                        // Is the category selected?
                        if (categoryPicker.SelectedItem == null)
                        {
                            DisplayAlert("Error", "Plese select a category to continue", "OK");
                            return;
                        }

                        category = categoryPicker.SelectedItem.ToString();
                        
                    }

                    i += 2;
                }

                
            }

            

            ArrayList results = new ArrayList(); // Search results 
            switch (Title)
            {
                case "Edit Birds":
                    results = SearchBirdData(searchParameters, category);
                    // What results page do we need to display ? 
                    switch (Device.RuntimePlatform)
                    {
                        case Device.UWP:
                            if (category == "Captive")
                            {
                                Navigation.PushAsync(new BirdResultsDesktop(results, typeof(CaptiveBird)));
                            }
                            else
                            {
                                Navigation.PushAsync(new BirdResultsDesktop(results, typeof(WildBird)));
                            }
                            
                            break;
                        case Device.Android:
                            if (category == "Captive")
                            {
                                Navigation.PushAsync(new ResultsMobile1(results, typeof(CaptiveBird)));
                            }
                            else
                            {
                                Navigation.PushAsync(new ResultsMobile1(results, typeof(WildBird)));
                            }
                            
                            break;
                        default:
                            Navigation.PushAsync(new BirdResultsDesktop(results, typeof(WildBird)));
                            break;
                    }
                    break;
                default:
                    break;
            }

            
            
        }

    }
}