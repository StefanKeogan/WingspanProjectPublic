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
	public partial class SelectMemberResultsDesktop : ContentPage
	{
		public SelectMemberResultsDesktop (List<Sponsor> results)
		{
			InitializeComponent ();
            resultsListView.ItemsSource = results;
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

            // display Sponsor address, if there is one
            if ((member.SaluationName != string.Empty) && (member.SaluationName != null))
            {
                selectMemberSalutationNameValueLabel.Text = member.SaluationName;
                selectMemberSalutationNameValueLabel.IsVisible = true;
            }
            else
            {
                selectMemberSalutationNameValueLabel.IsVisible = false;
            }

            // display Sponsor phone, if there is one
            if ((member.Company != string.Empty) && (member.Company != null))
            {
                selectMemberCompanyValueLabel.Text = member.Company;
                selectMemberCompanyValueLabel.IsVisible = true;
            }
            else
            {
                selectMemberCompanyValueLabel.IsVisible = false;
            }

            // display Sponsor email, if there is one
            if ((member.SponsorEmail != string.Empty) && (member.SponsorEmail != null))
            {
                selectSponsorEmailValueLabel.Text = member.SponsorEmail;
                selectSponsorEmailValueLabel.IsVisible = true;
            }
            else
            {
                selectSponsorEmailValueLabel.IsVisible = false;
            }

            // display any Sponsor notes
            if ((member.Comment != string.Empty) && (member.Comment != null))
            {
                selectMemberCommentsValueLabel.Text = member.Comment;
                selectMemberCommentsValueLabel.IsVisible = true;
            }
            else
            {
                selectMemberCommentsValueLabel.IsVisible = false;
            }
        }

        //button to update with this sponsor 
        private async void ThisMemberButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Member added", "", "OK");
            await Navigation.PopToRootAsync();
        }
    }
}