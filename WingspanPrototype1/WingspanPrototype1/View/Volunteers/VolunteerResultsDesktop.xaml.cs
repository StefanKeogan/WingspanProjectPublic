using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            if (item != null)
            {
                DisplayVolunteer(item);
            }
        }

        private void DisplayVolunteer(Volunteer volunteer)
        {
            id = volunteer._id;

            if (Validate.FeildPopulated(volunteer.Name))
            {
                nameValueLabel.Text = volunteer.Name;
                nameStack.IsVisible = true;
            }
            else
            {
                nameStack.IsVisible = false;
                nameEntry.IsVisible = true;
                entries.Add(nameEntry);
            }

            if (Validate.FeildPopulated(volunteer.Email))
            {
                emailValueLabel.Text = volunteer.Email;
                emailStack.IsVisible = true;
            }
            else
            {
                emailStack.IsVisible = false;
                emailEntry.IsVisible = true;
                entries.Add(emailEntry);
            }

            if (Validate.FeildPopulated(volunteer.Mobile.ToString()))
            {
                mobileValueLabel.Text = volunteer.Mobile.ToString();
                mobileStack.IsVisible = true;
            }
            else
            {
                mobileStack.IsVisible = false;
                mobileEntry.IsVisible = true;
                entries.Add(mobileEntry);
            }
        }

        private void HoursHistoryButton_Clicked(object sender, EventArgs e)
        {
            hoursListView.IsVisible = true;
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
                await DisplayAlert("Volunteer Deleted", "This volunteer has been deleted", "Ok");
            }

            // TODO: Erase volunteer, send to default page

        }

        // Save changes
        private async void SaveChangesButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Are you sure?", "Would you like to save changes to this volunteer?", "Yes", "No");

            if (result)
            {
                var database = DatabaseConnection.GetDatabase();

                var collection = database.GetCollection<BsonDocument>("Volunteers");

                var updateBuilder = Builders<BsonDocument>.Update;

                if (collection != null)
                {
                    foreach (var entry in entries)
                    {
                        if (Validate.FeildPopulated(entry.Text))
                        {
                            if (entry.StyleId == "emailEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Email", entry.Text));
                            if (entry.StyleId == "nameEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Name", entry.Text));
                            if (entry.StyleId == "mobileEntry") collection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id), updateBuilder.Set("Mobile", entry.Text));
                        }
                    }
                }
            
                await DisplayAlert("Volunteer Saved", "Changes to this volunteer have been saved", "Ok");
            }
            
        }
    }
}