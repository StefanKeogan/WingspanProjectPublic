using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Functions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage(string title)
        {
            InitializeComponent();

            Title = title;

            // Check device to determine size background image path


            double size = DeviceSize.ScreenArea();

            if (size < 783457) // If screen area is less than 7 inches set mobile image
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        backgroundImage.Source = "homemobile.png";
                        logoImage.Source = "goldlogo.png";
                        break;
                    case Device.UWP:
                        backgroundImage.Source = "Assets/home-mobile.png";
                        logoImage.Source = "gold-logo.png";
                        break;
                    default:
                        backgroundImage.Source = "";
                        break;
                }

                logoImage.WidthRequest = 250;
                logoImage.HeightRequest = 125;

            }
            else if (size < 1382400) // If screen area is less than 14 inches set small desktop / tablet image
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        backgroundImage.Source = "tablethome.png";
                        logoImage.Source = "darkbrownlogo.png";
                        break;
                    case Device.UWP:
                        backgroundImage.Source = "Assets/tablet-home.png";
                        logoImage.Source = "Assets/darkbrown-logo.png";
                        break;
                    default:
                        backgroundImage.Source = "";
                        break;
                }

                logoImage.WidthRequest = 350;
                logoImage.HeightRequest = 175;

            }
            else // Any larger and set the desktop image 
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        backgroundImage.Source = "desktophome.png";
                        logoImage.Source = "darkbrownlogo.png";
                        break;
                    case Device.UWP:
                        backgroundImage.Source = "Assets/desktophome.png";
                        logoImage.Source = "Assets/darkbrown-logo.png";
                        break;
                    default:
                        backgroundImage.Source = "";
                        break;
                }

                logoImage.WidthRequest = 400;
                logoImage.HeightRequest = 200;
            }


        }
    }
}