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
	public partial class SelectMemberResultsMobile2 : ContentPage
	{
		public SelectMemberResultsMobile2(Member member)
		{
			InitializeComponent();

            DisplayMember(member);
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

            //set these variables for this member
            SetSponsorDetails(member);
        }

        //set 'global' variables for sponsorship
        private void SetSponsorDetails(Member member)
        {
            SponsorshipInfo.thisFirstName = member.FirstName;
            SponsorshipInfo.thisLastName = member.LastName;
            SponsorshipInfo.thisCompany = member.Company;
        }

        //assign this sponsor button
        private async void ThisSponsorButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Sponsor added", "", "OK");
            await Navigation.PopToRootAsync();
        }
    }
}