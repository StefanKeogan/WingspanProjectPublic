using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMember : ContentPage
    {
        public AddMember(string title)
        {
            InitializeComponent();

            Title = title;

            // What device are we running on? 
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    addMemberStackLayout.Margin = new Thickness(300, 20, 300, 20);
                    break;
                case Device.Android:
                    addMemberStackLayout.Margin = new Thickness(5, 5, 5, 5);
                    break;
                default:
                    addMemberStackLayout.Margin = new Thickness(300, 20, 300, 20);
                    break;
            }


        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Member Added", "Member saved to database", "Ok");
        }
    }
}