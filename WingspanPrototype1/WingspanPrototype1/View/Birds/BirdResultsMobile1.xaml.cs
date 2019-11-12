using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Functions;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BirdResultsMobile1 : ContentPage
    {
        public BirdResultsMobile1(ArrayList results)
        {
            InitializeComponent();

            // Format wild wingspan ID's
            for (int i = 0; i > results.Count; i++)
            {
                if (results[i].GetType() == typeof(WildBird))
                {
                    WildBird wildResult = results[i] as WildBird;

                    wildResult.WingspanId = FormatText.FirstToUpper(wildResult.WingspanId);

                    results[i] = wildResult;

                }
            }

            mobileResults.ItemsSource = results;

        }

        private void MobileResults_ItemSelected_1(object sender, SelectedItemChangedEventArgs e)
        {
            // Get item from list
            var item = e.SelectedItem;

            // Array list allows us to store bird of either type
            ArrayList birds = new ArrayList();
            birds.Add(item);

            Navigation.PushAsync(new BirdResultsMobile2(birds));

        }
    }
}