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
    public partial class EditSponsorResultsDesktop : ContentPage
    {
        public EditSponsorResultsDesktop(ArrayList results)
        {
            InitializeComponent();
            resultsListView.ItemsSource = results;
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Sponsor;

            if (item != null)
            {
                DisplaySponsor(item);
                editSponsorResultsGrid.IsVisible = true;
            }
            
        }

        //execute when user selects the sponsor they want to see
        private void DisplaySponsor(Sponsor sponsor)
        {
            // Sponsor ID
            if ((sponsor.SponsorID != string.Empty) && (sponsor.SponsorID != null))
            {
                sponsorIDValueLabel.Text = sponsor.SponsorID;
                sponsorIDStack.IsVisible = true;
                sponsorIDEntry.IsVisible = false;
            }
            else
            {
                sponsorIDEntry.IsVisible = true;
                sponsorIDStack.IsVisible = false;
            }

            // Sponsor name
            if ((sponsor.SponsorName != string.Empty) && (sponsor.SponsorName != null))
            {
                sponsorNameValueLabel.Text = sponsor.SponsorName;
                sponsorNameStack.IsVisible = true;
                sponsorNameEntry.IsVisible = false;
            }
            else
            {
                sponsorNameEntry.IsVisible = true;
                sponsorNameStack.IsVisible = false;
            }

            // Sponsor address
            if ((sponsor.SponsorAddress != string.Empty) && (sponsor.SponsorAddress != null))
            {
                sponsorAddressValueLabel.Text = sponsor.SponsorAddress;
                sponsorAddressStack.IsVisible = true;
                sponsorAddressEntry.IsVisible = false;
            }
            else
            {
                sponsorAddressEntry.IsVisible = true;
                sponsorAddressStack.IsVisible = false;
            }

            // Sponsor phone number
            if ((sponsor.SponsorPhone != string.Empty) && (sponsor.SponsorPhone != null))
            {
                sponsorPhoneValueLabel.Text = sponsor.SponsorPhone;
                sponsorPhoneStack.IsVisible = true;
                sponsorPhoneEntry.IsVisible = false;
            }
            else
            {
                sponsorPhoneEntry.IsVisible = true;
                sponsorPhoneStack.IsVisible = false;
            }

            // Sponsor email
            if ((sponsor.SponsorEmail != string.Empty) && (sponsor.SponsorEmail != null))
            {
                sponsorEmailValueLabel.Text = sponsor.SponsorEmail;
                sponsorEmailStack.IsVisible = true;
                sponsorEmailEntry.IsVisible = false;
            }
            else
            {
                sponsorEmailEntry.IsVisible = true;
                sponsorEmailStack.IsVisible = false;
            }

            // Sponsor notes
            if ((sponsor.SponsorNotes != string.Empty) && (sponsor.SponsorNotes != null))
            {
                sponsorNotesValueLabel.Text = sponsor.SponsorNotes;
                sponsorNotesValueLabel.IsVisible = true;
            }
            else
            {
                sponsorNotesValueLabel.IsVisible = false;
            }
        }

        //delete sponsor button
        private async void DeleteSponsorButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("", "Are you sure you want to delete this sponsor?", "Yes", "No");

            if (answer)
            {
                await DisplayAlert("Sponsor deleted", "", "OK");
            }
            else
            {
                return;
            }
        }

        //save changes to sponsor details button
        private async void SaveSponsorChangesButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("", "Are you sure you want to save these changes?", "Yes", "No");

            if (answer)
            {
                await DisplayAlert("Changes saved", "", "OK");
            }
            else
            {
                return;
            }
        }


        //buttons for working adding notes about sponsors
        private void AddSponsorNoteButton_Clicked(object sender, EventArgs e)
        {
            addSponsorNoteView.IsVisible = true;
        }

        private void AddSponsorNoteExitButton_Clicked(object sender, EventArgs e)
        {
            addSponsorNoteView.IsVisible = false;
            sponsorNoteEntry.Text = string.Empty; //clear any text added in the field
        }

        private void SaveSponsorNoteButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Note added", "", "OK");
            addSponsorNoteView.IsVisible = false;
        }
    }
}