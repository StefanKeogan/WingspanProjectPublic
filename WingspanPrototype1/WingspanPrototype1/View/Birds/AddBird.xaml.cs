using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Controller.Birds;
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


        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {



            // What type of bird are we adding?
            if (Title == "New Wild Bird")
            {
                bool inserted = AddWildBird.InsertWildBirdDocumnet( new WildBird {
                    WingspanId = GenerateWingspanId(),
                    Species = wildSpeciesPicker.SelectedItem.ToString(),
                    Location = wildLocationEntry.Text,
                    Age = wildAgePicker.SelectedItem.ToString(),
                    Sex = wildSexPicker.SelectedItem.ToString(),
                    MetalBand = metalBandEntry.Text,
                    BandInfo = bandInfoEntry.Text,
                    Gps = gpsEntry.Text,
                    DateBanded = dateBandedPicker.Date,
                    BanderName = banderNameEntry.Text
                });

                if (inserted)
                {
                    DisplayAlert("Wild Bird Saved", "This bird has been saved in the database", "Ok");
                }
                else
                {
                    DisplayAlert("Connenction Error", "Could not connect to database please check connection and try again", "OK");
                }

                
            }
            else
            {
                DisplayAlert("Captive Bird Saved", "This bird has been saved in the database", "Ok");
            }
            
        }

        private string GenerateWingspanId()
        {
            int year = DateTime.Today.Year;

            int number = 12;

            string wingspanId = year.ToString() + "/" + number.ToString();

            return wingspanId;


        }

    }
}