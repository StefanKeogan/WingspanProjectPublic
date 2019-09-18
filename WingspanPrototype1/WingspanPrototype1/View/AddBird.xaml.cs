using System;
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
    public partial class AddBird : ContentPage
    {
        public AddBird(string title)
        {
            InitializeComponent();

            Title = title;

            // What type of bird are we adding?
            if (title == "New Wild Bird")
            {
                wildBirdForm.IsVisible = true;
            }
            else if (title == "New Captive Bird")
            {
                captiveBirdForm.IsVisible = true;
            }
            else
            {
                DisplayAlert("Error", "Please try again", "Ok");

            }

            // What device are we running on? 
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    addBirdStackLayout.Margin = new Thickness(300, 20, 300, 20);
                    break;
                case Device.Android:
                    addBirdStackLayout.Margin = new Thickness(5, 5, 5, 5);
                    break;
                default:
                    addBirdStackLayout.Margin = new Thickness(300, 20, 300, 20);
                    break;
            }


        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            // TODO: 
            // What type of bird are we adding?
            if (Title == "New Wild Bird")
            {
                DisplayAlert("Wild Bird Saved", "This bird has been saved in the data base", "Ok");
            }
            else if (Title == "New Captive Bird")
            {
                DisplayAlert("Captive Bird Saved", "This bird has been saved in the data base", "Ok");
            }

            
        }
    }
}