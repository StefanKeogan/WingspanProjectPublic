using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WingspanPrototype1.Controller;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Model;
using WingspanPrototype1.Functions;

namespace WingspanPrototype1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMemberForm : ContentPage
    {
        public AddMemberForm(string title)
        {
            InitializeComponent();

            Title = title;

            // What type of device are we running on 
            if (DeviceSize.ScreenArea() <= 783457)
            {
                memberMargin1.Width = 0;
                memberMargin2.Width = 0;
            }

        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            searchingIndicator.IsEnabled = true;
            searchingIndicator.IsVisible = true;
            searchingIndicator.IsRunning = true;

            bool allFeildsValid = true;

            await Task.Run(() =>
            {
                // First Name Validation
                if (Validate.FeildPopulated(memberFirstNameEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(memberFirstNameEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            firstNameError.IsVisible = true;
                            firstNameError.Text = "First Name feild can not contain numbers or symbols";
                        });
                        
                        allFeildsValid = false;
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        firstNameError.IsVisible = true;
                        firstNameError.Text = "First Name feild must be filled in";
                    });

                    allFeildsValid = false;
                }

                // Last Name Validation
                if (Validate.FeildPopulated(memberLastNameEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(memberFirstNameEntry.Text))
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

                // Salutation Name Validation
                if (Validate.FeildPopulated(memberLastNameEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(memberLastNameEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            salutationNameError.IsVisible = true;
                        });

                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            salutationNameError.IsVisible = false;
                        });
                    }
                }

                // Email Name Validation
                if (Validate.FeildPopulated(memberEmailEntry.Text))
                {
                    if (Validate.EmailFormatValid(memberEmailEntry.Text))
                    {
                        emailError.IsVisible = false;
                    }
                    else
                    {
                        emailError.IsVisible = true;
                    }
                }

                // City 
                if (Validate.FeildPopulated(memberCityEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(memberCityEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            cityError.IsVisible = true;
                        });

                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            cityError.IsVisible = false;
                        });
                    }
                }

                // Postcode 
                if (Validate.FeildPopulated(memberPostcodeEntry.Text))
                {
                    if (Validate.ContainsLetterOrSymbol(memberPostcodeEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            postcodeError.IsVisible = true;
                        });

                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            postcodeError.IsVisible = false;

                        });
                    }
                }

                // Country 
                if (Validate.FeildPopulated(memberCountryEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(memberCountryEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            countryError.IsVisible = true;
                        });

                        allFeildsValid = false;
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            countryError.IsVisible = false;
                        });
                    }
                }

                if (allFeildsValid)
                {
                    bool memberInserted = AddMember.InsertMemberDocument(new Member
                    {
                        FirstName = memberFirstNameEntry.Text,
                        LastName = memberLastNameEntry.Text,
                        SalutationName = memberSalutationNameEntry.Text,
                        Email = memberEmailEntry.Text,
                        Address1 = memberAddress1Entry.Text,
                        Address2 = memberAddress2Entry.Text,
                        Address3 = memberAddress3Entry.Text,
                        City = memberCityEntry.Text,
                        Country = memberCountryEntry.Text,
                        Postcode = memberPostcodeEntry.Text,
                        Comment = memberCommentEditor.Text,
                        JoinDate = memberJoindatePicker.Date

                    }); ;
                   
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (memberInserted)
                        {
                            memberFirstNameEntry.Text = null;
                            memberLastNameEntry.Text = null;
                            memberSalutationNameEntry.Text = null;
                            memberEmailEntry.Text = null;
                            memberAddress1Entry.Text = null;
                            memberAddress2Entry.Text = null;
                            memberAddress3Entry.Text = null;
                            memberCityEntry.Text = null;
                            memberCountryEntry.Text = null;
                            memberPostcodeEntry.Text = null;
                            memberCommentEditor.Text = null;

                            DisplayAlert("Member Added", "Member document inserted into the database", "OK");
                        }
                        else
                        {
                            DisplayAlert("Connenction Error", "Could not insert bird record, please check connection and try again", "OK");

                        }

                        searchingIndicator.IsEnabled = false;
                    });


                }
                else
                {
                    allFeildsValid = true;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        searchingIndicator.IsEnabled = false;
                    });
                }

            });

            

            


        }
    }
}