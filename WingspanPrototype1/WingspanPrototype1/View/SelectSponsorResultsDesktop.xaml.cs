﻿using System;
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
	public partial class SelectSponsorResultsDesktop : ContentPage
	{
		public SelectSponsorResultsDesktop (ArrayList results)
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
                selectSponsorIDValueLabel.Text = sponsor.SponsorID;
                selectSponsorIDValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorIDValueLabel.IsVisible = false;
            }

            // Sponsor name
            if ((sponsor.SponsorName != string.Empty) && (sponsor.SponsorName != null))
            {
                selectSponsorNameValueLabel.Text = sponsor.SponsorName;
                selectSponsorNameValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorNameValueLabel.IsVisible = false;
            }

            // Sponsor address
            if ((sponsor.SponsorAddress != string.Empty) && (sponsor.SponsorAddress != null))
            {
                selectSponsorAddressValueLabel.Text = sponsor.SponsorAddress;
                selectSponsorAddressValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorAddressValueLabel.IsVisible = false;
            }

            // Sponsor phone
            if ((sponsor.SponsorPhone != string.Empty) && (sponsor.SponsorPhone != null))
            {
                selectSponsorPhoneValueLabel.Text = sponsor.SponsorPhone;
                selectSponsorPhoneValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorPhoneValueLabel.IsVisible = false;
            }

            // Sponsor email
            if ((sponsor.SponsorEmail != string.Empty) && (sponsor.SponsorEmail != null))
            {
                selectSponsorEmailValueLabel.Text = sponsor.SponsorEmail;
                selectSponsorEmailValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorEmailValueLabel.IsVisible = false;
            }

            // Sponsor notes
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