using System;
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
        private ObjectId memberId;
        private string bird;
        private string level;
        private Editor notes;
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

            sponsorshipId = item._id;

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
                editBirdSelector.IsVisible = false;
            }
            else
            {
                editBirdSelector.IsVisible = true;
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
            if ((sponsorship.Notes != string.Empty) && (sponsorship.Notes != null))
            {
                editSponsorshipNotesValueLabel.Text = sponsorship.Notes;
                editSponsorshipNotesStack.IsVisible = true;
                editSponsorshipNotes.IsVisible = false;
            }
            else
            {
                editSponsorshipNotes.IsVisible = true;
                editSponsorshipNotesStack.IsVisible = false;
            }

            //display Sponsorship start date
            if (sponsorship.StartDate.ToString() != string.Empty)
            {
                editSponsorshipStartValueLabel.Text = sponsorship.StartDate.ToString();
                editSponsorshipStartStack.IsVisible = true;
                editSponsorshipStartPicker.IsVisible = false;
            }
            else
            {
                editSponsorshipStartPicker.IsVisible = true;
                editSponsorshipStartStack.IsVisible = false;
            }

            //display Sponsorship end date
            if (sponsorship.EndDate.ToString() != string.Empty)
            {
                editSponsorshipEndValueLabel.Text = sponsorship.EndDate.ToString();
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
            }
            else if ((!Validate.FeildPopulated(memberDetails.FirstName)) && (Validate.FeildPopulated(memberDetails.Company)))
            {
                editSponsorNameValueLabel.Text = memberDetails.Company;
                editSponsorNameStack.IsVisible = true;
            }
            else
            {
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

                    Sponsorship sponsorship = sponsorshipResults.Find(x => x._id == sponsorshipId);
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
                Sponsorship updatedSponsorship = UpdateSponsorship.UpdateDocument(sponsorshipId, memberId, bird, level, notes, start, end);
                if (updatedSponsorship != null)
                {               
                    // Find old sponsorship 
                    Sponsorship sponsorship = sponsorshipResults.Find(x => x._id == sponsorshipId);
                    int sponsorshipIndex = sponsorshipResults.IndexOf(sponsorship);

                    // Update with new sponsorship
                    sponsorshipResults[sponsorshipIndex] = updatedSponsorship;

                    resultsListView.ItemsSource = null;
                    resultsListView.ItemsSource = sponsorshipResults;

                    //reset the 'global' again
                    SponsorshipDetails.thisFirstName = null;
                    SponsorshipDetails.thisLastName = null;
                    SponsorshipDetails.thisCompany = null;
                    SponsorshipDetails.thisWingspanID = null;

                    DisplaySponsorship(updatedSponsorship);

                    await DisplayAlert("Sponsorship Saved", "Changes to this sponsorship have been saved", "OK");
                }
                else
                {
                    await DisplayAlert("Connection Error", "Could not connect to database, please check your connection and try again", "OK");
                }
            }               
        }



        //change the bird being sponsored
        private void SponsoredWingspanIDEditButton_ClickedAsync(object sender, EventArgs e)
        {
            sponsoredWingspanIDStack.IsVisible = false;
            editBirdSelector.IsVisible = true;
        }

        //dealing with the edit bird selector
        private async void EditBird_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            //change the bird being sponsored
            if (selectedIndex == 0)
            {
                await Navigation.PushAsync(new Edit("Select Bird"));
                sponsoredWingspanIDValueLabel.Text = SponsorshipDetails.thisWingspanID;
                sponsoredWingspanIDValueLabel.IsVisible = true;
            }
            //change the sponsored bird to something else
            else if (selectedIndex == 1)
            {
                sponsoredWingspanIDValueLabel.Text = "Other";
                sponsoredWingspanIDValueLabel.IsVisible = true;
            }

            //updated wingspan ID
            bird = sponsoredWingspanIDValueLabel.Text;
        }


        //selecting a new category
        private void EditCategoryEditButton_Clicked(object sender, EventArgs e)
        {
            editCategoryPicker.IsVisible = true;
            editCategoryStack.IsVisible = false;
            level = editCategoryPicker.SelectedItem.ToString();
        }

        private void EditSponsorshipNotesValueEditButton_Clicked(object sender, EventArgs e)
        {
            editSponsorshipNotes.IsVisible = true;
            editSponsorshipNotesStack.IsVisible = false;
            notes = editSponsorshipNotes;
        }

        private void EditSponsorshipStartEditButton_Clicked(object sender, EventArgs e)
        {
            editSponsorshipStartPicker.IsVisible = true;
            editSponsorshipStartStack.IsVisible = false;
            start = editSponsorshipStartPicker;
        }

        private void EditSponsorshipEndEditButton_Clicked(object sender, EventArgs e)
        {
            editSponsorshipEndPicker.IsVisible = true;
            editSponsorshipEndStack.IsVisible = false;
            end = editSponsorshipEndPicker;
        }

        //edit sponsor name means choose a new sponsor
        private async void EditSponsorNameEditButton_Clicked(object sender, EventArgs e)
        {
            //open this page to find a different member
            await Navigation.PushAsync(new Edit("Select Member"));

            //display either first name or company
            if (SponsorshipDetails.thisFirstName != null)
            {
                editSponsorNameValueLabel.Text = SponsorshipDetails.thisFirstName;
            }
            else if ((SponsorshipDetails.thisFirstName == null) && (SponsorshipDetails.thisCompany != null))
            {
                editSponsorNameValueLabel.Text = SponsorshipDetails.thisCompany;
            }

            //updated sponsor
            memberId = SponsorshipDetails.thisMember;
        }
    }
}
