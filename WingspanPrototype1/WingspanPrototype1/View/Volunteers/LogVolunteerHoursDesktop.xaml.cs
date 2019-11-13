using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Functions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WingspanPrototype1.Model;
using WingspanPrototype1.Controller.Volunteers;
using MongoDB.Bson;

namespace WingspanPrototype1.View.Volunteers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogVolunteerHoursDesktop : ContentPage
    {

        public ObjectId selectedVolunteerId;

        public LogVolunteerHoursDesktop(string title)
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

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Volunteer;

            if (item != null)
            {
                selectedVolunteerId = item.Volunteer_id;
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
                        addingIndicatior.IsRunning = false;
                        searchButton.IsEnabled = true;
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

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            bool allFeildsValid = true;

            addingIndicatior.IsRunning = true;
            addButton.IsEnabled = false;

            await Task.Run(() => 
            {
                if (selectedVolunteerId == ObjectId.Empty)
                {
                    Device.InvokeOnMainThreadAsync(() =>
                    {
                        DisplayAlert("Select a Volunteer", "A volunteer must be selected in order to log hours", "OK");
                        addingIndicatior.IsRunning = false;
                        addButton.IsEnabled = true;
                    });
                    return;
                }

                if (Validate.FeildPopulated(hoursEntry.Text))
                {
                    if (Validate.ContainsLetter(hoursEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            hoursError.IsVisible = true;
                            hoursError.Text = "The hours feild cannot contain letters";
                        });

                        allFeildsValid = false;

                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            hoursError.IsVisible = false;
                        });
                    }

                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        hoursError.IsVisible = true;
                        hoursError.Text = "Please fill in the hours feild to search";
                    });
                    allFeildsValid = false;
                    
                }

                if (allFeildsValid)
                {
                    bool inserted = LogVolunteerHours.InsertVolunteerHoursDocument(new VolunteerHours
                    {
                        Volunteer_id = selectedVolunteerId,
                        Date = datePicker.Date,
                        Amount = Convert.ToDouble(hoursEntry.Text),
                        Note = noteEditor.Text

                    });

                    if (inserted)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            hoursEntry.Text = null;
                            noteEditor.Text = null;
                            addingIndicatior.IsRunning = false;
                            addButton.IsEnabled = true;
                            DisplayAlert("Hours Logged", "This volunteers hours have been logged in the database", "OK");
                        });


                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            DisplayAlert("Connection Error", "Please check your connection and try again", "OK");
                            addingIndicatior.IsRunning = false;
                            addButton.IsEnabled = true;
                        });


                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() => 
                    {
                        addingIndicatior.IsRunning = false;
                        addButton.IsEnabled = true;
                    });

                    allFeildsValid = false;
                }

            });
          
        }
    }
}