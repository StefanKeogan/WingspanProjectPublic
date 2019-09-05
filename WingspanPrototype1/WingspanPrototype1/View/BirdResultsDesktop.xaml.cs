using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BirdResultsDesktop : ContentPage
    {

        public BirdResultsDesktop(List<WildBird> results, Type birdType)
        {
            InitializeComponent();          

            // Set result list item source to list of Bird objects
            ResultsListView.ItemsSource = results;

            if (birdType == typeof(WildBird))
            {
                ResultsListView.ItemSelected += ResultsListView_ItemSelected_Wild;
            }
            else
            {

            }


        }

        public void ResultsListView_ItemSelected_Wild(object sender, SelectedItemChangedEventArgs e)
        {
            birdResultsGrid.Children.Clear();
            birdResultsGrid.RowDefinitions.Clear();

            var item = e.SelectedItem as WildBird;

            List<BirdResultsItem> resultItems = new List<BirdResultsItem>();
            resultItems.Add(new BirdResultsItem { KeyLabelName = "wingspanIdLabel", KeyLabelText = "Wingspan Id: ", ValueLabelName = "wingspanIdValueLabel", ValueLabelText = item.WingspanId });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "speciesLabel", KeyLabelText = "Species: ", ValueLabelName = "speciesValueLabel", ValueLabelText = item.Species });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "locationLabel", KeyLabelText = "Location: ", ValueLabelName = "locationValueLabel", ValueLabelText = item.Location }); ;
            resultItems.Add(new BirdResultsItem { KeyLabelName = "gpsLabel", KeyLabelText = "GPS: ", ValueLabelName = "gpsValueLabel", ValueLabelText = item.Gps });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "sexLabel", KeyLabelText = "Sex: ", ValueLabelName = "sexValueLabel", ValueLabelText = item.Sex });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "ageLabel", KeyLabelText = "Age: ", ValueLabelName = "ageValueLabel" , ValueLabelText = item.Age});
            resultItems.Add(new BirdResultsItem { KeyLabelName = "metalBandIdLabel", KeyLabelText = "Metal Band Id: ", ValueLabelName = "metalBandIdValueLabel", ValueLabelText = item.MetalBandId });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "dateBandedLabel", KeyLabelText = "Date Banded: ", ValueLabelName = "dateBandedValueLabel", ValueLabelText = item.DateBanded.ToString() }); 
            resultItems.Add(new BirdResultsItem { KeyLabelName = "banderNameLabel", KeyLabelText = "Bander Name: ", ValueLabelName = "banderValueLabel" , ValueLabelText = item.BanderName});

            int rowIndex = 0;

            foreach (BirdResultsItem resultsItem in resultItems)
            {
                birdResultsGrid.RowDefinitions.Add(new RowDefinition());

                birdResultsGrid.Children.Add(new Label { Text = resultsItem.KeyLabelText, FontSize = 20 }, 0, rowIndex);
                birdResultsGrid.Children.Add(new Label { Text = resultsItem.ValueLabelText, FontSize = 20 }, 1, rowIndex);

                rowIndex++;
            }

        }

        public void ResultsListView_ItemSelected_Captive(object sender, SelectedItemChangedEventArgs e)
        {
            birdResultsGrid.Children.Clear();
            birdResultsGrid.RowDefinitions.Clear();

            var item = e.SelectedItem as WildBird;

            List<BirdResultsItem> resultItems = new List<BirdResultsItem>();
            resultItems.Add(new BirdResultsItem { KeyLabelName = "wingspanIdLabel", KeyLabelText = "Wingspan Id: ", ValueLabelName = "wingspanIdValueLabel", ValueLabelText = item.WingspanId });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "speciesLabel", KeyLabelText = "Species: ", ValueLabelName = "speciesValueLabel", ValueLabelText = item.Species });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "locationLabel", KeyLabelText = "Location: ", ValueLabelName = "locationValueLabel", ValueLabelText = item.Location }); ;
            resultItems.Add(new BirdResultsItem { KeyLabelName = "gpsLabel", KeyLabelText = "GPS: ", ValueLabelName = "gpsValueLabel", ValueLabelText = item.Gps });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "sexLabel", KeyLabelText = "Sex: ", ValueLabelName = "sexValueLabel", ValueLabelText = item.Sex });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "ageLabel", KeyLabelText = "Age: ", ValueLabelName = "ageValueLabel", ValueLabelText = item.Age });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "metalBandIdLabel", KeyLabelText = "Metal Band Id: ", ValueLabelName = "metalBandIdValueLabel", ValueLabelText = item.MetalBandId });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "dateBandedLabel", KeyLabelText = "Date Banded: ", ValueLabelName = "dateBandedValueLabel", ValueLabelText = item.DateBanded.ToString() });
            resultItems.Add(new BirdResultsItem { KeyLabelName = "banderNameLabel", KeyLabelText = "Bander Name: ", ValueLabelName = "banderValueLabel", ValueLabelText = item.BanderName });

            int rowIndex = 0;

            foreach (BirdResultsItem resultsItem in resultItems)
            {
                birdResultsGrid.RowDefinitions.Add(new RowDefinition());

                birdResultsGrid.Children.Add(new Label { Text = resultsItem.KeyLabelText, FontSize = 20 }, 0, rowIndex);
                birdResultsGrid.Children.Add(new Label { Text = resultsItem.ValueLabelText, FontSize = 20 }, 1, rowIndex);

                rowIndex++;
            }

        }

    }
}