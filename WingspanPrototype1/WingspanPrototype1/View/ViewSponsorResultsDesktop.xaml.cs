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
	public partial class ViewSponsorResultsDesktop : ContentPage
	{
		public ViewSponsorResultsDesktop (ArrayList results)
		{
			InitializeComponent ();
            resultsListView.ItemsSource = results;
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Sponsor;

            if (item != null)
            {
                DisplaySponsor(item);
            }
        }

        private void DisplaySponsor(Sponsor sponsor)
        {
            // Sponsor ID
            if ((sponsor.SponsorID != string.Empty) && (sponsor.SponsorID != null))
            {
                viewSponsorIDValueLabel.Text = sponsor.SponsorID;
                viewSponsorIDValueLabel.IsVisible = true;
            }
            else
            {
                viewSponsorIDValueLabel.IsVisible = false;
            }

            // Sponsor name
            if ((sponsor.SponsorName != string.Empty) && (sponsor.SponsorName != null))
            {
                viewSponsorNameValueLabel.Text = sponsor.SponsorName;
                viewSponsorNameValueLabel.IsVisible = true;
            }
            else
            {
                viewSponsorNameValueLabel.IsVisible = false;
            }

            // Sponsor address
            if ((sponsor.SponsorAddress != string.Empty) && (sponsor.SponsorAddress != null))
            {
                viewSponsorAddressValueLabel.Text = sponsor.SponsorAddress;
                viewSponsorAddressValueLabel.IsVisible = true;
            }
            else
            {
                viewSponsorAddressValueLabel.IsVisible = false;
            }

            // Sponsor phone
            if ((sponsor.SponsorPhone != string.Empty) && (sponsor.SponsorPhone != null))
            {
                viewSponsorPhoneValueLabel.Text = sponsor.SponsorPhone;
                viewSponsorPhoneValueLabel.IsVisible = true;
            }
            else
            {
                viewSponsorPhoneValueLabel.IsVisible = false;
            }

            // Sponsor email
            if ((sponsor.SponsorEmail != string.Empty) && (sponsor.SponsorEmail != null))
            {
                viewSponsorEmailValueLabel.Text = sponsor.SponsorEmail;
                viewSponsorEmailValueLabel.IsVisible = true;
            }
            else
            {
                viewSponsorEmailValueLabel.IsVisible = false;
            }

            // Sponsor notes
            if ((sponsor.SponsorNotes != string.Empty) && (sponsor.SponsorNotes != null))
            {
                viewSponsorNotesValueLabel.Text = sponsor.SponsorNotes;
                viewSponsorNotesValueLabel.IsVisible = true;
            }
            else
            {
                viewSponsorNotesValueLabel.IsVisible = false;
            }
        }

        //go back button which results user to the edit menu
        private async void GoBackSponsorButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //button to update with this sponsor's name 
        private void ThisSponsorButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Sponsor added", "", "OK");
        }
    }
}