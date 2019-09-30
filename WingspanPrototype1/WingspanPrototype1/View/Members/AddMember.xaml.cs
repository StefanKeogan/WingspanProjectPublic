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


        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Member Added", "Member saved to database", "Ok");
        }
    }
}