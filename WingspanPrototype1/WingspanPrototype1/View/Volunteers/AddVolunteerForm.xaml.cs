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
            searchingIndicator.IsRunning = true;

            bool allFeildsValid = true;

            await Task.Run(() =>
            {
                if (Validate.FeildPopulated(volunteerFirstNameEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(volunteerFirstNameEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            firstNameError.Text = "The first name feild cannot contain numbers or symbols";
                            firstNameError.IsVisible = true;
                        });

                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            firstNameError.IsVisible = false;
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        firstNameError.Text = "The first name feild must be filled in";
                        firstNameError.IsVisible = true;
                    });
                    allFeildsValid = false;
                }

                if (Validate.FeildPopulated(volunteerLastNameEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(volunteerLastNameEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            lastNameError.IsVisible = true;
                        });

                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            lastNameError.IsVisible = false;
                        });
                    }
                    
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        lastNameError.IsVisible = false;
                    });
                }

                if (Validate.FeildPopulated(volunteerMobileEntry.Text))
                {
                    if (Validate.ContainsLetterOrSymbol(volunteerMobileEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            mobileError.IsVisible = true;

                        });
                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            mobileError.IsVisible = false;
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        mobileError.IsVisible = false;
                    });
                }

                // Email 
                if (Validate.FeildPopulated(volunteerEmailEntry.Text))
                {
                    if (!Validate.EmailFormatValid(volunteerEmailEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            emailError.IsVisible = true;
                        });
                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            emailError.IsVisible = false;
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        emailError.IsVisible = false;
                    });
                }

                if ((!Validate.FeildPopulated(volunteerMobileEntry.Text)) && (!Validate.FeildPopulated(volunteerEmailEntry.Text)))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        contactError.IsVisible = true;
                    });
                    allFeildsValid = false;
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        contactError.IsVisible = false;
                    });

                }

                Device.BeginInvokeOnMainThread(() =>
                {

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

                            DisplayAlert("Volunteer Saved", "This volunteer has been inserted into the database", "OK");
                        }
                        else
                        {
                            DisplayAlert("Connection Error", "Please check connection and try again", "OK");
                        }

                        searchingIndicator.IsRunning = false;
                    }
                    else
                    {
                        allFeildsValid = true;
                        searchingIndicator.IsRunning = false;
                    }


                });

                
            });

                       

        }
    }
}