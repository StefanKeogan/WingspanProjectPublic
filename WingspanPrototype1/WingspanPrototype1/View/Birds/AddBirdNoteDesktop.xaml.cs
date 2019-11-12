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
        public string selectedWingspanId = null;

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

        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            bool allFeildsValid = true;

            addingIndicatior.IsRunning = true;
            addButton.IsEnabled = false;

            await Task.Run(() =>
            {
                if (selectedWingspanId == null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("No Bird Selected", "A bird must be selected to add a note", "OK");
                        addingIndicatior.IsRunning = false;
                    });

                    return;
                }

                if (categoryPicker.SelectedIndex == -1)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        noteCategoryError.IsVisible = true;                      
                        allFeildsValid = false;
                    });

                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        noteCategoryError.IsVisible = false;
                    });
                }

                if (!Validate.FeildPopulated(noteEditor.Text))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {                       
                        noteError.IsVisible = true;                       
                    });
                    allFeildsValid = false;

                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        noteError.IsVisible = false;
                    });
                }

                if (allFeildsValid)
                {

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
                            addingIndicatior.IsRunning = false;
                            addButton.IsEnabled = true;
                        });
                        return;
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        addingIndicatior.IsRunning = false;
                        addButton.IsEnabled = true;
                    });

                    allFeildsValid = true;
                }




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
            searchButton.IsEnabled = false;

            await Task.Run(() =>
            {
                // Validation
                if (!Validate.FeildPopulated(wingspanIdEntry.Text))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Feild Empty", "The search feild can not be empty", "OK");
                        searchingIndicator.IsRunning = false;
                        searchButton.IsEnabled = true;
                    });

                    return;
                }

                ArrayList results = SearchBirds.Search(wingspanIdEntry.Text, null, null);

                if (results != null)
                {
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
                    searchButton.IsEnabled = true;
                });


            });


        }
    }

    
}
