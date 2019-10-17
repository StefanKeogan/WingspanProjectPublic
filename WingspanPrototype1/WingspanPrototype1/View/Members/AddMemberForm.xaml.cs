using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WingspanPrototype1.Controller;
using WingspanPrototype1.Controller.Birds;
using WingspanPrototype1.Model;
using WingspanPrototype1.Functions;

namespace WingspanPrototype1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMemberForm : ContentPage
    {
        public AddMemberForm(string title)
        {
            InitializeComponent();

            Title = title;

            // What type of device are we running on 
            if (DeviceSize.ScreenArea() <= 783457)
            {
                memberMargin1.Width = 0;
                memberMargin2.Width = 0;
            }

        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            // TODO: Discuss manditory feilds

            // First Name Validation
            if (Validate.FeildPopulated(memberFirstNameEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(memberFirstNameEntry.Text))
                {
                    await DisplayAlert("Invalid Entry", "First Name feild can not contain numbers or symbols", "OK");
                    return;
                }
            }
            // Manditory?

            // Last Name Validation
            if (Validate.FeildPopulated(memberLastNameEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(memberLastNameEntry.Text))
                {
                    await DisplayAlert("Invalid Entry", "Last Name feild can not contain numbers or symbols", "OK");
                    return;
                }
            }
            // Manditory?

            // Salutation Name Validation
            if (Validate.FeildPopulated(memberLastNameEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(memberLastNameEntry.Text))
                {
                    await DisplayAlert("Invalid Entry", "Salutation Name feild can not contain numbers or symbols", "OK");
                    return;
                }
            }

            // Email Name Validation
            // Check if manditory?

            // Company
            // Check if manditory

            // Address 1
            // Check if manditory

            // Address 2
            // Check if manditory

            // Address 3
            // Check if manditory

            // City 
            if (Validate.FeildPopulated(memberCityEntry.Text))
            {
                if (Validate.ContainsNumberOrSymbol(memberCityEntry.Text))
                {
                    await DisplayAlert("Invalid Entry", "City feild can not contain numbers or symbols", "OK");
                    return;
                }
            }
            // Manditory?

            // Postcode 
            if (Validate.FeildPopulated(memberPostcodeEntry.Text))
            {
                if (Validate.ContainsLetterOrSymbol(memberPostcodeEntry.Text))
                {
                    await DisplayAlert("Invalid Entry", "Postcode feild can not contain letters or symbols", "OK");
                    return;
                }
                // Larger than 4?
            }
            // Manditory?

            // Comment
            // Check if maditory


            // TODO: Dynamic member document?
            bool memberInserted = AddMember.InsertMemberDocument(new Member
            {
                FirstName = memberFirstNameEntry.Text,
                LastName = memberLastNameEntry.Text,
                SalutationName = memberSalutationNameEntry.Text,
                Email = memberEmailEntry.Text,
                Address1 = memberAddress1Entry.Text,
                Address2 = memberAddress2Entry.Text,
                Address3 = memberAddress3Entry.Text,
                City = memberCityEntry.Text,
                Postcode = memberPostcodeEntry.Text,
                Comment = memberCommentEditor.Text,
                JoinDate = memberJoindatePicker.Date

            }) ;

            if (memberInserted)
            {
                memberFirstNameEntry.Text = null;
                memberLastNameEntry.Text = null;
                memberSalutationNameEntry.Text = null;
                memberEmailEntry.Text = null;
                memberAddress1Entry.Text = null;
                memberAddress2Entry.Text = null;
                memberAddress3Entry.Text = null;
                memberCityEntry.Text = null;
                memberPostcodeEntry.Text = null;
                memberCommentEditor.Text = null;
                
                await DisplayAlert("Member Added", "Member document inderted into the database", "OK");
            }
            else
            {
                await DisplayAlert("Connenction Error", "Could not insert bird record, please check connection and try again", "OK");

            }


        }
    }
}