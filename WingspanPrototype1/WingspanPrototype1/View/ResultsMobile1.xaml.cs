using MongoDB.Bson;
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
    public partial class ResultsMobile1 : ContentPage
    {
        public ResultsMobile1(List<string> results )
        {
            InitializeComponent();


            // Convert hard coded result data into Bird objects
            List<WildBird> birdResulsts = new List<WildBird>();
            foreach (var result in results)
            {
                birdResulsts.Add(new WildBird { WingspanId = result });
            }

            MobileResults.ItemsSource = birdResulsts;

        }

        private void MobileResults_ItemSelected_1(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as WildBird;

            Navigation.PushAsync(new ResultsMobile2(item.WingspanId));
        }
    }
}