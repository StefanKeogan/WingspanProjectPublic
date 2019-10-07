using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBirdForm : ContentPage
    {
        public AddBirdForm(string title)
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

        private async void AddButton_Clicked(object sender, EventArgs e)
        {

            // What type of bird are we adding?
            if (Title == "New Wild Bird")
            {
                // Insert record, return outcome
                bool inserted = AddWildBird.InsertWildBirdDocumnet( new WildBird {
                    WingspanId = GenerateWingspanId(),
                    Species = wildSpeciesPicker.SelectedItem.ToString(),
                    Location = wildLocationEntry.Text,
                    Age = wildAgePicker.SelectedItem.ToString(),
                    Sex = wildSexPicker.SelectedItem.ToString(),
                    MetalBand = wildMetalBandIdEntry.Text,
                    BandInfo = wildBandInfoEntry.Text,
                    Gps = wildGpsEntry.Text,
                    DateBanded = wildDateBandedPicker.Date,
                    BanderName = wildBanderNameEntry.Text
                });

                // Was the record inserted successfully?
                if (inserted)
                {
                    // Clear feilds
                    wildSpeciesPicker.SelectedIndex = -1;
                    wildLocationEntry.Text = null;
                    wildAgePicker.SelectedIndex = -1;
                    wildSexPicker.SelectedIndex = -1;
                    wildMetalBandIdEntry.Text = null;
                    wildBandInfoEntry.Text = null;
                    wildGpsEntry.Text = null;
                    wildBanderNameEntry.Text = null;

                    await DisplayAlert("Wild Bird Saved", "This bird has been saved in the database", "Ok");
                }
                else
                {
                    await DisplayAlert("Connenction Error", "Could not connect to database please check connection and try again", "OK");
                }
                
            }
            else if (Title == "New Captive Bird")
            {
                string wingspanId = GenerateWingspanId();

                bool birdInserted = AddCaptiveBird.InsertCaptiveBirdDocument(new CaptiveBird
                {
                    WingspanId = wingspanId,
                    Name = captiveNameEntry.Text,
                    BandNo = captiveBandNumberEntry.Text,
                    BandColour = captiveBandColourPicker.SelectedItem.ToString(),
                    Species = captiveSpeciesPicker.SelectedItem.ToString(),
                    Sex = captiveSexPicker.SelectedItem.ToString(),
                    Age = captiveAgePicker.SelectedItem.ToString(),
                    Location = captiveLocationEntry.Text,
                    DateArrived = wildDateBandedPicker.Date
                }) ;

                if (birdInserted)
                {
                    captiveNameEntry.Text = null;
                    captiveBandNumberEntry.Text = null;
                    captiveBandColourPicker.SelectedIndex = -1;
                    captiveSpeciesPicker.SelectedIndex = -1;
                    captiveSexPicker.SelectedIndex = -1;
                    captiveAgePicker.SelectedIndex = -1;
                    captiveLocationEntry.Text = null;                   

                    await DisplayAlert("Captive Bird Saved", "This bird has been saved in the database", "Ok");
                }
                else
                {
                    await DisplayAlert("Connenction Error", "Could not insert bird record, please check connection and try again", "OK");
                }

                if (Validate.FeildPopulated(cpativeInitialNoteEntry.Text))
                {
                    bool noteInserted = AddBirdNote.InsertBirdNote(new Note
                    {
                        Date = DateTime.Today,
                        Category = "Initial Note",
                        Comment = cpativeInitialNoteEntry.Text,
                        WingspanId = wingspanId
                    });

                    if (noteInserted)
                    {
                        await DisplayAlert("Initial Note Saved", "An initial note for this bird has been saved in the database", "Ok");
                        wildInitialNoteEditor.Text = null;
                    }
                    else
                    {
                        await DisplayAlert("Connenction Error", "Could not insert bird note, please check connection and try again", "OK");
                    }
                }

            }
            else
            {
                // TODO: Error message 
            }
            
        }

        private string GenerateWingspanId()
        {
            // TODO: Trim date 
            int year = DateTime.Today.Year;

            // TODO: Get number source
            int number = 12;

            string wingspanId = year.ToString() + "/" + number.ToString();

            return wingspanId;

        }

    }
}