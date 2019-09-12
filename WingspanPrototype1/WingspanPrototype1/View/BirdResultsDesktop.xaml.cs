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

        public BirdResultsDesktop(ArrayList results, Type birdType)
        {
            InitializeComponent();          

            // Set result list item source to list of Bird objects
            ResultsListView.ItemsSource = results;

            // Are we displaying a captive or wild bird
            if (birdType == typeof(WildBird))
            {
                ResultsListView.ItemSelected += ResultsListView_ItemSelected_Wild;
                locationButton.IsVisible = false;
            }
            else
            {
                ResultsListView.ItemSelected += ResultsListView_ItemSelected_Captive;
            }

            // Set picker content
            noteCategoryPicker.ItemsSource = new string[] { "Medical", "Breeding", "Transfer" };
            locationCategoryPicker.ItemsSource = new string[] { "Release", "Transfer" };

        }

        public void ResultsListView_ItemSelected_Wild(object sender, SelectedItemChangedEventArgs e)
        {
            
            var item = e.SelectedItem as WildBird;

            List<BirdResultsItem> resultItems = new List<BirdResultsItem>();
            resultItems.Add(new BirdResultsItem { KeyLabelName = "wingspanIdLabel", KeyLabelText = "Wingspan Id: ", ValueLabelName = "wingspanIdValueLabel", ValueLabelText = item.WingspanId });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "speciesLabel", KeyLabelText = "Species: ", ValueLabelName = "speciesValueLabel", ValueLabelText = item.Species });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "locationLabel", KeyLabelText = "Location: ", ValueLabelName = "locationValueLabel", ValueLabelText = item.Location }); ;
            resultItems.Add(new BirdResultsItem { KeyLabelName = "gpsLabel", KeyLabelText = "GPS: ", ValueLabelName = "gpsValueLabel", ValueLabelText = item.Gps });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "sexLabel", KeyLabelText = "Sex: ", ValueLabelName = "sexValueLabel", ValueLabelText = item.Sex });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "ageLabel", KeyLabelText = "Age: ", ValueLabelName = "ageValueLabel" , ValueLabelText = item.Age});
            resultItems.Add(new BirdResultsItem { KeyLabelName = "metalBandIdLabel", KeyLabelText = "Metal Band Id: ", ValueLabelName = "metalBandIdValueLabel", ValueLabelText = item.MetalBandId });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "dateBandedLabel", KeyLabelText = "Date Banded: ", ValueLabelName = "dateBandedValueLabel", ValueLabelText = item.DateBanded.ToString() }); 
            resultItems.Add(new BirdResultsItem { KeyLabelName = "banderNameLabel", KeyLabelText = "Bander Name: ", ValueLabelName = "banderValueLabel" , ValueLabelText = item.BanderName});

            AddGridChildren(resultItems);

        }

        public void ResultsListView_ItemSelected_Captive(object sender, SelectedItemChangedEventArgs e)
        {

            var item = e.SelectedItem as CaptiveBird;

            List<BirdResultsItem> resultItems = new List<BirdResultsItem>();
            resultItems.Add(new BirdResultsItem { KeyLabelName = "wingspanIdLabel", KeyLabelText = "Wingspan Id: ", ValueLabelName = "wingspanIdValueLabel", ValueLabelText = item.WingspanId });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "nameLabel", KeyLabelText = "Name: ", ValueLabelName = "nameValueLabel", ValueLabelText = item.Name });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "bandNoLabel", KeyLabelText = "Band Number: ", ValueLabelName = "bandNoValueLabel", ValueLabelText = item.BandNo }); ;
            resultItems.Add(new BirdResultsItem { KeyLabelName = "bandColourLabel", KeyLabelText = "Band Colour: ", ValueLabelName = "bandColourValueLabel", ValueLabelText = item.BandColour });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "speciesLabel", KeyLabelText = "Species: ", ValueLabelName = "speciesValueLabel", ValueLabelText = item.Species });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "sexLabel", KeyLabelText = "Sex: ", ValueLabelName = "sexValueLabel", ValueLabelText = item.Sex });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "ageLabel", KeyLabelText = "Age: ", ValueLabelName = "ageValueLabel", ValueLabelText = item.Age });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "locationLabel", KeyLabelText = "Location: ", ValueLabelName = "locationValueLabel", ValueLabelText = item.Location });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "dateArrivedLabel", KeyLabelText = "Date Arrived: ", ValueLabelName = "dateArrivedValueLabel", ValueLabelText = item.DateArrived.ToString() });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "dateDepartedLabel", KeyLabelText = "Date Departed: ", ValueLabelName = "dateDepartedValueLabel", ValueLabelText = item.DateDeparted.ToString() });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "dateDeceasedLabel", KeyLabelText = "Date Deceased: ", ValueLabelName = "dateDeceasedValueLabel", ValueLabelText = item.DateDeceased.ToString() });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "resultLabel", KeyLabelText = "Result: ", ValueLabelName = "resultValueLabel", ValueLabelText = item.Result });

            AddGridChildren(resultItems);

        }

        public void AddGridChildren(List<BirdResultsItem> resultItems)
        {
            // Clear any existing grid children and row definitions
            birdResultsGrid.Children.Clear();
            birdResultsGrid.RowDefinitions.Clear();

            int rowIndex = 0;

            foreach (BirdResultsItem resultsItem in resultItems)
            {
                // Add another row to our grid
                birdResultsGrid.RowDefinitions.Add(new RowDefinition());

                // Add two labels to our new row 
                birdResultsGrid.Children.Add(new Label { Text = resultsItem.KeyLabelText, FontSize = 20 }, 0, rowIndex);

                if ((resultsItem.ValueLabelText != null) && (resultsItem.ValueLabelText != "1/01/0001 12:00:00 AM")) // Is the feild blank
                {
                    StackLayout valueItemStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                    valueItemStackLayout.Children.Add(new Label { Text = resultsItem.ValueLabelText, FontSize = 20 });
                    valueItemStackLayout.Children.Add(new Label { Text = "[edit]", FontSize = 15, TextColor = Color.FromHex("#5C3838"), VerticalOptions = LayoutOptions.Center });
                    birdResultsGrid.Children.Add(valueItemStackLayout, 1, rowIndex);
                }
                else
                {
                    // What type of feild do we need to display
                    if ((resultsItem.KeyLabelName != "dateArrivedLabel")
                        && (resultsItem.KeyLabelName != "dateDeceasedLabel")
                        && (resultsItem.KeyLabelName != "dateDepartedLabel"))

                    {
                        birdResultsGrid.Children.Add(new Entry { StyleId = resultsItem.KeyLabelText, VerticalOptions = LayoutOptions.Center }, 1, rowIndex);
                    }
                    else
                    {
                        birdResultsGrid.Children.Add(new DatePicker { StyleId = resultsItem.KeyLabelText }, 1, rowIndex);

                    }

                }
                rowIndex++;
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