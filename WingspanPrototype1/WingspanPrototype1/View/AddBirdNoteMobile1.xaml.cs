using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingspanPrototype1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WingspanPrototype1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBirdNoteMobile1 : ContentPage
    {
        public AddBirdNoteMobile1(string title)
        {
            InitializeComponent();

            Title = title;

            List<CaptiveBird> results = new List<CaptiveBird>();

            results.Add(new CaptiveBird { WingspanId = "15/06", Name = "Professor feathers" });

            resultsListView.ItemsSource = results;
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new AddBirdNoteMobile2());
        }
    }
}