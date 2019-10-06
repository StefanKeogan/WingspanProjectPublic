using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View.Volunteers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VolunteerResultsDesktop : ContentPage
    {
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