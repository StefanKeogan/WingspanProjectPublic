using MongoDB.Bson;
using System;
using System.Collections;
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
    public partial class BirdResultsDesktop : ContentPage
    {
        private ObjectId id;
        private List<Entry> entries;
        private List<Picker> pickers;
        private Type birdType;
444
        public BirdResultsDesktop(ArrayList results)
        {
            InitializeComponent();          

            // Set result list item source to list of Bird objects
            resultsListView.ItemsSource = results;
        
            // Set picker content
            noteCategoryPicker.ItemsSource = new string[] { "Medical", "Breeding", "Transfer" };
            locationCategoryPicker.ItemsSource = new string[] { "Release", "Transfer" };

        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem;

            if (item.GetType() == typeof(WildBird))
            {
                var wildItem = item as WildBird;

                DisplayWildBird(wildItem);

                id = wildItem._id;

                birdType = typeof(WildBird);
            }
            else if (item.GetType() == typeof(CaptiveBird))
            {
                var captiveItem = item as CaptiveBird;

                DisplayCaptiveBird(captiveItem);

                id = captiveItem._id;

                birdType = typeof(CaptiveBird);
            }
        }

        // Display wild bird content view
        public void DisplayWildBird(WildBird bird)
        {
            if (captiveBirdDisplayForm.IsVisible == true)
            {
                captiveBirdDisplayForm.IsVisible = false;
            }

            // Hide location history button
            locationButton.IsVisible = false;

            // Display wild bird form
            wildBirdDisplayForm.IsVisible = true;

            // Determine which feilds are populated, if populated display the value else display an entry, picker etc.

            // Set Wingspan Id Value
            if (bird.WingspanId != string.Empty)
            {
                wildWingspanIdValueLabel.Text = bird.WingspanId;
                wildWingspanIdStack.IsVisible = true;
                wildWingspanIdEntry.IsVisible = false;

            }
            else
            {
                wildWingspanIdEntry.IsVisible = true;
                wildWingspanIdStack.IsVisible = false;
            }

            // Set Species Value 
            if (bird.Species != string.Empty)
            {
                wildSpeciesValueLabel.Text = bird.Species;
                wildSpeciesStack.IsVisible = true;
                wildSpeciesPicker.IsVisible = false;

            }
            else
            {
                wildSpeciesPicker.IsVisible = true;              
                wildSpeciesStack.IsVisible = false;

            }

            // Set Location Value 
            if (bird.Location!= string.Empty)
            {
                wildLocationValueLabel.Text = bird.Location;
                wildLocationStack.IsVisible = true;
                wildLocationEntry.IsVisible = false;

            }
            else
            {
                wildLocationEntry.IsVisible = true;
                wildLocationStack.IsVisible = false;

            }

            // Set GPS Value
            if (bird.Gps != string.Empty)
            {
                wildGpsValueLabel.Text = bird.Gps;
                wildGpsStack.IsVisible = true;
                wildGpsEntry.IsVisible = false;

            }
            else
            {
                wildGpsEntry.IsVisible = true;
                wildGpsStack.IsVisible = false;

            }

            // Set Sex Value
            if (bird.Sex != string.Empty)
            {
                wildSexValueLabel.Text = bird.Sex;
                wildSexStack.IsVisible = true;
                wildSexPicker.IsVisible = false;

            }
            else
            {
                wildSexPicker.IsVisible = true;
                wildSexStack.IsVisible = false;

            }

            // Set Age Value
            if (bird.Age != string.Empty)
            {
                wildAgeValueLabel.Text = bird.Age;
                wildAgeStack.IsVisible = true;
                wildAgePicker.IsVisible = false;

            }
            else
            {
                wildAgePicker.IsVisible = true;
                wildAgeStack.IsVisible = false;

            }

            // Set Metal Band Value
            if (bird.MetalBand != string.Empty)
            {
                wildMetalBandIdValueLabel.Text = bird.MetalBand;
                wildMetalBandStack.IsVisible = true;
                wildMetalBandIdEntry.IsVisible = false;

            }
            else
            {
                wildMetalBandIdEntry.IsVisible = true;
                wildMetalBandStack.IsVisible = false;

            }

            // Set Band Info Value
            if (bird.BandInfo != string.Empty)
            {
                wildMetalBandIdEditButton.Text = bird.MetalBand;
                wildMetalBandStack.IsVisible = true;
                wildMetalBandIdEntry.IsVisible = false;

            }
            else
            {
                wildMetalBandIdEntry.IsVisible = true;
                wildMetalBandStack.IsVisible = false;

            }

            // Set Date Banded Value 
            if (bird.DateBanded.ToString() != string.Empty)
            {
                wildDateBandedValueLabel.Text = bird.DateBanded.ToString();
                wildDateBandedStack.IsVisible = true;
                wildDateBandedPicker.IsVisible = false;

            }
            else
            {
                wildDateBandedPicker.IsVisible = true;
                wildDateBandedStack.IsVisible = false;

            }

            // Set Bander Name Value 
            if (bird.BanderName != string.Empty)
            {
                wildBanderNameValueLabel.Text = bird.BanderName;
                wildBanderNameStack.IsVisible = true;
                wildBanderNameEntry.IsVisible = false;

            }
            else
            {
                wildBanderNameEntry.IsVisible = true;
                wildBanderNameStack.IsVisible = false;

            }

        }

        // Display captive bird content view
        public void DisplayCaptiveBird(CaptiveBird bird)
        {
            // If wild bird is displayed hide wild bird form
            if (wildBirdDisplayForm.IsVisible == true)
            {
                wildBirdDisplayForm.IsVisible = false;
            }

            // Display location history button
            locationButton.IsVisible = true;

            // Display captive bird form
            captiveBirdDisplayForm.IsVisible = true;

            // Set Wingspan Id value
            if (bird.WingspanId != string.Empty)
            {
                captiveWingspanIdValueLabel.Text = bird.WingspanId;
                captiveWingspanIdStack.IsVisible = true;
                captiveWingspanIdEntry.IsVisible = false;

            }
            else
            {
                captiveWingspanIdEntry.IsVisible = true;
                captiveWingspanIdStack.IsVisible = false;

            }

            // Set Name Value
            if (bird.Name != string.Empty)
            {
                captiveNameValueLabel.Text = bird.Name;
                captiveNameStack.IsVisible = true;
                captiveNameEntry.IsVisible = false;

            }
            else
            {
                captiveNameEntry.IsVisible = true;
                captiveNameStack.IsVisible = false;

            }

            // Set Band Number Value
            if (bird.BandNo != string.Empty)
            {
                captiveBandNumberValueLabel.Text = bird.BandNo;
                captiveBandNumberStack.IsVisible = true;
                captiveBandNumberEntry.IsVisible = false;

            }
            else
            {
                captiveBandNumberEntry.IsVisible = true;
                captiveBandNumberStack.IsVisible = false;

            }

            // Set Band Colour Value 
            if ((bird.BandColour != string.Empty) && (bird.BandColour != "") && (bird.BandColour != null))
            {
                captiveBandColourValueLabel.Text = bird.BandColour;
                captiveBandColourStack.IsVisible = true;
                captiveBandColourPicker.IsVisible = false;

            }
            else
            {
                captiveBandColourPicker.IsVisible = true;
                captiveBandColourStack.IsVisible = false;

            }

            // Set Species Value 
            if (bird.Species != string.Empty)
            {
                captiveSpeciesValueLabel.Text = bird.Species;
                captiveSpeciesStack.IsVisible = true;
                captiveSpeciesPicker.IsVisible = false;

            }
            else
            {
                captiveSpeciesPicker.IsVisible = true;
                captiveSpeciesStack.IsVisible = false;

            }

            // Set Sex Value 
            if (bird.Sex != string.Empty)
            {
                captiveSexValueLabel.Text = bird.Sex;
                captiveSexStack.IsVisible = true;
                captiveSexPicker.IsVisible = false;

            }
            else
            {
                captiveSexPicker.IsVisible = true;
                captiveSexStack.IsVisible = false;

            }

            // Set Age Value 
            if (bird.Age != string.Empty)
            {
                captiveAgeValueLabel.Text = bird.Age;
                captiveAgeStack.IsVisible = true;
                captiveAgePicker.IsVisible = false;

            }
            else
            {
                captiveAgePicker.IsVisible = true;
                captiveAgeStack.IsVisible = false;

            }

            // Set Location Value 
            if (bird.Location != string.Empty)
            {
                captiveLocationValueLabel.Text = bird.Location;
                captiveLocationStack.IsVisible = true;
                captiveLocationEntry.IsVisible = false;

            }
            else
            {
                captiveLocationEntry.IsVisible = true;
                captiveLocationStack.IsVisible = false;

            }

            // Set Date Arrived Value 
            if (bird.DateArrived.ToString() != "1/01/0001 12:00:00 AM")
            {
                captiveDateArrivedValueLabel.Text = bird.DateArrived.ToString();
                captiveDateArrivedStack.IsVisible = true;
                captiveDateArrivedPicker.IsVisible = false;

            }
            else
            {
                captiveDateArrivedPicker.IsVisible = true;
                captiveDateArrivedStack.IsVisible = false;

            }

            // Set Date Departed Value 
            if (bird.DateDeparted.ToString() != "1/01/0001 12:00:00 AM")
            {
                captiveDateDepartedValueLabel.Text = bird.DateDeparted.ToString();
                captiveDateDepartedStack.IsVisible = true;
                captiveDateDepartedPicker.IsVisible = false;
            }
            else
            {
                captiveDateDepartedPicker.IsVisible = true;
                captiveDateDepartedStack.IsVisible = false;

            }

            // Set Date Deceased Value 
            if (bird.DateDeceased.ToString() != "1/01/0001 12:00:00 AM")
            {
                captiveDateDeceasedValueLabel.Text = bird.DateDeceased.ToString();
                captiveDatedeceasedStack.IsVisible = true;
                captiveDateDeceasedPicker.IsVisible = false;
            }
            else
            {
                captiveDateDeceasedPicker.IsVisible = true;
                captiveDatedeceasedStack.IsVisible = false;

            }

            // Set Result Value 
            if (Validate.FeildPopulated(bird.Result))
            {
                captiveResultValueLabel.Text = bird.Result;
                captiveResultStack.IsVisible = true;
                captiveResultEntry.IsVisible = false;

            }
            else
            {
                captiveResultEntry.IsVisible = true;
                captiveResultStack.IsVisible = false;

            }

        }

        // Note popup boxes
        private void NoteButton_Clicked(object sender, EventArgs e)
        {
            noteHistoryView.IsVisible = true;

            noteListView.ItemsSource = new List<Note> { new Note {Date = DateTime.Today, Category = "Transfer", Comment = "Transfrerred to new site" },
                new Note {Date = DateTime.Today, Category = "Medical", Comment = "Broken wing" }
            };
        }

        private void AddNewNoteButton_Clicked(object sender, EventArgs e)
        {
            addNewNoteView.IsVisible = true;

        }

        private void NoteExitButton_Clicked(object sender, EventArgs e)
        {
            noteHistoryView.IsVisible = false;
        }

        private void AddNoteButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Note added", "Note has been added to this birds note history", "Ok");
            addNewNoteView.IsVisible = false;
        }

        private void AddNewNoteExitButton_Clicked(object sender, EventArgs e)
        {
            addNewNoteView.IsVisible = false;
        }

        //Location popup boxes
        private void LocationButton_Clicked(object sender, EventArgs e)
        {
            locationHistoryView.IsVisible = true;

            locationListView.ItemsSource = new List<LocationHistory> { new LocationHistory {Date = DateTime.Today.ToString(), Category = "Transfer", Location = "Transfrerred to new site" },
                new LocationHistory {Date = DateTime.Today.ToString(), Category = "Release", Location = "Released over new site" }
            };
        }

        private void AddNewLocationButton_Clicked(object sender, EventArgs e)
        {
            addNewLocationView.IsVisible = true;
        }

        private void AddLocationButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Location added", "Location has been added to this birds note history", "Ok");
            addNewLocationView.IsVisible = false;
        }

        private void LocationExitButton_Clicked(object sender, EventArgs e)
        {
            locationHistoryView.IsVisible = false;
        }

        private void AddNewLocationExitButton_Clicked(object sender, EventArgs e)
        {
            addNewLocationView.IsVisible = false;
        }

        // Save and delete button click events
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Are you sure", "Would you like to save these changes", "Yes", "No");
            if (result == true)
            {
                if (birdType == typeof(WildBird))
                {
                    if (UpdateWildBird.UpdateDocument(id, entries, pickers))
                    {
                        await DisplayAlert("Bird Saved", "Your changes have been saved", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Connection Error", "Could not connect to database, please check your connection and try again", "OK");
                    }
                }
                else if (birdType == typeof(CaptiveBird))
                {
                    if (UpdateCaptiveBird.UpdateDocument(id, entries, pickers))
                    {
                        await DisplayAlert("Bird Saved", "Your changes have been saved", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Connection Error", "Could not connect to database, please check your connection and try again", "OK");
                    }
                }

                
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Are you sure", "Would you like to delete this bird", "Yes", "No");
            if (result == true)
            {
                await DisplayAlert("Bird Deleted", "This bird has been removed from the database", "OK");
            }
        }

       
    }
}