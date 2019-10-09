using System;
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
	public partial class SelectSponsorResultsMobile2 : ContentPage
	{
		public SelectSponsorResultsMobile2(Sponsor sponsor)
		{
			InitializeComponent();

            DisplaySponsor(sponsor);
		}


        private void DisplaySponsor(Sponsor sponsor)
        {
            // display Sponsor ID
            if ((sponsor.SponsorID != string.Empty) && (sponsor.SponsorID != null))
            {
                selectSponsorIDValueLabel.Text = sponsor.SponsorID;
                selectSponsorIDValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorIDValueLabel.IsVisible = false;
            }

            // display Sponsor name
            if ((sponsor.SponsorName != string.Empty) && (sponsor.SponsorName != null))
            {
                selectSponsorNameValueLabel.Text = sponsor.SponsorName;
                selectSponsorNameValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorNameValueLabel.IsVisible = false;
            }

            // display Sponsor address, if there is one
            if ((sponsor.SponsorAddress != string.Empty) && (sponsor.SponsorAddress != null))
            {
                selectSponsorAddressValueLabel.Text = sponsor.SponsorAddress;
                selectSponsorAddressValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorAddressValueLabel.IsVisible = false;
            }

            // display Sponsor phone, if there is one
            if ((sponsor.SponsorPhone != string.Empty) && (sponsor.SponsorPhone != null))
            {
                selectSponsorPhoneValueLabel.Text = sponsor.SponsorPhone;
                selectSponsorPhoneValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorPhoneValueLabel.IsVisible = false;
            }

            // display Sponsor email, if there is one
            if ((sponsor.SponsorEmail != string.Empty) && (sponsor.SponsorEmail != null))
            {
                selectSponsorEmailValueLabel.Text = sponsor.SponsorEmail;
                selectSponsorEmailValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorEmailValueLabel.IsVisible = false;
            }

            // display any Sponsor notes
            if ((sponsor.SponsorNotes != string.Empty) && (sponsor.SponsorNotes != null))
            {
                selectSponsorNotesValueLabel.Text = sponsor.SponsorNotes;
                selectSponsorNotesValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorNotesValueLabel.IsVisible = false;
            }
        }

        //assign this sponsor button
        private async void ThisSponsorButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Sponsor added", "", "OK");
            await Navigation.PopToRootAsync();
        }
    }
}