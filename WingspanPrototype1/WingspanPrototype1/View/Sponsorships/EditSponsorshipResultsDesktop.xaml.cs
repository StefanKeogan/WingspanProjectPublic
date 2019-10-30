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
		public EditSponsorshipResultsDesktop (List<Sponsorship> results)
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
                editSponsorshipResults.IsVisible = true;
                editSponsorshipButtons.IsVisible = true;
            }
        }


        private void DisplaySponsorship(Sponsorship sponsorship)
        {
            //display Wingspan ID
            if ((sponsorship.WingspanId != string.Empty) && (sponsorship.WingspanId != null))
            {
                sponsoredWingspanIDValueLabel.Text = sponsorship.WingspanId;
                sponsoredWingspanIDStack.IsVisible = true;
                sponsoredWingspanIDEntry.IsVisible = false;
            }
            else
            {
                sponsoredWingspanIDEntry.IsVisible = true;
                sponsoredWingspanIDStack.IsVisible = false;
            }

            //display Category
            if ((sponsorship.SponsorshipCategory != string.Empty) && (sponsorship.SponsorshipCategory != null))
            {
                editCategoryValueLabel.Text = sponsorship.SponsorshipCategory;
                editCategoryStack.IsVisible = true;
                editCategoryPicker.IsVisible = false;
            }
            else
            {
                editCategoryPicker.IsVisible = true;
                editCategoryStack.IsVisible = false;
            }

            //display notes
            if ((sponsorship.SponsorshipNotes != string.Empty) && (sponsorship.SponsorshipNotes != null))
            {
                editSponsorshipNotesValueLabel.Text = sponsorship.SponsorshipNotes;
                editSponsorshipNotesStack.IsVisible = true;
                editSponsorshipNotes.IsVisible = false;
            }
            else
            {
                editSponsorshipNotes.IsVisible = true;
                editSponsorshipNotesStack.IsVisible = false;
            }

            //display Sponsorship start date
            if ((sponsorship.SponsorshipStart.ToString() != string.Empty) && (sponsorship.SponsorshipStart != null))
            {
                editSponsorshipStartValueLabel.Text = sponsorship.SponsorshipStart.ToString();
                editSponsorshipStartStack.IsVisible = true;
                editSponsorshipStartPicker.IsVisible = false;
            }
            else
            {
                editSponsorshipStartPicker.IsVisible = true;
                editSponsorshipStartStack.IsVisible = false;
            }

            //display Sponsorship end date
            if ((sponsorship.SponsorshipEnd.ToString() != string.Empty) && (sponsorship.SponsorshipEnd != null))
            {
                editSponsorshipEndValueLabel.Text = sponsorship.SponsorshipEnd.ToString();
                editSponsorshipEndStack.IsVisible = true;
                editSponsorshipEndPicker.IsVisible = false;
            }
            else
            {
                editSponsorshipEndPicker.IsVisible = true;
                editSponsorshipEndStack.IsVisible = false;
            }

            //display member's first name
            if ((sponsorship.FirstName != string.Empty) && (sponsorship.FirstName != null))
            {
                editSponsorFirstNameValueLabel.Text = sponsorship.FirstName;
                editSponsorFirstNameStack.IsVisible = true;
                editSponsorFirstNameEntry.IsVisible = false;
            }
            else
            {
                editSponsorFirstNameEntry.IsVisible = true;
                editSponsorFirstNameStack.IsVisible = false;
            }

            //display member's last name
            if ((sponsorship.LastName != string.Empty) && (sponsorship.LastName != null))
            {
                editSponsorLastNameValueLabel.Text = sponsorship.LastName;
                editSponsorLastNameStack.IsVisible = true;
                editSponsorLastNameEntry.IsVisible = false;
            }
            else
            {
                editSponsorLastNameEntry.IsVisible = true;
                editSponsorLastNameStack.IsVisible = false;
            }

            //display sponsoring company name
            if ((sponsorship.Company != string.Empty) && (sponsorship.Company != null))
            {
                editSponsorCompanyNameValueLabel.Text = sponsorship.Company;
                editSponsorCompanyNameStack.IsVisible = true;
                editSponsorCompanyNameEntry.IsVisible = false;
            }
            else
            {
                editSponsorCompanyNameEntry.IsVisible = true;
                editSponsorCompanyNameStack.IsVisible = false;
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

        private void EditSponsorshipNotesValueEditButton_Clicked(object sender, EventArgs e)
        {

        }

        private void EditSponsorshipStartEditButton_Clicked(object sender, EventArgs e)
        {

        }

        private void EditSponsorshipEndEditButton_Clicked(object sender, EventArgs e)
        {

        }

        private void EditSponsorFirstNameEditButton_Clicked(object sender, EventArgs e)
        {

        }

        private void EditSponsorLastNameEditButton_Clicked(object sender, EventArgs e)
        {

        }

        private void EditSponsorCompanyNameEditButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}