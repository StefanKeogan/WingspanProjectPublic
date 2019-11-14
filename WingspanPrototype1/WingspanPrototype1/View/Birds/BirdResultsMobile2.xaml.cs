using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Controller.Sponsorships;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BirdResultsMobile2 : ContentPage
    {
        private ObjectId id;
        private string wingspanId;
        private List<Entry> entries = new List<Entry>();
        private List<Picker> pickers = new List<Picker>();
        private List<DatePicker> datePickers = new List<DatePicker>();
        private Type birdType;
        private List<WildBird> wildResults = new List<WildBird>();
        private List<CaptiveBird> captiveResults = new List<CaptiveBird>();
        // private ArrayList allResults = new ArrayList();


        public BirdResultsMobile2(ArrayList bird)
        {
            InitializeComponent();

            // What type of bird are we displaying
            if (bird[0].GetType() == typeof(WildBird))
            {
                var wildItem = bird[0] as WildBird;

                DisplayWildBird(wildItem);

                id = wildItem.Wild_id;

                wingspanId = wildItem.WingspanId;

                birdType = typeof(WildBird);
            }
            else if (bird[0].GetType() == typeof(CaptiveBird))
            {
                var captiveItem = bird[0] as CaptiveBird;

                DisplayCaptiveBird(captiveItem);

                id = captiveItem.Captive_id;

                wingspanId = captiveItem.WingspanId;

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

            wildWingspanIdValueLabel.Text = bird.WingspanId;

            // Determine which feilds are populated, if populated display the value else display an entry, picker etc.

            // Set Wingspan Id Value
            //if (Validate.FeildPopulated(bird.WingspanId))
            //{
            //    wildWingspanIdValueLabel.Text = bird.WingspanId;
            //    wildWingspanIdStack.IsVisible = true;
            //    wildWingspanIdEntry.IsVisible = false;

            //}
            //else
            //{
            //    wildWingspanIdEntry.IsVisible = true;
            //    wildWingspanIdStack.IsVisible = false;
            //    entries.Add(wildWingspanIdEntry);
            //}

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
                bandInfoValueLabel.Text = bird.BandInfo;
                bandInfoStack.IsVisible = true;
                bandInfoEntry.IsVisible = false;

            }
            else
            {
                bandInfoEntry.IsVisible = true;
                bandInfoStack.IsVisible = false;
                entries.Add(bandInfoEntry);
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

            captiveWingspanIdValueLabel.Text = bird.WingspanId;

            // Set Wingspan Id value
            //if (Validate.FeildPopulated(bird.WingspanId))
            //{
            //    captiveWingspanIdValueLabel.Text = bird.WingspanId;
            //    captiveWingspanIdStack.IsVisible = true;
            //    captiveWingspanIdEntry.IsVisible = false;

            //}
            //else
            //{
            //    captiveWingspanIdEntry.IsVisible = true;
            //    captiveWingspanIdStack.IsVisible = false;
            //    entries.Add(captiveWingspanIdEntry);
            //}

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
            if (Validate.FeildPopulated(bird.BandInfo))
            {
                captiveBandInfoValueLabel.Text = bird.BandInfo;
                captiveBandInfoStack.IsVisible = true;
                captiveBandInfoEntry.IsVisible = false;

            }
            else
            {
                captiveBandInfoEntry.IsVisible = true;
                captiveBandInfoStack.IsVisible = false;
                entries.Add(captiveBandInfoEntry);
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
            if (bird.DateArrived.ToShortDateString() != "01/01/0001")
            {

                captiveDateArrivedValueLabel.Text = bird.DateArrived.ToShortDateString();
                captiveDateArrivedStack.IsVisible = true;
                captiveDateArrivedPicker.IsVisible = false;

            }
            else
            {
                captiveDateArrivedPicker.IsVisible = true;
                captiveDateArrivedStack.IsVisible = false;

            }

            // Set Date Departed Value 
            if (bird.DateDeparted.ToShortDateString() != "01/01/0001")
            {
                captiveDateDepartedValueLabel.Text = bird.DateDeparted.ToShortDateString();
                captiveDateDepartedStack.IsVisible = true;
                captiveDateDepartedPicker.IsVisible = false;
            }
            else
            {
                captiveDateDepartedValueLabel.Text = "Date Not Set";
                captiveDateDepartedStack.IsVisible = true;
                captiveDateDepartedPicker.IsVisible = false;

            }

            // Set Date Deceased Value 
            if (bird.DateDeceased.ToShortDateString() != "01/01/0001")
            {

                captiveDateDeceasedValueLabel.Text = bird.DateDeceased.ToShortDateString();
                captiveDatedeceasedStack.IsVisible = true;
                captiveDateDeceasedPicker.IsVisible = false;
            }
            else
            {
                captiveDateDeceasedValueLabel.Text = "Date Not Set";
                captiveDatedeceasedStack.IsVisible = true;
                captiveDateDeceasedPicker.IsVisible = false;
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
            noteListView.ItemsSource = FindBirdNotes.GetNoteDocuments(wingspanId);
            noteHistoryView.IsVisible = true;

            // If location popups are visible, close them
            locationHistoryView.IsVisible = false;
            addNewLocationView.IsVisible = false;
        }

        private void AddNewNoteButton_Clicked(object sender, EventArgs e)
        {
            addNewNoteView.IsVisible = true;

        }

        private void NoteExitButton_Clicked(object sender, EventArgs e)
        {
            noteHistoryView.IsVisible = false;
        }

        private async void AddNoteButton_Clicked(object sender, EventArgs e)
        {
            if (!Validate.FeildPopulated(noteCategoryPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Feild Empty", "The category feild can not be empty", "OK");
                return;
            }

            if (!Validate.FeildPopulated(noteEditor.Text))
            {
                await DisplayAlert("Feild Empty", "The comment feild can not be empty", "OK");
                return;
            }


            if (AddBirdNote.InsertNoteDocument(new Note
            {
                Date = noteDatePicker.Date,
                Category = noteCategoryPicker.SelectedItem.ToString(),
                Comment = noteEditor.Text,
                WingspanId = wingspanId
            }))
            {
                List<Note> results = FindBirdNotes.GetNoteDocuments(wingspanId);
                if ((results != null) && (results.Count > 0))
                {
                    noteListView.ItemsSource = results;
                }
                noteEditor.Text = null;
                noteCategoryPicker.SelectedIndex = -1;
                addNewNoteView.IsVisible = false;
                await DisplayAlert("Bird Note Added", "Your note has been added to this birds note history", "OK");
                return;
            }

        }

        private void AddNewNoteExitButton_Clicked(object sender, EventArgs e)
        {
            addNewNoteView.IsVisible = false;
        }

        //Location popup boxes
        private void LocationButton_Clicked(object sender, EventArgs e)
        {
            locationListView.ItemsSource = FindBirdLocationHistory.GetLocationDocuments(wingspanId);
            locationHistoryView.IsVisible = true;

            // If the note popups are open close them
            addNewNoteView.IsVisible = false;
            noteHistoryView.IsVisible = false;

        }

        private void AddNewLocationButton_Clicked(object sender, EventArgs e)
        {
            addNewLocationView.IsVisible = true;
        }

        private async void AddLocationButton_Clicked(object sender, EventArgs e)
        {
            if (!Validate.FeildPopulated(locationCategoryPicker.SelectedIndex.ToString()))
            {
                await DisplayAlert("Feild Empty", "The category feild can not be empty", "OK");
                return;
            }

            if (!Validate.FeildPopulated(locationEntry.Text))
            {
                await DisplayAlert("Feild Empty", "The location feild can not be empty", "OK");
                return;
            }

            if (AddBirdLocation.InsertLocationDocument(new Location
            {
                Date = locationDatePicker.Date,
                Category = locationCategoryPicker.SelectedItem.ToString(),
                BirdLocation = locationEntry.Text,
                WingspanId = wingspanId

            }))
            {
                await DisplayAlert("Location added", "Location has been added to this birds note history", "Ok");
                locationListView.ItemsSource = FindBirdLocationHistory.GetLocationDocuments(wingspanId);
                locationCategoryPicker.SelectedIndex = -1;
                locationEntry.Text = null;
                addNewLocationView.IsVisible = false;
            }

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
                    // Do we have entry changes to validate?
                    if (entries.Count > 0)
                    {
                        // Is the input valid?
                        bool changesValid = ValidateWildBirdChanges(entries);
                        if (!changesValid) return;
                    }

                    WildBird wildBird = UpdateWildBird.UpdateDocument(id, entries, pickers);

                    if (wildBird != null)
                    {
                        // Clear entries
                        foreach (var entry in entries)
                        {
                            entry.Text = null;
                        }

                        // Reset pickers
                        foreach (var picker in pickers)
                        {
                            picker.SelectedIndex = -1;
                        }

                        //// Find old wild bird
                        //WildBird bird = wildResults.Find(x => x._id == id);
                        //int resultIndex = allResults.IndexOf(bird);
                        //int wildIndex = wildResults.IndexOf(bird);

                        //// Update old wild bird 
                        //wildResults[wildIndex] = wildBird;
                        //allResults[resultIndex] = wildBird;

                        DisplayWildBird(wildBird);
                        await DisplayAlert("Bird Saved", "Your changes have been saved", "OK");

                    }
                    else
                    {
                        await DisplayAlert("Connection Error", "Could not connect to database, please check your connection and try again", "OK");
                    }

                }
                else if (birdType == typeof(CaptiveBird))
                {
                    if (entries.Count > 0)
                    {
                        bool changesValid = ValidateCaptiveBirdChanges(entries);
                        if (!changesValid) return;
                    }

                    CaptiveBird captiveBird = UpdateCaptiveBird.UpdateDocument(id, entries, pickers, datePickers);

                    if (captiveBird != null)
                    {
                        // Clear entries
                        foreach (var entry in entries)
                        {
                            entry.Text = null;
                        }

                        // Reset pickers
                        foreach (var picker in pickers)
                        {
                            picker.SelectedIndex = -1;
                        }

                        //// Find old captive bird
                        //CaptiveBird bird = captiveResults.Find(x => x._id == id);
                        //int resultIndex = allResults.IndexOf(bird);
                        //int captiveIndex = captiveResults.IndexOf(bird);

                        //// Update old captive bird 
                        //captiveResults[captiveIndex] = captiveBird;
                        //allResults[resultIndex] = captiveBird;                       

                        //captiveBird.Name = FormatText.FirstToUpper(captiveBird.Name);

                        DisplayCaptiveBird(captiveBird);
                        await DisplayAlert("Bird Saved", "Your changes have been saved", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Connection Error", "Could not connect to database, please check your connection and try again", "OK");
                    }
                }


            }
        }

        private bool ValidateWildBirdChanges(List<Entry> entries)
        {
            bool allFeildsValid = true;
            string errorMessage = "";

            foreach (var entry in entries)
            {
                if (Validate.FeildPopulated(entry.Text))
                {
                    // Is the entry poulated
                    if (Validate.FeildPopulated(entry.Text))
                    {
                        if (entry.StyleId == "wildLocationEntry")
                        {
                            if (Validate.ContainsNumberOrSymbol(entry.Text))
                            {
                                errorMessage = errorMessage + "The location feild cannot contain numbers or symbols \n";
                                allFeildsValid = false;
                            }
                        }

                        if (entry.StyleId == "wildMetalBandIdEntry")
                        {
                            if (Validate.ContainsLetterOrSymbol(entry.Text))
                            {
                                errorMessage = errorMessage + "The metal band id feild cannot contain letters or symbols \n";
                                allFeildsValid = false;
                            }
                        }

                        if (entry.StyleId == "wildBanderNameEntry")
                        {
                            if (Validate.ContainsNumberOrSymbol(entry.Text))
                            {
                                errorMessage = errorMessage + "The Bander Name feild cannot contain numbers or symbols";
                                allFeildsValid = false;
                            }
                        }

                    }
                }

            }

            if (allFeildsValid)
            {
                return true;
            }
            else
            {
                DisplayAlert("Invalid Entry Format", errorMessage, "OK");
                return false;
            }
        }

        private bool ValidateCaptiveBirdChanges(List<Entry> entries)
        {
            bool allFeildsValid = true;

            string errorMessage = "";

            foreach (var entry in entries)
            {
                if (Validate.FeildPopulated(entry.Text))
                {
                    // Which feild are we updateing?
                    if (entry.StyleId == "captiveNameEntry")
                    {
                        if (Validate.ContainsNumberOrSymbol(entry.Text))
                        {
                            errorMessage = errorMessage + "The Name feild cannot contain numbers or symbols \n";
                            allFeildsValid = false;
                        }
                    }
                    if (entry.StyleId == "captiveBandNumberEntry")
                    {
                        if (Validate.ContainsLetterOrSymbol(entry.Text))
                        {
                            errorMessage = errorMessage + "The Band Number feild cannot contain letters or symbols \n";
                            allFeildsValid = false;
                        }
                    }
                    if (entry.StyleId == "captiveLocationEntry")
                    {
                        if (Validate.ContainsNumberOrSymbol(entry.Text))
                        {
                            errorMessage = errorMessage + "The Location feild cannot contain numbers or symbols \n";
                            allFeildsValid = false;
                        }
                    }
                }

            }

            if (allFeildsValid == true)
            {
                return true;
            }
            else
            {
                DisplayAlert("Invalid Input", errorMessage, "OK");
                errorMessage = "";
                return false;
            }


        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Are you sure", "Would you like to delete this bird", "Yes", "No");
            if (result == true)
            {
                // Does this bird have any sponsors?
                List<Sponsorship> sponsorships = SearchSponsorships.FindByBird(wingspanId);
                if (sponsorships.Count > 0)
                {
                    // Would you like to remove sponsorships accosiated with this bird
                    bool deleteSponsorships = await DisplayAlert("Bird Sponsored", "This bird has " + sponsorships.Count.ToString() + " sponsor(s)", "Delete bird and sponsorships", "Cancel");

                    // Delete sponsorships 
                    if (deleteSponsorships)
                    {
                        foreach (var sponsorship in sponsorships)
                        {
                            DeleteSponsorship.DropDocument(sponsorship.Sponsorship_id);
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                if (birdType == typeof(WildBird))
                {
                    if (DeleteWildBird.DropDocument(wingspanId))
                    {
                        await DisplayAlert("Bird Deleted", "This wild bird and its note history have been removed from the database", "OK");

                        addNewNoteView.IsVisible = false;
                        noteHistoryView.IsVisible = false;
                        addNewLocationView.IsVisible = false;
                        locationHistoryView.IsVisible = false;
                        wildBirdDisplayForm.IsVisible = false;

                        WildBird bird = wildResults.Find(x => x.Wild_id == id);
                        wildResults.Remove(bird);

                        if ((wildResults.Count <= 0) && (captiveResults.Count <= 0))
                        {
                            await Navigation.PopAsync();
                        }                       

                    }
                    else
                    {
                        await DisplayAlert("Connection Error", "Please check your connection and try again", "OK");
                    }
                }
                else if (birdType == typeof(CaptiveBird))
                {
                    if (DeleteCaptiveBird.DropDocument(wingspanId))
                    {
                        await DisplayAlert("Bird Deleted", "This captive bird, their notes and location history have been removed from the database", "OK");
                        addNewNoteView.IsVisible = false;
                        noteHistoryView.IsVisible = false;
                        addNewLocationView.IsVisible = false;
                        locationHistoryView.IsVisible = false;
                        captiveBirdDisplayForm.IsVisible = false;

                        CaptiveBird bird = captiveResults.Find(x => x.Captive_id == id);
                        captiveResults.Remove(bird);

                        if ((captiveResults.Count <= 0) && (wildResults.Count <= 0))
                        {
                            await Navigation.PopAsync();
                        }                       

                    }
                    else
                    {
                        await DisplayAlert("Connection Error", "Please check your connection and try again", "OK");

                    }
                }

            }
        }

        //private void WildWingspanIdEditButton_Clicked(object sender, EventArgs e)
        //{
        //    wildWingspanIdStack.IsVisible = false;
        //    wildWingspanIdEntry.IsVisible = true;
        //    entries.Add(wildWingspanIdEntry);
        //}

        private void WildSpeciesEditButton_Clicked(object sender, EventArgs e)
        {
            wildSpeciesStack.IsVisible = false;
            wildSpeciesPicker.IsVisible = true;
            pickers.Add(wildSpeciesPicker);
        }

        private void WildLocationEditButton_Clicked(object sender, EventArgs e)
        {
            wildLocationStack.IsVisible = false;
            wildLocationEntry.IsVisible = true;
            entries.Add(wildLocationEntry);
        }

        private void WildGpsEditButton_Clicked(object sender, EventArgs e)
        {
            wildGpsStack.IsVisible = false;
            wildGpsEntry.IsVisible = true;
            entries.Add(wildGpsEntry);
        }

        private void WildSexEditButton_Clicked(object sender, EventArgs e)
        {
            wildSexStack.IsVisible = false;
            wildSexPicker.IsVisible = true;
            pickers.Add(wildSexPicker);
        }

        private void WildAgeEditButton_Clicked(object sender, EventArgs e)
        {
            wildAgeStack.IsVisible = false;
            wildAgePicker.IsVisible = true;
            pickers.Add(wildAgePicker);
        }

        private void WildMetalBandIdEditButton_Clicked(object sender, EventArgs e)
        {
            wildMetalBandStack.IsVisible = false;
            wildMetalBandIdEntry.IsVisible = true;
            entries.Add(wildMetalBandIdEntry);
        }

        private void BandInfoEditButton_Clicked(object sender, EventArgs e)
        {
            bandInfoStack.IsVisible = false;
            bandInfoEntry.IsVisible = true;
            entries.Add(bandInfoEntry);
        }

        private void WildDateBandedEditButton_Clicked(object sender, EventArgs e)
        {
            wildDateBandedStack.IsVisible = false;
            wildDateBandedPicker.IsVisible = true;
            datePickers.Add(wildDateBandedPicker);
        }

        private void WildBanderNameEditButton_Clicked(object sender, EventArgs e)
        {
            wildBanderNameStack.IsVisible = false;
            wildBanderNameEntry.IsVisible = true;
            entries.Add(bandInfoEntry);
        }

        //private void CaptiveWingspanIdEditButton_Clicked(object sender, EventArgs e)
        //{
        //    captiveWingspanIdStack.IsVisible = false;
        //    captiveWingspanIdEntry.IsVisible = true;
        //    entries.Add(captiveWingspanIdEntry);
        //}

        private void CaptiveNameEditButton_Clicked(object sender, EventArgs e)
        {
            captiveNameStack.IsVisible = false;
            captiveNameEntry.IsVisible = true;
            entries.Add(captiveNameEntry);
        }

        private void CaptiveBandNumberEditButton_Clicked(object sender, EventArgs e)
        {
            captiveBandNumberStack.IsVisible = false;
            captiveBandNumberEntry.IsVisible = true;
            entries.Add(captiveBandNumberEntry);
        }

        private void CaptiveBandInfoEditButton_Clicked(object sender, EventArgs e)
        {
            captiveBandInfoStack.IsVisible = false;
            captiveBandInfoEntry.IsVisible = true;
            entries.Add(captiveBandInfoEntry);
        }

        private void CaptiveSpeciesEditButton_Clicked(object sender, EventArgs e)
        {
            captiveSpeciesStack.IsVisible = false;
            captiveSpeciesPicker.IsVisible = true;
            pickers.Add(captiveSpeciesPicker);
        }

        private void CaptiveSexEditButton_Clicked(object sender, EventArgs e)
        {
            captiveSexStack.IsVisible = false;
            captiveSexPicker.IsVisible = true;
            pickers.Add(captiveSexPicker);
        }

        private void CaptiveAgeEditButton_Clicked(object sender, EventArgs e)
        {
            captiveAgeStack.IsVisible = false;
            captiveAgePicker.IsVisible = true;
            pickers.Add(captiveAgePicker);
        }

        private void CaptiveLocationEditButton_Clicked(object sender, EventArgs e)
        {
            captiveLocationStack.IsVisible = false;
            captiveLocationEntry.IsVisible = true;
            entries.Add(captiveLocationEntry);
        }

        private void CaptiveDateArrivedEditButton_Clicked(object sender, EventArgs e)
        {
            captiveDateArrivedStack.IsVisible = false;
            captiveDateArrivedPicker.IsVisible = true;
            datePickers.Add(captiveDateArrivedPicker);
        }

        private void CaptiveDateDepartedEditButton_Clicked(object sender, EventArgs e)
        {
            captiveDateDepartedStack.IsVisible = false;
            captiveDateDepartedPicker.IsVisible = true;
            datePickers.Add(captiveDateDepartedPicker);
        }

        private void CaptiveDateDeceasedEditButton_Clicked(object sender, EventArgs e)
        {
            captiveDatedeceasedStack.IsVisible = false;
            captiveDateDeceasedPicker.IsVisible = true;
            datePickers.Add(captiveDateDeceasedPicker);
        }

        private void CaptiveResultEditButton_Clicked(object sender, EventArgs e)
        {
            captiveResultStack.IsVisible = false;
            captiveResultEntry.IsVisible = true;
            entries.Add(captiveResultEntry);
        }


    }
}