﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBirdNote : ContentPage
    {
        public AddBirdNote(string title)
        {
            InitializeComponent();

            Title = title;

            categoryPicker.ItemsSource = new string[] { "Medical", "Transfer", "Release" };
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Note Added", "Note has been added to Professor Feathers", "Ok");
        }
    }
}