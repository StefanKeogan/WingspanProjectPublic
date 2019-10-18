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
    public partial class AddVolunteerForm : ContentPage
    {
        public AddVolunteerForm(string title)
        {
            InitializeComponent();

            Title = title;

            // What type of device are we running on 
            if (DeviceSize.ScreenArea() <= 783457)
            {
                volunteerMargin1.Width = 0;
                volunteerMargin2.Width = 0;

            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (Validate.FeildPopulated(volunteerFirstNameEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(volunteerFirstNameEntry.Text))
                {
                    await DisplayAlert("Invalid Format", "The first name feild can not contain letters or symbols", "OK");
                    return;
                }
            }
            else
            {
                await DisplayAlert("Empty Feild", "Please fill in the first name feild", "OK");
                return;
            }

            if (Validate.FeildPopulated(volunteerLastNameEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(volunteerLastNameEntry.Text))
                {
                    await DisplayAlert("Invalid Format", "The first name feild can not contain letters or symbols", "OK");
                    return;
                }
            }
            else
            {
                await DisplayAlert("Empty Feild", "Please fill in the first name feild", "OK");
                return;
            }

            // Mobile 
            // Required feild? 

            // Email 
            // Required feild

            // Insert volunteer document
            bool inserted = AddVolunteer.InsertVolunteerDocument(new Volunteer
            {
                FirstName = volunteerFirstNameEntry.Text,
                LastName = volunteerLastNameEntry.Text,
                Mobile = Convert.ToInt64(volunteerMobileEntry.Text),
                Email = volunteerEmailEntry.Text

            }) ;

            // Was the document inserted successfully?
            if (inserted)
            {
                volunteerFirstNameEntry.Text = null;
                volunteerLastNameEntry.Text = null;
                volunteerMobileEntry.Text = null;
                volunteerEmailEntry.Text = null;

                await DisplayAlert("Volunteer Saved", "This volunteer has been inserted into the database", "OK");
            }
            else
            {
                await DisplayAlert("Connection Error", "Please check connection and try again", "OK");
            }

        }
    }
}