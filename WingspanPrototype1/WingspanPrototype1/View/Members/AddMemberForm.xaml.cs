﻿using System;
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
            addingIndicator.IsRunning = true;
            addButton.IsEnabled = false;

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
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            firstNameError.IsVisible = false;
                        });

                    }
                }
               
                if (!Validate.FeildPopulated(memberFirstNameEntry.Text) && (!Validate.FeildPopulated(memberCompanyEntry.Text)))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        firstNameError.IsVisible = true;
                        companyError.IsVisible = true;
                        firstNameError.Text = "Either the First Name or Company feilds must be filled in";
                    });

                    allFeildsValid = false;
                }


                if (Validate.FeildPopulated(memberFirstNameEntry.Text) || (Validate.FeildPopulated(memberCompanyEntry.Text)))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        firstNameError.IsVisible = false;
                        companyError.IsVisible = false;
                    });
                }

                // Last Name Validation
                if (Validate.FeildPopulated(memberLastNameEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(memberLastNameEntry.Text))
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

                // Salutation Name Validation
                if (Validate.FeildPopulated(memberSalutationNameEntry.Text))
                {
                    if (Validate.ContainsNumberOrSymbol(memberSalutationNameEntry.Text))
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
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        salutationNameError.IsVisible = false;
                    });
                }

                // Email Name Validation
                if (Validate.FeildPopulated(memberEmailEntry.Text))
                {
                    if (Validate.EmailFormatValid(memberEmailEntry.Text))
                    {
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            emailError.IsVisible = false ;
                        });
                        
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            emailError.IsVisible = true;
                        });

                        allFeildsValid = false;
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        emailError.IsVisible = false;
                    });
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
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        cityError.IsVisible = false;
                    });
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
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        postcodeError.IsVisible = false;
                    });
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
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        countryError.IsVisible = false;
                    });
                }

                if (allFeildsValid)
                {
                    bool memberInserted = AddMember.InsertMemberDocument(new Member
                    {
                        Number = Convert.ToInt64(memberNumberEntry.Text),
                        FirstName = memberFirstNameEntry.Text,
                        LastName = memberLastNameEntry.Text,
                        SalutationName = memberSalutationNameEntry.Text,
                        Email = memberEmailEntry.Text,
                        Company = memberCompanyEntry.Text,
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
                            memberNumberEntry.Text = null;
                            memberFirstNameEntry.Text = null;
                            memberLastNameEntry.Text = null;
                            memberSalutationNameEntry.Text = null;
                            memberEmailEntry.Text = null;
                            memberCompanyEntry.Text = null;
                            memberAddress1Entry.Text = null;
                            memberAddress2Entry.Text = null;
                            memberAddress3Entry.Text = null;
                            memberCityEntry.Text = null;
                            memberCountryEntry.Text = null;
                            memberPostcodeEntry.Text = null;
                            memberCommentEditor.Text = null;

                            DisplayAlert("Member Added", "This member has been saved", "OK");
                        }
                        else
                        {
                            DisplayAlert("Connenction Error", "Could not insert member record, please check your connection and try again", "OK");

                        }

                        addingIndicator.IsRunning = false;
                        addButton.IsEnabled = true;
                    });


                }
                else
                {
                    allFeildsValid = true;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        addingIndicator.IsRunning = false;
                        addButton.IsEnabled = true;
                    });
                }

            });

            

            


        }
    }
}