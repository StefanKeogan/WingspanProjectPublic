using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace WingspanPrototype1.Functions
{
    class DeviceSize
    {
        public static double ScreenArea()
        {
            double height = DeviceDisplay.MainDisplayInfo.Height;
            double width = DeviceDisplay.MainDisplayInfo.Width;
            double screenDensity = DeviceDisplay.MainDisplayInfo.Density;

            double screenAreaPixels = width * height;

            double relativeScreenArea = screenAreaPixels / screenDensity;

            return relativeScreenArea;

        }

    }
    
}
