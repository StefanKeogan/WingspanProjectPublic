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
            bool allFeildsValid = true;

            if (Validate.FeildPopulated(volunteerFirstNameEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(volunteerFirstNameEntry.Text))
                {                   
                    firstNameError.Text = "The first name feild cannot contain numbers or symbols";
                    firstNameError.IsVisible = true;
                    allFeildsValid = false;
                }
            }
            else
            {
                firstNameError.Text = "The first name feild must be filled in";
                firstNameError.IsVisible = true;
                allFeildsValid = false;
            }

            if (Validate.FeildPopulated(volunteerLastNameEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(volunteerLastNameEntry.Text))
                {
                    lastNameError.IsVisible = true;
                    allFeildsValid = false;
                }
                else
                {
                    lastNameError.IsVisible = false;
                }
            }

            if (Validate.FeildPopulated(volunteerMobileEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(volunteerMobileEntry.Text))
                {
                    mobileError.IsVisible = true;
                    allFeildsValid = false;
                }
                else
                {
                    mobileError.IsVisible = false;
                }
            }

            // Email 
            if (Validate.FeildPopulated(volunteerEmailEntry.Text))
            {
                if (!Validate.EmailFormatValid(volunteerEmailEntry.Text))
                {
                    emailError.IsVisible = true;
                    allFeildsValid = false;
                }
                else
                {
                    emailError.IsVisible = false;
                }
            }

            if ((!Validate.FeildPopulated(volunteerMobileEntry.Text)) && (!Validate.FeildPopulated(volunteerEmailEntry.Text)))
            {
                contactError.IsVisible = true;
                allFeildsValid = false;
            }

            if (allFeildsValid)
            {
                // Insert volunteer document
                bool inserted = AddVolunteer.InsertVolunteerDocument(new Volunteer
                {
                    FirstName = volunteerFirstNameEntry.Text,
                    LastName = volunteerLastNameEntry.Text,
                    Mobile = Convert.ToInt64(volunteerMobileEntry.Text),
                    Email = volunteerEmailEntry.Text

                });

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
            else
            {
                allFeildsValid = true;
            }            

        }
    }
}