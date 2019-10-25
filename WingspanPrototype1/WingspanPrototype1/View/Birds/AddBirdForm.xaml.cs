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

            // What type of device are we running on 
            if (DeviceSize.ScreenArea() <= 783457)
            {
                wildMargin1.Width = 0;
                wildMargin2.Width = 0;
                captiveMargin1.Width = 0;
                captiveMargin2.Width = 0;
            }


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
                bool allFeildsValid = true;

                // Species
                if (wildSpeciesPicker.SelectedIndex == -1)
                {
                    wildSpeciesError.Text = "The species feild is required";
                    wildSpeciesError.IsVisible = true;
                    allFeildsValid = false;
                }
                else
                {
                    wildSpeciesError.IsVisible = false;
                }

                // Location
                if (Validate.FeildPopulated(wildLocationEntry.Text))
                {
                    
                    if (Validate.ContainsNumberOrSymbol(wildLocationEntry.Text))
                    {
                        wildLocationError.Text = "The location feild cannot contain numbers or symbols";
                        wildLocationError.IsVisible = true;
                        allFeildsValid = false;
                    }
                    else
                    {
                        wildLocationError.IsVisible = false;
                    }
                }
                else
                {
                    wildLocationError.Text = "The location feild is required";
                    wildLocationError.IsVisible = true;
                    allFeildsValid = false;
                }
                                 
                // Metal Band
                if (Validate.FeildPopulated(wildMetalBandIdEntry.Text))
                {
                    
                    if (Validate.ContainsLetterOrSymbol(wildMetalBandIdEntry.Text))
                    {
                        wildBandIdError.Text = "The metal band Id feild can not contain letters or symbols";
                        wildBandIdError.IsVisible = true;
                        allFeildsValid = false;
                    }
                    else
                    {
                        wildBandIdError.IsVisible = false;
                    }
                }

                // Bander Name
                if (Validate.FeildPopulated(wildBanderNameEntry.Text))
                {
                    wildBanderNameError.IsVisible = false;
                    if (Validate.ContainsNumberOrSymbol(wildBanderNameEntry.Text))
                    {
                        wildBanderNameError.Text = "The bander name feild can not contain numbers or symbols";
                        wildBanderNameError.IsVisible = true;
                        allFeildsValid = false;
                    }
                }

                if (allFeildsValid)
                {
                    // TODO Dynamic wild bird object 
                    // Insert record, return outcome
                    bool inserted = AddWildBird.InsertWildBirdDocumnet(new WildBird
                    {
                        WingspanId = GenerateWildWingspanId.NewId(),
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
                else
                {
                    allFeildsValid = true;
                }

                
                
            }
            else if (Title == "New Captive Bird")
            {
                bool allFeildsValid = true;

                // Validate 
                // Name
                if (Validate.FeildPopulated(captiveNameEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(captiveNameEntry.Text))
                    {
                        captiveNameError.IsVisible = true;
                        allFeildsValid = false;
                    }
                    else
                    {
                        captiveNameError.IsVisible = false;
                    }                
                }
                else
                {
                    captiveNameError.IsVisible = false;
                }

                // Band number
                if (Validate.FeildPopulated(captiveBandNumberEntry.Text))
                {                   
                    if (Validate.ContainsLetterOrSymbol(captiveBandNumberEntry.Text))
                    {
                        captiveBandNumberError.IsVisible = true;
                        allFeildsValid = false;
                    }
                    else
                    {
                        captiveBandNumberError.IsVisible = false;
                    }
                }
                else
                {
                    captiveBandNumberError.IsVisible = false;
                }

                // Species
                if (captiveSpeciesPicker.SelectedIndex == -1)
                {
                    captiveSpeciesError.IsVisible = true;
                    allFeildsValid = false;
                }

                // Sex
                if (captiveSexPicker.SelectedIndex == -1)
                {
                    captiveSexError.IsVisible = true;
                    allFeildsValid = false;
                }


                // Location
                if (Validate.FeildPopulated(captiveLocationEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(captiveLocationEntry.Text))
                    {
                        captiveLocationError.IsVisible = true;
                        allFeildsValid = false;
                    }
                }

                if (allFeildsValid)
                {
                    // Instantiate wingspan id here so both add bird and initial note functions can access it
                    string wingspanId = GenerateCaptiveWingspanId.NewId();

                    bool birdInserted = AddCaptiveBird.InsertCaptiveBirdDocument(new CaptiveBird
                    {
                        WingspanId = wingspanId,
                        Name = captiveNameEntry.Text,
                        BandNo = captiveBandNumberEntry.Text,
                        BandInfo = captiveBandDetailsEntry.Text,
                        Species = captiveSpeciesPicker.SelectedItem.ToString(),
                        Sex = captiveSexPicker.SelectedItem.ToString(),
                        Age = captiveAgePicker.SelectedItem.ToString(),
                        Location = captiveLocationEntry.Text,
                        DateArrived = wildDateBandedPicker.Date
                    });

                    if (birdInserted)
                    {
                        captiveNameEntry.Text = null;
                        captiveBandNumberEntry.Text = null;
                        captiveBandDetailsEntry.Text = null;
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
                        bool noteInserted = AddBirdNote.InsertNoteDocument(new Note
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
                    allFeildsValid = true;
                }

            }
            else
            {
                // TODO: Error message 
            }
            
        }       

    }
}