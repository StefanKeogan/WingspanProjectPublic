using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Controller.Members;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MemberResultsMobile2 : ContentPage
    {
        private List<Entry> entries = new List<Entry>();
        private Editor editor;
        private DatePicker date;
        private ObjectId id;

        public MemberResultsMobile2(Member member)
        {
            InitializeComponent();

            DisplayMember(member);

            id = member._id;
        }

        private void DisplayMember(Member member)
        {

            // First Name
            if (Validate.FeildPopulated(member.FirstName))
            {

                memberFirstNameValueLabel.Text = member.FirstName;
                memberFirstNameStack.IsVisible = true;
                memberFirstNameEntry.IsVisible = false;
            }
            else
            {
                memberFirstNameEntry.IsVisible = true;
                memberFirstNameStack.IsVisible = false;
                entries.Add(memberFirstNameEntry);

            }

            // Last Name
            if (Validate.FeildPopulated(member.LastName))
            {
                memberLastNameValueLabel.Text = member.LastName;
                memberLastNameStack.IsVisible = true;
                memberLastNameEntry.IsVisible = false;
            }
            else
            {
                memberLastNameEntry.IsVisible = true;
                memberLastNameStack.IsVisible = false;
                entries.Add(memberLastNameEntry);

            }

            // Salutation Name
            if (Validate.FeildPopulated(member.SalutationName))
            {
                memberSalutationValueLabel.Text = member.SalutationName;
                memberSalutationStack.IsVisible = true;
                memberSalutationEntry.IsVisible = false;
            }
            else
            {
                memberSalutationEntry.IsVisible = true;
                memberSalutationStack.IsVisible = false;
                entries.Add(memberSalutationEntry);

            }

            // Email
            if (Validate.FeildPopulated(member.Email))
            {
                memberEmailValueLabel.Text = member.Email;
                memberEmailStack.IsVisible = true;
                memberEmailEntry.IsVisible = false;
            }
            else
            {
                memberEmailEntry.IsVisible = true;
                memberEmailStack.IsVisible = false;
                entries.Add(memberEmailEntry);

            }

            // Company 
            if (Validate.FeildPopulated(member.Company))
            {
                memberCompanyValueLabel.Text = member.Company;
                memberCompanyStack.IsVisible = true;
                memberCompanyEntry.IsVisible = false;
            }
            else
            {
                memberCompanyEntry.IsVisible = true;
                memberCompanyStack.IsVisible = false;
                entries.Add(memberCompanyEntry);

            }

            // Address 1
            if (Validate.FeildPopulated(member.Address1))
            {
                memberAddress1ValueLabel.Text = member.Address1;
                memberAddress1Stack.IsVisible = true;
                memberAddress1Entry.IsVisible = false;
            }
            else
            {
                memberAddress1Entry.IsVisible = true;
                memberAddress1Stack.IsVisible = false;
                entries.Add(memberAddress1Entry);

            }

            // Address 2
            if (Validate.FeildPopulated(member.Address2))
            {
                memberAddress2ValueLabel.Text = member.Address2;
                memberAddress2Stack.IsVisible = true;
                memberAddress2Entry.IsVisible = false;
            }
            else
            {
                memberAddress2Entry.IsVisible = true;
                memberAddress2Stack.IsVisible = false;
                entries.Add(memberAddress2Entry);

            }

            // Address 3
            if (Validate.FeildPopulated(member.Address3))
            {
                memberAddress3ValueLabel.Text = member.Address3;
                memberAddress3Stack.IsVisible = true;
                memberAddress3Entry.IsVisible = false;
            }
            else
            {
                memberAddress3Entry.IsVisible = true;
                memberAddress3Stack.IsVisible = false;
                entries.Add(memberAddress3Entry);

            }

            // City
            if (Validate.FeildPopulated(member.City))
            {
                memberCityValueLabel.Text = member.City;
                memberCityStack.IsVisible = true;
                memberCityEntry.IsVisible = false;
            }
            else
            {
                memberCityEntry.IsVisible = true;
                memberCityStack.IsVisible = false;
                entries.Add(memberCityEntry);

            }

            // Postcode
            if (Validate.FeildPopulated(member.Postcode))
            {
                memberPostCodeValueLabel.Text = member.Postcode;
                memberPostCodeStack.IsVisible = true;
                memberPostCodeEntry.IsVisible = false;
            }
            else
            {
                memberPostCodeEntry.IsVisible = true;
                memberPostCodeStack.IsVisible = false;
                entries.Add(memberPostCodeEntry);

            }

            // Country
            if (Validate.FeildPopulated(member.Country))
            {
                memberCountryValueLabel.Text = member.Country;
                memberCountryStack.IsVisible = true;
                memberCountryEntry.IsVisible = false;
            }
            else
            {
                memberCountryEntry.IsVisible = true;
                memberCountryStack.IsVisible = false;
                entries.Add(memberCountryEntry);

            }

            // Comment
            if (Validate.FeildPopulated(member.Comment))
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

            memberJoinDateValueLabel.Text = member.JoinDate.ToString();
            memberJoinDateStack.IsVisible = true;
            memberJoinDatePicker.IsVisible = false;

        }

        // Delte this member
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Are you sure?", "Would you like to delete this member", "Yes", "No");

            if (answer)
            {
                if (DeleteMember.DropDocument(id))
                {
                    await DisplayAlert("Member Deleted", "This member and their payment history have been delted", "OK");

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


        // Save changes to this member 
        private async void SaveChangesButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Are you sure?", "Would you like to save chnages to this member", "Yes", "No");

            if (answer)
            {
                Member updatedMember = UpdateMember.UpdateDocument(id, entries, editor, date);
                if (updatedMember != null)
                {
                    updatedMember.FirstName = FormatText.FirstToUpper(updatedMember.FirstName);
                    updatedMember.LastName = FormatText.FirstToUpper(updatedMember.LastName);
                    updatedMember.SalutationName = FormatText.FirstToUpper(updatedMember.SalutationName);

                    DisplayMember(updatedMember);
                    await DisplayAlert("Member Saved", "Changes to this member have been saved", "OK");
                }
                else
                {
                    await DisplayAlert("Connection Error", "Could not connect to database, please check your connection and try again", "OK");
                }

            }

        }

        private void PaymentButton_Clicked(object sender, EventArgs e)
        {
            // Find payments that belong to that member 
            paymentListView.ItemsSource = FindMembersPayments.GetPaymentDocuments(id);
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

        // Adds payment to database 
        private async void AddPaymentButton_Clicked(object sender, EventArgs e)
        {


            // Validate payment amount feild 
            if (Validate.FeildPopulated(paymentAmountEntry.Text))
            {
                if (Validate.ContainsLetter(paymentAmountEntry.Text))
                {
                    await DisplayAlert("Invalid Format", "The can not contain letters", "OK");
                    return;
                }
            }
            else
            {
                await DisplayAlert("Feild Empty", "Please fill in the amount feild to continue", "OK");
                return;
            }

            // Add payment to database 
            if (AddPayment.InsertPaymentDocument(id, new Payment { Date = paymentDatePicker.Date, Amount = Convert.ToDouble(paymentAmountEntry.Text), Member_id = id }))
            {
                await DisplayAlert("Payment Added", "This members donation has been recorded", "OK");
                paymentListView.ItemsSource = FindMembersPayments.GetPaymentDocuments(id);
                addNewPaymentView.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Connection Error", "Please check your connection and try again", "OK");
            }


        }

        private async void PaymentListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool result = await DisplayAlert("Delete Payment?", "Would you like to delete this payment item?", "Yes", "No");

            // If yes has been selected
            if (result)
            {
                // Store selected list item
                var item = e.SelectedItem as Payment;

                // If item is not null
                if (item != null)
                {
                    // Delete locattion document
                    if (DeletePayment.DropDocument(item._id))
                    {
                        await DisplayAlert("Payment Deleted", "This payment has been deleted", "OK");
                        paymentListView.ItemsSource = FindMembersPayments.GetPaymentDocuments(id);
                    }
                    else
                    {
                        await DisplayAlert("Connection Error", "Please check your connection and try again", "OK");
                    }
                }
            }
        }
    }
}
