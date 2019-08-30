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
    public partial class ResultsMobile2 : ContentPage
    {
        public ResultsMobile2(string name)
        {
            InitializeComponent();

            BirdNumberValue.Text = "6456";
            WingspanNumberValue.Text = "5455";
            BandNumberValue.Text = "4586";
            NameValue.Text = name;
            AgeValue.Text = "2";
            BreedValue.Text = "More Pork";
            StatusValue.Text = "Captive";
            SexValue.Text = "Male";
            
        }
    }
}