using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBird : ContentPage
    {
        public AddBird(string title)
        {
            InitializeComponent();

            Title = title;

            SexPicker.ItemsSource = new string[]{ "Male", "Female"};
            BreedPicker.ItemsSource = new string[]{ "Morepork", "Falcon", "Hawk"};
            StatusPicker.ItemsSource = new string[]{ "Captive", "Wild"};

            
        }
    }
}