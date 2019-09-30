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
    public partial class AddBirdNoteMobile2 : ContentPage
    {
        public AddBirdNoteMobile2()
        {
            InitializeComponent();
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Note Added", "Note has been added to Professor Feathers", "Ok");
        }
    }
}