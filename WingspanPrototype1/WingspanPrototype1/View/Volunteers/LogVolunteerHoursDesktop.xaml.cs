﻿using System;
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
                selectedVolunteerId = item._id;
            }
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            searchingIndicator.IsRunning = true;

            await Task.Run(() =>
            {
                if (!Validate.FeildPopulated(volunteerFirstNameEntry.Text))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Enter Name", "Please fill in the first name feild to search", "OK");
                        addingIndicatior.IsRunning = false;
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

                });

            });

            

        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            addingIndicatior.IsRunning = true;

            await Task.Run(() => 
            {
                if (Validate.FeildPopulated(hoursEntry.Text))
                {
                    if (Validate.ContainsLetter(hoursEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            DisplayAlert("Invalid Format", "The hours feild cannot contain letters", "OK");

                        });
                        return;
                        
                    }

                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Enter Hours", "Please fill in the hours feild to search", "OK");
                    });

                    return;
                }


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
                        DisplayAlert("Hours Logged", "This volunteers hours have been logged in the database", "OK");
                    });

                    
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Connection Error", "Please check your connection and try again", "OK");
                    });

                }

                Device.BeginInvokeOnMainThread(() => {

                    addingIndicatior.IsRunning = false;

                });

            });

            

        }
    }
}