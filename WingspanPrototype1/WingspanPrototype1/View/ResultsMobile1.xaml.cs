using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsMobile1 : ContentPage
    {
        private Type birdObjectType;

        public ResultsMobile1(ArrayList results, Type birdType)
        {
            InitializeComponent();

            birdObjectType = birdType;
         
            mobileResults.ItemsSource = results;

        }

        private void MobileResults_ItemSelected_1(object sender, SelectedItemChangedEventArgs e)
        {
            // Are we displaying a captive or wild bird
            if (birdObjectType == typeof(WildBird))
            {
                var item = e.SelectedItem as WildBird;

                ArrayList birds = new ArrayList();
                birds.Add(item as WildBird);

                Navigation.PushAsync(new ResultsMobile2(birds, typeof(WildBird)));
            }
            else
            {
                var item = e.SelectedItem as CaptiveBird;

                ArrayList birds = new ArrayList();
                birds.Add(item as CaptiveBird);

                Navigation.PushAsync(new ResultsMobile2(birds, typeof(CaptiveBird)));
            }

        }
    }
}