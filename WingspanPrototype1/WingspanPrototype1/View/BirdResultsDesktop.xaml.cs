using MongoDB.Bson;
using System;
using System.Collections;
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
    public partial class BirdResultsDesktop : ContentPage
    {

        public BirdResultsDesktop(ArrayList results)
        {
            InitializeComponent();          

            // Set result list item source to list of Bird objects
            ResultsListView.ItemsSource = results;

            // Set picker content
            noteCategoryPicker.ItemsSource = new string[] { "Medical", "Breeding", "Transfer" };
            locationCategoryPicker.ItemsSource = new string[] { "Release", "Transfer" };

        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem;

            if (item.GetType() == typeof(WildBird))
            {
                DisplayWildBird(item as WildBird);
            }
            else
            {
                DisplayCaptiveBird(item as CaptiveBird);
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
            }
            else
            {
                wildWingspanIdEntry.IsVisible = true;
            }

            // Set Species Value 
            if (bird.Species != string.Empty)
            {
                wildSpeciesValueLabel.Text = bird.Species;
                wildSpeciesStack.IsVisible = true;
            }
            else
            {
                wildSpeciesPicker.IsVisible = true;

            }

            // Set Location Value 
            if (bird.Location!= string.Empty)
            {
                wildLocationValueLabel.Text = bird.Location;
                wildLocationStack.IsVisible = true;
            }
            else
            {
                wildLocationEntry.IsVisible = true;
            }

            // Set GPS Value
            if (bird.Gps != string.Empty)
            {
                wildGpsValueLabel.Text = bird.Gps;
                wildGpsStack.IsVisible = true;
            }
            else
            {
                wildGpsEntry.IsVisible = true;
            }

            // Set Sex Value
            if (bird.Sex != string.Empty)
            {
                wildSexValueLabel.Text = bird.Sex;
                wildSexStack.IsVisible = true;
            }
            else
            {
                wildSexPicker.IsVisible = true;
            }

            // Set Age Value
            if (bird.Age != string.Empty)
            {
                wildAgeValueLabel.Text = bird.Age;
                wildAgeStack.IsVisible = true;
            }
            else
            {
                wildAgePicker.IsVisible = true;
            }

            // Set Metal Band Value
            if (bird.MetalBandId != string.Empty)
            {
                wildMetalBandIdValueLabel.Text = bird.MetalBandId;
                wildMetalBandStack.IsVisible = true;
            }
            else
            {
                wildMetalBandIdEntry.IsVisible = true;
            }

            // Set Date Banded Value 
            if (bird.DateBanded.ToString() != string.Empty)
            {
                wildDateBandedValueLabel.Text = bird.DateBanded.ToString();
                wildDateBandedStack.IsVisible = true;
            }
            else
            {
                wildDateBandedPicker.IsVisible = true;
            }

            // Set Bander Name Value 
            if (bird.BanderName != string.Empty)
            {
                wildBanderNameValueLabel.Text = bird.BanderName;
                wildBanderNameStack.IsVisible = true;
            }
            else
            {
                wildBanderNameEntry.IsVisible = true;
            }

        }

        // Display captive bird content view
        public void DisplayCaptiveBird(CaptiveBird bird)
        {
            // If Wild bird is displayed hide wild bird form
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
            }
            else
            {
                captiveWingspanIdEntry.IsVisible = true;
            }

            // Set Name Value
            if (bird.Name != string.Empty)
            {
                captiveNameValueLabel.Text = bird.Name;
                captiveNameStack.IsVisible = true;
            }
            else
            {
                captiveNameEntry.IsVisible = true;
            }

            // Set Band Number Value
            if (bird.BandNo != string.Empty)
            {
                captiveBandNumberValueLabel.Text = bird.BandNo;
                captiveBandNumberStack.IsVisible = true;
            }
            else
            {
                captiveBandNumberEntry.IsVisible = true;
            }

            // Set Band Colour Value 
            if ((bird.BandColour != string.Empty) && (bird.BandColour != "") && (bird.BandColour != null))
            {
                captiveBandColourValueLabel.Text = bird.BandColour;
                captiveBandColourStack.IsVisible = true;
            }
            else
            {
                captiveBandColourPicker.IsVisible = true;
            }

            // Set Species Value 
            if (bird.Species != string.Empty)
            {
                captiveSpeciesValueLabel.Text = bird.Species;
                captiveSpeciesStack.IsVisible = true;
            }
            else
            {
                captiveSpeciesPicker.IsVisible = true;
            }

            // Set Sex Value 
            if (bird.Sex != string.Empty)
            {
                captiveSexValueLabel.Text = bird.Sex;
                captiveSexStack.IsVisible = true;
            }
            else
            {
                captiveSexPicker.IsVisible = true;
            }

            // Set Age Value 
            if (bird.Age != string.Empty)
            {
                captiveAgeValueLabel.Text = bird.Age;
                captiveAgeStack.IsVisible = true;
            }
            else
            {
                captiveAgePicker.IsVisible = true;
            }

            // Set Location Value 
            if (bird.Location != string.Empty)
            {
                captiveLocationValueLabel.Text = bird.Location;
                captiveLocationStack.IsVisible = true;
            }
            else
            {
                captiveLocationEntry.IsVisible = true;
            }

            // Set Date Arrived Value 
            if (bird.DateArrived.ToString() != "1/01/0001 12:00:00 AM")
            {
                captiveDateArrivedValueLabel.Text = bird.DateArrived.ToString();
                captiveDateArrivedStack.IsVisible = true;
            }
            else
            {
                captiveDateArrivedPicker.IsVisible = true;
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
            if (bird.Result != string.Empty)
            {
                captiveResultValueLabel.Text = bird.Result.ToString();
                captiveResultStack.IsVisible = true;
            }
            else
            {
                captiveResultEntry.IsVisible = true;
            }

        }

        // Note popup boxes
        private void NoteButton_Clicked(object sender, EventArgs e)
        {
            noteHistoryView.IsVisible = true;

            noteListView.ItemsSource = new List<BirdNoteItem> { new BirdNoteItem {Date = DateTime.Today.ToString(), Category = "Transfer", Note = "Transfrerred to new site" },
                new BirdNoteItem {Date = DateTime.Today.ToString(), Category = "Medical", Note = "Broken wing" }
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

            locationListView.ItemsSource = new List<LocationItem> { new LocationItem {Date = DateTime.Today.ToString(), Category = "Transfer", Location = "Transfrerred to new site" },
                new LocationItem {Date = DateTime.Today.ToString(), Category = "Release", Location = "Released over new site" }
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
                await DisplayAlert("Bird Saved", "Your changes have been saved", "Ok");
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Are you sure", "Would you like to delete this bird", "Yes", "No");
            if (result == true)
            {
                await DisplayAlert("Bird Deleted", "This bird has been removed from the database", "Ok");
            }
        }

       
    }
}