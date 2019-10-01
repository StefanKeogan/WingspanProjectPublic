using System;
using System.Collections;
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
	public partial class ViewSponsorBirdResultsDesktop : ContentPage
	{
		public ViewSponsorBirdResultsDesktop (ArrayList results)
		{
			InitializeComponent ();
            resultsListView.ItemsSource = results;
        }

        private void ResultsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as CaptiveBird;

            if (item != null)
            {
                DisplayBird(item);
            }
        }

        private void DisplayBird(CaptiveBird bird)
        {

        }
    }
}