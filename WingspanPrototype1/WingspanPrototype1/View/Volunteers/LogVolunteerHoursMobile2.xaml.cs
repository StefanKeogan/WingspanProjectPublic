using MongoDB.Bson;
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
    public partial class LogVolunteerHoursMobile2 : ContentPage
    {
        public ObjectId selectedVolunteerId;

        public LogVolunteerHoursMobile2(Volunteer selectedVolunteer)
        {
            InitializeComponent();

            selectedVolunteerId = selectedVolunteer._id;
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
                            addingIndicatior.IsRunning = false;

                        });
                        return;

                    }

                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Enter Hours", "Please fill in the hours feild to search", "OK");
                        addingIndicatior.IsRunning = false;
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