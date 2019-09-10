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
    public partial class AddBird : ContentPage
    {
        public AddBird(string title)
        {
            InitializeComponent();

            Title = title;

            // SexPicker.ItemsSource = new string[]{ "Male", "Female"};
            // BreedPicker.ItemsSource = new string[]{ "Morepork", "Falcon", "Hawk"};
            // StatusPicker.ItemsSource = new string[]{ "Captive", "Wild"};

            // What type of bird are we adding?
            if (title == "New Wild Bird")
            {
                SetWildBirdFormFeilds();
            }
            else if (title == "New Captive Bird")
            {
                SetCaptiveBirdFormFeilds();
            }
            else
            {
                // Display error message
                addBirdGrid.Children.Add(new Label {Text = "Error" }, 0, 0);

            }

            // What device are we running on?
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    addBirdStackLayout.Margin = new Thickness(300, 20, 300, 20);
                    break;
                case Device.Android:
                    addBirdStackLayout.Margin = new Thickness(5, 5, 5, 5);
                    break;
                default:
                    addBirdStackLayout.Margin = new Thickness(300, 20, 300, 20);
                    break;
            }


        }

        public void SetWildBirdFormFeilds()
        {
            List<BirdFormItem> formItems = new List<BirdFormItem>(); // Stores form items

            // Add form items to list
            formItems.Add(new BirdFormItem { LabelName = "speciesLabel", LabelText = "Species: ", FeildName = "species", FeildType = typeof(Picker) });
            formItems.Add(new BirdFormItem { LabelName = "locationLabel", LabelText = "Location: ", FeildName = "location", FeildType = typeof(Entry) });
            formItems.Add(new BirdFormItem { LabelName = "gpsLabel", LabelText = "GPS: ", FeildName = "gps", FeildType = typeof(Entry) });
            formItems.Add(new BirdFormItem { LabelName = "sexLabel", LabelText = "Sex: ", FeildName = "sex", FeildType = typeof(Picker) });
            formItems.Add(new BirdFormItem { LabelName = "ageLabel", LabelText = "Age: ", FeildName = "age", FeildType = typeof(Entry) });
            formItems.Add(new BirdFormItem { LabelName = "metalBandLabel", LabelText = "Metal Band Id: ", FeildName = "metalband", FeildType = typeof(Entry) });
            formItems.Add(new BirdFormItem { LabelName = "dateBandedLabel", LabelText = "Date Banded: ", FeildName = "date", FeildType = typeof(DatePicker) });
            formItems.Add(new BirdFormItem { LabelName = "banderNameLabel", LabelText = "Bander Name: ", FeildName = "bandername", FeildType = typeof(Entry) });
            formItems.Add(new BirdFormItem { LabelName = "noteLabel", LabelText = "Initial Note: ", FeildName = "note", FeildType = typeof(Editor) });

            int rowIndex = 0;

            foreach (var formItem in formItems) // Add form items to grid
            { 
                addBirdGrid.RowDefinitions.Add(new RowDefinition()); // Add an aditional row to store the next items

                Label label = new Label { Text = formItem.LabelText, VerticalOptions = LayoutOptions.Center, FontSize = 20 };

                // What feild type do we need to display?
                if (formItem.FeildType == typeof(Entry)) 
                {
                    // If the feild is an entry, ad an entry to the grid
                    Entry feild = new Entry { VerticalOptions = LayoutOptions.Center, StyleId = formItem.FeildName };
                    addBirdGrid.Children.Add(feild, 1, rowIndex);
                }
                else if (formItem.FeildType == typeof(Picker)) 
                {
                    // If the feild is a picker, add a picker to the grid
                    Picker feild = new Picker { VerticalOptions = LayoutOptions.Center, StyleId = formItem.FeildName };

                    switch (formItem.FeildName) // What items do we need to add to the picker?
                    {
                        case "species":
                            feild.ItemsSource = new string[] { "Falcon", "Hawk", "Morepork", "Ruru" };
                            break;
                        case "sex":
                            feild.ItemsSource = new string[] { "Male", "Female"};
                            break;
                        default:
                            break;
                    }

                    addBirdGrid.Children.Add(feild, 1, rowIndex);
                }
                else if (formItem.FeildType == typeof(DatePicker))
                {
                    // If the feild is a date picker, add a picker to the grid
                    DatePicker feild = new DatePicker { VerticalOptions = LayoutOptions.Center, StyleId = formItem.FeildName};
                    addBirdGrid.Children.Add(feild, 1, rowIndex);

                }
                else if (formItem.FeildType == typeof(Editor))
                {
                    // If the feild is an editor, add a picker to the grid
                    Editor feild = new Editor {  VerticalOptions = LayoutOptions.Center, StyleId = formItem.FeildName, MaxLength = 250, AutoSize = EditorAutoSizeOption.TextChanges };
                    addBirdGrid.Children.Add(feild, 1, rowIndex);

                }

                addBirdGrid.Children.Add(label, 0, rowIndex);

                rowIndex++;
            }


        }

        public void SetCaptiveBirdFormFeilds()
        {
            List<BirdFormItem> formItems = new List<BirdFormItem>(); // Stores form items

            formItems.Add(new BirdFormItem { LabelName = "nameLabel", LabelText = "Name: ", FeildName = "name", FeildType = typeof(Entry) });
            formItems.Add(new BirdFormItem { LabelName = "bandNoLabel", LabelText = "Band Number: ", FeildName = "bandnumber", FeildType = typeof(Entry) });
            formItems.Add(new BirdFormItem { LabelName = "bandColourLabel", LabelText = "Band Colour: ", FeildName = "bandcolour", FeildType = typeof(Picker) });
            formItems.Add(new BirdFormItem { LabelName = "speciesLabel", LabelText = "Species: ", FeildName = "species", FeildType = typeof(Picker) });
            formItems.Add(new BirdFormItem { LabelName = "sexLabel", LabelText = "Sex: ", FeildName = "sex", FeildType = typeof(Picker) });
            formItems.Add(new BirdFormItem { LabelName = "ageLabel", LabelText = "Age: ", FeildName = "age", FeildType = typeof(Picker) });
            formItems.Add(new BirdFormItem { LabelName = "locationLabel", LabelText = "Location: ", FeildName = "location", FeildType = typeof(Picker) });
            formItems.Add(new BirdFormItem { LabelName = "dateArrivedLabel", LabelText = "Date Arrived: ", FeildName = "datearrived", FeildType = typeof(DatePicker) });
            formItems.Add(new BirdFormItem { LabelName = "noteLabel", LabelText = "Initial Note: ", FeildName = "note", FeildType = typeof(Editor) });

            int rowIndex = 0;

            foreach (var formItem in formItems) // Add form items to grid
            {
                addBirdGrid.RowDefinitions.Add(new RowDefinition()); // Add an aditional row to store the next items

                Label label = new Label { Text = formItem.LabelText, VerticalOptions = LayoutOptions.Center, FontSize = 20 };

                // What feild type do we need to display?
                if (formItem.FeildType == typeof(Entry))
                {
                    Entry feild = new Entry { VerticalOptions = LayoutOptions.Center, StyleId = formItem.FeildName };
                    addBirdGrid.Children.Add(feild, 1, rowIndex);
                }
                else if (formItem.FeildType == typeof(Picker)) // Is the feild a picker
                {
                    Picker feild = new Picker { VerticalOptions = LayoutOptions.Center, StyleId = formItem.FeildName };

                    switch (formItem.FeildName) // What items do we need to add to the picker?
                    {
                        case "species":
                            feild.ItemsSource = new string[] { "Falcon", "Hawk", "Morepork", "Ruru" };
                            break;
                        case "sex":
                            feild.ItemsSource = new string[] { "Male", "Female" };
                            break;
                        case "bandcolour":
                            feild.ItemsSource = new string[] { "M:O", "M:Y", "O:M", "M:W", "B:M" };
                            break;
                        case "location":
                            feild.ItemsSource = new string[] { "Auckland", "Tauranga", "Rotorua", "Mamaku", "Hamilton", "Matamata", "Whakatane", "Reporoa" }; // Could get list form database
                            break;
                        case "age":
                            feild.ItemsSource = new string[] { "Juvenile", "Adult" };
                            break;

                        default:
                            break;
                    }
                    addBirdGrid.Children.Add(feild, 1, rowIndex);
                }
                else if (formItem.FeildType == typeof(DatePicker))
                {
                    DatePicker feild = new DatePicker { VerticalOptions = LayoutOptions.Center, StyleId = formItem.FeildName };
                    addBirdGrid.Children.Add(feild, 1, rowIndex);


                }
                else if (formItem.FeildType == typeof(Editor))
                {
                    Editor feild = new Editor { VerticalOptions = LayoutOptions.Center, StyleId = formItem.FeildName, MaxLength = 100, AutoSize = EditorAutoSizeOption.TextChanges };
                    addBirdGrid.Children.Add(feild, 1, rowIndex);

                }

                addBirdGrid.Children.Add(label, 0, rowIndex);

                rowIndex++;
            }

        }

    }
}