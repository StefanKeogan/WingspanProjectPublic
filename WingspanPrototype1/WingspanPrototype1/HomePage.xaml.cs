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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            Title = "Home";          

            // Check device to determine background image path
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    BackgroundImageSource = "homemobile.png";
                    break;
                case Device.UWP:
                    BackgroundImageSource = "Assets/desktophome.png";
                    break;
                default: BackgroundImageSource = "homemobile.png";
                    break;
            }

        }
    }
}