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
    public partial class DetailPage : ContentPage
    {
        // The detail page displays the search / add menu, the content of this page is determined by the title of 
        // the menu item selected
        public DetailPage(string title)
        {
            InitializeComponent();

            Title = title; // Set title 

            List<SearchFeild> searchFeilds = new List<SearchFeild>(); // Stores the feild we want to display

            // Based on title value set feilds 
            switch (title)
            {
                case "Birds":
                    BoxTitle.Text = "Find Bird"; // Heading text

                    // Add feilds search box by adding them to list 
                    searchFeilds.Add(new SearchFeild { FeildName = "NameInput", LabelName = "Name", LabelText = "Name: " });
                    searchFeilds.Add(new SearchFeild { FeildName = "IDInput", LabelName = "ID", LabelText = "ID Number: " });
                    searchFeilds.Add(new SearchFeild { FeildName = "BreedInput", LabelName = "Breed", LabelText = "Breed: " });
                    break;
                default:
                    break;
            }

            // Set source of search feilds list view to the list we added our feilds to 
            SearchFeildsList.ItemsSource = searchFeilds;

        }
    }
}