using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBirdNoteMobile1 : ContentPage
    {
        public AddBirdNoteMobile1(string title)
        {
            InitializeComponent();

            Title = title;

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    searchButton.Source = "Assets/search-icon-small.png";
                    break;
                case Device.Android:
                    searchButton.Source = "searchiconsmall.png";
                    break;
                default:
                    searchButton.Source = "Assets/search-icon-small.png";
                    break;
            }

        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem.GetType() == typeof(WildBird))
            {
                var item = e.SelectedItem as WildBird;

                Navigation.PushAsync(new AddBirdNoteMobile2(item, null));

            }
            else if (e.SelectedItem.GetType() == typeof(CaptiveBird))
            {
                var item = e.SelectedItem as CaptiveBird;

                Navigation.PushAsync(new AddBirdNoteMobile2(null, item));

            }

            
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            searchingIndicator.IsRunning = true;

            await Task.Run(() =>
            {
                // Validation
                if (!Validate.FeildPopulated(wingspanIdEntry.Text))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Feild Empty", "The search feild can not be empty", "OK");
                        searchingIndicator.IsRunning = false;
                    });

                    return;
                }

                ArrayList results = SearchBirds.Search(wingspanIdEntry.Text, null, null);

                if (results.Count > 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        resultsListView.ItemsSource = results;
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("No Birds Found", "That bird could not be found", "OK");
                    });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    searchingIndicator.IsRunning = false;
                });

            });


        }
    }
}