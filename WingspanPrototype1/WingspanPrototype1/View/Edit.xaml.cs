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
            List<string> results = new List<string>();
            results.Add("Mr Beaks");
            results.Add("Professor Feathers");
            results.Add("Batman");

            // What results page do we need to display ? 
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    Navigation.PushAsync(new ResultsDesktop(results));
                    break;
                case Device.Android:
                    Navigation.PushAsync(new ResultsMobile1(results));
                    break;
                default:
                    Navigation.PushAsync(new ResultsDesktop(results));
                    break;
            }
            
        }


    }
}