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
    public partial class BirdResultsMobile1 : ContentPage
    {
        public BirdResultsMobile1(ArrayList results)
        {
            InitializeComponent();
         
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