using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Controller.Volunteers;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View.Volunteers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VolunteerResultsDesktop : ContentPage
    {
        private List<Entry> entries = new List<Entry>();
        private ObjectId id;
        private List<Volunteer> volunteerResults;

        public VolunteerResultsDesktop(List<Volunteer> results)
        {
            InitializeComponent();

            volunteerResults = results;

            foreach (var volunteer in results)
            {
                volunteer.FirstName = FormatText.FirstToUpper(volunteer.FirstName);
                volunteer.LastName = FormatText.FirstToUpper(volunteer.LastName);
            }

            resultsListView.ItemsSource = results;

        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Volunteer;

            resultsView.IsVisible = true;
            resultButtons.IsVisible = true;

            if (item != null)
            {
                DisplayVolunteer(item);
            }
        }

        private void DisplayVolunteer(Volunteer volunteer)
        {
            // Clear entries
            entries.Clear();            

            id = volunteer._id;

            if (Validate.FeildPopulated(volunteer.FirstName))
            {
                volunteerFirstNameValueLabel.Text = volunteer.FirstName;
                volunteerFirstNameEntry.IsVisible = false;
                volunteerFirstNameStack.IsVisible = true;
            }
            else
            {
                volunteerFirstNameStack.IsVisible = false;
                volunteerFirstNameEntry.IsVisible = true;
                entries.Add(volunteerFirstNameEntry);
            }

            if (Validate.FeildPopulated(volunteer.LastName))
            {
                volunteerLastNameValueLabel.Text = volunteer.LastName;
                volunteerLastNameEntry.IsVisible = false;
                volunteerLastNameStack.IsVisible = true;
            }
            else
            {
                volunteerLastNameStack.IsVisible = false;
                volunteerLastNameEntry.IsVisible = true;
                entries.Add(volunteerLastNameEntry);
            }

            if (Validate.FeildPopulated(volunteer.Email))
            {

                volunteerEmailValueLabel.Text = volunteer.Email;
                volunteerEmailEntry.IsVisible = false;
                volunteerEmailStack.IsVisible = true;
            }
            else
            {
                volunteerEmailStack.IsVisible = false;
                volunteerEmailEntry.IsVisible = true;
                entries.Add(volunteerEmailEntry);
            } 

            if (Validate.FeildPopulated(volunteer.Mobile.ToString())  && (volunteer.Mobile != 0))
            {
                volunteerMobileValueLabel.Text = volunteer.Mobile.ToString();
                volunteerMobileEntry.IsVisible = false;
                volunteerMobileStack.IsVisible = true;                
            }
            else
            {
                volunteerMobileStack.IsVisible = false;
                volunteerMobileEntry.IsVisible = true;
                entries.Add(volunteerMobileEntry);
            }
        }

        private void HoursHistoryButton_Clicked(object sender, EventArgs e)
        {
            hoursListView.ItemsSource = FindVolunteerWorkHours.GetHoursDocuments(id);
            hoursHistoryView.IsVisible = true;
        }

        private void HoursExitButton_Clicked(object sender, EventArgs e)
        {
            hoursHistoryView.IsVisible = false;
        }

        private void LogHoursButton_Clicked(object sender, EventArgs e)
        {
            logHoursView.IsVisible = true;
        }

        private void LogHoursExitButton_Clicked(object sender, EventArgs e)
        {
            logHoursView.IsVisible = false;
        }

        private async void LogButton_Clicked(object sender, EventArgs e)
        {
            if (!Validate.FeildPopulated(hoursEntry.Text))
            {
                await DisplayAlert("Amount Feild Empty", "The amount feild must be filled in to continue", "OK");
                return;
            }

            if (LogVolunteerHours.InsertVolunteerHoursDocument(new VolunteerHours { Date = hoursDatePicker.Date,
                Amount = Convert.ToDouble(hoursEntry.Text),
                Note = noteEditor.Text,
                Volunteer_id = id

            }))
            {
                hoursListView.ItemsSource = FindVolunteerWorkHours.GetHoursDocuments(id);
                hoursEntry.Text = null;
                noteEditor.Text = null;
                logHoursView.IsVisible = false;
                await DisplayAlert("Hours Logged", "This volunteer hours have been inserted into the database", "OK");
            }


        }

        // Delete volunteer
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Are you sure?", "Would you like to delete this voluneer?", "Yes", "No");

            if (result)
            {
                if (DeleteVolunteer.DropDocument(id))
                {
                    await DisplayAlert("Volunteer Deleted", "This volunteer has been deleted", "OK");

                    logHoursView.IsVisible = false;
                    hoursHistoryView.IsVisible = false;

                    Volunteer volunteer = volunteerResults.Find(x => x._id == id);
                    volunteerResults.Remove(volunteer);

                    if (volunteerResults.Count <= 0)
                    {
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        resultsListView.ItemsSource = null;

                        resultsListView.ItemsSource = volunteerResults;

                        DisplayVolunteer(volunteerResults[0]);
                    }

                }
                else
                {
                    await DisplayAlert("Connection Error", "Unable to delete volunteer please check your connection and try again", "OK");
                }
                
            }

        }

        // Save changes
        private async void SaveChangesButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Are you sure?", "Would you like to save changes to this volunteer?", "Yes", "No");

            if (result)
            {
                if (entries.Count > 0)
                {
                    bool changesValid = ValidateVolunteerChanges(entries);
                    if (!changesValid) return;
                }

                Volunteer volunteer = UpdateVolunteer.UpdateDocument(id, entries);                

                if (volunteer != null)
                {
                    volunteer.FirstName = FormatText.FirstToUpper(volunteer.FirstName);
                    volunteer.LastName = FormatText.FirstToUpper(volunteer.LastName);

                    // Find old volunteer
                    Volunteer oldVolunteer = volunteerResults.Find(x => x._id == id);
                    int volunteerIndex = volunteerResults.IndexOf(oldVolunteer);

                    // Replace old volunteer with updated
                    volunteerResults[volunteerIndex] = volunteer;

                    // Refresh result list 
                    resultsListView.ItemsSource = null;
                    resultsListView.ItemsSource = volunteerResults;

                    // Clear entry data
                    foreach (var entry in entries)
                    {
                        entry.Text = null;
                    }

                    DisplayVolunteer(volunteer);
                    await DisplayAlert("Volunteer Saved", "Changes to this volunteer have been saved", "Ok");
                }
                else
                {
                    await DisplayAlert("Connection Error", "Please check your connection and try again", "Ok");
                }           
                
            }
            
        }

        private bool ValidateVolunteerChanges(List<Entry> entries)
        {
            bool allFeildsValid = true;

            string errorMessage = "";

            foreach (var entry in entries)
            {
                // Update document id entries are populated 
                if (Validate.FeildPopulated(entry.Text))
                {
                    if (entry.StyleId == "volunteerEmailEntry")
                    {
                        if (!Validate.EmailFormatValid(entry.Text))
                        {
                            errorMessage = errorMessage + "The Email must contain a valid email address";
                        }
                    }

                    if (entry.StyleId == "volunteerFirstNameEntry")
                    {
                        if (Validate.ContainsNumberOrSymbol(entry.Text))
                        {
                            errorMessage = errorMessage + "The First Name feild cannot contain numbers or symbols";
                        }
                    }

                    if (entry.StyleId == "volunteerLastNameEntry")
                    {
                        if (Validate.ContainsNumberOrSymbol(entry.Text))
                        {
                            errorMessage = errorMessage + "The Last Name feild cannot contain numbers or symbols";
                        }
                    }

                    if (entry.StyleId == "volunteerMobileEntry")
                    {
                        if (Validate.ContainsNumberOrSymbol(entry.Text))
                        {
                            errorMessage = errorMessage + "The Mobile feild cannot contain letters or symbols";
                        }
                    }

                }
            }

            if (allFeildsValid)
            {
                errorMessage = "";
                return true;
            }
            else
            {
                DisplayAlert("Invalid Input", errorMessage, "OK");
                errorMessage = "";
                return false;
            }

            

        }

        private async void HoursListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool result = await DisplayAlert("Delete Hours?", "Would you like to delete this hours item?", "Yes", "No");

            // If yes has been selected
            if (result)
            {
                // Store selected list item
                var item = e.SelectedItem as VolunteerHours;

                // If item is not null
                if (item != null)
                {
                    // Delete locattion document
                    if (DeleteHours.DropDocument(item._id))
                    {
                        await DisplayAlert("Hours Deleted", "This volunteers hours has been deleted", "OK");
                        hoursListView.ItemsSource = FindVolunteerWorkHours.GetHoursDocuments(id);
                    }
                    else
                    {
                        await DisplayAlert("Connection Error", "Please check your connection and try again", "OK");
                    }
                }
            }
        }

        private void VolunteerFirstNameEditButton_Clicked(object sender, EventArgs e)
        {
            volunteerFirstNameStack.IsVisible = false;
            volunteerFirstNameEntry.IsVisible = true;
            entries.Add(volunteerFirstNameEntry);
        }

        private void VolunteerLastNameEditButton_Clicked(object sender, EventArgs e)
        {
            volunteerLastNameStack.IsVisible = false;
            volunteerLastNameEntry.IsVisible = true;
            entries.Add(volunteerLastNameEntry);
        }

        private void VolunteerMobileEditButton_Clicked(object sender, EventArgs e)
        {
            volunteerMobileStack.IsVisible = false;
            volunteerMobileEntry.IsVisible = true;
            entries.Add(volunteerMobileEntry);
        }

        private void VolunteerEmailEditButton_Clicked(object sender, EventArgs e)
        {
            volunteerEmailStack.IsVisible = false;
            volunteerEmailEntry.IsVisible = true;
            entries.Add(volunteerEmailEntry);
        }

        
    }
}