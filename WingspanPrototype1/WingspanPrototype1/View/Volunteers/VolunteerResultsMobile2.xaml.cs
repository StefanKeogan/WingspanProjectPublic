using MongoDB.Bson;
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
    public partial class VolunteerResultsMobile2 : ContentPage
    {
        private ObjectId id;
        private List<Entry> entries;

        public VolunteerResultsMobile2(Volunteer volunteer)
        {
            InitializeComponent();

            hoursListView.ItemsSource = new List<VolunteerHours>() {
                new VolunteerHours { HoursId = "1", Amount = 3.00, Date = DateTime.Today },
                new VolunteerHours { HoursId = "2", Amount = 5.00, Date = DateTime.Today },
                new VolunteerHours { HoursId = "3", Amount = 2.00, Date = DateTime.Today }
            };

            DisplayVolunteer(volunteer);
        }

        private void DisplayVolunteer(Volunteer volunteer)
        {
            id = volunteer._id;

            if (Validate.FeildPopulated(volunteer.Name))
            {
                volunteerNameValueLabel.Text = volunteer.Name;
                volunteerNameStack.IsVisible = true;
            }
            else
            {
                volunteerNameStack.IsVisible = false;
                volunteerNameEntry.IsVisible = true;
                entries.Add(volunteerNameEntry);
            }

            if (Validate.FeildPopulated(volunteer.Email))
            {
                volunteerEmailValueLabel.Text = volunteer.Email;
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

        // Save chnages
        private async void SaveChangesButton_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Are you sure?", "Would you like to save changes to this voluneer?", "Yes", "No");

            if (result)
            {
                await DisplayAlert("Volunteer Saved", "Changes to this volunteer have been saved", "Ok");
            }

            // TODO: Save changes method, reload page 
        }
    }
}
