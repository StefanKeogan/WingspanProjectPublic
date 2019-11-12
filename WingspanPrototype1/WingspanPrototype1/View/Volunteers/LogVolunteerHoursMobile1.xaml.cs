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
    public partial class LogVolunteerHoursMobile1 : ContentPage
    {
        public LogVolunteerHoursMobile1(string title)
        {
            InitializeComponent();

            Title = title;

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    searchButton.Source = "Assets/search-icon.png";
                    break;
                case Device.Android:
                    searchButton.Source = "searchicon.png";
                    break;
                default:
                    searchButton.Source = "Assets/search-icon.png";
                    break;
            }


        }

        private void resultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Volunteer;

            if (item != null)
            {
                Navigation.PushAsync(new LogVolunteerHoursMobile2(item));
            }

            
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            searchingIndicator.IsRunning = true;
            searchButton.IsEnabled = false;

            await Task.Run(() =>
            {
                if (!Validate.FeildPopulated(volunteerFirstNameEntry.Text))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Enter Name", "Please fill in the first name feild to search", "OK");
                    });

                    return;

                }

                List<Volunteer> results = SearchVolunteers.Search(null, volunteerFirstNameEntry.Text, null);

                if (results.Count > 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        resultsListView.ItemsSource = results;
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("No Volunteers Found", "No volunteers by the name of" + volunteerFirstNameEntry.Text + " were found in the database", "OK");
                    });
                }

                Device.BeginInvokeOnMainThread(() => {

                    searchingIndicator.IsRunning = false;
                    searchButton.IsEnabled = true;

                });

            });
        }
    }
}