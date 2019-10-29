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
    public partial class AddBirdNoteDesktop : ContentPage
    {
        public string selectedWingspanId;

        public AddBirdNoteDesktop(string title)
        {
            InitializeComponent();

            Title = title;

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    searchButton.Source = "Assets/search-icon.png";
                    break;
                case Device.Android:
                    searchButton.Source = "searchicon.png";
                    break;
                default:
                    searchButton.Source = "Assets/search-icon.png";
                    break;
            }

            categoryPicker.ItemsSource = new string[] { "Medical", "Transfer", "Release" };

        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            addingIndicatior.IsRunning = true;

            await Task.Run(() =>
            {
                if (categoryPicker.SelectedIndex == -1)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Feild Empty", "The category feild can not be empty", "OK");
                        addingIndicatior.IsRunning = false;
                    });

                    return;
                }

                if (!Validate.FeildPopulated(noteEditor.Text))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Feild Empty", "The comment feild can not be empty", "OK");
                        addingIndicatior.IsRunning = false;
                    });

                    return;
                }


                if (AddBirdNote.InsertNoteDocument(new Note
                {
                    Date = noteDatePicker.Date,
                    Category = categoryPicker.SelectedItem.ToString(),
                    Comment = noteEditor.Text,
                    WingspanId = selectedWingspanId
                }))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        categoryPicker.SelectedIndex = -1;
                        noteEditor.Text = null;
                        DisplayAlert("Bird Note Added", "Your note has been added to this birds note history", "OK");
                    });

                    return;
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    addingIndicatior.IsRunning = false;
                });

            });



        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem.GetType() == typeof(WildBird))
            {
                var item = e.SelectedItem as WildBird;

                selectedWingspanId = item.WingspanId;
            }
            else if (e.SelectedItem.GetType() == typeof(CaptiveBird))
            {
                var item = e.SelectedItem as CaptiveBird;

                selectedWingspanId = item.WingspanId;
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
