﻿using MongoDB.Bson;
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
        private List<Entry> entries = new List<Entry>();
        private List<Picker> pickers = new List<Picker>();
        private Type birdType;

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
            if (Validate.FeildPopulated(bird.WingspanId))
            {
                wildWingspanIdValueLabel.Text = bird.WingspanId;
                wildWingspanIdStack.IsVisible = true;
                wildWingspanIdEntry.IsVisible = false;

            }
            else
            {
                wildWingspanIdEntry.IsVisible = true;
                wildWingspanIdStack.IsVisible = false;
                entries.Add(wildWingspanIdEntry);
            }

            // Set Species Value 
            if (Validate.FeildPopulated(bird.Species))
            {
                wildSpeciesValueLabel.Text = bird.Species;
                wildSpeciesStack.IsVisible = true;
                wildSpeciesPicker.IsVisible = false;

            }
            else
            {
                wildSpeciesPicker.IsVisible = true;              
                wildSpeciesStack.IsVisible = false;
                pickers.Add(wildSpeciesPicker);
            }

            // Set Location Value 
            if (Validate.FeildPopulated(bird.Location))
            {
                wildLocationValueLabel.Text = bird.Location;
                wildLocationStack.IsVisible = true;
                wildLocationEntry.IsVisible = false;

            }
            else
            {
                wildLocationEntry.IsVisible = true;
                wildLocationStack.IsVisible = false;
                entries.Add(wildLocationEntry);
            }

            // Set GPS Value
            if (Validate.FeildPopulated(bird.Gps))
            {
                wildGpsValueLabel.Text = bird.Gps;
                wildGpsStack.IsVisible = true;
                wildGpsEntry.IsVisible = false;

            }
            else
            {
                wildGpsEntry.IsVisible = true;
                wildGpsStack.IsVisible = false;
                entries.Add(wildGpsEntry);

            }

            // Set Sex Value
            if (Validate.FeildPopulated(bird.Sex))
            {
                wildSexValueLabel.Text = bird.Sex;
                wildSexStack.IsVisible = true;
                wildSexPicker.IsVisible = false;

            }
            else
            {
                wildSexPicker.IsVisible = true;
                wildSexStack.IsVisible = false;
                pickers.Add(wildSexPicker);
            }

            // Set Age Value
            if (Validate.FeildPopulated(bird.Age))
            {
                wildAgeValueLabel.Text = bird.Age;
                wildAgeStack.IsVisible = true;
                wildAgePicker.IsVisible = false;

            }
            else
            {
                wildAgePicker.IsVisible = true;
                wildAgeStack.IsVisible = false;
                pickers.Add(wildAgePicker);
            }

            // Set Metal Band Value
            if (Validate.FeildPopulated(bird.MetalBand))
            {
                wildMetalBandIdValueLabel.Text = bird.MetalBand;
                wildMetalBandStack.IsVisible = true;
                wildMetalBandIdEntry.IsVisible = false;

            }
            else
            {
                wildMetalBandIdEntry.IsVisible = true;
                wildMetalBandStack.IsVisible = false;
                entries.Add(wildMetalBandIdEntry);
            }

            // Set Band Info Value
            if (Validate.FeildPopulated(bird.BandInfo))
            {
                wildMetalBandIdEditButton.Text = bird.MetalBand;
                wildMetalBandStack.IsVisible = true;
                wildMetalBandIdEntry.IsVisible = false;

            }
            else
            {
                wildMetalBandIdEntry.IsVisible = true;
                wildMetalBandStack.IsVisible = false;
                entries.Add(wildMetalBandIdEntry);
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
            if (Validate.FeildPopulated(bird.BanderName))
            {
                wildBanderNameValueLabel.Text = bird.BanderName;
                wildBanderNameStack.IsVisible = true;
                wildBanderNameEntry.IsVisible = false;

            }
            else
            {
                wildBanderNameEntry.IsVisible = true;
                wildBanderNameStack.IsVisible = false;
                entries.Add(wildBanderNameEntry);
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
            if (Validate.FeildPopulated(bird.WingspanId))
            {
                captiveWingspanIdValueLabel.Text = bird.WingspanId;
                captiveWingspanIdStack.IsVisible = true;
                captiveWingspanIdEntry.IsVisible = false;

            }
            else
            {
                captiveWingspanIdEntry.IsVisible = true;
                captiveWingspanIdStack.IsVisible = false;
                entries.Add(captiveWingspanIdEntry);
            }

            // Set Name Value
            if (Validate.FeildPopulated(bird.Name))
            {
                captiveNameValueLabel.Text = bird.Name;
                captiveNameStack.IsVisible = true;
                captiveNameEntry.IsVisible = false;

            }
            else
            {
                captiveNameEntry.IsVisible = true;
                captiveNameStack.IsVisible = false;
                entries.Add(captiveNameEntry);
            }

            // Set Band Number Value
            if (Validate.FeildPopulated(bird.BandNo))
            {
                captiveBandNumberValueLabel.Text = bird.BandNo;
                captiveBandNumberStack.IsVisible = true;
                captiveBandNumberEntry.IsVisible = false;

            }
            else
            {
                captiveBandNumberEntry.IsVisible = true;
                captiveBandNumberStack.IsVisible = false;
                entries.Add(captiveBandNumberEntry);
            }

            // Set Band Colour Value 
            if (Validate.FeildPopulated(bird.BandColour))
            {
                captiveBandColourValueLabel.Text = bird.BandColour;
                captiveBandColourStack.IsVisible = true;
                captiveBandColourPicker.IsVisible = false;

            }
            else
            {
                captiveBandColourPicker.IsVisible = true;
                captiveBandColourStack.IsVisible = false;
                pickers.Add(captiveBandColourPicker);
            }

            // Set Species Value 
            if (Validate.FeildPopulated(bird.Species))
            {
                captiveSpeciesValueLabel.Text = bird.Species;
                captiveSpeciesStack.IsVisible = true;
                captiveSpeciesPicker.IsVisible = false;

            }
            else
            {
                captiveSpeciesPicker.IsVisible = true;
                captiveSpeciesStack.IsVisible = false;
                pickers.Add(captiveSpeciesPicker);
            }

            // Set Sex Value 
            if (Validate.FeildPopulated(bird.Sex))
            {
                captiveSexValueLabel.Text = bird.Sex;
                captiveSexStack.IsVisible = true;
                captiveSexPicker.IsVisible = false;

            }
            else
            {
                captiveSexPicker.IsVisible = true;
                captiveSexStack.IsVisible = false;
                pickers.Add(captiveSexPicker);
            }

            // Set Age Value 
            if (Validate.FeildPopulated(bird.Age))
            {
                captiveAgeValueLabel.Text = bird.Age;
                captiveAgeStack.IsVisible = true;
                captiveAgePicker.IsVisible = false;

            }
            else
            {
                captiveAgePicker.IsVisible = true;
                captiveAgeStack.IsVisible = false;
                pickers.Add(captiveAgePicker);
            }

            // Set Location Value 
            if (Validate.FeildPopulated(bird.Location))
            {
                captiveLocationValueLabel.Text = bird.Location;
                captiveLocationStack.IsVisible = true;
                captiveLocationEntry.IsVisible = false;

            }
            else
            {
                captiveLocationEntry.IsVisible = true;
                captiveLocationStack.IsVisible = false;
                entries.Add(captiveLocationEntry);
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
                entries.Add(captiveResultEntry);
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
                if (birdType == typeof(WildBird))
                {
                    if (DeleteWildBird.DropDocument(id))
                    {
                        await DisplayAlert("Bird Deleted", "This wild bird and location history have been removed from the database", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Connection Error", "Please check your connection and try again", "OK");
                        await Navigation.PopAsync();
                    }
                }
                else if (birdType == typeof(CaptiveBird))
                {
                    if (DeleteCaptiveBird.DropDocument(id))
                    {
                        await DisplayAlert("Bird Deleted", "This captive bird, their notes and location history have been removed from the database", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Connection Error", "Please check your connection and try again", "OK");
                        
                    }
                }


            }
        }

       
    }
}