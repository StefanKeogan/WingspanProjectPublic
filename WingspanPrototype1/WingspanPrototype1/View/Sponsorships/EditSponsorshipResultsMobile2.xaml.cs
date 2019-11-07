using System;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Controller.Sponsorships;
using WingspanPrototype1.Functions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditSponsorshipResultsMobile2 : ContentPage
	{
        private ObjectId sponsorshipId;
        private ObjectId memberId;
        private string bird;
        private Picker category = new Picker();
        private Editor notes;
        private List<DatePicker> dates = new List<DatePicker>();
        private List<Sponsorship> sponsorshipResults = null;

        public EditSponsorshipResultsMobile2(Sponsorship sponsorship)
		{
			InitializeComponent();

            DisplaySponsorship(sponsorship);

            sponsorshipId = sponsorship._id;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (SponsorshipDetails.thisWingspanID != null)
            {
                sponsoredWingspanIDValueLabel.Text = SponsorshipDetails.thisWingspanID;
                sponsoredWingspanIDStack.IsVisible = true;
                editBirdSelector.IsVisible = false;
                bird = SponsorshipDetails.thisWingspanID;
            }

            if (SponsorshipDetails.thisFirstName != null)
            {
                editSponsorNameValueLabel.Text = SponsorshipDetails.thisFirstName;
                editSponsorNameStack.IsVisible = true;
            }

            if (SponsorshipDetails.thisMember != ObjectId.Empty)
            {
                memberId = SponsorshipDetails.thisMember;
            }

        }


        private void DisplaySponsorship(Sponsorship sponsorship)
        {
            //need to get the object of this member for display purposes
            Member memberDetails = SearchMembers.SearchById(sponsorship.Member_id);

            // Set member name items to upper case
            memberDetails.FirstName = FormatText.FirstToUpper(memberDetails.FirstName);
            memberDetails.LastName = FormatText.FirstToUpper(memberDetails.LastName);
            memberDetails.Company = FormatText.FirstToUpper(memberDetails.Company);


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
            if (Validate.FeildPopulated(sponsorship.Category))
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
            if (Validate.FeildPopulated(sponsorship.Notes))
            {
                editSponsorshipNotesValueLabel.Text = sponsorship.Notes;
                editSponsorshipNotesStack.IsVisible = true;
                editSponsorshipNotes.IsVisible = false;
            }
            else
            {
                editSponsorshipNotes.IsVisible = true;
                notes = editSponsorshipNotes;
                editSponsorshipNotesStack.IsVisible = false;
            }

            //display Sponsorship start date
            if (sponsorship.StartDate.ToString() != string.Empty)
            {
                editSponsorshipStartValueLabel.Text = sponsorship.StartDate.ToShortDateString();
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
                editSponsorshipEndValueLabel.Text = sponsorship.EndDate.ToShortDateString();
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


            //    //display member's first name
            //    if ((sponsorship.FirstName != string.Empty) && (sponsorship.FirstName != null))
            //    {
            //        editSponsorFirstNameValueLabel.Text = sponsorship.FirstName;
            //        editSponsorFirstNameStack.IsVisible = true;
            //        editSponsorFirstNameEntry.IsVisible = false;
            //    }
            //    else
            //    {
            //        editSponsorFirstNameEntry.IsVisible = true;
            //        editSponsorFirstNameStack.IsVisible = false;
            //    }

            //    //display member's last name
            //    if ((sponsorship.LastName != string.Empty) && (sponsorship.LastName != null))
            //    {
            //        editSponsorLastNameValueLabel.Text = sponsorship.LastName;
            //        editSponsorLastNameStack.IsVisible = true;
            //        editSponsorLastNameEntry.IsVisible = false;
            //    }
            //    else
            //    {
            //        editSponsorLastNameEntry.IsVisible = true;
            //        editSponsorLastNameStack.IsVisible = false;
            //    }

            //    //display sponsoring company name
            //    if ((sponsorship.Company != string.Empty) && (sponsorship.Company != null))
            //    {
            //        editSponsorCompanyNameValueLabel.Text = sponsorship.Company;
            //        editSponsorCompanyNameStack.IsVisible = true;
            //        editSponsorCompanyNameEntry.IsVisible = false;
            //    }
            //    else
            //    {
            //        editSponsorCompanyNameEntry.IsVisible = true;
            //        editSponsorCompanyNameStack.IsVisible = false;
            //    }
        }


        //buttons
        private async void DeleteSponsorshipButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("", "Are you sure you want to delete this sponsorship?", "Yes", "No");

            //if yes is selected
            if (answer)
            {
                if (DeleteSponsorship.DropDocument(sponsorshipId))
                {
                    await DisplayAlert("Sponsorship deleted", "", "OK");
                    await Navigation.PopAsync();
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
                Sponsorship updatedSponsorship = UpdateSponsorship.UpdateDocument(sponsorshipId, memberId, bird, category, notes, dates);
                if (updatedSponsorship != null)
                {
                    // Find old sponsorship 
                    Sponsorship sponsorship = sponsorshipResults.Find(x => x._id == sponsorshipId);
                    int sponsorshipIndex = sponsorshipResults.IndexOf(sponsorship);

                    // Update with new sponsorship
                    sponsorshipResults[sponsorshipIndex] = updatedSponsorship;

                    //reset the 'global' again
                    SponsorshipDetails.thisFirstName = null;
                    SponsorshipDetails.thisLastName = null;
                    SponsorshipDetails.thisCompany = null;
                    SponsorshipDetails.thisWingspanID = null;

                    // Clear pickers and entrys
                    notes.Text = null;
                    category.SelectedIndex = -1;
                    dates.Clear();

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
        private void SponsoredWingspanIDEditButton_Clicked(object sender, EventArgs e)
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
                // Set from add sponsorship to false as we are editing 
                SponsorshipDetails.thisAddingSponsorship = false;

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
            category = editCategoryPicker;
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
            dates.Add(editSponsorshipStartPicker);
        }

        private void EditSponsorshipEndEditButton_Clicked(object sender, EventArgs e)
        {
            editSponsorshipEndPicker.IsVisible = true;
            editSponsorshipEndStack.IsVisible = false;
            dates.Add(editSponsorshipEndPicker);
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