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

namespace WingspanPrototype1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMemberForm : ContentPage
    {
        public AddMemberForm(string title)
        {
            InitializeComponent();

            Title = title;


        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            bool memberInserted = AddMember.InsertMemberDocument(new Member
            {
                FirstName = memberFirstNameEntry.Text,
                LastName = memberLastNameEntry.Text,
                SaluationName = memberSalutationNameEntry.Text,
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