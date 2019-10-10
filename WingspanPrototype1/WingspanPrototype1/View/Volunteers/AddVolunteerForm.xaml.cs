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
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (Validate.FeildPopulated(volunteerNameEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(volunteerNameEntry.Text))
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
                Name = volunteerNameEntry.Text,
                Mobile = Convert.ToInt64(volunteerMobileEntry.Text),
                Email = volunteerEmailEntry.Text

            }) ;

            // Was the document inserted successfully?
            if (inserted)
            {
                volunteerNameEntry.Text = null;
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