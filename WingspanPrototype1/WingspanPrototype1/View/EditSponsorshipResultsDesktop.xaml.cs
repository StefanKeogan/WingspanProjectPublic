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
	public partial class EditSponsorshipResultsDesktop : ContentPage
	{
		public EditSponsorshipResultsDesktop (ArrayList results)
		{
			InitializeComponent ();
            resultsListView.ItemsSource = results;
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Sponsorship;

            if (item != null)
            {
                DisplaySponsorship(item);
                editSponsorshipResultsGrid.IsVisible = true;
            }
        }


        private void DisplaySponsorship(Sponsorship sponsorship)
        {
            // Sponsorship ID
            if ((sponsorship.SponsorshipID != string.Empty) && (sponsorship.SponsorshipID != null))
            {
                sponsorshipIDLabel.Text = sponsorship.SponsorID;
                sponsorshipIDStack.IsVisible = true;
                sponsorshipIDEntry.IsVisible = false;
            }
            else
            {
                sponsorshipIDEntry.IsVisible = true;
                sponsorshipIDStack.IsVisible = false;
            }

            // Wingspan ID
            if ((sponsorship.WingspanId != string.Empty) && (sponsorship.WingspanId != null))
            {
                sponsoredWingspanIDLabel.Text = sponsorship.WingspanId;
                sponsoredWingspanIDStack.IsVisible = true;
                sponsoredWingspanIDEntry.IsVisible = false;
            }
            else
            {
                sponsoredWingspanIDEntry.IsVisible = true;
                sponsoredWingspanIDStack.IsVisible = false;
            }

            // Category
            if ((sponsorship.SponsorshipCategory != string.Empty) && (sponsorship.SponsorshipCategory != null))
            {
                editCategoryLabel.Text = sponsorship.SponsorshipCategory;
                editCategoryStack.IsVisible = true;
                editCategoryPicker.IsVisible = false;
            }
            else
            {
                editCategoryPicker.IsVisible = true;
                editCategoryStack.IsVisible = false;
            }

            // Sponsorship start date
            if ((sponsorship.SponsorshipStart.ToString() != string.Empty) && (sponsorship.SponsorshipStart != null))
            {
                editSponsorshipStartLabel.Text = sponsorship.SponsorshipStart.ToString();
                editSponsorshipStartStack.IsVisible = true;
                editSponsorshipStartPicker.IsVisible = false;
            }
            else
            {
                editSponsorshipStartPicker.IsVisible = true;
                editSponsorshipStartStack.IsVisible = false;
            }

            // Sponsorship end date
            if ((sponsorship.SponsorshipEnd.ToString() != string.Empty) && (sponsorship.SponsorshipEnd != null))
            {
                editSponsorshipEndLabel.Text = sponsorship.SponsorshipEnd.ToString();
                editSponsorshipEndStack.IsVisible = true;
                editSponsorshipEndPicker.IsVisible = false;
            }
            else
            {
                editSponsorshipEndPicker.IsVisible = true;
                editSponsorshipEndStack.IsVisible = false;
            }

            // Sponsor ID
            if ((sponsorship.SponsorID != string.Empty) && (sponsorship.SponsorID != null))
            {
                editSponsorIDLabel.Text = sponsorship.SponsorID;
                editSponsorIDStack.IsVisible = true;
                editSponsorIDEntry.IsVisible = false;
            }
            else
            {
                editSponsorIDEntry.IsVisible = true;
                editSponsorIDStack.IsVisible = false;
            }

            // Sponsor name
            if ((sponsorship.SponsorName != string.Empty) && (sponsorship.SponsorName != null))
            {
                editSponsorNameLabel.Text = sponsorship.SponsorName;
                editSponsorNameStack.IsVisible = true;
                editSponsorNameEntry.IsVisible = false;
            }
            else
            {
                editSponsorNameEntry.IsVisible = true;
                editSponsorNameStack.IsVisible = false;
            }
        }


        //methods for buttons
        private async void DeleteSponsorshipButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("", "Are you sure you want to delete this sponsorship?", "Yes", "No");

            if (answer)
            {
                await DisplayAlert("Sponsorship deleted", "", "OK");
            }
            else
            {
                return;
            }
        }

        private async void SaveSponsorshipChangesButton_Clicked(object sender, EventArgs e)
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

        //selecting a new category
        private void EditCategorySelector_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}