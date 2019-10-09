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
            // Species
            // Feild manditory?

            // Location
            if (Validate.FeildPopulated(wildLocationEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(wildLocationEntry.Text))
                {
                    await DisplayAlert("Invalid Entry", "The location feild can not contain numbers or symbols", "OK");
                    return;
                }
            }
            // Required?

            // Age
            // Required?

            // Sex
            // Required?

            // Metal Band
            if (Validate.FeildPopulated(wildMetalBandIdEntry.Text))
            {
                if (Validate.ContainsLetterOrSymbol(wildMetalBandIdEntry.Text))
                {
                    await DisplayAlert("Invalid Entry", "The metal band Id feild can not contain letters or symbols", "OK");
                    return;
                }
            }
            // Required?

            // Band Info
            // Required?

            // Gps
            // Required?

            // Bander Name
            if (Validate.FeildPopulated(wildBanderNameEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(wildBanderNameEntry.Text))
                {
                    await DisplayAlert("Invalid Entry", "The bander name feild can not contain numbers or symbols", "OK");
                    return;
                }
            }
            // Required?

            // What type of bird are we adding?
            if (Title == "New Wild Bird")
            {
                // TODO Dynamic wild bird object 
                // Insert record, return outcome
                bool inserted = AddWildBird.InsertWildBirdDocumnet( new WildBird {
                    WingspanId = "W" + GenerateWingspanId.NewId(),
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
                // Validate 
                // Name
                if (Validate.FeildPopulated(captiveNameEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(captiveNameEntry.Text))
                    {
                        await DisplayAlert("Invalid Entry", "The name feild cant contain numbers or symbols", "OK");
                        return;
                    }
                 
                }
                // Required?

                // Band number
                if (Validate.FeildPopulated(captiveBandNumberEntry.Text))
                {
                    if (Validate.ContainsLetterOrSymbol(captiveNameEntry.Text))
                    {
                        await DisplayAlert("Invalid Entry", "The band number feild can not contain letters or symbols", "OK");
                        return;
                    }
                }
                // Required?

                // Band Number
                // Required feild?

                // Species
                // Required feild?

                // Sex
                // Required feild?

                // Age
                // Required feild?

                // Location
                if (Validate.FeildPopulated(captiveLocationEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(captiveLocationEntry.Text))
                    {
                        await DisplayAlert("Invalid Entry", "The location feild can not contain numbers or symbols", "OK");
                        return;
                    }
                }
                // Required feild?

                // Instantilate wingspan ID here so both add note and add bird functions can access it 
                string wingspanId = GenerateWingspanId.NewId();

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

    }
}