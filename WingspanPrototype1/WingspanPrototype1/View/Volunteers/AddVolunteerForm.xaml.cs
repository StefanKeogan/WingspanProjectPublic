﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Controller.Volunteers;
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
            // Insert volunteer document
            bool inserted = AddVolunteer.InsertVolunteerDocument(new Volunteer
            {
                Name = nameEntry.Text,
                Mobile = Convert.ToInt64(mobileEntry.Text),
                Email = emailEntry.Text

            }) ;

            // Was the document inserted successfully?
            if (inserted)
            {
                nameEntry.Text = null;
                mobileEntry.Text = null;
                emailEntry.Text = null;

                await DisplayAlert("Volunteer Saved", "This volunteer has been inserted into the database", "OK");
            }
            else
            {
                await DisplayAlert("Connection Error", "Please check connection and try again", "OK");
            }

        }
    }
}