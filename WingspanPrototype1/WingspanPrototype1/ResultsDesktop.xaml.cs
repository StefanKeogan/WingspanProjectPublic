﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsDesktop : ContentPage
    {
        public ResultsDesktop(List<string> results)
        {
            InitializeComponent();

            // Convert hard coded result data into Bird objects
            List<Bird> birdResulsts = new List<Bird>();
            foreach (var result in results)
            {
                birdResulsts.Add(new Bird { Name = result });
            }

            // Set result list item source to list of Bird objects
            ResultsListView.ItemsSource = birdResulsts;



        }
    }
}