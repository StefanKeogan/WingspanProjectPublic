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
    public partial class MemberResultsDesktop : ContentPage
    {
        public MemberResultsDesktop(List<Member> results)
        {
            InitializeComponent();

            resultsListView.ItemsSource = results;
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Member;

            if (item != null)
            {
                DisplayMember(item);
            }
        }

        private void DisplayMember(Member member)
        {
            // Member Id
            if ((member._id != string.Empty) && (member._id != null))
            {
                memberIdValueLabel.Text = member._id;
                memberIdStack.IsVisible = true;
                memberIdEntry.IsVisible = false;
            }
            else
            {
                memberIdEntry.IsVisible = true;
                memberIdStack.IsVisible = false;
                
            }

            // First Name
            if ((member.FirstName != string.Empty) && (member.FirstName != null))
            {
                memberFirstNameValueLabel.Text = member.FirstName;
                memberFirstNameStack.IsVisible = true;
                memberFirstNameEntry.IsVisible = false;
            }
            else
            {
                memberFirstNameValueLabel.IsVisible = true;
                memberFirstNameStack.IsVisible = false;

            }

            // Last Name
            if ((member.LastName != string.Empty) && (member.LastName != null))
            {
                memberLastNameValueLabel.Text = member.LastName;
                memberLastNameStack.IsVisible = true;
                memberLastNameEntry.IsVisible = false;
            }
            else
            {
                memberLastNameEntry.IsVisible = true;
                memberLastNameStack.IsVisible = false;

            }
            
            // Salutation Name
            if ((member.SaluationName != string.Empty)  && (member.SaluationName != null))
            {
                memberSalutationValueLabel.Text = member.SaluationName;
                memberSalutationStack.IsVisible = true;
                memberSalutationEntry.IsVisible = false;
            }
            else
            {
                memberSalutationEntry.IsVisible = true;
                memberSalutationStack.IsVisible = false;

            }

            // Email
            if ((member.Email != string.Empty) && (member.Email != null))
            {
                memberEmailValueLabel.Text = member.Email;
                memberEmailStack.IsVisible = true;
                memberEmailEntry.IsVisible = false;
            }
            else
            {
                memberEmailEntry.IsVisible = true;
                memberEmailStack.IsVisible = false;

            }

            // Company 
            if ((member.Company != string.Empty)  && (member.Company != null))
            {
                memberCompanyValueLabel.Text = member.Company;
                memberCompanyStack.IsVisible = true;
                memberCompanyEntry.IsVisible = false;
            }
            else
            {
                memberCompanyEntry.IsVisible = true;
                memberCompanyStack.IsVisible = false;

            }

            // Address 1
            if ((member.Address1 != string.Empty)  && (member.Address1 != null))
            {
                memberAddress1ValueLabel.Text = member.Address1;
                memberAddress1Stack.IsVisible = true;
                memberAddress1Entry.IsVisible = false;
            }
            else
            {
                memberAddress1Entry.IsVisible = true;
                memberAddress1Stack.IsVisible = false;

            }

            // Address 2
            if ((member.Address2 != string.Empty)  && (member.Address2 != null))
            {
                memberAddress2ValueLabel.Text = member.Address2;
                memberAddress2Stack.IsVisible = true;
                memberAddress2Entry.IsVisible = false;
            }
            else
            {
                memberAddress2Entry.IsVisible = true;
                memberAddress2Stack.IsVisible = false;

            }

            // Address 3
            if ((member.Address3 != string.Empty)  && (member.Address3 != null))
            {
                memberAddress3ValueLabel.Text = member.Address3;
                memberAddress3Stack.IsVisible = true;
                memberAddress3Entry.IsVisible = false;
            }
            else
            {
                memberAddress3Entry.IsVisible = true;
                memberAddress3Stack.IsVisible = false;

            }

            // City
            if ((member.City != string.Empty) && (member.City != null))
            {
                memberCityValueLabel.Text = member.City;
                memberCityStack.IsVisible = true;
                memberCityEntry.IsVisible = false;
            }
            else
            {
                memberCityEntry.IsVisible = true;
                memberCityStack.IsVisible = false;

            }

            // Postcode
            if ((member.Postcode != string.Empty) && (member.Postcode != null))
            {
                memberPostCodeValueLabel.Text = member.Postcode;
                memberPostCodeStack.IsVisible = true;
                memberPostCodeEntry.IsVisible = false;
            }
            else
            {
                memberPostCodeEntry.IsVisible = true;
                memberPostCodeStack.IsVisible = false;

            }

            // Comment
            if ((member.Comment != string.Empty) && (member.Comment != null))
            {
                memberCommentValueLabel.Text = member.Comment;
                memberCommentStack.IsVisible = true;
                memberCommentEditor.IsVisible = false;
            }
            else
            {
                memberCommentEditor.IsVisible = true;
                memberCommentStack.IsVisible = false;

            }

            // Join Date
            if (member.JoinDate.ToString() == "1/01/0001 12:00:00 AM")
            {
                memberJoinDateValueLabel.Text = member.JoinDate.ToString();
                memberJoinDateStack.IsVisible = true;
                memberJoinDatePicker.IsVisible = false;
            }
            else
            {
                memberJoinDatePicker.IsVisible = true;
                memberJoinDateValueLabel.IsVisible = false;

            }

            // Set member donation history TODO connect to database
            donationListView.ItemsSource = new List<Payment> { new Payment { PaymentDate = DateTime.Today, Donation = 100 } };




        }

        // Delte this member
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Are you sure?", "Would you like to delete this member", "Yes", "No");

            if (answer)
            {
                await DisplayAlert("Member Deleted", "This member and their payment history have been delted", "Ok");
            }
            else
            {
                return;
            }

            
        }


        // Save changes to this member 
        private async void SaveChangesButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Are you sure?", "Would you like to save chnages to this member", "Yes", "No");

            if (answer)
            {
                await DisplayAlert("Member Saved", "Changes to this member have been saved", "Ok");
            }
            else
            {
                return;
            }
        }

        private void PaymentButton_Clicked(object sender, EventArgs e)
        {
            paymentHistoryView.IsVisible = true;
        }

        private void AddNewPaymentButton_Clicked(object sender, EventArgs e)
        {
            addNewPaymentView.IsVisible = true;
        }

        private void AddNewPaymentExitButton_Clicked(object sender, EventArgs e)
        {
            addNewPaymentView.IsVisible = false;
        }

        private void PaymentExitButton_Clicked(object sender, EventArgs e)
        {
            paymentHistoryView.IsVisible = false;
        }

        private void AddPaymentButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Payment Added", "This members donation has been recorded", "Ok");

            paymentHistoryView.IsVisible = false;
        }
    }
}