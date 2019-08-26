using MongoDB.Bson;
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
                // Hard coded data 
                birdResulsts.Add(new Bird {
                    Name = result,
                    BirdNumber = "3243",
                    WingspanNumber = "5434",
                    BandNumber = "5176",
                    Sex = "Male",
                    Age = "2",
                    Breed = "Morepork",
                    Status = "Captive"

                });
            }

            // Set result list item source to list of Bird objects
            ResultsListView.ItemsSource = birdResulsts;

        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Bird;

            BirdNumberValue.Text = item.BirdNumber;
            WingspanNumberValue.Text = item.WingspanNumber;
            BandNumberValue.Text = item.BandNumber;
            NameValue.Text = item.Name;
            AgeValue.Text = item.Age;
            BreedValue.Text = item.Breed;
            StatusValue.Text = item.Status;
            SexValue.Text = item.Sex;
        }
    }
}