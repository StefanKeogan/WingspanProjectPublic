using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectBirdResultsDesktop : ContentPage
	{

        private string selectedWingspanId;

        public SelectBirdResultsDesktop (ArrayList results)
		{
			InitializeComponent ();
            resultsListView.ItemsSource = results;
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem;

            if (item != null)
            {
                selectBirdSearchGrid.IsVisible = true;

                if (item.GetType() == typeof(WildBird))
                {
                    var wildItem = item as WildBird;

                    DisplayWildBird(wildItem);

                    selectedWingspanId = wildItem.WingspanId;

                    selectWildBirdGrid.IsVisible = true;
                    selectCaptiveBirdGrid.IsVisible = false;

                    // id = wildItem._id;
                    // birdType = typeof(WildBird);
                }
                else if (item.GetType() == typeof(CaptiveBird))
                {
                    var captiveItem = item as CaptiveBird;

                    // Only display captive birds that are alive
                    if ((captiveItem.DateDeceased == null) && (captiveItem.DateDeceased.ToString() == string.Empty))
                    {
                        DisplayCaptiveBird(captiveItem);
                    }

                    DisplayCaptiveBird(captiveItem);

                    selectedWingspanId = captiveItem.WingspanId;

                    selectCaptiveBirdGrid.IsVisible = true;
                    selectWildBirdGrid.IsVisible = false;

                    //id = captiveItem._id;
                    //birdType = typeof(CaptiveBird);
                }
            }
          
        }
     
        private void DisplayWildBird(WildBird bird)
        {
            ////if captive bird is visible, hide it
            //if (selectCaptiveBirdGrid.IsVisible == true)
            //{
            //    selectCaptiveBirdGrid.IsVisible = false;
            //}

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
            ////if wild bird grid is visible, hide it
            //if (selectWildBirdGrid.IsVisible == true)
            //{
            //    selectWildBirdGrid.IsVisible = false;
            //}

            //display Wingspan ID 
            if (Validate.FeildPopulated(bird.WingspanId))
            {
                selectCaptiveWingspanIdValueLabel.Text = bird.WingspanId;
                selectCaptiveWingspanIdValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveWingspanIdValueLabel.IsVisible = false;
            }

            //display bird name
            if (Validate.FeildPopulated(bird.Name))
            {
                selectCaptiveNameValueLabel.Text = bird.Name;
                selectCaptiveNameValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveNameValueLabel.IsVisible = false;
            }

            //display band number
            if (Validate.FeildPopulated(bird.BandNo))
            {
                selectCaptiveBandNumberValueLabel.Text = bird.BandNo;
                selectCaptiveBandNumberValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveBandNumberValueLabel.IsVisible = false;
            }

            //display species
            if (Validate.FeildPopulated(bird.Species))
            {
                selectCaptiveSpeciesValueLabel.Text = bird.Species;
                selectCaptiveSpeciesValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveSpeciesValueLabel.IsVisible = false;
            }

            //display sex
            if (Validate.FeildPopulated(bird.Sex))
            {
                selectCaptiveSexValueLabel.Text = bird.Sex;
                selectCaptiveSexValueLabel.IsVisible = true;
            }
            else
            {
                selectCaptiveSexValueLabel.IsVisible = false;
            }

            //display age
            if (Validate.FeildPopulated(bird.Sex))
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

        //choose this bird button
        private async void ThisBirdButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bird added", "You have selected this bird to sponsor", "OK");

            if (SponsorshipDetails.thisAddingSponsorship)
            {
                await Navigation.PopToRootAsync();
            }
            else
            {
                // Remove the search page from the navigation stack
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                await Navigation.PopAsync();
            }

            
        }
    }
}