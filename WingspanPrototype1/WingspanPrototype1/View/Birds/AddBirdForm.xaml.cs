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
                addingIndicator.IsRunning = true;

                bool allFeildsValid = true;

                await Task.Run(() => {

                    // Species
                    if (wildSpeciesPicker.SelectedIndex == -1)
                    {
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            wildSpeciesError.Text = "The species feild is required";
                            wildSpeciesError.IsVisible = true;
                        });
                        
                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            wildSpeciesError.IsVisible = false;
                        });
                    }

                    // Location
                    if (Validate.FeildPopulated(wildLocationEntry.Text))
                    {
                        if (Validate.ContainsNumberOrSymbol(wildLocationEntry.Text))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                wildLocationError.Text = "The location feild cannot contain numbers or symbols";
                                wildLocationError.IsVisible = true;
                            });
                            allFeildsValid = false;
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                wildLocationError.IsVisible = false;
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            wildLocationError.Text = "The location feild is required";
                            wildLocationError.IsVisible = true;
                        });
                        allFeildsValid = false;
                    }

                    // Metal Band
                    if (Validate.FeildPopulated(wildMetalBandIdEntry.Text))
                    {

                        if (Validate.ContainsLetterOrSymbol(wildMetalBandIdEntry.Text))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                wildBandIdError.Text = "The metal band Id feild can not contain letters or symbols";
                                wildBandIdError.IsVisible = true;
                            });
                            allFeildsValid = false;
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                wildBandIdError.IsVisible = false;

                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            wildBandIdError.IsVisible = false;

                        });
                    }

                    // Bander Name
                    if (Validate.FeildPopulated(wildBanderNameEntry.Text))
                    {                        
                        if (Validate.ContainsNumberOrSymbol(wildBanderNameEntry.Text))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                wildBanderNameError.Text = "The bander name feild can not contain numbers or symbols";
                                wildBanderNameError.IsVisible = true;                               
                            });

                            allFeildsValid = false;
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                wildBanderNameError.IsVisible = false;
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            wildBanderNameError.IsVisible = false;
                        });
                    }

                    if (allFeildsValid)
                    {

                        string species = null;
                        string sex = null;
                        string age = null;

                        if (wildSpeciesPicker.SelectedIndex != -1) species = wildSpeciesPicker.SelectedItem.ToString();
                        if (wildAgePicker.SelectedIndex != -1) age = wildAgePicker.SelectedItem.ToString();
                        if (wildSexPicker.SelectedIndex != -1) sex = wildSexPicker.SelectedItem.ToString();

                        // Insert record, return outcome
                        bool inserted = AddWildBird.InsertWildBirdDocumnet(new WildBird
                        {
                            WingspanId = GenerateWildWingspanId.NewId(),
                            Species = species,
                            Location = wildLocationEntry.Text,
                            Age = age,
                            Sex = sex,
                            MetalBand = wildMetalBandIdEntry.Text,
                            BandInfo = wildBandInfoEntry.Text,
                            Gps = wildGpsEntry.Text,
                            DateBanded = wildDateBandedPicker.Date,
                            BanderName = wildBanderNameEntry.Text
                        });

                        // Run on interface thread
                        Device.BeginInvokeOnMainThread(() => 
                        {

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

                                DisplayAlert("Wild Bird Saved", "This bird has been saved in the database", "Ok");
                            }
                            else
                            {
                                DisplayAlert("Connenction Error", "Could not connect to database please check connection and try again", "OK");
                            }

                            addingIndicator.IsRunning = false;

                        });                      
                        
                    }
                    else
                    {
                        allFeildsValid = true;

                        Device.BeginInvokeOnMainThread(() => 
                        {
                            addingIndicator.IsRunning = false;
                        });

                    }

                });               
                
            }
            else if (Title == "New Captive Bird")
            {
                addingIndicator.IsRunning = true;

                bool allFeildsValid = true;

                await Task.Run(() => 
                {

                    // Validate 
                    // Name
                    if (Validate.FeildPopulated(captiveNameEntry.Text))
                    {
                        if (Validate.ContainsNumberOrSymbol(captiveNameEntry.Text))
                        {
                            Device.BeginInvokeOnMainThread(() => 
                            {
                                captiveNameError.IsVisible = true;
                            });
                            
                            allFeildsValid = false;

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() => 
                            {
                                captiveNameError.IsVisible = false;
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            captiveNameError.IsVisible = false;
                        });

                            
                    }

                    // Band number
                    if (Validate.FeildPopulated(captiveBandNumberEntry.Text))
                    {
                        if (Validate.ContainsLetterOrSymbol(captiveBandNumberEntry.Text))
                        {
                            Device.BeginInvokeOnMainThread(() => 
                            {
                                captiveBandNumberError.IsVisible = true;
                            });                            
                            allFeildsValid = false;
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveBandNumberError.IsVisible = false;
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            captiveBandNumberError.IsVisible = false;
                        });
                    }

                    // Species
                    if (captiveSpeciesPicker.SelectedIndex == -1)
                    {
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            captiveSpeciesError.IsVisible = true;
                        });                       
                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            captiveSpeciesError.IsVisible = false;
                        });
                    }

                    // Sex
                    if (captiveSexPicker.SelectedIndex == -1)
                    {
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            captiveSexError.IsVisible = true;
                        });
                            
                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            captiveSexError.IsVisible = false;
                        });
                    }


                    // Location
                    if (Validate.FeildPopulated(captiveLocationEntry.Text))
                    {
                        if (Validate.ContainsNumberOrSymbol(captiveLocationEntry.Text))
                        {
                            Device.BeginInvokeOnMainThread(() => 
                            {
                                captiveLocationError.IsVisible = true;
                            });                            
                            allFeildsValid = false;
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                captiveLocationError.IsVisible = false;
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            captiveLocationError.IsVisible = false;
                        });
                    }

                    if (allFeildsValid)
                    {
                        // Instantiate wingspan id here so both add bird and initial note functions can access it
                        string wingspanId = GenerateCaptiveWingspanId.NewId();

                        // Stores picker items, set default values
                        string speciesValue = null;
                        string sexValue = null;
                        string ageValue = null;

                        // Handle pickers first
                        if (captiveSpeciesPicker.SelectedIndex != -1) speciesValue = captiveSpeciesPicker.SelectedItem.ToString();
                        if (captiveSexPicker.SelectedIndex != -1) sexValue = captiveSexPicker.SelectedItem.ToString();
                        if (wildAgePicker.SelectedIndex != -1) ageValue = wildAgePicker.SelectedItem.ToString();


                        bool birdInserted = AddCaptiveBird.InsertCaptiveBirdDocument(new CaptiveBird
                        {
                            WingspanId = wingspanId,
                            Name = captiveNameEntry.Text,
                            BandNo = captiveBandNumberEntry.Text,
                            BandInfo = captiveBandDetailsEntry.Text,
                            Species = speciesValue,
                            Sex = sexValue,
                            Age = ageValue,
                            Location = captiveLocationEntry.Text,
                            DateArrived = wildDateBandedPicker.Date
                        });

                        // Run on interface thread
                        Device.BeginInvokeOnMainThread(() => 
                        {

                            if (birdInserted)
                            {
                                captiveNameEntry.Text = null;
                                captiveBandNumberEntry.Text = null;
                                captiveBandDetailsEntry.Text = null;
                                captiveSpeciesPicker.SelectedIndex = -1;
                                captiveSexPicker.SelectedIndex = -1;
                                captiveAgePicker.SelectedIndex = -1;
                                captiveLocationEntry.Text = null;

                                DisplayAlert("Captive Bird Saved", "This bird has been saved in the database", "Ok");
                            }
                            else
                            {
                                DisplayAlert("Connenction Error", "Could not insert bird record, please check connection and try again", "OK");
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
                                    DisplayAlert("Initial Note Saved", "An initial note for this bird has been saved in the database", "Ok");
                                    wildInitialNoteEditor.Text = null;
                                }
                                else
                                {
                                    DisplayAlert("Connenction Error", "Could not insert bird note, please check connection and try again", "OK");
                                }
                            }

                            addingIndicator.IsRunning = false;

                        });

                                                                                   
                    }
                    else
                    {
                        allFeildsValid = true;

                        Device.BeginInvokeOnMainThread(() => 
                        {
                            addingIndicator.IsRunning = false;
                        });

                        return;

                    }

                });

            }
            else
            {
                // TODO: Error message 
            }
            
        }       

    }
}