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

                addButton.IsEnabled = false;

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
                    if (!Validate.FeildPopulated(wildLocationEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            wildLocationError.Text = "The location feild is required";
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


                        WildBird wildBird = new WildBird();

                        wildBird.WingspanId = GenerateWildWingspanId.NewId();

                        if (wildBird.WingspanId == null)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DisplayAlert("Connenction Error", "Could not generate wingspan Id, please check connection and try again", "OK");
                            });
                        }

                        if (wildSpeciesPicker.SelectedIndex != -1) wildBird.Species = wildSpeciesPicker.SelectedItem.ToString();
                        if (wildAgePicker.SelectedIndex != -1) wildBird.Age = wildAgePicker.SelectedItem.ToString();
                        if (wildSexPicker.SelectedIndex != -1) wildBird.Sex = wildSexPicker.SelectedItem.ToString();

                        wildBird.Name = wildNameEntry.Text;
                        wildBird.Location = wildLocationEntry.Text;
                        wildBird.MetalBand = wildMetalBandIdEntry.Text;
                        wildBird.BandInfo = wildBandInfoEntry.Text;
                        wildBird.Gps = wildGpsEntry.Text;                       
                        wildBird.BanderName = wildBanderNameEntry.Text;

                        if (dateBandedCheck.IsChecked) wildBird.DateBanded = wildDateBandedPicker.Date;


                        // Insert record, return outcome
                        bool inserted = AddWildBird.InsertWildBirdDocumnet(wildBird);

                        // Run on interface thread
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            // Was the record inserted successfully?
                            if (inserted)
                            {
                                // Clear feilds
                                wildNameEntry.Text = null;
                                wildSpeciesPicker.SelectedIndex = -1;
                                wildLocationEntry.Text = null;
                                wildAgePicker.SelectedIndex = -1;
                                wildSexPicker.SelectedIndex = -1;
                                wildMetalBandIdEntry.Text = null;
                                wildBandInfoEntry.Text = null;
                                wildGpsEntry.Text = null;
                                wildBanderNameEntry.Text = null;
                                dateBandedCheck.IsChecked = false;
                                wildDateBandedPicker.IsEnabled = false;

                                DisplayAlert("Wild Bird Added", "This bird has been saved in the database", "OK");
                            }
                            else
                            {
                                DisplayAlert("Connenction Error", "Could not insert bird record, please check connection and try again", "OK");
                            }

                            addingIndicator.IsRunning = false;
                            addButton.IsEnabled = true;

                        });                      
                        
                    }
                    else
                    {
                        allFeildsValid = true;

                        Device.BeginInvokeOnMainThread(() => 
                        {
                            addingIndicator.IsRunning = false;
                            addButton.IsEnabled = true;
                        });

                    }

                });               
                
            }
            else if (Title == "New Captive Bird")
            {
                addingIndicator.IsRunning = true;
                addButton.IsEnabled = false;

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

                    if (allFeildsValid)
                    {
                        // Instantiate wingspan id here so both add bird and initial note functions can access it
                        string wingspanId = GenerateCaptiveWingspanId.NewId();

                        if (wingspanId == null)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                DisplayAlert("Connenction Error", "Could not generate wingspan Id, please check connection and try again", "OK");
                            });
                        }

                        // Stores picker items, set default values
                        string speciesValue = null;
                        string sexValue = null;
                        string ageValue = null;

                        // Handle pickers first
                        if (captiveSpeciesPicker.SelectedIndex != -1) speciesValue = captiveSpeciesPicker.SelectedItem.ToString();
                        if (captiveSexPicker.SelectedIndex != -1) sexValue = captiveSexPicker.SelectedItem.ToString();
                        if (captiveAgePicker.SelectedIndex != -1) ageValue = captiveAgePicker.SelectedItem.ToString();


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

                                DisplayAlert("Captive Bird Added", "This bird has been saved in the database", "Ok");
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
                            addButton.IsEnabled = true;

                        });

                                                                                   
                    }
                    else
                    {
                        allFeildsValid = true;

                        Device.BeginInvokeOnMainThread(() => 
                        {
                            addingIndicator.IsRunning = false;
                            addButton.IsEnabled = true;
                        });

                        return;

                    }

                });

            }
            
        }

        private void dateBandedCheck_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (dateBandedCheck.IsChecked)
            {
                wildDateBandedPicker.IsEnabled = true;
            }
            else
            {
                wildDateBandedPicker.IsEnabled = false;
            }
        }
    }
}