﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View.Volunteers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogVolunteerHoursDesktop : ContentPage
    {
        public LogVolunteerHoursDesktop(string title)
        {
            InitializeComponent();

            Title = title;
        }
    }
}