using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using WingspanPrototype1.View;
using Xamarin.Forms;
using WingspanPrototype1.Functions;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectMemberResultsDesktop : ContentPage
	{
        public SelectMemberResultsDesktop (List<Member> results)
		{
			InitializeComponent ();
            resultsListView.ItemsSource = results;

            // What type of device are we running on 
            if (DeviceSize.ScreenArea() <= 783457)
            {
                selectMemberMargin1.Width = 0;
                selectMemberMargin2.Width = 0;
            }
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Member;

            if (item != null)
            {
                DisplayMember(item);
                //display the results once an item is selected from the list
                selectMemberResultsGrid.IsVisible = true;
            }
          
        }

        private void DisplayMember(Member member)
        {
            // display member's first name
            if ((member.FirstName != string.Empty) && (member.FirstName != null))
            {
                selectMemberFirstNameValueLabel.Text = member.FirstName;
                selectMemberFirstNameValueLabel.IsVisible = true;
            }
            else
            {
                selectMemberFirstNameValueLabel.IsVisible = false;
            }

            // display member's last name
            if ((member.LastName != string.Empty) && (member.LastName != null))
            {
                selectMemberLastNameValueLabel.Text = member.LastName;
                selectMemberLastNameValueLabel.IsVisible = true;
            }
            else
            {
                selectMemberLastNameValueLabel.IsVisible = false;
            }

            // display member's salutation name, if there is one
            if ((member.SalutationName != string.Empty) && (member.SalutationName != null))
            {
                selectMemberSalutationNameValueLabel.Text = member.SalutationName;
                selectMemberSalutationNameValueLabel.IsVisible = true;
            }
            else
            {
                selectMemberSalutationNameValueLabel.IsVisible = false;
            }

            // display company name, if there is one
            if ((member.Company != string.Empty) && (member.Company != null))
            {
                selectMemberCompanyValueLabel.Text = member.Company;
                selectMemberCompanyValueLabel.IsVisible = true;
            }
            else
            {
                selectMemberCompanyValueLabel.IsVisible = false;
            }

            // display any member notes
            if ((member.Comment != string.Empty) && (member.Comment != null))
            {
                selectMemberCommentsValueLabel.Text = member.Comment;
                selectMemberCommentsValueLabel.IsVisible = true;
            }
            else
            {
                selectMemberCommentsValueLabel.IsVisible = false;
            }

            //set the 'global' variables using this member
            SetSponsorDetails(member);            
        }


        //set 'global' variables for sponsorship
        private void SetSponsorDetails(Member member)
        {
            SponsorshipDetails.thisFirstName = member.FirstName;
            SponsorshipDetails.thisLastName = member.LastName;
            SponsorshipDetails.thisCompany = member.Company;
            SponsorshipDetails.thisMember = member._id;
        }


        //button to update using this member 
        private async void ThisMemberButton_Clicked(object sender, EventArgs e)
        {

            await DisplayAlert("Member added", "", "OK");

            if (SponsorshipDetails.thisAddingSponsorship)
            {
                await Navigation.PopToRootAsync();
            }
            else
            {
                // Remove the edit page off of the back stack
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                await Navigation.PopAsync();
            }

           
        }
    }
}