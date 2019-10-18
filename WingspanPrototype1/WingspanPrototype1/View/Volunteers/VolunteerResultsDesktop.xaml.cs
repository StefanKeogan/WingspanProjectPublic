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

        public VolunteerResultsDesktop(List<Volunteer> results)
        {
            InitializeComponent();

            resultsListView.ItemsSource = results;

            // Hard coded hours items
            hoursListView.ItemsSource = new List<VolunteerHours>() {
                new VolunteerHours { HoursId = "1", Amount = 3.00, Date = DateTime.Today },
                new VolunteerHours { HoursId = "2", Amount = 5.00, Date = DateTime.Today },
                new VolunteerHours { HoursId = "3", Amount = 2.00, Date = DateTime.Today }
            };

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

            if (Validate.FeildPopulated(volunteer.Mobile.ToString()))
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

        private void LogButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Hours Logged", "This volunteers hours have been logged in the database", "Ok");
        }

        // Delete volunteer
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Are you sure?", "Would you like to delete this voluneer?", "Yes", "No");

            if (result)
            {
                if (DeleteVolunteer.DropDocument(id))
                {
                    await DisplayAlert("Volunteer Deleted", "This volunteer has been deleted", "Ok");

                    await Navigation.PopAsync();

                    // TODO: Refresh page? 

                }
                else
                {
                    await DisplayAlert("Connection Error", "Unable to delete volunteer please check your connection and try again", "OK");
                }
                
            }

            // TODO: Erase volunteer, send to default page

        }

        // Save changes
        private async void SaveChangesButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Are you sure?", "Would you like to save changes to this volunteer?", "Yes", "No");

            if (result)
            {
                Volunteer volunteer = UpdateVolunteer.UpdateDocument(id, entries);

                if (volunteer != null)
                {
                    DisplayVolunteer(volunteer);
                    await DisplayAlert("Volunteer Saved", "Changes to this volunteer have been saved", "Ok");
                }
                else
                {
                    await DisplayAlert("Connection Error", "Please check your connection and try again", "Ok");
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