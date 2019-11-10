using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Controller.WingspanId;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsMenu : ContentPage
    {
        public SettingsMenu(string title)
        {
            InitializeComponent();

            Title = title;

            IdValue wildWingspanId  = FindIdValue.GetWingspanIdValue("WildIdValue");
            IdValue captiveWingspanId = FindIdValue.GetWingspanIdValue("CaptiveIdValue");

            if ((wildWingspanId != null) || (captiveWingspanId != null))
            {
                wildWingspanIdValue.Text = wildWingspanId.Value.ToString();
                captiveWingspanIdValue.Text = captiveWingspanId.Value.ToString();
            }
            else
            {
                DisplayAlert("Connection Error", "Could not get Id values, please check your connection and try again", "OK");
            }                       

        }

        private void updateWildIdButton_Clicked(object sender, EventArgs e)
        {
            // Update value 
            bool updated = UpdateIdValue.UpdateIdDocument("WildIdValue", Convert.ToInt32(newWildWingspanIdEntry.Text));

            if (updated)
            {
                IdValue idValue = FindIdValue.GetWingspanIdValue("WildIdValue");

                wildWingspanIdValue.Text = idValue.Value.ToString();

                // Clear entry
                newWildWingspanIdEntry.Text = null;

                DisplayAlert("Wild Id Value Updated", "The wild wingspan id value is now: " + idValue.Value.ToString(), "OK");
            }
            else
            {
                DisplayAlert("Failed to Update", "Please check your connection and try again", "OK");
            }
        }

        private void updateCaptiveIdButton_Clicked(object sender, EventArgs e)
        {
            // Update value 
            bool updated = UpdateIdValue.UpdateIdDocument("CaptiveIdValue", Convert.ToInt32(newCaptiveWingspanIdEntry.Text));

            // Has value been updated successfully?
            if (updated)
            {
                // Get value 
                IdValue idValue = FindIdValue.GetWingspanIdValue("CaptiveIdValue");

                captiveWingspanIdValue.Text = idValue.Value.ToString();

                // Clear entry
                newCaptiveWingspanIdEntry.Text = null;

                DisplayAlert("Captive Id Value Updated", "The captive wingspan id value is now: " + idValue.Value.ToString(), "OK");
            }
            else
            {
                DisplayAlert("Failed to Update", "Please check your connection and try again", "OK");
            }
        }
    }
}