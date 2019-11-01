using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectBirdResultsMobile2 : ContentPage
	{
		public SelectBirdResultsMobile2(ArrayList bird)
		{
			InitializeComponent();

            // What type of bird are we displaying
            if (bird[0].GetType() == typeof(WildBird))
            {
                DisplayWildBird(bird[0] as WildBird);
            }
            else if (bird[0].GetType() == typeof(CaptiveBird))
            {
                var captiveBird = bird[0] as CaptiveBird;

                //only display captive birds that are alive
                if ((captiveBird.DateDeceased == null) && (captiveBird.DateDeceased.ToString() == string.Empty))
                {
                    DisplayCaptiveBird(captiveBird);
                }
            }
            else
            {
                DisplayAlert("Error", "Could not get bird type", "Ok");
            }
        }


        private void DisplayWildBird(WildBird bird)
        {
            //if captive bird is visible, hide it
            if (selectCaptiveBirdGrid.IsVisible == true)
            {
                selectCaptiveBirdGrid.IsVisible = false;
            }


            //display Wingspan ID
            if ((bird.WingspanId != string.Empty) && (bird.WingspanId != null))
            {
                selectWildWingspanIdValueLabel.Text = bird.WingspanId;
                selectWildWingspanIdValueLabel.IsVisible = true;
            }
            else
            {
                selectWildWingspanIdValueLabel.IsVisible = false;
            }

            //display species
            if ((bird.Species != string.Empty) && (bird.Species != null))
            {
                selectWildSpeciesValueLabel.Text = bird.Species;
                selectWildSpeciesValueLabel.IsVisible = true;
            }
            else
            {
                selectWildSpeciesValueLabel.IsVisible = false;
            }

            //display location, if there is one
            if ((bird.Location != string.Empty) && (bird.Location != null))
            {
                selectWildLocationValueLabel.Text = bird.Location;
                selectWildLocationValueLabel.IsVisible = true;
            }
            else
            {
                selectWildLocationValueLabel.IsVisible = false;
            }

            //display sex of the bird
            if ((bird.Sex != string.Empty) && (bird.Sex != null))
            {
                selectWildSexValueLabel.Text = bird.Sex;
                selectWildSexValueLabel.IsVisible = true;
            }
            else
            {
                selectWildSexValueLabel.IsVisible = false;
            }

            //display age of bird
            if ((bird.Age != string.Empty) && (bird.Age != null))
            {
                selectWildAgeValueLabel.Text = bird.Age;
                selectWildAgeValueLabel.IsVisible = true;
            }
            else
            {
                selectWildAgeValueLabel.IsVisible = false;
            }

            //display metal band number, if there is one
            if ((bird.MetalBand != string.Empty) && (bird.MetalBand != null))
            {
                selectWildMetalBandIdValueLabel.Text = bird.MetalBand;
                selectWildMetalBandIdValueLabel.IsVisible = true;
            }
            else
            {
                selectWildMetalBandIdValueLabel.IsVisible = false;
            }

            //display any metal band info
            if ((bird.BandInfo != string.Empty) && (bird.BandInfo != null))
            {
                selectBandInfoValueLabel.Text = bird.BandInfo;
                selectBandInfoValueLabel.IsVisible = true;
            }
            else
            {
                selectBandInfoValueLabel.IsVisible = false;
            }

            //set these variables for this bird
            SetWildBirdDetails(bird);
        }


        private void DisplayCaptiveBird(CaptiveBird bird)
        {
            //if wild bird grid is visible, hide it
            if (selectWildBirdGrid.IsVisible == true)
            {
                selectWildBirdGrid.IsVisible = false;
            }


            //display Wingspan ID 
            if ((bird.WingspanId != string.Empty) && (bird.WingspanId != null))
            {
                selectCaptiveWingspanIdValueLabel.Text = bird.WingspanId;
                selectCaptiveWingspanIdValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveWingspanIdValueLabel.IsVisible = false;
            }

            //display bird name
            if ((bird.Name != string.Empty) && (bird.Name != null))
            {
                selectCaptiveNameValueLabel.Text = bird.Name;
                selectCaptiveNameValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveNameValueLabel.IsVisible = false;
            }

            //display band number
            if ((bird.BandNo != string.Empty) && (bird.BandNo != null))
            {
                selectCaptiveBandNumberValueLabel.Text = bird.BandNo;
                selectCaptiveBandNumberValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveBandNumberValueLabel.IsVisible = false;
            }

            //display species
            if ((bird.Species != string.Empty) && (bird.Species != null))
            {
                selectCaptiveSpeciesValueLabel.Text = bird.Species;
                selectCaptiveSpeciesValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveSpeciesValueLabel.IsVisible = false;
            }

            //display sex
            if ((bird.Sex != string.Empty) && (bird.Sex != null))
            {
                selectCaptiveSexValueLabel.Text = bird.Sex;
                selectCaptiveSexValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveSexValueLabel.IsVisible = false;
            }

            //display age
            if ((bird.Age != string.Empty) && (bird.Age != null))
            {
                selectCaptiveAgeValueLabel.Text = bird.Age;
                selectCaptiveAgeValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveAgeValueLabel.IsVisible = false;
            }

            //set these variables for this bird
            SetCaptiveBirdDetails(bird);
        }


        //set 'global' variable for captive sponsorship
        private void SetCaptiveBirdDetails(CaptiveBird bird)
        {
            SponsorshipDetails.thisWingspanID = bird.WingspanId;
        }

        //set 'global' variable for wild sponsorship
        private void SetWildBirdDetails(WildBird bird)
        {
            SponsorshipDetails.thisWingspanID = bird.WingspanId;
        }


        private async void ThisBirdButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bird added", "", "OK");
            await Navigation.PopToRootAsync();
        }
    }
}