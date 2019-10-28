using System;
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
    public partial class AddBirdNoteMobile2 : ContentPage
    {
        string selectedWingspanId;

        public AddBirdNoteMobile2(WildBird wildBird, CaptiveBird captiveBird)
        {
            InitializeComponent();

            if (wildBird != null)
            {
                selectedWingspanId = wildBird.WingspanId;
            }
            else if (captiveBird != null)
            {
                selectedWingspanId = captiveBird.WingspanId;
            }

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
                        addingIndicatior.IsRunning = false;
                        DisplayAlert("Feild Empty", "The category feild can not be empty", "OK");
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

                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    addingIndicatior.IsRunning = false;
                });

            });



        }



    }
}