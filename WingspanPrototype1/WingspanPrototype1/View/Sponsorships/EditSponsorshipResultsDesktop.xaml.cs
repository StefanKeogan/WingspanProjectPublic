﻿using System;
using MongoDB.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Controller.Sponsorships;
using WingspanPrototype1.Functions;

namespace WingspanPrototype1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditSponsorshipResultsDesktop : ContentPage
	{
        private ObjectId sponsorshipId;
        private Picker level;
        private DatePicker start;
        private DatePicker end;
        private List<Sponsorship> sponsorshipResults = null;



        public EditSponsorshipResultsDesktop (List<Sponsorship> results)
		{
			InitializeComponent ();

            sponsorshipResults = results;

            resultsListView.ItemsSource = sponsorshipResults;
        }


        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Sponsorship;

            id = item._id;

            if (item != null)
            {
                DisplaySponsorship(item);
                editSponsorshipResults.IsVisible = true;
                editSponsorshipButtons.IsVisible = true;
            }
        }


        private void DisplaySponsorship(Sponsorship sponsorship)
        {
            //need to get the object of this member for display purposes
            Member memberDetails = SearchMembers.Find(sponsorship.Member_id);

            // Set member name items to upper case
            memberDetails.FirstName = FormatText.FirstToUpper(memberDetails.FirstName);
            memberDetails.LastName = FormatText.FirstToUpper(memberDetails.LastName);
            memberDetails.Company = FormatText.FirstToUpper(memberDetails.Company);


            //clear previous input items
            //entries.Clear();

            //display Wingspan ID
            if (Validate.FeildPopulated(sponsorship.WingspanId))
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
            if ((sponsorship.Category != string.Empty) && (sponsorship.Category != null))
            {
                editCategoryValueLabel.Text = sponsorship.Category;
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
            if (sponsorship.SponsorshipStart.ToString() != string.Empty)
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
            if (sponsorship.SponsorshipEnd.ToString() != string.Empty)
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

            //display member's first name or company name
            if (Validate.FeildPopulated(memberDetails.FirstName))
            {
                editSponsorNameValueLabel.Text = memberDetails.FirstName;
                editSponsorNameStack.IsVisible = true;
                editSponsorNameEntry.IsVisible = false;
            }
            else if ((!Validate.FeildPopulated(memberDetails.FirstName)) && (Validate.FeildPopulated(memberDetails.Company)))
            {
                editSponsorNameValueLabel.Text = memberDetails.Company;
                editSponsorNameStack.IsVisible = true;
                editSponsorNameEntry.IsVisible = false;
            }
            else
            {
                editSponsorNameEntry.IsVisible = true;
                editSponsorNameStack.IsVisible = false;
            }

            ////display member's last name
            //if ((memberDetails.LastName != string.Empty) && (memberDetails.LastName != null))
            //{
            //    editSponsorLastNameValueLabel.Text = memberDetails.LastName;
            //    editSponsorLastNameStack.IsVisible = true;
            //    editSponsorLastNameEntry.IsVisible = false;
            //}
            //else
            //{
            //    editSponsorLastNameEntry.IsVisible = true;
            //    editSponsorLastNameStack.IsVisible = false;
            //}

            ////display sponsoring company name
            //if ((memberDetails.Company != string.Empty) && (memberDetails.Company != null))
            //{
            //    editSponsorCompanyNameValueLabel.Text = memberDetails.Company;
            //    editSponsorCompanyNameStack.IsVisible = true;
            //    editSponsorCompanyNameEntry.IsVisible = false;
            //}
            //else
            //{
            //    editSponsorCompanyNameEntry.IsVisible = true;
            //    editSponsorCompanyNameStack.IsVisible = false;
            //}
        }


        //methods for buttons
        private async void DeleteSponsorshipButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("", "Are you sure you want to delete this sponsorship?", "Yes", "No");

            //if yes is selected
            if (answer)
            {
                if (DeleteSponsorship.DropDocument(sponsorshipId))
                {
                    await DisplayAlert("Sponsorship deleted", "", "OK");

                    Sponsorship sponsorship = sponsorshipResults.Find(x => x._id == id);
                    sponsorshipResults.Remove(sponsorship);

                    if (sponsorshipResults.Count <= 0)
                    {
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        // Clear list view
                        resultsListView.ItemsSource = null;

                        // Add any items left over
                        resultsListView.ItemsSource = sponsorshipResults;

                        //display the first item in the list again
                        DisplaySponsorship(sponsorshipResults[0]);
                    }
                }
                else
                {
                    await DisplayAlert("Connection Error", "Please check your connection and try again", "OK");
                }
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

        private void EditSponsorNameEditButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
