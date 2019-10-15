using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View.Volunteers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogVolunteerHoursMobile1 : ContentPage
    {
        public LogVolunteerHoursMobile1(string title)
        {
            InitializeComponent();

            Title = title;

            resultsListView.ItemsSource = new List<Volunteer> { new Volunteer { Name = "Person" , Email = "person@email.co.nz"} };
        }

        private void resultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new LogVolunteerHoursMobile2());
        }
    }
}