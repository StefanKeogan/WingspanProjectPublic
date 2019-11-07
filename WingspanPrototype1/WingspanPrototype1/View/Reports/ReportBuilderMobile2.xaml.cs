using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View.Reports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportBuilderMobile2 : ContentPage
    {
        public ReportBuilderMobile2(ArrayList results)
        {
            InitializeComponent();

            if (results[0].GetType() == typeof(WildBird))
            {
                wildBirdListView.IsVisible = true;
                wildBirdListView.ItemsSource = results;
            }
            else if (results[0].GetType() == typeof(CaptiveBird))
            {
                captiveBirdListView.IsVisible = true;
                captiveBirdListView.ItemsSource = results;
            }
            else if (results[0].GetType() == typeof(Member))
            {
                memberListView.IsVisible = true;
                memberListView.ItemsSource = results;
            }
            else if (results[0].GetType() == typeof(Volunteer))
            {
                volunteerListView.IsVisible = true;
                volunteerListView.ItemsSource = results;
            }
            else if (results[0].GetType() == typeof(Sponsorship))
            {
                sponsorshipListView.IsVisible = true;
                sponsorshipListView.ItemsSource = results;
            }

        }






    }
}